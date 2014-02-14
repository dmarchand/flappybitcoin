using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour {

	public Sprite[] enemySprites;
	public ScalePair[] spriteScales;

	public bool isGameActive;
	bool _powerupEnabled;
	
	public float timeUntilSpawn;
	float _elapsedTime = 0;
	float _elapsedPowerupTime = 0;

	public float gapHeight;
	public float topPillarMaxOffset;
	public int pickupSpawnChance;

	public int score;
	public float powerupTime;
	
	GameObject _enemyPillarPrefab;
	GameObject _powerupPrefab;

	float _pausedTime = 0f;

	[SerializeField]
	public bool IsGameOver 
	{
		get 
		{
			return !isGameActive;
		}
	}

	[SerializeField]
	public bool IsGamePaused {
		get;
		set;
	}

	public bool IsGameDisabled {
		get 
		{
			if(IsGameOver || IsGamePaused) { return true; }
			return false;
		}
	}


	// Use this for initialization
	void Start () {
		isGameActive = false;
		IsGamePaused = false;
		_enemyPillarPrefab = Resources.Load<GameObject> ("Prefabs/EnemyPillar");
		_powerupPrefab = Resources.Load<GameObject> ("Prefabs/Powerup");

		score = 0;
		AdMobPlugin.ShowBannerView ();
	}

	public void StartNewGame() {
		AdMobPlugin.HideBannerView ();
		isGameActive = true;
		BroadcastMessage("ActivateGame");
		SpawnDynamicObstacles();
	}

	public void PauseToggle() {

		if (!IsGamePaused) {
			FindObjectOfType<PlayerController> ().GetComponent<Rigidbody2D> ().Sleep ();
		} else {
			FindObjectOfType<PlayerController> ().GetComponent<Rigidbody2D> ().WakeUp ();
		}

		IsGamePaused = !IsGamePaused;
	}

	// Update is called once per frame
	void FixedUpdate () {	
		if (!IsGameDisabled) {
			AdMobPlugin.HideBannerView ();
			_elapsedTime += Time.deltaTime;

			if (_elapsedTime >= timeUntilSpawn) {
				_elapsedTime = 0;
				SpawnDynamicObstacles();
				SpawnPickups();
			}

			if(_powerupEnabled) {
				_elapsedPowerupTime += Time.deltaTime;
				if(_elapsedPowerupTime >= powerupTime) {
					DisablePowerup();
				}
			}
		}


	}

	void SpawnPickups() {
		if (Random.Range (0, 100) > pickupSpawnChance) {
			float yPosition = Random.Range(-40, 40) / 10;

			PowerupController powerup = ((GameObject)(Instantiate(_powerupPrefab, new Vector3(4, yPosition, 0), UnityEngine.Quaternion.identity))).GetComponent<PowerupController>();
			powerup.transform.parent = this.transform;
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

		bottomPillar.transform.parent = this.transform;
		topPillar.transform.parent = this.transform;

		if (_powerupEnabled) {
			BroadcastMessage("PowerupEnable");
		}
	}

	void GetPowerup() {
		BroadcastMessage ("PowerupEnable");
		_elapsedPowerupTime = 0;
		_powerupEnabled = true;
	}

	void DisablePowerup() {
		BroadcastMessage ("PowerupDisable");
		_powerupEnabled = false;
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
