using UnityEngine;
using System.Collections;

public class PowerupController : MonoBehaviour {

	public Sprite sprite;
	float _scrollSpeed = .5f;

	SpriteRenderer _spriteRenderer;

	// Use this for initialization
	void Start () {
		_spriteRenderer = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = new Vector3();
		float currentSpeed = _scrollSpeed;
		newPosition.Set(transform.position.x - Time.deltaTime * currentSpeed, transform.position.y, transform.position.z);
		_spriteRenderer.sprite = sprite;
	}
}
