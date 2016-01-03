using UnityEngine;
using System.Collections.Generic;

public class Innfallsystemet : MonoBehaviour {

	public enum Innfall {Score, Strandtur, Nothing};
	public Dictionary<Innfall, int> innfallsOversikt;
	public int innfallsTall;

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
			CheckInnfall();
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
				Score();
				break;
			}
		case Innfall.Strandtur:
			{
				Strandtur();
				break;
			}
		case Innfall.Nothing:
			{
				break;
			}
		}
	}

	//Innfallshandlinger
	void Score (){
		print ("I kveld scorer jeg!");
	}

	void Strandtur (){
		print ("Jeg liker lange turer på stranden.");
	}

	//Bli drept av spilleren
	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "Player" && Input.GetKeyDown("space")){
			GetComponentInParent<BandMember>().Dying();
		}
	}
}
