using UnityEngine;
using System;

public class BandMember : MonoBehaviour{
	
	public enum Role{Drummer, Singer, GuitarPlayer, BassPlayer}; 

	public Role role;
	public Stats stats;
	public int skill;
	public int innfallsTall;
	public bool active = true;
	public bool dead = false; 

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
		if (Input.GetKeyDown("z")){
			//Generer innfallstall og sjekk mot innfallsoversikten.
			innfallsTall = UnityEngine.Random.Range(1, 100);
			Innfall();
		}
	}

	//Innfallsoversikten
	void Innfall () {
		print (innfallsTall);
		if (innfallsTall <= 50){
			Score();
		}
		if (innfallsTall > 50){
			Strandtur();
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
			dead = true;
			Dying();
		}
	}

	//Blir bonka av spilleren
	void Dying (){
		print ("I'm dying!");
		Destroy(gameObject);
	}
}

