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


	//Innfall blir triggered av dette.////						V Innfall her V
	private bool scoreInnfall = false;
	private bool strandInnfall = false;

	//Innfallstriggers over.									^ Innfall her ^

	bool setActionCounter = false;
	bool innfallComplete = false;

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
			setActionCounter = false;
			CheckInnfall();
		}
		if (target != null){
			//For at den ikke skal freake ut
		}

		if (scoreInnfall == true){
			Score();
		}
		if (strandInnfall == true){
			Strandtur();
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
				scoreInnfall = true;
				break;
			}
		case Innfall.Strandtur:
			{
				strandInnfall = true;
				break;
			}
		case Innfall.Nothing:
			{
				break;
			}
		}
	}


	//BLI DREPT AV SPILLEREN
	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Player" && Input.GetKeyDown("space")){
			GetComponentInParent<BandMember>().Dying();
		}
	}

	//INTERRUPTION
	public void Interrupt(){
		if (harInfall == true){
			harInfall = false;
			//Fjerner ikke target
			target = null;
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = null;
		}
	}


	//INNFALLSHANDLINGER
	/// <summary>
	/// Her er alle innfallshandlingene!/////////////////////////////////////////////////////////////////
	/// </summary>
	//Skal score
	void Score (){
		print ("I kveld scorer jeg!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().scoreTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("ScoreSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			int reduceCounter = 1 * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				print (handlingGjennomført);
				innfallComplete = true;
			}
		}
		if (handlingGjennomført == true){
			handlingGjennomført = "Jeg scorte!";
			WrapUp();
		}
	}
		
	void Strandtur (){
		print ("Jeg liker lange turer på stranden.");
		harInfall = true;
		target = GameObject.Find("StrandSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Jeg gikk på stranden!";
		//Tid
		actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().strandTid;
	}
		
	void Solokarriere (){
		print ("Jeg vil starte en solokarriere.");
		harInfall = true;
		target = GameObject.Find("SoloSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "I'm outta here!";
		//Tid
		actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().soloTid;
	}

	void MusikkLytting (){
		print ("Jeg vil høre på musikk.");
		harInfall = true;
		target = GameObject.Find("MusikkSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Jeg hørte på litt muzak!";
		//Tid
		actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().høreTid;
	}

	void SintTweet (){
		print ("Jeg er pissed og vil at hele verden skal vite det!");
		harInfall = true;
		target = GameObject.Find("TweetSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Så sant som det er sagt! Av meg!";
		//Tid
		actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().tweeteTid;
	}

	void GladTweet (){
		print ("Jeg vil fortelle fansen hvor mye de betyr for meg!");
		harInfall = true;
		target = GameObject.Find("TweetSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Ah, det føles så deilig å få masse anerkjennelse fra fornøyde fans!";
		//Tid
		actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().tweeteTid;
	}

	void Drikke (){
		print ("Nå skarre drekkes!");
		harInfall = true;
		target = GameObject.Find("DrikkeSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Yay, drekking! Livet betyr mer nå!";
		//Tid
		actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().drikkeTid;
	}

	void Dusje (){
		print ("Oh boy, jeg trenger en dusj!");
		harInfall = true;
		target = GameObject.Find("DusjeSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Ah, det føles så deilig å få masse anerkjennelse fra fornøyde fans!";
		//Tid
		actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().dusjeTid;
	}

	void Spise (){
		print ("Nå er jeg sulten!");
		harInfall = true;
		target = GameObject.Find("SpiseSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Nå er jeg mett!";
		//Tid
		actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().dusjeTid;
	}

	void SexyDance (){
		print ("Nå skal jeg ha litt alenetid!");
		harInfall = true;
		target = GameObject.Find("DanceSted");
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		handlingGjennomført = "Phew! Litt av en runde!";
		//Tid
		actionCounter = GameObject.Find("CameControl").GetComponent<GameControl>().danceTid;
	}

	//Øve på spilling
	//Krever spesifikk ordning pr. bandmedlem i forhold til instrument etc.


	//Ordner opp i ting når et innfall er over. Ikke nødvendig?
	//Må ordne at hvert innfall deaktiveres her.
	/// <summary>
	/// Ferdig med innfallshandlinger/////////////////////////////////////////////////////////////////////////
	/// </summary>


	void WrapUp(){
		print (handlingGjennomført);
		harInfall = false;
		scoreInnfall = false;
		strandInnfall = false;
	}
}
