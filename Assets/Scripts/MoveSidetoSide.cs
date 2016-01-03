using UnityEngine;
using System.Collections;

public class MoveSidetoSide : MonoBehaviour {
	private float newPositionX;
	private float newPositionY;
	public float moveSpeed = 0.5f;
	public int currentFloor = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("left")){
			newPositionX = transform.position.x-moveSpeed;
			transform.position = new Vector3 (newPositionX, transform.position.y, transform.position.z);
		}
		if (Input.GetKey("right")){
			newPositionX = transform.position.x+moveSpeed;
			transform.position = new Vector3 (newPositionX, transform.position.y, transform.position.z);
		}
	}

	void OnTriggerStay(Collider coll){
		if(coll.gameObject.tag == "Stairs" && Input.GetKeyDown("up") && currentFloor < 2){
			coll.gameObject.SendMessage ("MoveUp", this.gameObject);
			currentFloor++;
		}
		if(coll.gameObject.tag == "Stairs" && Input.GetKeyDown("down") && currentFloor > 0){
			coll.gameObject.SendMessage ("MoveDown", this.gameObject);
			currentFloor--;
		}
	}
}
