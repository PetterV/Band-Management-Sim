using UnityEngine;
using System.Collections.Generic;

public class Innfallsystemet : MonoBehaviour {

	public enum Innfall {Score, Strandtur, Solo, Lytte, SintTweet, GladTweet, Drikke, Spise, Dusje, Danse, Ove, GoLeft, GoRight, Nothing};
	public Dictionary<Innfall, int> innfallsOversikt;
	public int innfallsTall;
	public bool harInfall = false;
	public GameObject target;
	public bool riktigPlass = false;
	private float actionCounter;
	public bool success = false;
	public string handlingGjennomfort;
	private Animator animator;

	private bool tooCloseToLeftWall;
	private bool tooCloseToRightWall;

    public GameObject gameControl;


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
	private bool innfallGoRight = false;
	private bool innfallGoLeft = false;
	//Innfallstriggers over.									^ Innfall her ^

	public string textToDisplay;


	bool setActionCounter = false;
	bool innfallComplete = false;
	bool goingThere = false;
	float moveTarget;
	float moveThisStep;

	private int innfallSum = 0;

	void Start (){
		tooCloseToLeftWall = false;
		tooCloseToRightWall = false;
		InitializeInnfall();
		animator = GetComponent<Animator>();
        gameControl = GameObject.FindWithTag("GameController");
	}

	private void InitializeInnfall()
	{
		//Dette er Innfalls-oversikten. Hvis du vil legge til flere, gjør du det på samme måte som her.
		this.innfallsOversikt = new Dictionary<Innfall, int>()
		{
			{Innfall.Score, 1 },
			{Innfall.Strandtur, 1 }, //sannsynligheten for Strandtur er dobbelt så stor som Score.
			{Innfall.Solo, 1 },
			{Innfall.Lytte, 1 },
			{Innfall.SintTweet, 1 },
			{Innfall.GladTweet, 1 },
			{Innfall.Drikke, 1 },
			{Innfall.Spise, 1 },
			{Innfall.Dusje, 1 },
			{Innfall.Danse, 1 },
			{Innfall.Ove, 1 },
			{Innfall.GoLeft, 1 },
			{Innfall.GoRight, 1 },
			{Innfall.Nothing, 1 } //Sannsynligheten for Nothing er sju ganger større enn Score
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
		if (harInfall == false){
			textToDisplay = null;
		}
		if (Input.GetKeyDown("z")){
			
		}
		if (harInfall == false){
		setActionCounter = false;
		CheckInnfall();
		}
		if (target == null){
			
		}
		if (scoreInnfall == true){
			Score();
		}
		if (strandInnfall == true){
			Strandtur();
		}
		if (soloInnfall == true){
			Solokarriere();
		}
		if (lytteInnfall == true){
			MusikkLytting();
		}
		if (sintTweetInnfall == true){
			SintTweet();
		}
		if (gladTweetInnfall == true){
			GladTweet();
		}
		if (drikkeInnfall == true){
			Drikke();
		}
		if (dusjeInnfall == true){
			Dusje();
		}
		if (spiseInnfall == true){
			Spise();
		}
		if (danseInnfall == true){
			SexyDance();
		}
		if (oveInnfall == true){
			Ove();
		}
		if (innfallGoLeft == true && !tooCloseToLeftWall){
			GoLeft();
		}
		if (innfallGoRight == true && !tooCloseToRightWall){
			GoRight();
		}
	}

	void OnTriggerStay(Collider colli)
	{
		if (colli.gameObject.tag == "Wall") {
			if (innfallGoRight || innfallGoLeft) {
				if (NextToLeftWall (colli)) {
					this.tooCloseToLeftWall = true;
				} else {
					this.tooCloseToRightWall = true;
				}
			}
		}
	}

	void OnTriggerExit(Collider coll){
		if (coll.gameObject.tag == "Wall") {
			this.tooCloseToLeftWall = false;
			this.tooCloseToRightWall = false;
		}
	}

	bool NextToLeftWall(Collider colli){
		return colli.transform.position.x < this.transform.position.x;
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
	}

	void performInnfall(Innfall inn)
    {
        ///spiller av innfallslyd
        if (inn != Innfall.Nothing)
        {
            gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
        }

        switch (inn)
		{
		case Innfall.Score:
			{
				scoreInnfall = true;
				print ("I kveld scorer jeg!");
				textToDisplay = "I kveld scorer jeg!";
				break;
			}
		case Innfall.Strandtur:
			{
				strandInnfall = true;
				print ("Jeg liker lange turer på stranden!");
				textToDisplay = "I kveld scorer jeg!";
				break;
			}
		case Innfall.Solo:
			{
				soloInnfall = true;
				textToDisplay = "I kveld scorer jeg!";
				break;
			}
		case Innfall.Lytte:
			{
				lytteInnfall = true;
				textToDisplay = "I kveld scorer jeg!";
				break;
			}
		case Innfall.SintTweet:
			{
				sintTweetInnfall = true;
				break;
			}
		case Innfall.GladTweet:
			{
				gladTweetInnfall = true;
				break;
			}
		case Innfall.Drikke:
			{
				drikkeInnfall = true;
				break;
			}
		case Innfall.Dusje:
			{
				dusjeInnfall = true;
				break;
			}
		case Innfall.Spise:
			{
				spiseInnfall = true;
				break;
			}
		case Innfall.Danse:
			{
				danseInnfall = true;
				break;
			}
		case Innfall.Ove:
			{
				oveInnfall = true;
				break;
			}
		case Innfall.GoRight:
			{
				innfallGoRight = true;
				animator.SetInteger("Walking", 1);
				textToDisplay = "Jeg går til høyre!";
				break;
			}
		case Innfall.GoLeft:
			{
				innfallGoLeft = true;
				animator.SetInteger("Walking", 1);
				textToDisplay = "Jeg går til venstre!";
				break;
			}
		case Innfall.Nothing:
			{
				break;
			}
		}
	}





	//Prøv å finne ut hvorfor jeg flyttet denne hit. Var det et uhell eller en god idé?
	//BLI DREPT AV SPILLEREN

	//INTERRUPTION
	public void Interrupt(){
		if (harInfall == true){
			WrapUp();
			this.gameObject.GetComponent<BandMemberMoving>().waypointToMoveTo = null;
			target = null;
		}
	}






	//INNFALLSHANDLINGER
	/// <summary>
	/// Her er alle innfallshandlingene!/////////////////////////////////////////////////////////////////
	/// </summary>
	//Skal score
	void Score (){
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().scoreTid;
			setActionCounter = true;
		}
		if (riktigPlass == false){
			target = GameObject.Find("ScoreSted");
			GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;	
		}
		else if (riktigPlass == true){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomfort = "Jeg scorte!";
			WrapUp();
		}
	}
		//Strandturinnfall
	void Strandtur (){
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
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
			handlingGjennomfort = "Nå har jeg strand i skoene.";
			WrapUp();
		}
	}
		//solokarriæreinnfall
	void Solokarriere (){
		print ("Jeg vil starte solokarriere!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
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
			handlingGjennomfort = "I'm outta here!";
			WrapUp();
		}
	}
    //Musikklytteinnfal
	void MusikkLytting (){
		print ("Jeg vil høre på musikk.");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().lytteTid;
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
			handlingGjennomfort = "Jeg hørte på litt muzak.";
			WrapUp();
		}
	}
        //SintTweetInnfal
	void SintTweet (){
		print ("Jeg er pissed og vil at hele verden skal vite det!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
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
			handlingGjennomfort = "Det var godt å få fram!";
			WrapUp();
		}
	}
        //Glad Tweet Innfall
	void GladTweet (){
		print ("Jeg vil fortelle fansen hvor mye jeg setter pris på dem!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
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
			handlingGjennomfort = "Jeg elsker å bli satt pris på selv.";
			WrapUp();
		}
	}
       //Drikkeinnfall
	void Drikke (){
		print ("Nå skarre drekkes!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
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
			handlingGjennomfort = "Yay drekking! Livet betyr mer nå!";
			WrapUp();
		}
	}
        //Dusjeinnfall
	void Dusje (){
		print ("Oh boy, jeg trenger en dusj!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
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
			handlingGjennomfort = "Er dette sånn jeg egentlig lukter?";
			WrapUp();
		}
	}
        //Spiseinnfall
	void Spise (){
		print ("Nå er jeg sulten!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
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
			handlingGjennomfort = "Nå er jeg mett!";
			WrapUp();
		}
	}
        //SexyDanceinnfall
	void SexyDance (){
		print ("Nå trenger jeg litt alenetid!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
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
			handlingGjennomfort = "Det var godt å få fram!";
			WrapUp();
		}
	}
        //Practiceinnfall
	void Ove (){
		print ("Nå skal jeg øve!");
		harInfall = true;
		GetComponentInParent<BandMemberMoving>().waypointToMoveTo = target;
		if (setActionCounter == false){
			actionCounter = GameObject.Find("GameControl").GetComponent<GameControl>().oveTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find("OveSted");
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
			handlingGjennomfort = "Nå er jeg bedre!";
			int skillIncrease = UnityEngine.Random.Range(1, 5);
			GetComponentInParent<BandMember>().skill = GetComponentInParent<BandMember>().skill + skillIncrease; 
			WrapUp();
		}
	}

	void GoRight (){
		if (goingThere == false){
			moveThisStep = this.transform.position.x + 1;
			float walkDistance = UnityEngine.Random.Range(0, 3);
			moveTarget = transform.position.x + walkDistance;
			goingThere = true;
		}
		if (transform.position.x < moveTarget){
			moveThisStep = transform.position.x + 0.1f;
			//print ("Going right!");
			this.gameObject.GetComponent<BandMemberMoving>().HeadRight();
			transform.position = new Vector3 (moveThisStep, this.transform.position.y, this.transform.position.z);
		}
		if (transform.position.x >= moveTarget){
			//print ("Got there!");
			WrapUp();
		}
	}

	void GoLeft (){
		if (goingThere == false){
			moveThisStep = this.transform.position.x + 1;
			float walkDistance = UnityEngine.Random.Range(0, 3);
			moveTarget = transform.position.x - walkDistance;
			goingThere = true;
		}
		if (transform.position.x > moveTarget){
			moveThisStep = transform.position.x - 0.1f;
			//print ("Going left!");
			this.gameObject.GetComponent<BandMemberMoving>().HeadLeft();
			transform.position = new Vector3 (moveThisStep, this.transform.position.y, this.transform.position.z);
		}
		if (transform.position.x <= moveTarget){
			//print ("Got there");
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
		//print ("Wrapping up!");
		if (innfallComplete == true){
			print (handlingGjennomfort);
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
		goingThere = false;
		innfallGoLeft = false;
		innfallGoRight = false;
		animator.SetInteger("Walking", 0);
		GetComponent<BandMember>().fikkKjeft = false;
	}
}
