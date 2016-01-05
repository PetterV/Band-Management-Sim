using UnityEngine;
using System.Collections.Generic;

public class Innfallsystemet : MonoBehaviour {

	public enum Innfall {Score, Strandtur, Nothing};
	// Legg til enum: Solo, Lytte, SintTweet, GladTweet, Drikke, Spise, Dusje, Danse, Øve,
	public Dictionary<Innfall, int> innfallsOversikt;
	public int innfallsTall;
	public bool harInfall = false;
	public GameObject target;
	public bool riktigPlass = false;
	public float actionCounter;
	public bool success = false;
	public string handlingGjennomført;


	//Innfall blir triggered av dette.////						V Innfall her V
	private bool scoreInnfall = false;
	private bool strandInnfall = false;
	private bool soloInnfall = false;
	private bool lytteInnfall = false;
	private bool sintTweetInnfall = false;
	private bool gladTweetInnfall = false;
	private bool drikkeInnfall = false;
	private bool dusjeInnfall = false;
	private bool spiseInnfall = false;
	private bool danseInnfall = false;
	private bool oveInnfall = false;
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
//			{Innfall.Solo, 1 },
//			{Innfall.Lytte, 3 },
//			{Innfall.SintTweet, 2 },
//			{Innfall.GladTweet, 3 },
//			{Innfall.Drikke, 5 },
//			{Innfall.Spise, 6 },
//			{Innfall.Dusje, 4 },
//			{Innfall.Danse, 4 },
//			{Innfall.Øve, 6 },
			{Innfall.Nothing, 50 } //Sannsynligheten for Nothing er sju ganger større enn Score
		};
		foreach (KeyValuePair<Innfall, int> entry in innfallsOversikt)
		{
			innfallSum += entry.Value;
		}
	}





	//UPDATE LIGGGER HER
	/// <summary>
	/// 
	/// ///////////////////////////////////////////////
	/// </summary>

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("z")){
			setActionCounter = false;
			CheckInnfall();
		}
		if (scoreInnfall == true){
			Score();
		}
		if (strandInnfall == true){
			Strandtur();
		}
	}





	/// <summary>
	/// CHECK OG PERFORM INNFALL LIGGER HER	/////////////////////////
	/// </summary>
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
//		case Innfall.Solo:
//			{
//				soloInnfall = true;
//				break;
//			}
//		case Innfall.Score:
//			{
//				lytteInnfall = true;
//				break;
//			}
//		case Innfall.Strandtur:
//			{
//				sintTweetInnfall = true;
//				break;
//			}
//		case Innfall.Solo:
//			{
//				gladTweetInnfall = true;
//				break;
//			}
//		case Innfall.Nothing:
//			{
//				drikkeInnfall = true;
//				break;
//			}
//		case Innfall.Score:
//			{
//				scoreInnfall = true;
//				break;
//			}
//		case Innfall.Strandtur:
//			{
//				dusjeInnfall = true;
//				break;
//			}
//		case Innfall.Solo:
//			{
//				spiseInnfall = true;
//				break;
//			}
//		case Innfall.Nothing:
//			{
//				danseInnfall = true;
//				break;
//			}
//		case Innfall.Øve:
//			{
//				oveInnfall = true;
//				break;
//			}
		case Innfall.Nothing:
			{
				break;
			}
		}
	}





	//Prøv å finne ut hvorfor jeg flyttet denne hit. Var det et uhell eller en god idé?
	//BLI DREPT AV SPILLEREN
	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Player" && Input.GetKeyDown("space")){
			Interrupt();
			GetComponentInParent<BandMember>().Dying();
		}
	}

	//INTERRUPTION
	public void Interrupt(){
		if (harInfall == true){
			WrapUp();
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
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().scoreTid;
			setActionCounter = true;
		}
		if (riktigPlass = false){
			target = GameObject.Find("ScoreSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass = true){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "Jeg scorte!";
			WrapUp();
		}
	}
		
	void Strandtur (){
		print ("Jeg liker lange turer på stranden!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().strandTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("StrandSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "Nå har jeg sand i skoene.";
			WrapUp();
		}
	}
		
	void Solokarriere (){
		print ("Jeg vil starte solokarriere!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().soloTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("SoloSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "I'm outta here!";
			WrapUp();
		}
	}

	void MusikkLytting (){
		print ("Jeg vil høre på musikk.");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().høreTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("LytteSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "Jeg hørte på litt muzak.";
			WrapUp();
		}
	}

	void SintTweet (){
		print ("Jeg er pissed og vil at hele verden skal vite det!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().tweeteTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("TweeteSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "Det var godt å få fram!";
			WrapUp();
		}
	}

	void GladTweet (){
		print ("Jeg vil fortelle fansen hvor mye jeg setter pris på dem!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().tweeteTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("TweeteSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "Jeg elsker å bli satt pris på selv.";
			WrapUp();
		}
	}

	void Drikke (){
		print ("Nå skarre drekkes!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().drikkeTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("DrikkeSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "Yay drekking! Livet betyr mer nå!";
			WrapUp();
		}
	}

	void Dusje (){
		print ("Oh boy, jeg trenger en dusj!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().dusjeTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("DusjeSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "Er dette sånn jeg egentlig lukter?";
			WrapUp();
		}
	}

	void Spise (){
		print ("Nå er jeg sulten!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().spiseTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("SpiseSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "Nå er jeg mett!";
			WrapUp();
		}
	}

	void SexyDance (){
		print ("Nå trenger jeg litt alenetid!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter = false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().danceTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("DanseSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomført = "Det var godt å få fram!";
			WrapUp();
		}
	}

	//Øve på spilling
	//Krever spesifikk ordning pr. bandmedlem i forhold til instrument etc.


	//Ordner opp i ting når et innfall er over. Ikke nødvendig?
	//Må ordne at hvert innfall deaktiveres her.
	/// <summary>
	/// Ferdig med innfallshandlinger/////////////////////////////////////////////////////////////////////////
	/// </summary>


	void WrapUp(){
		if (innfallComplete == true){
			print (handlingGjennomført);
		}
		harInfall = false;
		scoreInnfall = false;
		strandInnfall = false;
		soloInnfall = false;
		lytteInnfall = false;
		sintTweetInnfall = false;
		gladTweetInnfall = false;
		drikkeInnfall = false;
		dusjeInnfall = false;
		spiseInnfall = false;
		danseInnfall = false;
		oveInnfall = false;
		innfallComplete = false;
	}
}
