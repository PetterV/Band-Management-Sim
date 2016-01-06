using UnityEngine;
using System.Collections;

public class Boxes : MonoBehaviour {

	//GUI-boksgreier
	public bool displayTrommisBoks = false;
	public bool displayVokalistBoks = false;
	public bool displayGitaristBoks = false;
	public bool displayBassistBoks = false;
	private string trommisTextToDisplay;
	private string vokalistTextToDisplay;
	private string gitaristTextToDisplay;
	private string bassistTextToDisplay;
	Camera camera;
	Vector3 screenPos;
	//Done
	public GameObject trommis;
	public GameObject vokalist;
	public GameObject gitarist;
	public GameObject bassist;


	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}

	void Update (){
		trommisTextToDisplay = trommis.GetComponent<Innfallsystemet>().textToDisplay;
		bassistTextToDisplay = bassist.GetComponent<Innfallsystemet>().textToDisplay;
		vokalistTextToDisplay = vokalist.GetComponent<Innfallsystemet>().textToDisplay;
		gitaristTextToDisplay = gitarist.GetComponent<Innfallsystemet>().textToDisplay;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (displayTrommisBoks == true){
			screenPos = camera.WorldToScreenPoint(trommis.transform.position);
			GUI.Label(new Rect(screenPos.x - 50, screenPos.y + 190, Screen.width / 4, 40), trommisTextToDisplay);
			print ("Boks boks boks boks");
		}
		if (displayVokalistBoks == true){
			screenPos = camera.WorldToScreenPoint(vokalist.transform.position);
			GUI.Label(new Rect(screenPos.x - 50, screenPos.y + 30, Screen.width / 4, 40), vokalistTextToDisplay);
			print ("Boks boks boks boks");
		}
		if (displayBassistBoks == true){
			screenPos = camera.WorldToScreenPoint(bassist.transform.position);
			GUI.Label(new Rect(screenPos.x - 50, screenPos.y + 190, Screen.width / 4, 40), bassistTextToDisplay);
			print ("Boks boks boks boks");
		}
		if (displayGitaristBoks == true){
			screenPos = camera.WorldToScreenPoint(gitarist.transform.position);
			GUI.Label(new Rect(screenPos.x - 50, screenPos.y + 190, Screen.width / 4, 40), gitaristTextToDisplay);
			print ("Boks boks boks boks");
		}
	}
}
