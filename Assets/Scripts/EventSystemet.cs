using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EventSystemet : MonoBehaviour {

	public enum Event {Rasisme, DickPicLeak, Nothing};
	delegate void MyDelegate(int choice);

	private Dictionary<Event, int> eventOversikt;
	private int probabilitySum = 0;
	public int minimumSecondDelayBetweenEvents = 120;
	public float timeSinceLastEvent = 0;
	MyDelegate eventFunction;
	public GameObject EventCanvas;
	private bool playerChoosing = false;
	private bool eventRunning = false;

	private GameObject[] buttons;
	private Text text;
	private Text headline;
	private RawImage img;

	// Use this for initialization
	void Start () {
		InitializeEvents ();
		eventFunction = PrintNum;
		eventFunction(50);

		eventFunction = DoubleNum;
		eventFunction(50);
		this.EventCanvas = GameObject.FindGameObjectWithTag ("EventCanvas");
		headline = GameObject.FindGameObjectWithTag ("EventOverskrift").GetComponent<Text>();
		img = GameObject.FindGameObjectWithTag ("EventBilde").GetComponent<RawImage>();
		text = GameObject.FindGameObjectWithTag ("EventText").GetComponent<Text>();
		buttons = GameObject.FindGameObjectsWithTag ("EventKnapp");
		this.EventCanvas.SetActive (false);

	}

	// Update is called once per frame
	void Update () {
		if(!eventRunning)
			FireEventsOrNothing ();
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
		string img = "yellow";
		SetUpCanvas (overskrift, img, text, "I guess I am racist :(");
		this.eventFunction = SolveRasisme;	
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
				print ("rasisme1");
				break;
			}
		case 2:
			{
				print ("rasisme2");
				break;
			}
		case 3:
			{
				print ("rasisme3");
				break;
			}
		}
	}

	private void DickPicLeak(){
		string flavour = "A fan claims that they have a dick pic from one of your band members. If this is true, it will alienate your fans from the Christian Right. What do you want to do?";
		SetUpCanvas ("DICK PIC LEAKED!", "pink", flavour, "One dick", "Two dicks", "Three dicks", "ALL THE DICKS");
		this.eventFunction = SolveDickPicLeak;
	}
		

	private void SetUpCanvas(string overskrift, string image, string tekst, params string[] test){
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
		img.color = image == "yellow" ? Color.yellow : Color.magenta;

		this.playerChoosing = true;

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

	void EventFinished(){
		this.timeSinceLastEvent = 0f;
		this.playerChoosing = false;
		this.eventRunning = false;
		this.EventCanvas.SetActive (false);
	}

	void PrintNum(int num)
	{
		print ("Print Num: " + num);
	}

	void DoubleNum(int num)
	{
		print ("Double Num: " + num * 2);
	}

	public void ChooseAlternative(int alternative){
		eventFunction (alternative);
		EventFinished ();
	}

}
