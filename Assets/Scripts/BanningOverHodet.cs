using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BanningOverHodet : MonoBehaviour {

	public Image banning;
	bool tellBanning = false;
	float banneTimer = 0f;
	public float banneLength = 1f;
    bool setTimer = false;

    // Use this for initialization
    void Start () {
		banning.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		float banneTimerStep = 1f * Time.deltaTime;
        if (Input.GetKeyDown("q")){
            if (setTimer == false)
            {
                banneTimer = 0f;
                setTimer = true;
                banning.enabled = true;
            }
		}
	    banneTimer = banneTimer + banneTimerStep;
		if (banneTimer > banneLength){
			tellBanning = false;
            setTimer = false;
			banning.enabled = false;
		}
	}
}
