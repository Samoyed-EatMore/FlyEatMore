using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public GameObject crow;
	public GameObject startMenuCanvas;
	public GameObject sphere;
	public GameObject anim;
	public GameObject main;
	private int status = 0;
	private int speed = 1;

	// Use this for initialization
	void Start ()
	{
		transform.parent = startMenuCanvas.transform;
		transform.localPosition = new Vector3 (-29, -71, -946);
		transform.localRotation = new Quaternion (0.003127605f, 0.007225577f, -0.01433141f, 0.9998663f);
		crow.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
		if (status == 0) {
			transform.Translate (Vector3.forward * speed);
		}
		else if (status == 1 && GvrViewer.Instance.Triggered) {
			status = 2;
			transform.parent = anim.transform;
			anim.SetActive (true);
			sphere.SetActive (false);
			transform.localPosition = new Vector3 (0, 0, 0);
			transform.localRotation = new Quaternion (0, 0, 0, 0); 
			transform.localScale = new Vector3 (5.927566f, 15.57658f, 6.676364f);
		}
		else if (status == 2){
			AnimatorStateInfo info = anim.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);
			Debug.Log (info.IsName ("Base Layer.Exit"));
			if (info.IsName ("Base Layer.Exit")) {
				status = 3;
				crow.SetActive (true);
				main.SetActive (true);
				transform.parent = main.transform;
				transform.localScale = new Vector3 (1, 1, 1);
				transform.localScale = new Vector3 (1, 1, 1);
				anim.SetActive (false);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("StartCancus"))
		{
			status = 1;
			other.gameObject.SetActive(false);
			sphere.SetActive (true);
			startMenuCanvas.SetActive (false);
			transform.parent = sphere.transform;
			transform.localPosition = new Vector3 (0, 0, 0);
			transform.localRotation = new Quaternion (0, 0, 0, 0); 

		}
	}
}

