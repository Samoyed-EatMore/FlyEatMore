using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {
	private Rigidbody rb;
	private float tumble;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		tumble = 0.5f;
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}

}
