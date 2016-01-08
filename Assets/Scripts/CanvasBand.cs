using UnityEngine;
using System.Collections;

public class CanvasBand : MonoBehaviour {

	Quaternion lockedQuaternion = Quaternion.Euler(0, 0, 0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = lockedQuaternion;
	}
}
