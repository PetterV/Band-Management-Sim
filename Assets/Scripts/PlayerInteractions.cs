﻿using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour {

	public bool bandCollision = false;
	public bool genMatCollision = false;
	public bool cloneMachineCollision = false;
	public bool happinessCollision = false;
	public bool computerCollision = false;
	public GameObject currentBandMember;
	public GameObject currentGenMat;
	public GameObject currentCloneMachine;
	public GameObject currentHappinessObject;
	public GameObject computer;
	public GameObject computerCanvas;
    public Canvas salesScreen;
	public GameObject bandMemberToKill;
	public BandMember.Role bringingRole;
	public float bringingSkill;
	public float bringingMedgjørlighet;
	public bool carryingGenMat = false;
	public bool carryingHappiness = false;
	public bool carryingBody = false;
	public bool carryingAny = false;
	public bool computerActive = false;
	public float happinessToGive;
    private Animator animator;
	public bool suspiciousAction = false;

    public float cloneTimer = 1.0f;

	//Stuff til whacking
	float animationTimer;
	public bool isHitting = false;
	private bool stoppedMusic = false;
	private bool killedBandMember = false;
	public float eventSpeed = 1f;
	private float hitTimerStep;
	private float hitTimer;
	public float musicStopTarget = 0.1f;
	public float killTarget = 0.4f;
	public float doneTarget = 0.5f;
	public GameObject weapon;
	//

	//Sensor for kjefting
	CapsuleCollider kjefteSensor;
	//

    public GameObject mainCam;

    void Start (){
		computer = GameObject.Find("Computer");
		computerCanvas = GameObject.FindGameObjectWithTag("ComputerCanvas");
        salesScreen = computerCanvas.GetComponent<Canvas>();
        mainCam = GameObject.FindWithTag("MainCamera");
		animator = GetComponent<Animator>();
		weapon = GameObject.FindWithTag("Weapon");
		kjefteSensor = GetComponent<CapsuleCollider>();
		kjefteSensor.enabled = false;
		weapon.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        if (cloneTimer > 0)
            cloneTimer -= Time.deltaTime;

		if (carryingGenMat == true || carryingBody == true || carryingHappiness == true){
			carryingAny = true;
		}
		if (carryingGenMat == false && carryingBody == false && carryingHappiness == false){
			carryingAny = false;
		}

		if (carryingBody == true){
			suspiciousAction = true;
		}
		if (carryingBody == false){
			suspiciousAction = false;
		}

		//Gi kjeft med Q
		if (Input.GetKeyDown("q")){
			kjefteSensor.enabled = true;
			if(bandCollision == true && currentBandMember.GetComponent<BandMember>().fikkKjeft == false && currentBandMember.GetComponent<CloneActivation>().active == true && currentBandMember.GetComponent<BandMember>().dead == false){
				print ("Don't do that!");
				currentBandMember.GetComponent<BandMember>().Kjeft();
			}
		}
		if (Input.GetKeyUp("q")){
			kjefteSensor.enabled = false;
		}
		//Går nedover lista i prioritert rekkefølge for å interacte 
		if (Input.GetKeyUp("e")){
			if (bandCollision == true && happinessCollision == false){
				if(currentBandMember.GetComponent<BandMember>().dead == true && currentBandMember.GetComponent<BandMember>().beingCarried == false && carryingAny == false ){
					print ("Carrying dead body");
					currentBandMember.GetComponent<BandMember>().beingCarried = true;
					carryingBody = true;
				}
				else if (currentBandMember.GetComponent<BandMember>().beingCarried == true){
					print ("No longer carrying dead body");
					carryingAny = false;
					carryingBody = false;
					currentBandMember.GetComponent<BandMember>().beingCarried = false;
				}
			}
			if (happinessCollision == true){
				if (carryingAny == false){
					print ("This will make someone happy");
					happinessToGive = currentHappinessObject.GetComponent<HappinessObject>().happinessGained;
					carryingHappiness = true;
					currentHappinessObject.GetComponent<HappinessObject>().happinessBeingCarried = true;
				}
				else if (carryingHappiness == true && bandCollision == false){
					print ("Who needs to make anyone happy?");
					currentHappinessObject.GetComponent<HappinessObject>().happinessBeingCarried = false;
					carryingHappiness = false;
				}
				else if (carryingHappiness == true && bandCollision == true){
					currentBandMember.GetComponent<BandMember>().HappinessGain();
					carryingHappiness = false;
					Destroy(currentHappinessObject);
				}
			}
			if (genMatCollision == true){
				if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == false && carryingAny == false){
				print ("Yuck!");
				currentGenMat.GetComponent<GenetiskMateriale>().beingCarried = true;
				bringingRole = currentGenMat.GetComponent<GenetiskMateriale>().roleForCloning;
				bringingSkill = currentGenMat.GetComponent<GenetiskMateriale>().skillForCloning;
				bringingMedgjørlighet = currentGenMat.GetComponent<GenetiskMateriale>().medgjørlighetForCloning;
				carryingGenMat = true;
				}
				else if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == true && cloneMachineCollision == true){
					//print ("Destroy me - GenMat");
                    if(cloneTimer <= 0)
                    {
                        GameObject.FindWithTag("CloneMachine").GetComponent<CloneMachine>().Cloning();
                        Destroy(currentGenMat);
                        carryingGenMat = false;
                        cloneTimer = 1.0f;
                    }
				}
				else if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == true){
					currentGenMat.GetComponent<GenetiskMateriale>().beingCarried = false;
					print ("I'm not carrying this!");
					carryingGenMat = false;
				}
			}
			else if (computerCollision == true){
				if (computerActive == false){
					print ("Use computer");
					computerActive = true;
				}
				else if (computerActive == true){
					print ("No longer using computer");
					computerActive = false;
				}
			}
		}

		if (Input.GetButtonDown("w")){
			if(currentBandMember.GetComponent<CloneActivation>().active == false && currentBandMember.GetComponent<BandMember>().dead == false){
				currentBandMember.GetComponent<CloneActivation>().active = true;
				currentBandMember.GetComponent<CloneActivation>().Activation();
				print ("I'm active now!");
			}
		}

		if (computerActive == true){
            salesScreen.enabled = true;
		}
		if (computerActive == false){
            salesScreen.enabled = false;
		}

		//Whacking bandmembers
		if (Input.GetKeyDown("space")){
			animator.SetInteger("Punch", 1);
			animationTimer = 0;
			suspiciousAction = true;
			weapon.SetActive(true);
		}
		float animationTimerStep = 1f * Time.deltaTime;
		animationTimer = animationTimer + animationTimerStep;
		if (!Input.GetKey("space") && animationTimer > 1f){
			animator.SetInteger("Punch", 0);
        if (weapon != null)
            {
                weapon.SetActive(false);
            }
			
			suspiciousAction = false;
		}
		if (isHitting == true){
			HittingBandMember();
		}
		if (genMatCollision == false && carryingAny == true){
			carryingGenMat = false;
		}
		if (bandCollision == false && carryingAny == true){
			carryingBody = false;
		}
	}


	void OnTriggerEnter(Collider coll){
		//print ("Hanging out with" + coll.gameObject.tag);
	}
		

	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "BandMember" && isHitting == false){
			bandCollision = true;
			currentBandMember = coll.gameObject;
		}
		if (coll.gameObject.tag == "GeneticMaterial" && carryingGenMat == false){
			genMatCollision = true;
			currentGenMat = coll.gameObject;
		}
		if (coll.gameObject.tag == "CloneMachine"){
			cloneMachineCollision = true;
			currentCloneMachine = coll.gameObject;
		}
		if (coll.gameObject.tag == "HappinessObject"){
			happinessCollision = true;
			currentHappinessObject = coll.gameObject;
		}
		if (coll.gameObject.tag == "Computer"){
			computerCollision = true;
		}

//		if (coll.gameObject.tag == "BandMember" && Input.GetKeyDown("space") && currentBandMember.GetComponent<BandMember>().dead == false){
//			isHitting = true;
//			bandMemberToKill.GetComponent<Innfallsystemet>().Interrupt();
//			bandMemberToKill.GetComponent<BandMemberMoving>().enabled = false;
//        }
	}


	void OnTriggerExit (Collider coll){
		if (coll.gameObject.tag == "BandMember"){
			bandCollision = false;
		}
		if (coll.gameObject.tag == "GeneticMaterial" && carryingGenMat == false){
			genMatCollision = false;
		}
		if (coll.gameObject.tag == "CloneMachine"){
			cloneMachineCollision = false;
		}
		if (coll.gameObject.tag == "HappinessObject"){
			happinessCollision = false;
		}
		if (coll.gameObject.tag == "Computer"){
			computerCollision = false;
			computerActive = false;
		}
		//print ("No longer hanging out with" + coll.gameObject.tag);
	}


	void HittingBandMember (){
		//Timing the hit
		hitTimerStep = eventSpeed * Time.deltaTime;
		hitTimer = hitTimer + hitTimerStep;
		if (hitTimer >= musicStopTarget && stoppedMusic == false){
			stoppedMusic = true;
			mainCam.GetComponent<PauseMusic>().Pause();
		}
		if (hitTimer >= killTarget && killedBandMember == false){
			bandMemberToKill.GetComponent<BandMember>().Dying();
			killedBandMember = true;
		}
		if (hitTimer >= doneTarget){
			stoppedMusic = false;
			killedBandMember = false;
			hitTimer = 0f;
			isHitting = false;
		}
	}
}
