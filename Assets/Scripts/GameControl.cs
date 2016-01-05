﻿using UnityEngine;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

	public GameObject[] waypoints;
	public GameObject[] stairs;


	// Penger og popularitet
	public float penger = 100000;
	public float popularitet = 1000000;
	public float popularitetsfaktor = 100;


	//Tid innfall tar
	public float scoreTid = 3;
	public float strandTid = 3;
	public float oveTid = 3;
	public float lytteTid = 3;
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
		float popularitetOppNed = popularitetsfaktor * Time.deltaTime;
		popularitet = popularitet + popularitetOppNed;
		if (popularitet < 1000000){
			penger = penger - 10;
		}
		if (popularitet > 1000000){
			penger = penger + 10;
		}
	}
}
