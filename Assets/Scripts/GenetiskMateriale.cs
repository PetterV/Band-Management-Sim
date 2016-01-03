using UnityEngine;
using System.Collections;

public class GenetiskMateriale : MonoBehaviour {

	public bool beingCarried = false;
	public int skillForCloning = 0;

	// Update is called once per frame
	void Update () {
		if (beingCarried == true){
			transform.position = GameObject.Find("Player").transform.position;
		}
	}
}
