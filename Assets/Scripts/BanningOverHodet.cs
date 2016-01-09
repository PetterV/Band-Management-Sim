using UnityEngine;
using System.Collections;

public class BanningOverHodet : MonoBehaviour {

	public GameObject banning;
	bool tellBanning = false;
	float banneTimer = 0f;
	public float banneLength = 1f;

	// Use this for initialization
	void Start () {
		banning = GameObject.FindWithTag("Banning");
		banning.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		float banneTimerStep = 1f * Time.deltaTime;
		if (Input.GetKeyDown("q") && banneTimer < banneLength){
			banneTimer = 0f;
			banning.SetActive(true);
			tellBanning = true;
		}
		if (tellBanning == true){
			banneTimer = banneTimer + banneTimerStep;
		}
		if (banneTimer > banneLength){
			tellBanning = false;
			banning.SetActive(false);
		}
	}
}
