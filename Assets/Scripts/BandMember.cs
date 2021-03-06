﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BandMember : MonoBehaviour{

	public enum Role{Drummer, Singer, GuitarPlayer, BassPlayer};

	public Role role;
	public Stats stats;
	public float skill;
	public bool dead = false;
	public bool beingCarried = false;
	public bool active = true;
	public Rigidbody rb;

	public GameObject myCanvas;

	//Genetic material
	public GameObject GenMat1;
	public GameObject instantiatedGenMat;

	private float genDropTimer;
	private float genDropStep;
	public float startGenDropTimer = 20f;
	public float genDropRarity = 0.5f;
	//

	public float carryOffsetX = 10f;
	public float carryOffsetY = 1f;


	//Kjeftesystem
	public float myMedgjørlighet = 70;
	public bool fikkKjeft = false;

	//Happinessystem
	public float myHappiness = 1f;
	private float happinessImprovementTimer = 60;
	public int startHappinessTimer = 60;
	public bool canImproveHappiness = true;
	//Erstatt happinessImprovementTimerStart og medgjørlighetsReduksjon med én public int i et Game Control-objekt.
	public float medgjørlighetReduksjon = 15;

	//Mistenkelighet
	public float mySuspicion = 90f;
	public float increaseSuspicion = 0.01f;
	public float decreaseSuspicion = 0.0005f;

	public float deadRotationSpeed = 0.1f;
	//Quaternion startRot;
	//Quaternion endRot = Quaternion.Euler(-90, 0, 0);
	//bool deadRotate = false;

	Animator animator;

	//Bars på canvaset!
	public Image medgjorlighetsBar;
	public Image skillBar;
	public Image suspicionBar;


	//Particle System
	public GameObject bloodParticles;


	public BandMember (String name, float skill, Role role)
	{
		this.name = name;
		this.stats = new Stats(skill);
		this.role = role;
	}

	public void InitializeBandMember(String name, float skill, float myMedgjørlighet, Role role)
    {
        this.name = name;
        this.skill = skill;
        this.role = role;
		this.myMedgjørlighet = myMedgjørlighet;
    }

    void Start ()
    {
        print("DEBUG - CLONE WAS MADE - CLICK ON THIS MESSAGE FOR MORE INFO:\n " + "Name: " + this.name + "\nSkill: " + this.skill + "\nRole: " + this.role);
		happinessImprovementTimer = startHappinessTimer;
		genDropTimer = startGenDropTimer;
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () {
//		startRot = transform.rotation;
//		if (deadRotate = true){
//			transform.rotation = Quaternion.Lerp(startRot, endRot, deadRotationSpeed * Time.time);
//		}
		//Stuff til canvas!
		float maxMedgjørlighet = 100;
		float maxSkill = 50;
		float maxSuspicion = 100;

		medgjorlighetsBar.fillAmount = myMedgjørlighet / maxMedgjørlighet;
		skillBar.fillAmount = skill / maxSkill;
		float suspicionToBeFilled = mySuspicion / maxSuspicion;
		suspicionBar.fillAmount = suspicionToBeFilled;

		float greenAndBlueLevel = 1 - suspicionToBeFilled;

		suspicionBar.color = new Color (1, greenAndBlueLevel, greenAndBlueLevel);
		//Woop woop!


		if (Input.GetKey (KeyCode.Tab)) {
			myCanvas.SetActive (true);
		}
		if (!Input.GetKey (KeyCode.Tab)) {
			myCanvas.SetActive (false);
		}

		if (Input.GetKeyDown ("g")) {
			LeaveGenetics ();
		}
		if (beingCarried == true) {
			float carryPosX = GameObject.FindWithTag("Player").transform.position.x + carryOffsetX; 
			float carryPosY = GameObject.FindWithTag("Player").transform.position.y + carryOffsetY;
			Vector3 carryPos = new Vector3(carryPosX, carryPosY, this.transform.position.z);
			this.transform.position = carryPos;
		}

		//Happiness - Vi har en greie som bare direkte feeder inn i myMedgjørlighet, og så har vi en separat greie som er nærmere det "ekte" systemet, som ikke brukes ennå
		//Sørg for at man ikke kan øke Happiness ubegrenset:
		float happinessTimerStep = 1f * Time.deltaTime;
		happinessImprovementTimer = happinessImprovementTimer - happinessTimerStep;
		if (happinessImprovementTimer < 0) {
			canImproveHappiness = true;
		}


		//Reduser timer for genetic droppings
		genDropStep = 1f * Time.deltaTime;
		genDropTimer = genDropTimer - genDropStep;
		if (genDropTimer < 0 && dead == false){
			float genDropChance = UnityEngine.Random.Range(0, 100);
			if (genDropRarity > genDropChance){
				LeaveGenetics();
			}
		}
		//

		//Reduser suspicion over tid
		mySuspicion = mySuspicion - decreaseSuspicion;
	}

    //Blir bonka av spilleren
    public void Dying (){
		print ("I'm dying!");
		LeaveGenetics();
        dead = true;
		animator.SetInteger("Walking", 0);
        Innfallsystemet infs = GetComponent<Innfallsystemet>();
        infs.progressBar.enabled = false;
        infs.progressFrame.enabled = false;
        Destroy(myCanvas);
        GetComponent<Innfallsystemet>().enabled = false;
		GetComponent<BandMemberMoving>().enabled = false;
		this.gameObject.transform.eulerAngles = new Vector3(-90, -90, 0);
		GetComponent<CloneActivation>().active = false;
		bloodParticles.GetComponent<ParticleSystem>().Play();
	}

	public void HappinessGain (){
		print ("Yay! Happiness!");
		//Endre dette hvis man bruker "ordentlig" happiness
		myMedgjørlighet = myMedgjørlighet + GameObject.Find("Player").GetComponent<PlayerInteractions>().happinessToGive;
	} 

	public void LeaveGenetics (){
		float spawnY = this.transform.position.y + 0.7f; 
		Vector3 spawnPosition = new Vector3(this.transform.position.x, spawnY, this.transform.position.z);
		//Quaternion spawnRotation = this.transform.rotation;
		instantiatedGenMat = (GameObject)Instantiate(GenMat1, spawnPosition, GenMat1.transform.rotation);
		float skillvariation = UnityEngine.Random.Range(-10, 10);
		float skillTransfer = skill + skillvariation;
		float medgjørligvariation = UnityEngine.Random.Range(-20, 40);
		float medgjørligTransfer = myMedgjørlighet + medgjørligvariation;
		if (medgjørligTransfer > 99){
			medgjørligTransfer = 99;
		}
		instantiatedGenMat.GetComponent<GenetiskMateriale>().enabled = true;
		instantiatedGenMat.GetComponent<GenetiskMateriale>().skillForCloning = skillTransfer;
		instantiatedGenMat.GetComponent<GenetiskMateriale>().medgjørlighetForCloning = medgjørligTransfer;
		instantiatedGenMat.GetComponent<GenetiskMateriale>().roleForCloning = role;
		genDropTimer = startGenDropTimer;
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
			float medgjørlig = UnityEngine.Random.Range (0, 99);
			if (medgjørlig <= myMedgjørlighet){
				print ("Tror ikke jeg gjør det, jeg.");
				GetComponentInParent<Innfallsystemet>().Interrupt();
			}
			if (medgjørlig > myMedgjørlighet){
				print ("Jeg gjør det jeg vil!");
			}
			//Gjør det mindre sannsynlig at de gjør som du sier neste gang
			myMedgjørlighet = myMedgjørlighet - medgjørlighetReduksjon;
			print(myMedgjørlighet);
			//Legg inn avbrudd av innfall her.
		}
	}


	//Ser noe suspicious
	public void SuspicionIncrease (){
		mySuspicion = mySuspicion + increaseSuspicion;
	}
}

