using UnityEngine;
using System.Collections;

public class BandMemberMoving : MonoBehaviour {

	public bool moving = true;
	public float moveTiming = 300f;
	public float moveTimingStartValue = 300f;
	private float moveValue;
	public float speed = 5f;
	private Vector3 targetPosition;

	
	// Update is called once per frame
	void Update () {
		if (moving == true){
			moveTiming--;
			if (moveTiming < 0){
				moveValue = UnityEngine.Random.Range(0, 8) - 4f;
				float currentMove = transform.position.x + moveValue;
				targetPosition = new Vector3 (currentMove, transform.position.y, transform.position.z);
				moveTiming = moveTimingStartValue;
			}
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
		}
	}
}
