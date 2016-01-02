using UnityEngine;
using System.Collections;

public class MoveSidetoSide : MonoBehaviour {
	private float newPositionX;
	private float newPositionY;
	public float moveSpeed = 0.5f;
	public float floorDifference = 1f;
	public int currentFloor = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("left")){
			newPositionX = transform.position.x-moveSpeed;
			transform.position = new Vector3 (newPositionX, transform.position.y, 0);
		}
		if (Input.GetKey("right")){
			newPositionX = transform.position.x+moveSpeed;
			transform.position = new Vector3 (newPositionX, transform.position.y, 0);
		}
	}

	void OnTriggerStay(Collider coll){
		if(coll.gameObject.tag == "Stairs" && Input.GetKeyDown("up") && currentFloor < 2){
			newPositionY = transform.position.y+floorDifference;
			transform.position = new Vector3 (transform.position.x, newPositionY, 0);
			currentFloor++;
		}
		if(coll.gameObject.tag == "Stairs" && Input.GetKeyDown("down") && currentFloor > 0){
			newPositionY = transform.position.y-floorDifference;
			transform.position = new Vector3 (transform.position.x, newPositionY, 0);
			currentFloor--;
		}
	}
}
