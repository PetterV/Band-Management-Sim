using UnityEngine;
using System.Collections.Generic;
using System;

public class BandMemberMoving : MonoBehaviour {

	public bool moving = true;
	public float moveTiming = 300f;
	//public float moveTimingStartValue = 300f;
	public float speed = 5f;
	public GameObject waypointToMoveTo;
	public int[] houseTransferFloors;

	private GameControl gameControl;
	private Vector3 targetPosition;
	private GameObject[] waypoints;
	private GameObject[] stairs;
	private float moveValue;
	public int startFloor = 0;
	private Animator animator;
	public int startHouse = 0;
	public int currentFloor;
	public int currentHouse;

	void Start(){
		currentFloor = startFloor;
		this.gameControl = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameControl>();
		this.waypoints = this.gameControl.waypoints;
		this.stairs = this.gameControl.stairs;
		animator = GetComponent<Animator>();
		this.currentHouse = startHouse;
	}

	// Update is called once per frame
	void Update () {
		if(this.stairs == null || this.stairs.Length == 0)
			this.stairs = this.gameControl.stairs;
		if (moving == true) {
			if (waypointToMoveTo == null) {
				//x% sjanse for å bevege, x% for å stå stille - skal bli kontrollert av innfall
			} else {
				MoveToWayPoint (waypointToMoveTo);
				
			}
		}
	}

	void MoveToWayPoint(GameObject waypoint){
		Waypoint wp = waypoint.GetComponent<Waypoint>();
		int wpHouse = GetHouseOfObject (wp.transform.parent.tag);
		if (this.currentHouse != wpHouse) {
			GoToOtherHouse ();
		}
		else if (this.currentFloor != wp.floor) {
			GoToFloor (wp.floor);
		} else {
			WalkToWayPoint (wp);
		}
	}

	void GoToOtherHouse ()
	{
		int nearestHouseTransferFloor = GetNearestHouseTransferFloor ();
		if (this.currentFloor != nearestHouseTransferFloor) {
			GoToFloor (nearestHouseTransferFloor);
		}
		else {
			Waypoint transferCenter = GetTransferCenter (this.currentHouse, this.currentFloor);
			if (transferCenter == null) {
				print ("FINNER IKKE TRANSFER CENTER :(((( - STOPPING MOVEMENT");
				this.waypointToMoveTo = null;
			}
			WalkToWayPoint (transferCenter);
		}
	}

	int GetNearestHouseTransferFloor(){
		int nearestFloor = 99;
		foreach(GameObject houseTransfer in this.gameControl.houseTransfers){
			int transferFloor = houseTransfer.GetComponent<Waypoint>().floor;
			int currentBestDistance = Math.Abs(this.currentFloor - nearestFloor);
			int tempFloorDistance = Math.Abs(this.currentFloor - transferFloor);
			if(tempFloorDistance < currentBestDistance){
				nearestFloor = transferFloor;
			}
		}
		return nearestFloor;
	}

	Waypoint GetTransferCenter(int house, int floor){
		foreach(GameObject waypoint in this.gameControl.houseTransfers){
			Waypoint wp = waypoint.GetComponent<Waypoint>();
			int wpHouse =  GetHouseOfObject(wp.transform.parent.gameObject.tag);
			if (wp.floor == floor && wpHouse != house)
				return wp;
		}
		return null;
	}
		
	void GoToFloor(int targetFloor){
		float step = speed * Time.deltaTime;
		bool goingUp = targetFloor > this.currentFloor ? true : false;
		Stairs nearestStair = GetNearestStair(goingUp);
		if(nearestStair == null){
			print("En trapp er tagga 'Stairs' uten aa ha scriptet Stairs paa seg. Fiks!!!");
			return;
		}
		Vector3 stairLocation = new Vector3(nearestStair.gameObject.transform.position.x, this.transform.position.y, this.transform.position.z);
		Vector3 stepTowardsStairs = Vector3.MoveTowards (this.transform.position, stairLocation, step);
		if (stairLocation == stepTowardsStairs) {
			TakeStairsTo (goingUp, nearestStair);
		}
		else{
			HeadCorrectDirection (transform.position, stepTowardsStairs);
			transform.position = stepTowardsStairs;
            animator.SetInteger("Walking", 1);
        }
	}

	void WalkToWayPoint(Waypoint wp){
		float step = speed * Time.deltaTime;
		Vector3 wpLocation = new Vector3(wp.gameObject.transform.position.x, this.transform.position.y, this.transform.position.z);
		Vector3 stepTowardsWp = Vector3.MoveTowards (this.transform.position, wpLocation, step);
		if (wpLocation == stepTowardsWp) {

            if (wp.gameObject.tag == "TransferCenter")
            {
                switchHouses();
            }
            else
            {
				this.waypointToMoveTo = null;
                GetComponent<Innfallsystemet>().riktigPlass = true;
				animator.SetInteger("Walking", 0);
				HeadTowardsCamera();
                print("Arrived at waypoint!");
            }
			
		} else {
			HeadCorrectDirection(transform.position, stepTowardsWp);
			this.transform.position = stepTowardsWp;
            animator.SetInteger("Walking", 1);
        }
	}

	Stairs GetNearestStair(bool goingUp){
		Stairs tempClosest = null;
		foreach (GameObject go in this.stairs) {
			Stairs possibleClosest = go.GetComponent<Stairs> ();
			if(possibleClosest == null)
				return null;
			if (possibleClosest.floor == this.currentFloor) {
				bool possibleClosestHasExitInWrongDirection = (possibleClosest.upExit == null && goingUp) || (possibleClosest.downExit == null && !goingUp);
				bool possibleClosestIsInWrongHouse = GetHouseOfObject (possibleClosest.gameObject.transform.parent.tag) != this.currentHouse;
				if (possibleClosestHasExitInWrongDirection || possibleClosestIsInWrongHouse)
                {
                    continue;
                }
				if (tempClosest == null)
					tempClosest = possibleClosest;
				else if(possibleClosest.transform.position.x < tempClosest.transform.position.x) {
					tempClosest = possibleClosest;
				}
			}
		}
		if (tempClosest == null)
			print ("ERROR ERROR ERROR, CANNOT FIND STAIRS!!! Check floors, tags and transfer center plz");
        return tempClosest;
	}

	void TakeStairsTo (bool goingUp, Stairs nearestStair)
	{
		if (goingUp) {
			nearestStair.SendMessage ("MoveUp", this.gameObject);
			this.currentFloor++;
		} else {
			nearestStair.SendMessage ("MoveDown", this.gameObject);
			this.currentFloor--;
		}
		new WaitForSeconds (1);
	}

	void switchHouses(){
		this.currentHouse = this.currentHouse == 0 ? 1 : 0;
	}

	public void HeadCorrectDirection(Vector3 currentPos, Vector3 headedTo){
		if (IsHeadedRight (transform.position, headedTo))
			HeadRight ();
		else
			HeadLeft ();
	}

	bool IsHeadedRight(Vector3 currentPos, Vector3 headedTo){
		return headedTo.x > currentPos.x;
	}

	public void HeadLeft(){
		this.gameObject.transform.eulerAngles = new Vector3 (0, 270.0f, 0);
	}

	public void HeadRight(){
		this.gameObject.transform.eulerAngles = new Vector3 (0, 90.0f, 0);
	}

    public void HeadTowardsCamera()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0, 180.0f, 0);
    }

    public void HeadAwayFromCamera()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    int GetHouseOfObject (String tag)
	{
        if (tag == "House 1")
        {
            return 0;
        }
            
        if (tag == "House 2")
        {
            return 1;
        }
        return -1;
	}
}
