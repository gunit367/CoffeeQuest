using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public MazeGenerator mazeGenerator;

	public GameObject player;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		mazeGenerator = GetComponent < MazeGenerator>();

		InitializeGame();
	}
	
	// Update is called once per frame
	void InitializeGame () {
		mazeGenerator.generate();

		if (player != null) {
			Instantiate(player, new Vector3(1, 1, 1), Quaternion.identity);
		}
	}
}
