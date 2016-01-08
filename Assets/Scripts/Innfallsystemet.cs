using UnityEngine;
using UnityEngine.UI;
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

	//Needs to be set individually for each member
	public Image progressBar;
	private float maxActionCounter;


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

	public string crossDisplay;
	public string ronnyDisplay;
	public string keithDisplay;
	public string hannahDisplay;
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
			{Innfall.Score, 5 },
			{Innfall.Strandtur, 5 }, //sannsynligheten for Strandtur er dobbelt så stor som Score.
			{Innfall.Solo, 1 },
			{Innfall.Lytte, 5 },
			{Innfall.SintTweet, 3 },
			{Innfall.GladTweet, 3 },
			{Innfall.Drikke, 5 },
			{Innfall.Spise, 8 },
			{Innfall.Dusje, 5 },
			{Innfall.Danse, 10 },
			{Innfall.Ove, 10 },
			{Innfall.GoLeft, 50 },
			{Innfall.GoRight, 50 },
			{Innfall.Nothing, 5000 } //Sannsynligheten for Nothing er sju ganger større enn Score
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
			if (GetComponent<BandMember>().role == BandMember.Role.GuitarPlayer){
			gameControl.GetComponent<Innfallsprinting>().crossSier = null;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.Drummer){
				gameControl.GetComponent<Innfallsprinting>().ronnySier = null;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.Singer){
				gameControl.GetComponent<Innfallsprinting>().keithSier = null;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.BassPlayer){
				gameControl.GetComponent<Innfallsprinting>().crossSier = null;
			}
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
		this.innfallsTall = UnityEngine.Random.Range(0, innfallSum+1);
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
          //  gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
        }

        switch (inn)
		{
		case Innfall.Score:
			{
				harInnfall = true;
				scoreInnfall = true;
				print ("I kveld scorer jeg!");
				textToDisplay = "I kveld scorer jeg!";
				break;
			}
		case Innfall.Strandtur:
			{
				harInnfall = true;
				strandInnfall = true;
				print ("Jeg liker lange turer på stranden!");
				textToDisplay = "I kveld scorer jeg!";
				break;
			}
		case Innfall.Solo:
			{
				harInnfall = true;
				soloInnfall = true;
				textToDisplay = "I kveld scorer jeg!";
				break;
			}
		case Innfall.Lytte:
			{
				harInnfall = true;
				lytteInnfall = true;
				textToDisplay = "I kveld scorer jeg!";
				break;
			}
		case Innfall.SintTweet:
			{
				harInnfall = true;
				sintTweetInnfall = true;
				break;
			}
		case Innfall.GladTweet:
			{
				harInnfall = true;
				gladTweetInnfall = true;
				break;
			}
		case Innfall.Drikke:
			{
				harInnfall = true;
				drikkeInnfall = true;
				break;
			}
		case Innfall.Dusje:
			{
				harInnfall = true;
				dusjeInnfall = true;
				break;
			}
		case Innfall.Spise:
			{
				harInnfall = true;
				spiseInnfall = true;
				break;
			}
		case Innfall.Danse:
			{
				harInnfall = true;
				danseInnfall = true;
				break;
			}
		case Innfall.Ove:
			{
				harInnfall = true;
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
		ActuallyDoInfall("I kveld scorer jeg!", "ScoreSted", "Nå scorer jeg, du.", "Jeg scorte", false, false, true, false, false, false, 0, false, 0, true, 1);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().scoreTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
		//Strandturinnfall
	void Strandtur (){
		ActuallyDoInfall("Jeg vil gå på stranden", "StrandSted", "Jeg går på stranden", "Nå har jeg strand i skoene.", false, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().strandTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
		//solokarriæreinnfall
	void Solokarriere (){
		ActuallyDoInfall("Jeg vil starte solokarriere!", "SoloSted", "Jeg forlater bandet!", "I'm outta here!", false, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().soloTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
    //Musikklytteinnfal
	void MusikkLytting (){
		ActuallyDoInfall("Jeg vil høre på musikk.", "LytteSted", "Mmm, dette er good shit.", "Jeg hørte på litt muzak.", false, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().lytteTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //SintTweetInnfal
	void SintTweet (){
		ActuallyDoInfall("Jeg er pissed of vil at hele verden skal vite det!", "TweeteSted", "Denne tweeten kommer til å sjokkere!", "Det var godt å få fram!", false, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().tweeteTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //Glad Tweet Innfall
	void GladTweet (){
		ActuallyDoInfall("Jeg vil fortelle fansen hvor mye jeg setter pris på dem!", "TweeteSted", "Publikum kommer til å digge denne tweeten.", "Jeg elsker å bli satt pris på.", false, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().tweeteTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
       //Drikkeinnfall
	void Drikke (){
		ActuallyDoInfall("Nå skarre drekkes", "DrikkeSted", "Jeg drekker som bare fy!", "Yay drekking! Livet betyr mer nå!", false, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().drikkeTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //Dusjeinnfall
	void Dusje (){
		ActuallyDoInfall("Oh boy, jeg trenger en dusj.", "DusjeSted", "Deilig å dusje, as.", "Er dette sånn jeg egentlig lukter?", false, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().dusjeTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //Spiseinnfall
	void Spise (){
		ActuallyDoInfall("Nå er jeg sulten!", "SpiseSted", "Nom nom nom.", "Nå er jeg mett!", false, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().spiseTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //SexyDanceinnfall
	void SexyDance (){
		ActuallyDoInfall("Nå trenger jeg litt alenetid!", "DanseSted", "Ooooh yeah.", "Det var godt å få fram.", false, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().danceTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}

		//Practiceinnfall
	void Ove (){
		ActuallyDoInfall("Nå skal jeg øve.", "OveSted", "La oss se... A, så G, så...", "Nå er jeg bedre!", true, false, false, false, false, false, 0, false, 0, false, 0);
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().oveTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}

	void ActuallyDoInfall(string statement, string placeToWalkTo, string doing, string handlingGjennomfort, bool increaseSkill, bool decreaseSkill, bool increaseHappiness, bool decreaseHappiness, bool leaveGenetics, bool increasePublicSuspicion, float susPubInc, bool increaseSuspicion, float mySusInc, bool increasePopFactor, float popFacInc){
		if (!riktigPlass){
			target = GameObject.Find(placeToWalkTo);
			if (target == null)
				print ("ERROR ERROR ERROR, COULD NOT FIND " + placeToWalkTo);
			GetComponent<BandMemberMoving>().waypointToMoveTo = target;

			//Send beskjed til tekstbobler
			if (GetComponent<BandMember>().role == BandMember.Role.GuitarPlayer){
				gameControl.GetComponent<Innfallsprinting>().crossSier = statement;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.Drummer){
				gameControl.GetComponent<Innfallsprinting>().ronnySier = statement;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.Singer){
				gameControl.GetComponent<Innfallsprinting>().keithSier = statement;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.BassPlayer){
				gameControl.GetComponent<Innfallsprinting>().hannahSier = statement;
			}

		}
		else if (riktigPlass){
			float reduceCounter = 1f * Time.deltaTime;
			actionCounter = actionCounter - reduceCounter;
			float progToBeFilled = actionCounter / maxActionCounter;
			progressBar.fillAmount = 1 - progToBeFilled;
			//Send beskjed til tekstbobler
			//ERSTATT DISSE MED EN TREDJE STRING!!!
			if (GetComponent<BandMember>().role == BandMember.Role.GuitarPlayer){
				gameControl.GetComponent<Innfallsprinting>().crossSier = doing;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.Drummer){
				gameControl.GetComponent<Innfallsprinting>().ronnySier = doing;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.Singer){
				gameControl.GetComponent<Innfallsprinting>().keithSier = doing;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.BassPlayer){
				gameControl.GetComponent<Innfallsprinting>().hannahSier = doing;
			}
			if (actionCounter <= 0){
				innfallComplete = true;
			}

		}
		if (innfallComplete == true){
			handlingGjennomfort = handlingGjennomfort;

			//Si ifra til snakkebobla
			if (GetComponent<BandMember>().role == BandMember.Role.GuitarPlayer){
				gameControl.GetComponent<Innfallsprinting>().crossSier = handlingGjennomfort;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.Drummer){
				gameControl.GetComponent<Innfallsprinting>().ronnySier = handlingGjennomfort;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.Singer){
				gameControl.GetComponent<Innfallsprinting>().keithSier = handlingGjennomfort;
			}
			if (GetComponent<BandMember>().role == BandMember.Role.BassPlayer){
				gameControl.GetComponent<Innfallsprinting>().hannahSier = handlingGjennomfort;
			}

			//Effekter av å fullføre
			if(increaseSkill){
				int skillIncrease = UnityEngine.Random.Range(1, 5);
				GetComponent<BandMember>().skill = GetComponent<BandMember>().skill + skillIncrease; 
			}
			if(decreaseSkill){
				int skillIncrease = UnityEngine.Random.Range(-5, -1);
				GetComponent<BandMember>().skill = GetComponent<BandMember>().skill + skillIncrease;
			}
			if(increaseHappiness){
				int happinessIncrease = UnityEngine.Random.Range(1, 25);
				GetComponent<BandMember>().myHappiness = GetComponent<BandMember>().myHappiness + happinessIncrease;
			}
			if(decreaseHappiness){
				int happinessIncrease = UnityEngine.Random.Range(-15, -1);
				GetComponent<BandMember>().myHappiness = GetComponent<BandMember>().myHappiness + happinessIncrease;
			}
			if(leaveGenetics){
				GetComponent<BandMember>().LeaveGenetics();
			}
			if(increasePublicSuspicion){
				gameControl.GetComponent<GameControl>().publicSuspicion = gameControl.GetComponent<GameControl>().publicSuspicion + susPubInc;
			}
			if(increaseSuspicion){
				GetComponent<BandMember>().mySuspicion = GetComponent<BandMember>().mySuspicion + mySusInc;
			}
			if(increasePopFactor){
				gameControl.GetComponent<GameControl>().popularitetsfaktor = gameControl.GetComponent<GameControl>().popularitetsfaktor + popFacInc;
			}
			WrapUp();
		}
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
