﻿using UnityEngine;
using System.Collections;

public class GameplayController : MonoBehaviour {

	bool _isGameActive;

	int _numObstacleTypes = 3;
	float _timeUntilSpawn = 4;
	float _elapsedTime = 0;

	int _previousObstacle = -1;

	GameObject _smallObamaPrefab;
	GameObject _mediumObamaPrefab;
	GameObject _largeObamaPrefab;

	// Use this for initialization
	void Start () {
		_isGameActive = false;

		_smallObamaPrefab = Resources.Load<GameObject> ("Prefabs/ObamaSmall");
		_mediumObamaPrefab = Resources.Load<GameObject> ("Prefabs/ObamaMedium");
		_largeObamaPrefab = Resources.Load<GameObject> ("Prefabs/ObamaLarge");
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
		GameObject smallObama = (GameObject)Instantiate(_smallObamaPrefab, new Vector3(2, 2.04f, 0), UnityEngine.Quaternion.identity);
		GameObject largeObama = (GameObject)Instantiate(_largeObamaPrefab, new Vector3(2, 0, 0), UnityEngine.Quaternion.identity);
	}

	void SpawnMediumObstacle() {
		GameObject mediumObama = (GameObject)Instantiate(_mediumObamaPrefab, new Vector3(2, 2.04f, 0), UnityEngine.Quaternion.identity);
		GameObject mediumObama2 = (GameObject)Instantiate(_mediumObamaPrefab, new Vector3(2, -1, 0), UnityEngine.Quaternion.identity);
	}

	void SpawnLargeObstacle() {
		GameObject largeObama = (GameObject)Instantiate(_largeObamaPrefab, new Vector3(2, 2.04f, 0), UnityEngine.Quaternion.identity);
		GameObject smallObama = (GameObject)Instantiate(_smallObamaPrefab, new Vector3(2, -2.1f, 0), UnityEngine.Quaternion.identity);
	}
}