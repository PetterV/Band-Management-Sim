using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EventSystemet : MonoBehaviour {

	public enum Event {Rasisme, DickPicLeak, Nothing};
	delegate void MyDelegate(int choice);

	public Image rasismeBilde;
	public Image dickPicBilde;
	public Image[] personBilder;

	private Dictionary<Event, int> eventOversikt;
	private int probabilitySum = 0;
	public int minimumSecondDelayBetweenEvents = 120;
	public float timeSinceLastEvent = 0;
	MyDelegate eventFunction;
	MyDelegate hoverFunction;
	public GameObject EventCanvas;
	private bool playerChoosing = false;
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
		flavourImage = GameObject.FindGameObjectsWithTag ("EventBilde")[0].GetComponent<Image>();
		personImage = GameObject.FindGameObjectsWithTag ("EventBilde")[1].GetComponent<Image>();
		text = GameObject.FindGameObjectWithTag ("EventText").GetComponent<Text>();
		buttons = GameObject.FindGameObjectsWithTag ("EventKnapp");
		hoverBox = GameObject.FindGameObjectWithTag ("EventHover");
		this.EventCanvas.SetActive (false);

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
			{Event.Rasisme, 1 },
			{Event.DickPicLeak, 1 },
			{Event.Nothing, 0}
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
				break;
			}
		case Event.DickPicLeak:
			{
				DickPicLeak ();
				break;
			}
		}
	}

	private void Rasisme(){
		string overskrift = "Hey, that's racist!";
		string text = "Something racist has happened!";
		Image personenSomVarRasistisk = GetRandomPerson (); 
		SetUpCanvas (overskrift, rasismeBilde, personenSomVarRasistisk, text, "I guess I am racist :(");
		this.eventFunction = SolveRasisme;	
		this.hoverFunction = hoverRasisme;
	}

	private void SolveRasisme(int alt){
		switch (alt) {
		case 0:
			{
				print ("rasisme0");
				break;
			}
		}
	}
	private void hoverRasisme(int alt){
		string hoverText = "";
		switch (alt) {
		case 0:
			{
				hoverText = "hoverRasisme0";
				break;
			}
		}
		SetHoverText(hoverText);
	}

	private void DickPicLeak(){
		string flavour = "A fan claims that they have a dick pic from one of your band members. If this is true, it will alienate your fans from the Christian Right. What do you want to do?";
		Image personenSomLeakaDick = GetRandomPerson ();
		SetUpCanvas ("DICK PIC LEAKED!", dickPicBilde, personenSomLeakaDick, flavour, "One dick", "Two dicks", "Three dicks", "ALL THE DICKS");
		this.eventFunction = SolveDickPicLeak;
		this.hoverFunction = hoverDickPicLeak;
	}

	private void SolveDickPicLeak(int alt){
		switch (alt) {
		case 0:
			{
				print ("dickpicleak0");
				break;
			}
		case 1:
			{
				print ("dickpicleak1");
				break;
			}
		case 2:
			{
				print ("dickpicleak2");
				break;
			}
		case 3:
			{
				print ("dickpicleak3");
				break;
			}
		}
	}

	private void hoverDickPicLeak(int alt){
		string hovertext = "";
		switch (alt) {
		case 0:
			{
				hovertext = "dickpicHover0";
				break;
			}
		case 1:
			{
				hovertext = "dickpicHover1";
				break;
			}
		case 2:
			{
				hovertext = "dickpiHover2";
				break;
			}
		case 3:
			{
				hovertext = "dickpicHover3";
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

	private void SetUpCanvas(string overskrift, Image flavourImage, Image personImage, string tekst, params string[] test){
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
		return this.personBilder [randomIndex];
	}

	public void ChooseAlternative(int alternative){
		eventFunction (alternative);
		EventFinished ();
	}

	public void HoverAlternative(int alternative){
		hoverBox.SetActive (true);
		hoverFunction (alternative);
	}

	public void hoverExit(int alternative){
		hoverBox.SetActive (false);
	}

	public void UpdateHoverBoxPosition(){
		Vector3 rectPosition = Input.mousePosition;
		RectTransform hoverRect = hoverBox.GetComponent<RectTransform> ();
		rectPosition.y += (hoverRect.rect.height/4.0f)+1.0f;
		hoverBox.transform.position = rectPosition;
	}

}
