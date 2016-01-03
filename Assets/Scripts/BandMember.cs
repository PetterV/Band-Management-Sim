using UnityEngine;
using System;
using System.Collections;

public class BandMember : MonoBehaviour{

	public enum Role{Drummer, Singer, GuitarPlayer, BassPlayer};

	public Role role;
	public Stats stats;
	public int skill;
	public bool active = true;
	public bool dead = false;

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

    void Start ()
    {
        print("DEBUG - CLONE WAS MADE - CLICK ON THIS MESSAGE FOR MORE INFO:\n " + "Name: " + this.name + "\nSkill: " + this.skill + "\nRole: " + this.role);
    }

	// Update is called once per frame
	void Update () {
		if (active == true && Input.GetKeyDown("g")){
			LeaveGenetics();
		}
	}



	//Blir bonka av spilleren
	public void Dying (){
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

