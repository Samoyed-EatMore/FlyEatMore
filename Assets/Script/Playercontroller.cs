using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour {

	private float speed = 1;// 前进速度
	private float degree = 2;// 旋转角
	private float dSpeed = 2;// 每次加速的变量，可设置为合理值
	private float maxSpeed = 30;// 最大速度，可设置为合理值
	private float timeBefore = 0;
	private float maxPos = 75;// 最高位置
	public Text countText;
	public Text gameText;// 游戏结果
	private bool isOutOfBound = false;// 边界

	private Rigidbody rb;
	private int count;

	private bool isMoving = true;// 控制前进

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		timeBefore = Time.time;
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

		if (isOutOfBound) {
			if (Time.time - timeBefore == 1) {
				gameText.text = "You'll lose in 3 seconds!";
			} else if (Time.time - timeBefore == 2) {
				gameText.text = "You'll lose in 2 seconds!";
			} else if (Time.time - timeBefore == 3) {
				gameText.text = "You'll lose in 1 seconds!";
			} else if (Time.time - timeBefore == 4){
				gameText.text = "You'll lose in 0 seconds!";
			} else if (Time.time - timeBefore > 5){
				if (rb.position.y > maxPos) { // confirm again
					GameOver ();
				} else {
					isOutOfBound = false;
					ClearGameText ();
				}
			}
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
			GameOver ();
		} else if (other.gameObject.CompareTag ("Boundary")) {
			OutOfBoundaryWarning ();
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

	// Out of Boundary 
	void OutOfBoundaryWarning () {
		isOutOfBound = !isOutOfBound;
		if (isOutOfBound) {
			gameText.text = "You'll lose the game \n if you keep on \n flying too high!";
			timeBefore = Time.time;
		} else {
			ClearGameText ();
		}
	}

	void ClearGameText(){
		gameText.text = "";
	}
	// Game over.
	void GameOver ()
	{
		isMoving = false;
		gameText.text = "Game over! \n Your score is: " + count.ToString ();
	}
}
