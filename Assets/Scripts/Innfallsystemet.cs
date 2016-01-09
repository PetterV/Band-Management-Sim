using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Innfallsystemet : MonoBehaviour {

	public enum Innfall {Score, Strandtur, Solo, Lytte, SintTweet, GladTweet, Drikke, Spise, Dusje, Danse, Ove, Ove2, Ove3, GoLeft, GoRight, Nothing};
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
	public Image progressFrame;
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
    private bool ove2Innfall = false;
    private bool ove3Innfall = false;
	//Innfallstriggers over.									^ Innfall her ^


	//Her er GameObjects til å vise innfallssnakkebobler
	/*public GameObject strandBoble;
	public GameObject scoreBoble;
	public GameObject soloBoble;
	public GameObject lytteBoble;
	public GameObject sintTweetBoble;
	public GameObject gladTweetBoble;
	public GameObject drikkeBoble;
	public GameObject spiseBoble;
	public GameObject dusjeBoble;
	public GameObject danseBoble;
	public GameObject oveBoble;
	GameObject bobleToDisplay;*/
	///  //  // //
	/// 
	/// 
	Camera mainCam;


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
		//bobleToDisplay = null;

	}

	private void InitializeInnfall()
	{
		//Dette er Innfalls-oversikten. Hvis du vil legge til flere, gjør du det på samme måte som her.
		this.innfallsOversikt = new Dictionary<Innfall, int>()
		{
			{Innfall.Score, 5 },
			{Innfall.Strandtur, 5 }, //sannsynligheten for Strandtur er dobbelt så stor som Score.
			{Innfall.Solo, 5 },
			{Innfall.Lytte, 5 },
			{Innfall.SintTweet, 3 },
			{Innfall.GladTweet, 3 },
			{Innfall.Drikke, 7 },
			{Innfall.Spise, 7 },
			{Innfall.Dusje, 15 },
			{Innfall.Danse, 10 },
			{Innfall.Ove, 3 },
			{Innfall.GoLeft, 0 },
			{Innfall.GoRight, 0 },
            {Innfall.Ove2, 3 },
            {Innfall.Ove3, 3 },
            {Innfall.Nothing,80000 } //Sannsynligheten for Nothing er sju ganger større enn Score
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
				gameControl.GetComponent<Innfallsprinting>().hannahSier = null;
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
            if (ove2Innfall == true)
            {
                Ove2();
            }

            if (ove3Innfall == true)
            {
                Ove3();
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

        switch (inn)
		{
		case Innfall.Score:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				scoreInnfall = true;
				break;
			}
		case Innfall.Strandtur:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				strandInnfall = true;
				break;
			}
		case Innfall.Solo:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				soloInnfall = true;
				break;
			}
		case Innfall.Lytte:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				lytteInnfall = true;
				break;
			}
		case Innfall.SintTweet:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				sintTweetInnfall = true;
				break;
			}
		case Innfall.GladTweet:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				gladTweetInnfall = true;
				break;
			}
		case Innfall.Drikke:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				drikkeInnfall = true;
				break;
			}
		case Innfall.Dusje:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				dusjeInnfall = true;
				break;
			}
		case Innfall.Spise:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				spiseInnfall = true;
				break;
			}
		case Innfall.Danse:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				danseInnfall = true;
				break;
			}
		case Innfall.Ove:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				oveInnfall = true;
				break;
			}
        case Innfall.Ove2:
			{
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
				ove2Innfall = true;
				break;
			}
            case Innfall.Ove3:
                {
                    gameControl.GetComponent<Infallslyd>().PlaySound("innfall");
                    harInnfall = true;
                    ove3Innfall = true;
                    break;
                }
            case Innfall.GoRight:
			{
				innfallGoRight = true;
				animator.SetInteger("Walking", 1);
				break;
			}
		case Innfall.GoLeft:
			{
				innfallGoLeft = true;
				animator.SetInteger("Walking", 1);
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
		ActuallyDoInfall("Nå skal jeg se på TV.", "ScoreSted", "Dette er chill.", "TV er nesten like bra som å være rockestjerne.", false, true, true, false, false, false, 0, false, 0, true, -5f, 2, "negativt");
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().scoreTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
		//Strandturinnfall
	void Strandtur (){
		ActuallyDoInfall("Jeg vil chille litt.", "StrandSted", "Jeg liker å henge på rommet.", "Nå er jeg uthvilt.", false, false, true, false, false, false, 0, true, -5f, false, 0, 0, "positivt");
		if (setActionCounter == false){
		//	bobleToDisplay = strandBoble;
			actionCounter = this.gameControl.GetComponent<GameControl>().strandTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
		//solokarriæreinnfall
	void Solokarriere (){
		ActuallyDoInfall("Jeg vil starte solokarriere!", "SoloSted", "Jeg forlater bandet!", "I'm outta here!", false, false, false, false, true, false, 0, false, 0, true, -50f, 2, "negativt");
		if (setActionCounter == false){
		//	bobleToDisplay = soloBoble;
			actionCounter = this.gameControl.GetComponent<GameControl>().soloTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
    //Musikklytteinnfal
	void MusikkLytting (){

		ActuallyDoInfall("Jeg vil høre på musikk.", "LytteSted", "Mmm, dette er good shit.", "Jeg hørte på litt muzak.", true, false, true, false, false, false, 0, false, 0, true, 5, 2, "positivt");
		if (setActionCounter == false){
		//	bobleToDisplay = lytteBoble;
			actionCounter = this.gameControl.GetComponent<GameControl>().lytteTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //SintTweetInnfal
	void SintTweet (){
		ActuallyDoInfall("Jeg er pissed, og vil at hele verden skal vite det!", "TweeteSted", "Denne tweeten kommer til å sjokkere!", "Det var godt å få fram!", false, false, true, false, false, true, 10f, true, 10, true, -15, 2, "negativt");
		if (setActionCounter == false){
		//	bobleToDisplay = sintTweetBoble;
			actionCounter = this.gameControl.GetComponent<GameControl>().tweeteTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //Glad Tweet Innfall
	void GladTweet (){
		ActuallyDoInfall("Jeg vil fortelle fansen hvor mye jeg setter pris på dem!", "TweeteSted", "Publikum kommer til å digge denne tweeten.", "Jeg elsker å bli satt pris på.", false, false, true, false, false, true, 5f, false, 0, true, 10f, 2, "positivt");
		if (setActionCounter == false){
		//	bobleToDisplay = gladTweetBoble;
			actionCounter = this.gameControl.GetComponent<GameControl>().tweeteTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
       //Drikkeinnfall
	void Drikke (){
		ActuallyDoInfall("Nå skarre drekkes", "DrikkeSted", "Jeg drekker som bare fy!", "Yay drekking! Livet betyr mer nå!", false, true, true, false, true, false, 0, false, 0, false, 0, 2, "negativt");
		if (setActionCounter == false){
		//	bobleToDisplay = drikkeBoble;
			actionCounter = this.gameControl.GetComponent<GameControl>().drikkeTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //Dusjeinnfall
	void Dusje (){
		ActuallyDoInfall("Oh boy, jeg trenger en dusj.", "DusjeSted", "Deilig å dusje, as.", "Er dette sånn jeg egentlig lukter?", false, false, false, false, true, false, 0, false, 0, false, 0, 2, "positivt");
		if (setActionCounter == false){
			actionCounter = this.gameControl.GetComponent<GameControl>().dusjeTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //Spiseinnfall
	void Spise (){
		ActuallyDoInfall("Nå er jeg sulten!", "SpiseSted", "Nom nom nom.", "Nå er jeg mett!", false, false, false, false, true, false, 0, false, 0, false, 0, 2, "positivt");
		if (setActionCounter == false){
		//	bobleToDisplay = spiseBoble;
			actionCounter = this.gameControl.GetComponent<GameControl>().spiseTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
        //SexyDanceinnfall
	void SexyDance (){
		ActuallyDoInfall("Nå trenger jeg litt alenetid!", "DanseSted", "Ooooh yeah.", "Det var godt å få fram.", false, false, true, false, false, false, 0, false, 0, true, -5f, 0, "positivt");
		if (setActionCounter == false){
		//	bobleToDisplay = danseBoble;
			actionCounter = this.gameControl.GetComponent<GameControl>().danceTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}

		//Practiceinnfall
	void Ove (){
		ActuallyDoInfall("Nå skal jeg øve.", "OveSted", "La oss se... A, så G, så...", "Nå er jeg bedre!", true, false, false, true, false, false, 0, false, 0, true, 5f, 0, "positivt");
		if (setActionCounter == false){
		//	bobleToDisplay = oveBoble;
			actionCounter = this.gameControl.GetComponent<GameControl>().oveTid;
			maxActionCounter = actionCounter;
			setActionCounter = true;
		}
	}
    void Ove2()
    {
        ActuallyDoInfall("Nå skal jeg øve.", "OveSted 2", "La oss se... E, så C, så...", "Nå er jeg bedre!", true, false, false, true, false, false, 0, false, 0, true, 5f, 0, "positivt");
        if (setActionCounter == false)
        {
        //    bobleToDisplay = oveBoble;
            actionCounter = this.gameControl.GetComponent<GameControl>().oveTid;
            maxActionCounter = actionCounter;
            setActionCounter = true;
        }
    }
    void Ove3()
    {
        ActuallyDoInfall("Nå skal jeg øve.", "OveSted 3", "La oss se... Æ, så Ø, så... Å", "Nå er jeg bedre!", true, false, false, true, false, false, 0, false, 0, true, 5f, 0, "positivt");
        if (setActionCounter == false)
        {
        //    bobleToDisplay = oveBoble;
            actionCounter = this.gameControl.GetComponent<GameControl>().oveTid;
            maxActionCounter = actionCounter;
            setActionCounter = true;
        }
    }
    void ActuallyDoInfall(string statement, string placeToWalkTo, string doing, string handlingGjennomfort, bool increaseSkill, bool decreaseSkill, bool increaseHappiness, bool decreaseHappiness, bool leaveGenetics, bool increasePublicSuspicion, float susPubInc, bool increaseSuspicion, float mySusInc, bool increasePopFactor, float popFacInc, int turn, string utfall){
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
			progressBar.enabled = true;
			progressFrame.enabled = true;
			progressBar.fillAmount = 1 - progToBeFilled;
			//Send beskjed til tekstbobler
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
			if (turn == 0){
				GetComponent<BandMemberMoving>().HeadTowardsCamera();
			}
			if (turn == 1){
				GetComponent<BandMemberMoving>().HeadLeft();
			}
			if (turn == 2){
				GetComponent<BandMemberMoving>().HeadAwayFromCamera();
			}
			if (turn == 3){
				GetComponent<BandMemberMoving>().HeadRight();
			}

		}
		if (innfallComplete == true){

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
			gameControl.GetComponent<Infallslyd>().PlaySound(utfall);
			WrapUp();
		}
	}

	void GoRight (){
		harInnfall = true;
		if (goingThere == false){
            float walkDistance = 15;//UnityEngine.Random.Range(2, 15);
			moveTarget = transform.position.x + walkDistance;
			goingThere = true;
		}
		if (transform.position.x < moveTarget){
            moveThisStep = transform.position.x + Time.deltaTime * 3.0f;
			print ("Going right!");
			this.gameObject.GetComponent<BandMemberMoving>().HeadRight();
			transform.position = new Vector3 (moveThisStep, this.transform.position.y, this.transform.position.z);
		}
		if (transform.position.x >= moveTarget){
			print ("Got there!");
			WrapUp();
		}
	}

	void GoLeft (){
		harInnfall = true;
		if (goingThere == false){
			float walkDistance = UnityEngine.Random.Range(2, 15);
			moveTarget = transform.position.x - walkDistance;
			goingThere = true;
		}
		if (transform.position.x > moveTarget){
			moveThisStep = transform.position.x - 1f;
			print ("Going left!");
			this.gameObject.GetComponent<BandMemberMoving>().HeadLeft();
			transform.position = new Vector3 (moveThisStep, this.transform.position.y, this.transform.position.z);
		}
		if (transform.position.x <= moveTarget){
			print ("Got there");
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
        progressBar.enabled = false;
		progressFrame.enabled = false;
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
        ove2Innfall = false;
        ove3Innfall = false;
		animator.SetInteger("Walking", 0);
		GetComponent<BandMember>().fikkKjeft = false;
	}
}
