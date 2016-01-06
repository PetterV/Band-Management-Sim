using UnityEngine;
using System.Collections;

public class HappinessObject : MonoBehaviour {

	public int happinessGained = 15;
	public bool happinessBeingCarried = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (happinessBeingCarried == true){
			transform.position = GameObject.Find("Player").transform.position;
		}
	}
}
