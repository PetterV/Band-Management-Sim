using UnityEngine;
using System;

public class BandMember : MonoBehaviour{
	
	//maa kanskje flytte denne ut til egen klasse senere, slik at andre GameObjects/klasser kan accesse :)
	public enum Role{Drummer, Singer, GuitarPlayer, BassPlayer}; 

	public String givenName;
	public int id;
	public Role role;
	public Stats stats;
	public int skill;
	public int innfallsTall;
	public bool active = true;
	public bool dead = false; 

	public BandMember (String name, int id, int skill, Role role)
	{
		this.name = name;
		this.id = id;
		this.stats = new Stats(skill);
		this.role = role;
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
		print ("Jeg liker lange turer på stranden.");
	}
}

