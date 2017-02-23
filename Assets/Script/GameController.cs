using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	private Vector3 spawnValues = new Vector3 (20,20,50);
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Transform target;
	public GameObject parent;

	void Start ()
	{
		StartCoroutine (SpawnWaves ());
	}

	void FixedUpdate ()
	{
		transform.position = target.transform.position + new Vector3(0, 0, 20);
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = transform.position + new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), spawnValues.z);
//				Quaternion spawnRotation = Quaternion.identity;
				Vector3 relativePos = spawnPosition - target.position;
				Quaternion spawnRotation = Quaternion.LookRotation(relativePos);
				GameObject relativePosition = Instantiate (parent, transform.position, transform.rotation);
				Instantiate (hazard, spawnPosition, spawnRotation, relativePosition.transform);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}
