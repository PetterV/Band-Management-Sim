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
        ErViFulltallig();
		float popularitetOppNed = popularitetsfaktor * Time.deltaTime;
		popularitet = popularitet + popularitetOppNed;
		if (popularitet < 40000){
            float pengerNed = 10 * Time.deltaTime;
            penger = penger - pengerNed;
		}
        if (popularitet < 30000)
        {
            float pengerNed = 20 * Time.deltaTime;
            penger = penger - pengerNed;
        }
        if (popularitet < 10000)
        {
            float pengerNed = 500 * Time.deltaTime;
            penger = penger - 1;
        }
        if (popularitet == 0)
        {
            float pengerNed = 10000 * Time.deltaTime;
            penger = penger - 1;
        }
        if (popularitet < 40000){
            float pengerNed = 10 * Time.deltaTime;
            penger = penger - pengerNed;
		}
        if (popularitet > 40000)
        {
            float pengerNed = 1 * Time.deltaTime;
            penger = penger - pengerNed;
        }
        if (popularitet > 50000)
        {
            float pengerNed = 1 * Time.deltaTime;
            penger = penger + pengerNed;
        }
		if (popularitet > 60000){
            float pengerNed = 10 * Time.deltaTime;
            penger = penger + pengerNed;
		}
		if (publicSuspicion > maxPublicSuspicion){
			gameOver = true;
			print ("You got found out!");
		}

		if (gameOver == true){
			print ("Game over!");
		}
	}

    void ErViFulltallig()
    {
        int activeBandMembers = GetNumberOfActiveBandMembers();
        if (activeBandMembers == 3)
            popularitetsfaktor = -1000.0f;
        if (activeBandMembers == 2)
            popularitetsfaktor = -2000.0f;
        if (activeBandMembers == 1)
            popularitetsfaktor = -3000.0f;
        if (activeBandMembers == 0)
            popularitetsfaktor = -5000.0f;
    }

    int GetNumberOfActiveBandMembers()
    {
        GameObject[] bandmembers = GameObject.FindGameObjectsWithTag("BandMember");
        int count = 0;
        foreach (GameObject bandmember in bandmembers)
        {
            BandMember bm = bandmember.GetComponent<BandMember>();
            if (!bm.dead && bm.active)
                count++;
        }
        return count;
    }
}
