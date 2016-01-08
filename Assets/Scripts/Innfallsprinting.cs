using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Innfallsprinting : MonoBehaviour {

	public GameObject crossBoble;
	public GameObject ronnyBoble;
	public GameObject keithBoble;
	public GameObject hannahBoble;

	public Text crossText;
	public Text ronnyText;
	public Text keithText;
	public Text hannahText;

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

		crossText = GameObject.FindWithTag("CrossText").GetComponent<Text>();
		ronnyText = GameObject.FindWithTag("RonnyText").GetComponent<Text>();
		keithText = GameObject.FindWithTag("KeithText").GetComponent<Text>();
		hannahText = GameObject.FindWithTag("HannahText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		crossText.text = crossSier;
		ronnyText.text = ronnySier;
		keithText.text = keithSier;
		hannahText.text = hannahSier;

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
