using UnityEngine;
using System.Collections.Generic;

public class BandMemberMoving : MonoBehaviour {
	public enum WaypointID{None = -1, FirstFloor = 0, SecondFloor = 1, ThirdFloor = 2};

	public bool moving = true;
	public float moveTiming = 300f;
	public float moveTimingStartValue = 300f;
	public float speed = 5f;
	public WaypointID movingToWaypoint = WaypointID.None;

	private GameControl gameControl;
	private Vector3 targetPosition;
	private List<GameObject> waypoints = new List<GameObject>();
	private List<GameObject> stairs = new List<GameObject>();
	private float moveValue;
	private int currentFloor = 2;

	void Start(){
		this.gameControl = GameObject.Find ("GameControl").GetComponent<GameControl>();
		this.waypoints = this.gameControl.waypoints;
		this.stairs = this.gameControl.stairs;
	}

	// Update is called once per frame
	void Update () {
		/*if (moving == true){
			moveTiming--;
			if (moveTiming < 0){
				moveValue = UnityEngine.Random.Range(0, 8) - 4f;
				float currentMove = transform.position.x + moveValue;
				targetPosition = new Vector3 (currentMove, transform.position.y, transform.position.z);
				moveTiming = moveTimingStartValue;
			}
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
		}*/
		if (moving == true) {
			if (movingToWaypoint == WaypointID.None) {
				//x% sjanse for å bevege, x% for å stå stille - skal bli kontrollert av innfall
			} else {
				MoveToWayPoint ((int)movingToWaypoint);
			}
		}
	}

	void MoveToWayPoint(int waypoint){
		Waypoint wp = waypoints [waypoint].GetComponent<Waypoint> ();
		if (this.currentFloor != wp.floor) {
			GoToFloor (wp.floor);
		} else {
			WalkToWayPoint (wp);
		}
	}
		
	void GoToFloor(int targetFloor){
		float step = speed * Time.deltaTime;
		Vector3 stairLocation = stairs[this.currentFloor-1].transform.position;
		Vector3 stepTowardsStairs = Vector3.MoveTowards (this.transform.position, stairLocation, step);
		if (stairLocation == stepTowardsStairs) {
			TakeStairsTo (targetFloor);
		}
		else{
			transform.position = stepTowardsStairs;
		}
	}

	void WalkToWayPoint(Waypoint wp){
		float step = speed * Time.deltaTime;
		Vector3 wpLocation = wp.gameObject.transform.position;
		Vector3 stepTowardsWp = Vector3.MoveTowards (this.transform.position, wpLocation, step);
		if (wpLocation == stepTowardsWp) {
			this.movingToWaypoint = WaypointID.None;
			print ("Arrived at waypoint!");
		} else {
			this.transform.position = stepTowardsWp;
		}
	}

	void TakeStairsTo (int targetFloor)
	{
		if (this.currentFloor < targetFloor) {
			stairs [this.currentFloor - 1].SendMessage ("MoveUp", this.gameObject);
			this.currentFloor++;
		} else {
			stairs [this.currentFloor - 1].SendMessage ("MoveDown", this.gameObject);
			this.currentFloor--;
		}
	}

}
