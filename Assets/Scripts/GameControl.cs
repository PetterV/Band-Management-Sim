using UnityEngine;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

	public GameObject[] waypoints;
	public GameObject[] stairs;


	//Tid innfall tar
	public float scoreTid = 3;
	public float strandTid = 3;
	public float øveTid = 3;
	public float høreTid = 3;
	public float soloTid = 3;
	public float tweeteTid = 3;
	public float drikkeTid = 3;
	public float dusjeTid = 3;
	public float bajsTid = 3;
	public float spiseTid = 3;
	public float danceTid = 3;
	public float oppkastTid = 3;

	// Use this for initialization
	void Start () {
		this.waypoints = GameObject.FindGameObjectsWithTag ("Waypoint");
		this.stairs = GameObject.FindGameObjectsWithTag ("Stairs");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
