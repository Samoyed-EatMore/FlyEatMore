using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crow : MonoBehaviour
{
	public GameObject explosion;
	public Playercontroller controller;

	// 给其他物体加上标签，本物体（与其他物体）碰撞时，实现相应动作

	void Start() {
		
	}

	void Update() {
	
	}

	void OnTriggerEnter(Collider other) 
	{
		if (controller.targetCamera.activeInHierarchy) {
			if (other.gameObject.CompareTag ("Food")) {
				Debug.Log ("Eat!");
				controller.Eat (other);
			} else if (other.gameObject.CompareTag ("barrier")) {
				controller.GameOver ();
			} else if (other.gameObject.CompareTag ("Boundary")) {
				controller.OutOfBoundaryWarning ();
			} else if (other.gameObject.CompareTag ("Edges")) {
				controller.OutOfBoundaryWarning ();
				controller.changeIsOutOfEdge ();
			} else if (other.gameObject.CompareTag ("Edge2NextRound")) {
				if (controller.NextRound()) {
				    controller.food1.SetActive (false);
				    controller.food2.SetActive (true);
				    controller.SetRoundText ();
				}
			}
			else if (other.gameObject.CompareTag ("hazard")) {
				Instantiate(explosion, transform.position, transform.rotation, transform);
				Destroy (other.gameObject);
				controller.GameOver ();
			} else if (other.gameObject.CompareTag ("MyOwnBoundary")) {
				return;
			}
		}
	}
}

