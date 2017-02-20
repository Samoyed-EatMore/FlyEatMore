using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;

	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag( "Player")) {
//			Instantiate(explosion, transform.position, transform.rotation);
		}
	}
}
