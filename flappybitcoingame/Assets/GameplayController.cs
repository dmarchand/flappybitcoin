using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour {

	public Sprite[] enemySprites;
	public ScalePair[] spriteScales;

	bool _isGameActive;

	int _numObstacleTypes = 3;
	float _timeUntilSpawn = 4;
	float _elapsedTime = 0;

	int _previousObstacle = -1;
	public int score;

	GameObject _smallObamaPrefab;
	GameObject _mediumObamaPrefab;
	GameObject _largeObamaPrefab;

	// Use this for initialization
	void Start () {
		_isGameActive = false;

		_smallObamaPrefab = Resources.Load<GameObject> ("Prefabs/ObamaSmall");
		_mediumObamaPrefab = Resources.Load<GameObject> ("Prefabs/ObamaMedium");
		_largeObamaPrefab = Resources.Load<GameObject> ("Prefabs/ObamaLarge");

		score = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!_isGameActive && Input.GetButton ("Fire1")) {
			_isGameActive = true;
			BroadcastMessage("ActivateGame");
			SpawnObstacle();
		}

		if (_isGameActive) {
			_elapsedTime += Time.deltaTime;

			if (_elapsedTime >= _timeUntilSpawn) {
				_elapsedTime = 0;
				SpawnObstacle ();
			}
		}


	}

	void SpawnObstacle() {
		int obstacleType = _previousObstacle;
		while (obstacleType == _previousObstacle) {
			obstacleType = Random.Range (0, _numObstacleTypes);
		}

		switch (obstacleType) {
			case 0:
			SpawnSmallObstacle();
			break;
		case 1:
			SpawnMediumObstacle();
			break;
		case 2:
			SpawnLargeObstacle();
			break;
			
		}
	}

	void SpawnSmallObstacle() {
		ObstacleContainerController smallObama = ((GameObject)(Instantiate(_smallObamaPrefab, new Vector3(2, 2.04f, 0), UnityEngine.Quaternion.identity))).GetComponent<ObstacleContainerController>();
		ObstacleContainerController largeObama = ((GameObject)(Instantiate(_largeObamaPrefab, new Vector3(2, 0, 0), UnityEngine.Quaternion.identity))).GetComponent<ObstacleContainerController>();


		int rand = Random.Range (0, enemySprites.Length);
		Sprite sprite = enemySprites[rand];
		smallObama.SetSprite(sprite, spriteScales[rand].scaleX, spriteScales[rand].scaleY, spriteScales[rand].rectX, spriteScales[rand].rectY);
		smallObama.tracksScore = true;
		largeObama.SetSprite(sprite, spriteScales[rand].scaleX, spriteScales[rand].scaleY, spriteScales[rand].rectX, spriteScales[rand].rectY);
		largeObama.SetGameplayController (this);
		smallObama.SetGameplayController (this);

	}

	void SpawnMediumObstacle() {
		ObstacleContainerController mediumObama = ((GameObject)(Instantiate(_mediumObamaPrefab, new Vector3(2, 2.04f, 0), UnityEngine.Quaternion.identity))).GetComponent<ObstacleContainerController>();
		ObstacleContainerController mediumObama2 = ((GameObject)(Instantiate(_mediumObamaPrefab, new Vector3(2, -1, 0), UnityEngine.Quaternion.identity))).GetComponent<ObstacleContainerController>();

		int rand = Random.Range (0, enemySprites.Length);
		Sprite sprite = enemySprites[rand];
		mediumObama.SetSprite(sprite, spriteScales[rand].scaleX, spriteScales[rand].scaleY, spriteScales[rand].rectX, spriteScales[rand].rectY);
		mediumObama2.SetSprite(sprite, spriteScales[rand].scaleX, spriteScales[rand].scaleY, spriteScales[rand].rectX, spriteScales[rand].rectY);
		mediumObama.SetGameplayController (this);
		mediumObama2.SetGameplayController (this);
		mediumObama.tracksScore = true;
	}

	void SpawnLargeObstacle() {
		ObstacleContainerController largeObama = ((GameObject)(Instantiate(_largeObamaPrefab, new Vector3(2, 2.04f, 0), UnityEngine.Quaternion.identity))).GetComponent<ObstacleContainerController>();
		ObstacleContainerController smallObama = ((GameObject)(Instantiate(_smallObamaPrefab, new Vector3(2, -2.1f, 0), UnityEngine.Quaternion.identity))).GetComponent<ObstacleContainerController>();

		int rand = Random.Range (0, enemySprites.Length);
		Sprite sprite = enemySprites[rand];
		largeObama.SetSprite (sprite, spriteScales[rand].scaleX, spriteScales[rand].scaleY, spriteScales[rand].rectX, spriteScales[rand].rectY);
		smallObama.SetSprite (sprite, spriteScales[rand].scaleX, spriteScales[rand].scaleY, spriteScales[rand].rectX, spriteScales[rand].rectY);
		largeObama.SetGameplayController (this);
		smallObama.SetGameplayController (this);
		largeObama.tracksScore = true;

	}

	public void AddScore() {
		score++;
	}

	[System.Serializable]
	public class ScalePair {
		public float scaleX;
		public float scaleY;
		public float rectX;
		public float rectY;
		
	}
}
