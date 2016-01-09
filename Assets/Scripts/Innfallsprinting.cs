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

		crossSier = null;
		ronnySier = null;
		keithSier = null;
		hannahSier = null;
	}
	
	// Update is called once per frame
	void Update () {
		crossText.text = crossSier;
		ronnyText.text = ronnySier;
		keithText.text = keithSier;
		hannahText.text = hannahSier;

		if (crossSier != null){
			crossBoble.SetActive(true);
		}
		if (crossSier == null){
			crossBoble.SetActive(false);
		}



		if (ronnySier != null){
			ronnyBoble.SetActive(true);		}
		if (ronnySier == null){
			ronnyBoble.SetActive(false);
		}



		if (keithSier != null){
			keithBoble.SetActive(true);
		}
		if (keithSier == null){
			keithBoble.SetActive(false);
		}



		if (hannahSier != null){
			hannahBoble.SetActive(true);
		}
    	if (hannahSier == null){
			hannahBoble.SetActive(false);
		}
	}
}
