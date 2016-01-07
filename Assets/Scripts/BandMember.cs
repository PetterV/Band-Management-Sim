using UnityEngine;
using System;
using System.Collections;

public class BandMember : MonoBehaviour{

	public enum Role{Drummer, Singer, GuitarPlayer, BassPlayer};

	public Role role;
	public Stats stats;
	public float skill;
	public bool active = true;
	public bool dead = false;
	public bool beingCarried = false;

	public GameObject myCanvas;

	//Genetic material
	public GameObject GenMat1;
	public GameObject instantiatedGenMat;

	//Kjeftesystem
	public int myMedgjørlighet = 70;
	public bool fikkKjeft = false;

	//Happinessystem
	public float myHappiness = 1f;
	private float happinessImprovementTimer = 60;
	public int startHappinessTimer = 60;
	public bool canImproveHappiness = true;
	//Erstatt happinessImprovementTimerStart og medgjørlighetsReduksjon med én public int i et Game Control-objekt.
	//Funker ikke?????
	public int medgjørlighetReduksjon = 15;

	public BandMember (String name, float skill, Role role)
	{
		this.name = name;
		this.stats = new Stats(skill);
		this.role = role;
	}

	public void InitializeBandMember(String name, float skill, int myMedgjørlighet, Role role)
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
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Tab)) {
			//myCanvas.SetActive (true);
		}
		if (!Input.GetKey (KeyCode.Tab)) {
			//myCanvas.SetActive (false);
		}

		if (active == true && Input.GetKeyDown ("g")) {
			LeaveGenetics ();
		}
		if (beingCarried == true) {
			this.transform.position = GameObject.Find ("Player").transform.position;	
		}

		//Happiness - Vi har en greie som bare direkte feeder inn i myMedgjørlighet, og så har vi en separat greie som er nærmere det "ekte" systemet, som ikke brukes ennå
		//Sørg for at man ikke kan øke Happiness ubegrenset:
		float happinessTimerStep = 1f * Time.deltaTime;
		happinessImprovementTimer = happinessImprovementTimer - happinessTimerStep;
		if (happinessImprovementTimer < 0) {
			canImproveHappiness = true;
		}
	}

    //Blir bonka av spilleren
    public void Dying (){
		print ("I'm dying!");
        dead = true;
		Vector3 deadRot = new Vector3(-90, 0, 0);
		transform.Rotate(deadRot, Space.Self);
	}

	public void HappinessGain (){
		print ("Yay! Happiness!");
		//Endre dette hvis man bruker "ordentlig" happiness
		myMedgjørlighet = myMedgjørlighet + GameObject.Find("Player").GetComponent<PlayerInteractions>().happinessToGive;
	} 

	public void LeaveGenetics (){
		Vector3 spawnPosition = this.transform.position;
		//Quaternion spawnRotation = this.transform.rotation;
		instantiatedGenMat = (GameObject)Instantiate(GenMat1, spawnPosition, GenMat1.transform.rotation);
		float skillvariation = UnityEngine.Random.Range(-10, 10);
		float skillTransfer = skill + skillvariation;
		int medgjørligvariation = UnityEngine.Random.Range(-20, 40);
		int medgjørligTransfer = myMedgjørlighet + medgjørligvariation;
		if (medgjørligTransfer > 99){
			medgjørligTransfer = 99;
		}
		instantiatedGenMat.GetComponent<GenetiskMateriale>().skillForCloning = skillTransfer;
		instantiatedGenMat.GetComponent<GenetiskMateriale>().medgjørlighetForCloning = medgjørligTransfer;
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
				GetComponentInParent<Innfallsystemet>().Interrupt();
			}
			if (medgjørlig > myMedgjørlighet){
				print ("Jeg gjør det jeg vil!");
			}
			//Gjør det mindre sannsynlig at de gjør som du sier neste gang
			myMedgjørlighet = myMedgjørlighet - 15;
			print(myMedgjørlighet);
			//Legg inn avbrudd av innfall her.
		}
	}
}

