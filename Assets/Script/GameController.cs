using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject hazard;
	private Vector3 spawnValues = new Vector3 (20,20,20);
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Transform target;

	void Start ()
	{
		StartCoroutine (SpawnWaves ());
	}

	void FixedUpdate ()
	{
		//keep moving with crown
		transform.position = target.transform.position + new Vector3(0, 0, 20);
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = transform.position + Vector3.forward * 20 + Vector3.right * Random.Range (-20,20) + Vector3.up *  Random.Range (-10,10);//new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), Random.Range (-spawnValues.z, spawnValues.z));
//				Quaternion spawnRotation = Quaternion.identity;
				Vector3 relativePos = spawnPosition - target.position;
				Quaternion spawnRotation = Quaternion.LookRotation(relativePos);
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}
}
