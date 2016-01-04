using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") > 0){
			print ("Scroll Up");
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0){
			print ("Scroll Down");
		}
	}
}
