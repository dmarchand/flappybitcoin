using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	float _scrollSpeed = .5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = new Vector3();
		float currentSpeed = _scrollSpeed;
		newPosition.Set(transform.position.x - Time.deltaTime * currentSpeed, transform.position.y, transform.position.z);

		transform.position = newPosition;

		if (transform.position.x <= -4) {
			Destroy(this.gameObject);
		}
	}
}
