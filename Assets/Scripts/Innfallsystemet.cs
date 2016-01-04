using UnityEngine;
using System.Collections.Generic;

public class Innfallsystemet : MonoBehaviour {

	public enum Innfall {Score, Strandtur, Nothing};
	public Dictionary<Innfall, int> innfallsOversikt;
	public int innfallsTall;
	public bool harInfall = false;
	public GameObject target;
	public bool riktigPlass = false;
	public int actionCounter;
	public string handlingGjennomført;

	private int innfallSum = 0;

	void Start (){
		InitializeInnfall();
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

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("z")){
			CheckInnfall();
		}
		if (target != null){
			//For at den ikke skal freake ut
		}
		if (harInfall == true){
			if (riktigPlass == true){
				actionCounter--;
			}
			if (actionCounter < 0){
				print (handlingGjennomført);
				//Add happiness
				harInfall = false;
			}
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


	//Bli drept av spilleren
	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Player" && Input.GetKeyDown("space")){
			GetComponentInParent<BandMember>().Dying();
		}
	}

	//Interruption
	public void Interrupt(){
		if (harInfall == true){
			harInfall = false;
			//Fjerner ikke target
			target = null;
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = null;
		}
	}


	//INNFALLSHANDLINGER
	void Score (){
		print ("I kveld scorer jeg!");
		harInfall = true;
		target = GameObject.Find("ScoreSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Jeg scorte!";
		//Tid
		actionCounter = 100;
	}
	
	void Strandtur (){
		print ("Jeg liker lange turer på stranden.");
		//harInfall = true;
		target = GameObject.Find("StrandSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Jeg gikk på stranden!";
		//Tid
		actionCounter = 600;
	}
}
