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
	private int currentFloor;
	private int currentHouse;

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
		if(this.stairs.Length == 0)
			this.stairs = this.gameControl.stairs;
		if (moving == true) {
			if (waypointToMoveTo == null) {
				//x% sjanse for å bevege, x% for å stå stille - skal bli kontrollert av innfall
			} else {
				MoveToWayPoint (waypointToMoveTo);
				animator.SetInteger("Walking", 1);
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
			transform.position = stepTowardsStairs;
		}
	}

	void WalkToWayPoint(Waypoint wp){
		float step = speed * Time.deltaTime;
		Vector3 wpLocation = new Vector3(wp.gameObject.transform.position.x, this.transform.position.y, this.transform.position.z);
		Vector3 stepTowardsWp = Vector3.MoveTowards (this.transform.position, wpLocation, step);
		if (wpLocation == stepTowardsWp) {
			this.waypointToMoveTo = null;
			if (wp.gameObject.tag == "TransferCenter")
				switchHouses ();
			print ("Arrived at waypoint!");
			animator.SetInteger("Walking", 0);
			//Lagt til innfall
			/*TODO: DET KAN VÆRE BUG HER: "Parent" sikter til direkte parent (tror jeg),
			som kan gjøre at dersom band members er barn av flere objekter, vil ikke neste linje funke.*/
			GetComponentInParent<Innfallsystemet>().riktigPlass = true;
		} else {
			this.transform.position = stepTowardsWp;
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
				if (possibleClosestHasExitInWrongDirection && possibleClosestIsInWrongHouse)
					continue;
				if (tempClosest == null)
					tempClosest = possibleClosest;
				else if(possibleClosest.transform.position.x < tempClosest.transform.position.x) {
					tempClosest = possibleClosest;
				}
			}
		}
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

	int GetHouseOfObject (String tag)
	{
		return tag == "House 1" ? 0 : 1;
	}
}
