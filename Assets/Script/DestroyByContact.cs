using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	public Transform parent;

	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag( "Player")) {
			Instantiate(explosion, transform.position, transform.rotation, parent);
		}
	}
}
