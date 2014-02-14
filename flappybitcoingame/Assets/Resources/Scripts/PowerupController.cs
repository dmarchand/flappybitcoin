using UnityEngine;
using System.Collections;

public class PowerupController : MonoBehaviour {

	public Sprite sprite;
	float _scrollSpeed = .7f;

	SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
		_spriteRenderer = renderer as SpriteRenderer;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_spriteRenderer.sprite = sprite;

		Vector3 newPosition = new Vector3();
		float currentSpeed = _scrollSpeed;
		newPosition.Set(transform.position.x - Time.deltaTime * currentSpeed, transform.position.y, transform.position.z);
		this.transform.position = newPosition;
	}

	void OnTriggerEnter2D(Collider2D other){
		SendMessageUpwards ("GetPowerup");
		Destroy (this.gameObject);
	}
}
