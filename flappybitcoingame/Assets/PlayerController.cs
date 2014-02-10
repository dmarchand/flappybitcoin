using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Sprite[] sprites;
	public float framesPerSecond;
	public float forcePerClick;
	
	private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rigidBody;
	private bool _isMovingUp;
	
	// Use this for initialization
	void Start () {
		_spriteRenderer = renderer as SpriteRenderer;
		_isMovingUp = false;
		_rigidBody = this.rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {
		int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
		index = index % sprites.Length;
		_spriteRenderer.sprite = sprites[ index ];

		print(_rigidBody.velocity.y);

		if (Input.GetButton ("Fire1")) {
			this._rigidBody.velocity = new Vector2(0, forcePerClick * 10);
			_isMovingUp = true;
		}

		if (_isMovingUp && this._rigidBody.velocity.y < .05f) {
			_isMovingUp = false;
		}
	}
}
