using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour {

	public Sprite[] enemySprites;
	public ScalePair[] spriteScales;

	bool _isGameActive;
	
	public float timeUntilSpawn;
	float _elapsedTime = 0;

	public float gapHeight;
	public float topPillarMaxOffset;
	public int pickupSpawnChance;

	public int score;
	
	GameObject _enemyPillarPrefab;
	GameObject _powerupPrefab;




	// Use this for initialization
	void Start () {
		_isGameActive = false;
		_enemyPillarPrefab = Resources.Load<GameObject> ("Prefabs/EnemyPillar");
		_powerupPrefab = Resources.Load<GameObject> ("Prefabs/Powerup");

		score = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!_isGameActive && Input.GetButton ("Fire1")) {
			_isGameActive = true;
			BroadcastMessage("ActivateGame");
			SpawnDynamicObstacles();
		}

		if (_isGameActive) {
			_elapsedTime += Time.deltaTime;

			if (_elapsedTime >= timeUntilSpawn) {
				_elapsedTime = 0;
				SpawnDynamicObstacles();
				SpawnPickups();
			}
		}


	}

	void SpawnPickups() {
		if (Random.Range (0, 100) > pickupSpawnChance) {
			float yPosition = Random.Range(-20, 765) / 10;

			PowerupController topPillar = ((GameObject)(Instantiate(_enemyPillarPrefab, new Vector3(2, yPosition, 0), UnityEngine.Quaternion.identity))).GetComponent<PowerupController>();
		}
	}

	void SpawnDynamicObstacles() {

		// Spawn Pillars
		ObstacleContainerController topPillar = ((GameObject)(Instantiate(_enemyPillarPrefab, new Vector3(2, 8.65f, 0), UnityEngine.Quaternion.identity))).GetComponent<ObstacleContainerController>();
		ObstacleContainerController bottomPillar = ((GameObject)(Instantiate(_enemyPillarPrefab, new Vector3(2, 8.65f, 0), UnityEngine.Quaternion.identity))).GetComponent<ObstacleContainerController>();


		// Configure Pillar Properties
		int rand = Random.Range (0, enemySprites.Length);
		Sprite sprite = enemySprites[rand];

		topPillar.SetSprite (sprite, spriteScales[rand].scaleX, spriteScales[rand].scaleY, spriteScales[rand].rectX, spriteScales[rand].rectY);
		bottomPillar.SetSprite (sprite, spriteScales[rand].scaleX, spriteScales[rand].scaleY, spriteScales[rand].rectX, spriteScales[rand].rectY);
		topPillar.SetGameplayController (this);
		bottomPillar.SetGameplayController (this);
		topPillar.tracksScore = true;

		// Configure Pillar Offsets
		float offset = Random.Range (8, topPillarMaxOffset * 10);
		offset /= 10;
		topPillar.transform.position = new Vector3 (topPillar.transform.position.x, topPillar.transform.position.y - offset, topPillar.transform.position.z);
		bottomPillar.transform.position = new Vector3 (topPillar.transform.position.x, topPillar.transform.position.y - gapHeight - 6.5f, topPillar.transform.position.z);
		
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
