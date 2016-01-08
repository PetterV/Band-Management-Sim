using UnityEngine;
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
	public GameObject bandMemberToKill;
	public BandMember.Role bringingRole;
	public float bringingSkill;
	public int bringingMedgjørlighet;
	public bool carryingGenMat = false;
	public bool carryingHappiness = false;
	public bool carryingBody = false;
	public bool carryingAny = false;
	public bool computerActive = false;
	public int happinessToGive;
    private Animator animator;
	public bool suspiciousAction = false;


	//Stuff til whacking
	float animationTimer;
	public bool isHitting = false;
	private bool madeHit = false;
	private bool stoppedMusic = false;
	private bool killedBandMember = false;
	public float eventSpeed = 1f;
	private float hitTimerStep;
	private float hitTimer;
	public float musicStopTarget = 0.1f;
	public float killTarget = 0.4f;
	public float doneTarget = 0.5f;
	public GameObject weapon;
	public CapsuleCollider weaponColl;
	//

    public GameObject mainCam;

    void Start (){
		computer = GameObject.Find("Computer");
		computerCanvas = GameObject.FindGameObjectWithTag("ComputerCanvas");
        mainCam = GameObject.FindWithTag("MainCamera");
		animator = GetComponent<Animator>();
		weapon = GameObject.FindWithTag("Weapon");
		weaponColl = weapon.GetComponent<CapsuleCollider>();
		weaponColl.enabled = false;
    }

	// Update is called once per frame
	void Update () {


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
		if (Input.GetKeyUp("q")){
			if(bandCollision == true && currentBandMember.GetComponent<BandMember>().fikkKjeft == false && currentBandMember.GetComponent<CloneActivation>().active == true && currentBandMember.GetComponent<BandMember>().dead == false){
				print ("Don't do that!");
				currentBandMember.GetComponent<BandMember>().Kjeft();
			}
		}
		//Går nedover lista i prioritert rekkefølge for å interacte 
		if (Input.GetKeyDown("e")){
			if (bandCollision == true && happinessCollision == false){
				if(currentBandMember.GetComponent<BandMember>().dead == true && currentBandMember.GetComponent<BandMember>().beingCarried == false && carryingAny == false ){
					print ("Carrying dead body");
					currentBandMember.GetComponent<BandMember>().beingCarried = true;
					carryingBody = true;
				}
				else if (currentBandMember.GetComponent<BandMember>().beingCarried == true){
					print ("No longer carrying dead body");
					currentBandMember.GetComponent<BandMember>().beingCarried = false;
					carryingBody = false;
				}
				else if(currentBandMember.GetComponent<CloneActivation>().active == false){
					currentBandMember.GetComponent<CloneActivation>().active = true;
					currentBandMember.GetComponent<CloneActivation>().Activation();
					print ("I'm active now!");
				}
			}
			else if (happinessCollision == true){
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
			else if (genMatCollision == true){
				if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == false && carryingBody == false && carryingHappiness == false && carryingGenMat == false){
				print ("Yuck!");
				currentGenMat.GetComponent<GenetiskMateriale>().beingCarried = true;
				bringingRole = currentGenMat.GetComponent<GenetiskMateriale>().roleForCloning;
				bringingSkill = currentGenMat.GetComponent<GenetiskMateriale>().skillForCloning;
				bringingMedgjørlighet = currentGenMat.GetComponent<GenetiskMateriale>().medgjørlighetForCloning;
				carryingGenMat = true;
				}
				else if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == true && cloneMachineCollision == true){
					//print ("Destroy me - GenMat");
					GameObject.FindWithTag("CloneMachine").GetComponent<CloneMachine>().Cloning();
					Destroy(currentGenMat);
					carryingGenMat = false;
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
		if (computerActive == true){
			//computerCanvas.SetActive(true);
		}
		if (computerActive == false){
			//computerCanvas.SetActive(false);
		}

		//Whacking bandmembers
		if (Input.GetKeyDown("space")){
			animator.SetInteger("Punch", 1);
			animationTimer = 0;
			weaponColl.enabled = true;
		}
		float animationTimerStep = 1f * Time.deltaTime;
		animationTimer = animationTimer + animationTimerStep;
		if (!Input.GetKey("space") && animationTimer > 1f){
			animator.SetInteger("Punch", 0);
			weaponColl.enabled = false;
		}
		if (isHitting == true){
			HittingBandMember();
		}
	}


	void OnTriggerEnter(Collider coll){
		print ("Hanging out with" + coll.gameObject.tag);
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
		print ("No longer hanging out with" + coll.gameObject.tag);
	}
}
