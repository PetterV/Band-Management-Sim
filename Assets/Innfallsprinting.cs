using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Innfallsprinting : MonoBehaviour {

	public GameObject crossBoble;
	public GameObject ronnyBoble;
	public GameObject keithBoble;
	public GameObject hannahBoble;

	private Text crossText;
	private Text ronnyText;
	private Text keithText;
	private Text hannahText;

	public string crossSier;
	public string ronnySier;
	public string keithSier;
	public string hannahSier;

	// Use this for initialization
	void Start () {
		crossBoble = GameObject.Find("Boble_Cross");
		ronnyBoble = GameObject.Find("Boble_Ronny");
		keithBoble = GameObject.Find("Boble_Keith");
		hannahBoble = GameObject.Find("Boble_Hannah");

		crossText = crossBoble.GetComponentInChildren<Text>();
		ronnyText = ronnyBoble.GetComponentInChildren<Text>();
		keithText = keithBoble.GetComponentInChildren<Text>();
		hannahText = hannahBoble.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (crossText.text != null){
			crossBoble.SetActive(true);
		}
		if (crossText.text == null){
			crossBoble.SetActive(false);
		}



		if (ronnyText.text != null){
			ronnyBoble.SetActive(true);
		}
		if (ronnyText.text == null){
			ronnyBoble.SetActive(false);
		}



		if (keithText.text != null){
			keithBoble.SetActive(true);
		}
		if (keithText.text == null){
			keithBoble.SetActive(false);
		}



		if (hannahText.text != null){
			hannahBoble.SetActive(true);
		}
		if (hannahText.text == null){
			hannahBoble.SetActive(false);
		}
	}
}
