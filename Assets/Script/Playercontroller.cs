using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour {


	Vector3 oldPos;// 记录位置
	public float speed;// 前进速度
	public float degree;// 旋转角
	public Text countText;

	private Rigidbody rb;
	private int count;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// 转弯
		if (moveHorizontal > 0) {
			rb.transform.Rotate (Vector3.up * degree);
		}
		if (moveHorizontal < 0) {
			rb.transform.Rotate (Vector3.up * -degree);
		}
		if (moveVertical > 0) {
			rb.transform.Rotate (Vector3.left * degree);
		}
		if (moveVertical < 0) {
			rb.transform.Rotate (Vector3.left * -degree);
		}

		oldPos = transform.position;//记录位置
		rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);//移动

	}

	//TODO: 给其他物体加上标签，本物体（与其他物体）碰撞时，实现相应动作
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Food")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		} else if (other.gameObject.CompareTag ("barrier")) {
			//TODO
		}			
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		//TODO: speed up
	}
		
}
