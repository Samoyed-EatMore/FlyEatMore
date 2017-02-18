using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour {

	private float speed = 5;// 前进速度
	private float degree = 2;// 旋转角
	private float dSpeed = 5;// 每次加速的变量，可设置为合理值
	private float maxSpeed = 30;// 最大速度，可设置为合理值
	public Text countText;
	public Text gameText;// 游戏结果

	private Rigidbody rb;
	private int count;

	private bool isMoving = true;// 控制前进

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

		if(isMoving){
		    rb.transform.Translate(Vector3.forward * speed * Time.deltaTime);//移动
		}

	}

	// 给其他物体加上标签，本物体（与其他物体）碰撞时，实现相应动作
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Food")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
			SpeedUp ();
		} else if (other.gameObject.CompareTag ("barrier")) {
			GameOver();
		}			
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
	}
		
	// Speed up
	void SpeedUp ()
	{
		if (speed < maxSpeed) {
			speed += dSpeed; 
		}
	}

	// Game over.
	void GameOver ()
	{
		isMoving = false;
		gameText.text = "Game over! \n Your score is: " + count.ToString ();
	}
}
