using UnityEngine;
using System.Collections.Generic;

public class EventSystem : MonoBehaviour {

	public enum Event {Rasisme, DickPicLeak, Nothing};

	private Dictionary<Event, int> eventOversikt;
	private int probabilitySum = 0;
	public int minimumSecondDelayBetweenEvents = 120;
	public float timeSinceLastEvent = 0f;

	// Use this for initialization
	void Start () {
		InitializeEvents ();
	}
	
	// Update is called once per frame
	void Update () {
		FireEventsOrNothing ();
	}

	private void InitializeEvents()
	{
		//Dette er Innfalls-oversikten. Hvis du vil legge til flere, gjør du det på samme måte som her.
		this.eventOversikt = new Dictionary<Event, int>()
		{
			{Event.Rasisme, 1 },
			{Event.DickPicLeak, 1 },
			{Event.Nothing, 1500}
		};
		foreach (KeyValuePair<Event, int> entry in eventOversikt)
		{
			probabilitySum += entry.Value;
		}
	}

	private void FireEventsOrNothing(){
		if (timeSinceLastEvent < minimumSecondDelayBetweenEvents) {
			timeSinceLastEvent += Time.deltaTime;
			return;
		}
		int randomNumber = UnityEngine.Random.Range(0, probabilitySum);
		int tempUpperBound = 0;
		foreach(KeyValuePair<Event, int> entry in this.eventOversikt)
		{
			tempUpperBound += entry.Value;
			if (randomNumber < tempUpperBound)
			{
				FireEvent(entry.Key);
				break;
			}

		}
	}

	private void FireEvent(Event ev){
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

	void Rasisme(){
		print ("Hey, that's racist!");
		EventFinished ();
	}

	void DickPicLeak(){
		print ("A fan claims that they have a dick pic from one of your band members. If this is true, it will alienate your fans from the Christian Right. What do you want to do?");
		EventFinished ();
	}

	void EventFinished(){
		this.timeSinceLastEvent = 0f;
	}
}
