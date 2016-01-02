using UnityEngine;
using System;

public class BandMember : MonoBehaviour{
	
	public enum Role{Drummer, Singer, GuitarPlayer, BassPlayer};

    //Dette er Innfalls-oversikten. Hvis du vil legge til flere, gjør du det på samme måte som her. Navnet på innfallet = <Sjansen for at innfallet slår til, høyere = mer sannsynlig>

    public enum Innfall { Score = 2, Strandtur = 1, Nothing = 10 };

	public Role role;
	public Stats stats;
	public int skill;
	public int innfallsTall;
	public bool active = true;
	public bool dead = false;

    private int innfallSum = 0;

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
        
        foreach (Innfall inn in Enum.GetValues(typeof(Innfall)))
        {
            innfallSum += (int)inn;
        }
        print("DEBUG - CLONE WAS MADE - CLICK ON THIS MESSAGE FOR MORE INFO:\n " + "Name: " + this.name + "\nSkill: " + this.skill + "\nRole: " + this.role);
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("z")){
			CheckInnfall();
		}
	}

	//Innfallsoversikten ligger i enumen Innfall øverst.
	void CheckInnfall () {
        //Generer innfallstall og sjekk mot innfallsoversikten.
        this.innfallsTall = UnityEngine.Random.Range(0, innfallSum);
        int tempUpperBound = 0;
        foreach(Innfall inn in Enum.GetValues(typeof(Innfall)))
        {
            tempUpperBound += (int)inn;
            if (innfallsTall < tempUpperBound)
            {
                performInnfall(inn);
                break;
            }
                
        }
        print (innfallsTall);
		/*if (innfallsTall <= 50){
			Score();
		}
		if (innfallsTall > 50){
			Strandtur();
		}*/
	}

    void performInnfall(Innfall inn)
    {
        if (inn == Innfall.Score)
            Score();
        else if (inn == Innfall.Strandtur)
            Strandtur();
        else if (inn == Innfall.Nothing)
            return;
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

