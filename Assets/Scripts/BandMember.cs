﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class BandMember : MonoBehaviour{

	public enum Role{Drummer, Singer, GuitarPlayer, BassPlayer};
    public enum Innfall {Score, Strandtur, Nothing};
    public Dictionary<Innfall, int> innfallsOversikt;

	public Role role;
	public Stats stats;
	public int skill;
	public int innfallsTall;
	public bool active = true;
	public bool dead = false;

    private int innfallSum = 0;

	//Genetic material
	public GameObject GenMat1;
	public GameObject instantiatedGenMat;

	//Kjeftesystem
	private int myMedgjørlighet = 70;
	public bool fikkKjeft = false;

	//Happinessystem
	public int myHappiness = 1;
	private int happinessImprovementTimer = 3000;
	//Erstatt happinessImprovementTimerStart og medgjørlighetsReduksjon med én public int i et Game Control-objekt.
	public int happinessImprovementTimerStart = 3000;
	private int medgjørlighetReduksjon = 15;

	public BandMember (String name, int skill, Role role)
	{
		this.name = name;
		this.stats = new Stats(skill);
		this.role = role;
	}

    public void InitializeBandMember(String name, int skill, Role role)
    {
        this.name = name;
        this.skill = skill;
        this.role = role;
    }

    private void InitializeInnfall()
    {
        //Dette er Innfalls-oversikten. Hvis du vil legge til flere, gjør du det på samme måte som her.
        this.innfallsOversikt = new Dictionary<Innfall, int>()
        {
            {Innfall.Score, 1 },
            {Innfall.Strandtur, 2 }, //sannsynligheten for Strandtur er dobbelt så stor som Score.
            {Innfall.Nothing, 7 } //Sannsynligheten for Nothing er sju ganger større enn Score
        };
        foreach (KeyValuePair<Innfall, int> entry in innfallsOversikt)
        {
            innfallSum += entry.Value;
        }
    }

    void Start ()
    {
        InitializeInnfall();
        print("DEBUG - CLONE WAS MADE - CLICK ON THIS MESSAGE FOR MORE INFO:\n " + "Name: " + this.name + "\nSkill: " + this.skill + "\nRole: " + this.role);
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("z")){
			CheckInnfall();
		}
		if (Input.GetKeyDown("g")){
			LeaveGenetics();
		}
	}

	//Innfallsoversikten ligger i enumen Innfall øverst.
	void CheckInnfall () {
        //Generer innfallstall og sjekk mot innfallsoversikten.
        this.innfallsTall = UnityEngine.Random.Range(0, innfallSum);
        int tempUpperBound = 0;
        foreach(KeyValuePair<Innfall, int> entry in this.innfallsOversikt)
        {
            tempUpperBound += entry.Value;
            if (innfallsTall < tempUpperBound)
            {
                performInnfall(entry.Key);
                break;
            }
                
        }
        print (innfallsTall);
	}

    void performInnfall(Innfall inn)
    {
        switch (inn)
        {
            case Innfall.Score:
                {
                    Score();
                    break;
                }
            case Innfall.Strandtur:
                {
                    Strandtur();
                    break;
                }
            case Innfall.Nothing:
                {
                    break;
                }
        }
    }

	//Innfallshandlinger
	void Score (){
		print ("I kveld scorer jeg!");
	}

	void Strandtur (){
		print ("Jeg liker lange turer på stranden.");
	}

	//Bli drept av spilleren
	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Player" && Input.GetKeyDown("space")){
			Dying();
		}
	}

	//Blir bonka av spilleren
	void Dying (){
		print ("I'm dying!");
        dead = true;
        Destroy(gameObject);
	}

	void LeaveGenetics (){
		Vector3 spawnPosition = this.transform.position;
		Quaternion spawnRotation = this.transform.rotation;
		instantiatedGenMat = (GameObject)Instantiate(GenMat1, spawnPosition, spawnRotation);
		instantiatedGenMat.GetComponent<GenetiskMateriale>().skillForCloning = skill;
		instantiatedGenMat.GetComponent<GenetiskMateriale>().roleForCloning = role;
	}

	//Bli kjefta på
	public void Kjeft (){
		//Hvis de allerede har fått kjeft:
		if (fikkKjeft == true){
			print ("Jeg har bestemt meg allerede!");
		}
		if (fikkKjeft == false){
			print ("Jeg fikk kjeft!");
			//Kan bare få kjeft én gang i løpet av et innfall
			fikkKjeft = true;
			int medgjørlig = UnityEngine.Random.Range (0, 99);
			if (medgjørlig <= myMedgjørlighet){
				print ("Tror ikke jeg gjør det, jeg.");
			}
			if (medgjørlig > myMedgjørlighet){
				print ("Jeg gjør det jeg vil!");
			}
			//Gjør det mindre sannsynlig at de gjør som du sier neste gang
			myMedgjørlighet = myMedgjørlighet - medgjørlighetReduksjon;
			//Legg inn avbrudd av innfall her.
		}
	}
}

