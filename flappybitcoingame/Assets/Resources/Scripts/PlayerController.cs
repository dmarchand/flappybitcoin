using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public Sprite[] sprites;

	public float framesPerSecond;
	public float forcePerClick;
	
	private SpriteRenderer _spriteRenderer;
	private Rigidbody2D _rigidBody;
	bool _isActive;
	bool _buttonReleased;
	
	// Use this for initialization
	void Start () {
		_spriteRenderer = renderer as SpriteRenderer;
		_rigidBody = this.rigidbody2D;
		_isActive = false;
		_rigidBody.Sleep ();
		_buttonReleased = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_isActive) {
			return;
		}

		int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
		index = index % sprites.Length;
		_spriteRenderer.sprite = sprites[ index ];




		if (_buttonReleased && Input.GetButton ("Fire1")) {
			this._rigidBody.velocity = new Vector2 (0, forcePerClick);
			_buttonReleased = false;
		} 

		if (!Input.GetButton ("Fire1")) {
			_buttonReleased = true;
		}



		if (this.transform.position.y > 3.0f || this.transform.position.y < -3.0f) {
			Application.LoadLevel(0);
		}
	}



	void ActivateGame() {
		_isActive = true;
		_rigidBody.WakeUp ();
	}


}
