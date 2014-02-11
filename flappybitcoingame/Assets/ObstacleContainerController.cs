using UnityEngine;
using System.Collections;

public class ObstacleContainerController : MonoBehaviour {

	float _scrollSpeed = .5f;
	Sprite _sprite;
	bool _hasBeenScored;
	GameplayController _gameplayController;

	public bool tracksScore = false;
	
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

		if (!_hasBeenScored && tracksScore && transform.position.x <= -0.5) {
			_hasBeenScored = true;
			_gameplayController.AddScore();
		}
	}

	public void SetSprite(Sprite sprite, float x, float y, float rectX, float rectY) {
		this._sprite = sprite;
		SpriteAndScale sands = new SpriteAndScale ();
		sands.sprite = sprite;
		sands.x = x;
		sands.y = y;
		sands.rectX = rectX;
		sands.rectY = rectY;
		BroadcastMessage ("UpdateSprite", sands);
	}

	public void SetGameplayController(GameplayController controller) {
		_gameplayController = controller;
	}

	public class SpriteAndScale {

		public Sprite sprite;
		public float x;
		public float y;
		public float rectX;
		public float rectY;
	}
}
