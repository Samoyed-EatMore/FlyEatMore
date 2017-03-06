using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMover : MonoBehaviour {
	public Playercontroller player;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = player.speed * (-1.5f) * (transform.position - player.transform.position).normalized;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		transform.Translate(player.speed * (-1.5f) * (transform.position - player.transform.position).normalized);
	}
}
