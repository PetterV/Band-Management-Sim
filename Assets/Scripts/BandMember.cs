using UnityEngine;
using System;

public class BandMember : MonoBehaviour{
	
	public enum Role{Drummer, Singer, GuitarPlayer, BassPlayer}; 

	public Role role;
	public Stats stats;
	public int skill;
	public int innfallsTall;

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
			innfallsTall = UnityEngine.Random.Range(1, 100);
			Innfall();
		}
	}

	void Innfall () {
		print (innfallsTall);
		if (innfallsTall <= 50){
			Score();
		}
		if (innfallsTall > 50){
			Strandtur();
		}
	}

	void Score (){
		print ("I kveld scorer jeg!");
	}

	void Strandtur (){
		print ("Jeg liker lange turer på stranden");
	}
}

