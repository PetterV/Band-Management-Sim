using UnityEngine;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

	public GameObject[] waypoints;
	public GameObject[] stairs;
	public GameObject[] houseTransfers;

	public bool gameOver = false;


	// Penger og popularitet
	public float penger = 100000f;
	public float popularitet = 500000f;
	public float maxPopularitet = 1000000f;
	public float popularitetsfaktor = 100f;

	public float caymanKonto = 0f;


	//Suspicion
	public float publicSuspicion = 100f;
	public float maxPublicSuspicion = 1000f;
	//

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
		this.houseTransfers = GameObject.FindGameObjectsWithTag ("TransferCenter");
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



		if (publicSuspicion > maxPublicSuspicion){
			gameOver = true;
			print ("You got found out!");
		}

		if (gameOver == true){
			print ("Game over!");
		}

//		if (Input.GetKeyDown("p")){
//			playGood();
//		}
	}



//	public void playGood (){
//		goodSound.PlayOneShot(vellykket_utfall, 1);
//	}
//	public void playBad (){
//		failSound.PlayOneShot(mislykket_utfall, 1);
//	}
}
