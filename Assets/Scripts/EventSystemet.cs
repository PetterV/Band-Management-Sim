using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EventSystemet : MonoBehaviour {

	public enum Event {Rasisme, DickPicLeak, FanGift, StalkStart, Nothing};
	delegate void MyDelegate(int choice);

	public GameObject rasismeBilde;
	public GameObject dickPicBilde;
	public GameObject[] personBilder;

	private Dictionary<Event, int> eventOversikt;
	private int probabilitySum = 0;
	public int minimumSecondDelayBetweenEvents = 120;
	public float timeSinceLastEvent = 0;
	MyDelegate solveFunction;
	MyDelegate hoverFunction;
	public GameObject EventCanvas;
	public bool playerChoosing = false;
	private bool eventRunning = false;

	private GameObject[] buttons;
	private Text text;
	private Text headline;
	private Image flavourImage;
	private Image personImage;
	private GameObject hoverBox;

	// Use this for initialization
	void Start () {
		InitializeEvents ();
		this.EventCanvas = GameObject.FindGameObjectWithTag ("EventCanvas");
		headline = GameObject.FindGameObjectWithTag ("EventOverskrift").GetComponent<Text>();
		//flavourImage = GameObject.FindGameObjectsWithTag ("EventBilde")[0].GetComponent<Image>();
		//personImage = GameObject.FindGameObjectsWithTag ("EventBilde")[1].GetComponent<Image>();
		text = GameObject.FindGameObjectWithTag ("EventText").GetComponent<Text>();
		InitializeButtons(GameObject.FindGameObjectsWithTag ("EventKnapp"));
		hoverBox = GameObject.FindGameObjectWithTag ("EventHover");
		this.EventCanvas.SetActive (false);

	}

	void InitializeButtons(GameObject[] buttonList){
		this.buttons = new GameObject[4];
		for (int i = 0; i < buttons.Length; i++) {
			if (buttonList [i].name == "Knapp1")
				buttons [0] = buttonList [i];
			else if (buttonList [i].name == "Knapp2")
				buttons [1] = buttonList [i];
			else if (buttonList [i].name == "Knapp3")
				buttons [2] = buttonList [i];
			else if (buttonList [i].name == "Knapp4")
				buttons [3] = buttonList [i];
		}
	}

	// Update is called once per frame
	void Update () {
		if(!eventRunning)
			FireEventsOrNothing ();
		if (hoverBox.activeSelf)
			UpdateHoverBoxPosition ();
	}

	private void InitializeEvents()
	{
		this.eventOversikt = new Dictionary<Event, int>()
		{
			{Event.Rasisme, 1},
			{Event.DickPicLeak, 1},
			{Event.Nothing, 1},
			{Event.FanGift, 1},
            {Event.StalkStart, 1}
		};
		foreach (KeyValuePair<Event, int> entry in eventOversikt)
		{
			probabilitySum += entry.Value;
		}
	}

	private void FireEventsOrNothing(){
		if (playerChoosing)
			return;
		if (timeSinceLastEvent < minimumSecondDelayBetweenEvents) {
			timeSinceLastEvent += Time.deltaTime;
			return;
		}
		int randomNumber = UnityEngine.Random.Range(0, probabilitySum+1);
		int tempUpperBound = 0;
		foreach(KeyValuePair<Event, int> entry in this.eventOversikt)
		{
			tempUpperBound += entry.Value;
			if (randomNumber < tempUpperBound)
			{
				print ("our number: " + randomNumber + "the bounds: " + tempUpperBound +", "+ entry.Key);
				FireEvent(entry.Key);
				break;
			}

		}
	}

	private void FireEvent(Event ev){
		this.eventRunning = true;
		switch (ev)
		{
		case Event.Rasisme:
			{
				Rasisme ();
				break;
			}
		case Event.Nothing:
			{
				this.eventRunning = false;
				break;
			}
		case Event.DickPicLeak:
			{
				DickPicLeak ();
				break;
			}
		case Event.FanGift:
			{
				FanGift ();
				break;
			}
            case Event.StalkStart:
                {
                    StalkStart();
                    break;
                }
        }
	}

		//FanGift Event
	private void FanGift()
	{
		string overskrift = "Fans vil gi bandet en gave!";
		string text = "Fans elsker deg!\nDe trenger hjelp for å finne den beste gaven til sitt favorittbandmedlem.";
		//Image rasismeBilde = this.rasismeBilde.GetComponent<Image> ();
		//Image personenSomVarRasistisk = GetRandomPerson ();
		//Karakterer som for eksempel " vil avslutte strengen. Dersom du vil ha det med, skriv \".
		//Linjeskift er \n
		SetUpCanvas (overskrift, text, "Han trenger teddybjørner!", "Gi meg pengene, så fikser jeg det :)", "Send ham kjærlighetstweets!");
		this.solveFunction = SolveFanGift;
		this.hoverFunction = hoverFanGift;
	}

	private void SolveFanGift(int alt){
		switch (alt) {
		case 0:
			{
				print ("FanGift0");
				break;
			}
		case 1:
			{
				print ("FanGift1");
				break;
			}
		case 2:
			{
				print ("FanGift2");
				break;
			}
		}
	}

	private void hoverFanGift(int alt){
		string hoverText = "";
		switch (alt) {
		case 0:
			{
				hoverText = "Happiness: +10";
				break;
			}
		case 1:
			{
				hoverText = "Money: +10 000";
				break;
			}
		case 2:
			{
				hoverText = "Popularity: +5\nHappiness: +5";
				break;
			}
		}
		SetHoverText(hoverText);
	}
	
        //Stalker Event
    private void StalkStart()
    {
        string overskrift = "En Stalker forfølger bandet!";
        string text = "En person har startet å overvåke bandmedlemmene dine \n hen tar bilder og har forsøkt å ta seg inn på eiendommen.";
        //Image rasismeBilde = this.rasismeBilde.GetComponent<Image> ();
        //Image personenSomVarRasistisk = GetRandomPerson (); 
        SetUpCanvas(overskrift, text, "Anmelde til politiet", "Betal Stalkeren for å stoppe", "La Stalkeren treffe bandet", "Ignorer problemet");
        this.solveFunction = SolveStalker;
        this.hoverFunction = hoverStalker;
    }	

    private void SolveStalker(int alt)
    {
        switch (alt)
        {
            case 0:
                {
                    print("Stalker0");
                    break;
                }
            case 1:
                {
                    print("Stalker1");
                    break;
                }
            case 2:
                {
                    print("Stalker2");
                    break;
                }
            case 3:
                {
                    print("Stalker3");
                    break;
                }
        }
    }

    private void hoverStalker(int alt)
    {
        string hoverText = "";
        switch (alt)
        {
            case 0:
                {
                    hoverText = "Populæritet -15";
                    break;
                }
            case 1:
                {
                    hoverText = "Penger: -10 000";
                    break;
                }
            case 2:
                {
                    hoverText = "Populæritet +25: \n 50% sjangse for at Keith dør";
                    break;
                }
            case 3:
                {
                    hoverText = "Stalkeren kan bli mer problematisk i fremtiden";
                    //Bare drit i å gjøre noe her, vi kan heller snakke om det hvis det blir en greie
                    break;
                }
        }
        SetHoverText(hoverText);
    }


    //Rasisme event
    private void Rasisme(){
		string overskrift = "Dømt etter rasismeparagrafen";
		string text = "Bandets offisielle Twitterkonto har blitt dømt \n etter Rasismeparagrafen. Uten noen å stille direkte til ansvar \n må du svare pressen alene.";
		//Image rasismeBilde = this.rasismeBilde.GetComponent<Image> ();
		//Image personenSomVarRasistisk = GetRandomPerson (); 
		SetUpCanvas (overskrift, text, "Legger oss flate", "Dette skulle aldri ha skjedd.", "Per Sandberg har skylden");
		this.solveFunction = SolveRasisme;	
		this.hoverFunction = hoverRasisme;
	}

	private void SolveRasisme(int alt){
		switch (alt) {
		case 0:
			{
				print ("rasisme0");
				break;
			}
        case 1:
            {
                print("rasisme1");
                break;
            }
        case 2:
                {
                    print("rasisme2");
                    break;
                }
		}
	}
	private void hoverRasisme(int alt){
		string hoverText = "";
		switch (alt) {
		case 0:
			{
				hoverText = "Populæritet -25";
				break;
			}
            case 1:
                {
                    hoverText = "Populæritet -10 \n Penger -20 000";
                    break;
                }

            case 2:
                {
                    hoverText = "Populæritet -20";
                    break;
                }
        }
		SetHoverText(hoverText);
	}

	private void DickPicLeak(){
		string flavour = "En fan hevder å ha et lekket bilde av et bandmedlems penis. Hvis dette viser seg å være riktig, vil du miste populæritet blant kristne fans.";
		//Image dickPicBilde = this.dickPicBilde.GetComponent<Image> ();
		//Image personenSomLeakaDick = GetRandomPerson ();
		SetUpCanvas ("Fan truer med å lekke penisbilder.", flavour, "Betal vedkommende til stillhet", "La bildet lekke", "Peniser har da genetisk materiale?");
		this.solveFunction = SolveDickPicLeak;
		this.hoverFunction = hoverDickPicLeak;
	}

	private void SolveDickPicLeak(int alt){
		switch (alt) {
		case 0:
			{
				print ("dickpicleak 0");
				break;
			}
		case 1:
			{
				print ("dickpicleak 1");
				break;
			}
		case 2:
			{
				print ("dickpicleak 2");
				break;
			}
		
		}
	}

	private void hoverDickPicLeak(int alt){
		string hovertext = "";
		switch (alt) {
		case 0:
			{
				hovertext = "-40 000 Penger";
				break;
			}
		case 1:
			{
				hovertext = "-10 Mistenkelighet\n+10 Populæritet";
				break;
			}
		case 2:
			{
				hovertext = "Genetisk Materiale dukker opp i Klonekjelleren";
				break;
			}
		}
		SetHoverText (hovertext);
	}

	void EventFinished(){
		this.timeSinceLastEvent = 0f;
		this.playerChoosing = false;
		this.eventRunning = false;
		this.EventCanvas.SetActive (false);
	}

	void SetHoverText(string hoverText){
		Text hoverBoxText = hoverBox.GetComponentInChildren<Text> ();
		hoverBoxText.text =  hoverText;
	}

	private void SetUpCanvas(string overskrift,/* Image flavourImage, Image personImage, */string tekst, params string[] test){
		this.EventCanvas.SetActive (true);

		for (int i = 0; i < test.Length; i++) {
			buttons [i].SetActive (true);
			Text buttonText = buttons [i].GetComponent<Button> ().GetComponentInChildren<Text> ();
			buttonText.text = test [i];
		}
		for (int i = test.Length; i < buttons.Length; i++) {
			buttons [i].SetActive (false);
		}

		headline.text = overskrift;
		text.text = tekst;
		this.flavourImage = flavourImage;
		this.personImage = personImage;
		hoverBox.SetActive (false);

		this.playerChoosing = true;
	}

	Image GetRandomPerson(){
		int randomIndex = Random.Range (0, 5);
		return this.personBilder[randomIndex].GetComponent<Image>();
	}

	public void ChooseAlternative(int alternative){
		solveFunction (alternative);
		EventFinished ();
	}

	public void HoverAlternative(int alternative){
		hoverBox.SetActive (true);
		hoverFunction (alternative);
	}

	public void hoverExit(){
		hoverBox.SetActive (false);
	}

	public void UpdateHoverBoxPosition(){
		Vector3 rectPosition = Input.mousePosition;
		RectTransform hoverRect = hoverBox.GetComponent<RectTransform> ();
		rectPosition.y += (hoverRect.rect.height/3.0f)+1.0f;
		hoverBox.transform.position = rectPosition;
	}

}
