using UnityEngine;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

	public GameObject[] waypoints;
	public GameObject[] stairs;


	//Tid innfall tar
	public int scoreTid = 3;
	public int strandTid = 3;
	public int øveTid = 3;
	public int høreTid = 3;
	public int soloTid = 3;
	public int tweeteTid = 3;
	public int drikkeTid = 3;
	public int dusjeTid = 3;
	public int bajsTid = 3;
	public int spiseTid = 3;
	public int danceTid = 3;
	public int oppkastTid = 3;

	// Use this for initialization
	void Start () {
		this.waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		this.stairs = GameObject.FindGameObjectsWithTag ("Stairs");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
