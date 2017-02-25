using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimController : MonoBehaviour {
	public GameObject targetCamera;
	private Animator anim;
//	private int exitStateHash = Animator.StringToHash("Base Layer.Exit");

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);//base layer is 0
//		if (animStateInfo.nameHash == exitStateHash) {
//			
//		}
		if (animStateInfo.IsName("Base Layer.Exit")) {
			gameObject.SetActive(false);
			targetCamera.gameObject.SetActive(true);
		}
	}
}
