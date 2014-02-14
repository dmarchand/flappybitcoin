using UnityEngine;
using System.Collections;

public class StartButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick( dfControl sender, dfMouseEventArgs args )
	{
		FindObjectOfType<GameplayController> ().StartNewGame ();
	}
}
