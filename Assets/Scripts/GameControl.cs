using UnityEngine;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

	public GameObject[] waypoints;
	public GameObject[] stairs;


	//Tid innfall tar
	public int scoreTid = 100;
	public int strandTid = 100;
	public int øveTid = 100;
	public int høreTid = 100;
	public int soloTid = 100;
	public int tweeteTid = 100;
	public int drikkeTid = 100;
	public int dusjeTid = 100;
	public int bajsTid = 100;
	public int spiseTid = 100;
	public int danceTid = 100;
	public int oppkastTid = 100;

	// Use this for initialization
	void Start () {
		this.waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		this.stairs = GameObject.FindGameObjectsWithTag ("Stairs");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
