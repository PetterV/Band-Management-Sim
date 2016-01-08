using UnityEngine;
using System.Collections.Generic;

public class Innfallsystemet : MonoBehaviour {

	public enum Innfall {Score, Strandtur, Solo, Lytte, SintTweet, GladTweet, Drikke, Spise, Dusje, Danse, Ove, GoLeft, GoRight, Nothing};
	public Dictionary<Innfall, int> innfallsOversikt;
	public int innfallsTall;
	public bool harInnfall = false;
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
			{Innfall.GoLeft, 0 },
			{Innfall.GoRight, 0 },
			{Innfall.Nothing, 100 } //Sannsynligheten for Nothing er sju ganger større enn Score
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
		if (harInnfall == false){
			textToDisplay = null;
			setActionCounter = false;
			CheckInnfall();
		}
		if (harInnfall == true){
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

	void CheckInnfall () {
		//Generer innfallstall og sjekk mot innfallsoversikten.
		this.innfallsTall = UnityEngine.Random.Range(0, innfallSum);
		int tempUpperBound = 0;
		foreach(KeyValuePair<Innfall, int> entry in this.innfallsOversikt)
		{
			tempUpperBound += entry.Value;
			if (innfallsTall < tempUpperBound)
			{
				harInnfall = true;
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
				WrapUp();
				break;
			}
		}
	}





	//Prøv å finne ut hvorfor jeg flyttet denne hit. Var det et uhell eller en god idé?
	//BLI DREPT AV SPILLEREN

	//INTERRUPTION
	public void Interrupt(){
		if (harInnfall == true){
			WrapUp();
		}
	}

	void Score (){
		ActuallyDoInfall("", "ScoreSted", "Jeg scorte", false);
	}
		//Strandturinnfall
	void Strandtur (){
		ActuallyDoInfall("", "StrandSted", "Nå har jeg strand i skoene.", false);
	}
		//solokarriæreinnfall
	void Solokarriere (){
		ActuallyDoInfall("Jeg vil starte solokarriere!", "SoloSted", "I'm outta here!", false);
	}
    //Musikklytteinnfal
	void MusikkLytting (){
		ActuallyDoInfall("Jeg vil høre på musikk.", "LytteSted", "Jeg hørte på litt muzak.", false);
	}
        //SintTweetInnfal
	void SintTweet (){
		ActuallyDoInfall("Jeg er pissed of vil at hele verden skal vite det!", "TweeteSted", "Det var godt å få fram!", false);
	}
        //Glad Tweet Innfall
	void GladTweet (){
		ActuallyDoInfall("Jeg vil fortelle fansen hvor mye jeg setter pris på dem!", "TweeteSted", "Jeg elsker å bli satt pris på.", false);
	}
       //Drikkeinnfall
	void Drikke (){
		ActuallyDoInfall("Nå skarre drekkes", "DrikkeSted", "Yay drekking! Livet betyr mer nå!", false);
	}
        //Dusjeinnfall
	void Dusje (){
		ActuallyDoInfall("Oh boy, jeg trenger en dusj.", "DusjeSted", "Er dette sånn jeg egentlig lukter?", false);
	}
        //Spiseinnfall
	void Spise (){
		ActuallyDoInfall("Nå er jeg sulten!", "SpiseSted", "Nå er jeg mett!", false);
	}
        //SexyDanceinnfall
	void SexyDance (){
		ActuallyDoInfall("Nå trenger jeg litt alenetid!", "DanseSted", "Det var godt å få fram.", false);
	}

	void ActuallyDoInfall(string statement, string placeToWalkTo, string handlingGjennomfort, bool increaseSkill){
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().oveTid;
			setActionCounter = true;
		}
		if (!riktigPlass){
			target = GameObject.Find(placeToWalkTo);
			if (target == null)
				print ("ERROR ERROR ERROR, COULD NOT FIND " + placeToWalkTo);
			GetComponent<BandMemberMoving>().waypointToMoveTo = target;
		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			if (actionCounter <= 0){
				innfallComplete = true;
			}
		}
		if (innfallComplete == true){
			handlingGjennomfort = handlingGjennomfort;
			if(increaseSkill){
				int skillIncrease = UnityEngine.Random.Range(1, 5);
				GetComponent<BandMember>().skill = GetComponent<BandMember>().skill + skillIncrease; 
			}
			WrapUp();
		}
	}
        //Practiceinnfall
	void Ove (){
		ActuallyDoInfall("Nå skal jeg øve.", "OveSted", "Nå er jeg bedre!", true);
	}

	void GoRight (){
		harInnfall = true;
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
		harInnfall = true;
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
		harInnfall = false;
		target = null;
		riktigPlass = false;
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
