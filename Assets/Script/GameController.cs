using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	private Vector3 spawnValues;

	void Start ()
	{
		spawnValues = new Vector3 (20.0f, 20.0f, 50.0f);
		SpawnWaves ();
	}

	void SpawnWaves ()
	{
		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), spawnValues.z);//10.0f, 0.0f, 50.0f
		Debug.Log (spawnPosition);
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (hazard, transform.position+spawnPosition, spawnRotation, transform);
	}
}
