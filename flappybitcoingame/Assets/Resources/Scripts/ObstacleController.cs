using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	float _scrollSpeed = .7f;
	Sprite sprite;
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
		//transform.position = newPosition;


	}

	void UpdateSprite(ObstacleContainerController.SpriteAndScale s) {
		this.sprite = s.sprite;
		gameObject.transform.localScale = new Vector3 (s.x, s.y, 1);
		gameObject.GetComponent<BoxCollider2D> ().size = new Vector2 (s.rectX, s.rectY);
	}

	void OnTriggerEnter2D(Collider2D other){
		Application.LoadLevel(0);
		//Debug.Log ("Collision");
	}

	void PowerupEnable() {
		((SpriteRenderer)renderer).color = new Color (1f, 1f, 1f, .3f);
		collider2D.enabled = false;
	}

	void PowerupDisable() {
		((SpriteRenderer)renderer).color = new Color (1f, 1f, 1f, 1f);
		collider2D.enabled = true;
	}
}
