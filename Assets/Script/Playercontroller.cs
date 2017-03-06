using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Playercontroller : MonoBehaviour {
	public float speed;// 前进速度
//	private float degree = 2;// 旋转角
	private float dSpeed = 2;// 每次加速的变量，可设置为合理值
//	private float totalDegree = 0;// vertical
	private float maxSpeed = 40;// 最大速度，可设置为合理值
	private float timeOutBefore = 0;
	private float timeMoveBefore = 0;
	private float timeAttackBefore = 0;
	private float timeShowRoundBefore = 0;
	private float maxPos = 75;// 最高位置
	private float moveHorizontal;
	private float moveVertical;
	private float count2NextRound = 1;

	public Text countText;
	public Text gameText;// 游戏结果
	public Text speedText;
	public Text roundText;

	public Animator anim;// 物体动画

	public AudioClip audioDead;
	public AudioClip audioEat;

	private Rigidbody rb;
	private int count;

	private bool isMoving = true;// 控制前进
	private bool isOutOfBound = false;// 上边界
	private bool isOutOfEdge = false;// 侧面边界
	private bool isAttack = false;// 攻击
	private bool dashing = false;
	private bool isNextRound = false;
	private bool isTrrigerNextRound = false;

	public GameObject targetCamera;
	public GameObject food1;
	public GameObject food2;

	void Start()
	{
			rb = GetComponent<Rigidbody> ();
			anim.SetBool ("isDead", false);
			anim.SetBool ("isAttack", false);
			count = 0;
			SetCountText ();
			timeOutBefore = Time.time;
			timeMoveBefore = Time.time;
	}

	void FixedUpdate ()
	{
		if (targetCamera.activeInHierarchy) {
//			moveHorizontal = Input.GetAxis ("Horizontal");
//			moveVertical = Input.GetAxis ("Vertical");
//			if (moveVertical > 0) {
//				totalDegree += degree;
//				rb.transform.Rotate (Vector3.left * degree);
//			}
//			if (moveVertical < 0) {
//				totalDegree -= degree;
//				rb.transform.Rotate (Vector3.left * -degree);
//			}
//
//			// 左右转弯，首先保持水平
//			if (totalDegree == 0) {
//				moveHorizontal = Input.GetAxis ("Horizontal");
//				// 左右转弯
//				if (moveHorizontal > 0) {
//					rb.transform.Rotate (Vector3.up * degree);
//				}
//				if (moveHorizontal < 0) {
//					rb.transform.Rotate (Vector3.up * -degree);
//				}
//				timeMoveBefore = Time.time;
//			} else {// 方向非水平，需调整
//				if (Time.time - timeMoveBefore > 0.05) {
//					if (totalDegree > 0) {
//						totalDegree -= degree;
//						rb.transform.Rotate (Vector3.left * -degree);
//					}
//					if (totalDegree < 0) {
//						totalDegree += degree;
//						rb.transform.Rotate (Vector3.left * degree);
//					}
//					timeMoveBefore = Time.time;
//				}
//			}

			Quaternion rot = GvrViewer.Instance.HeadPose.Orientation;
//			rb.transform.rotation = rot;
			bool triggered = GvrViewer.Instance.Triggered;
			if (triggered) {
				dashing = !dashing;
				updateSpeedText ();
			}
			if (isMoving) {
				float actualSpeed = dashing ? maxSpeed : speed;
				rb.transform.Translate (targetCamera.transform.forward.normalized * actualSpeed * Time.deltaTime);//移动
			}

			if (isOutOfBound) {
				if (Time.time - timeOutBefore == 1) {
					gameText.text = "You'll lose in 3 seconds!";
				} else if (Time.time - timeOutBefore == 2) {
					gameText.text = "You'll lose in 2 seconds!";
				} else if (Time.time - timeOutBefore == 3) {
					gameText.text = "You'll lose in 1 seconds!";
				} else if (Time.time - timeOutBefore == 4) {
					gameText.text = "You'll lose in 0 seconds!";
				} else if (Time.time - timeOutBefore > 5) {
					if (rb.position.y > maxPos || isOutOfEdge) { // confirm again
						GameOver ();
					} else {
						isOutOfBound = false;
						ClearText (gameText);
					}
				}
			}

			if (isNextRound && !isTrrigerNextRound) {
				if (Time.time - timeShowRoundBefore > 3) {
					ClearText (roundText);
					isNextRound = false;
					isTrrigerNextRound = true;
				}
			}

			if (isAttack && Time.time - timeAttackBefore > 0.5) {
				anim.SetBool ("isAttack", false);
				isAttack = false;
			}

			if (anim.GetBool ("isDead") && triggered) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}

//  Moved to Crow.cs
//	// 给其他物体加上标签，本物体（与其他物体）碰撞时，实现相应动作
//	void OnTriggerEnter(Collider other) 
//	{
//		if (targetCamera.activeInHierarchy) {
//			if (other.gameObject.CompareTag ("Food")) {
//				Eat (other);
//			} else if (other.gameObject.CompareTag ("barrier")) {
//				GameOver ();
//			} else if (other.gameObject.CompareTag ("Boundary")) {
//				OutOfBoundaryWarning ();
//		    } else if (other.gameObject.CompareTag ("Edges")) {
//			    OutOfBoundaryWarning ();
//				isOutOfEdge = !isOutOfEdge;
//		    }
//			else if (other.gameObject.CompareTag ("hazard")) {
//				Destroy (other.gameObject);
//				GameOver ();
//			} else if (other.gameObject.CompareTag ("MyOwnBoundary")) {
//				return;
//			}
//		}
//	}

	public void Eat(Collider other){
		anim.SetBool ("isAttack", true);
		isAttack = true;
		timeAttackBefore = Time.time;
		other.gameObject.SetActive (false);
		targetCamera.GetComponent<AudioSource> ().PlayOneShot (audioEat);
		count = count + 1;
		SetCountText ();
		SpeedUp ();
	}

	public bool NextRound(){
		if (!isTrrigerNextRound) {
			if (count > count2NextRound) {
				return true;
			} else {
				return false;
			}
		}
		return false;
	}

	public void SetRoundText (){
		roundText.text = "Round 2!";
		timeShowRoundBefore = Time.time;
		isNextRound = true;
	}

	void SetCountText ()
	{
		countText.text = "Score: " + count.ToString ();
	}

	// Speed up
	void SpeedUp ()
	{
		if (speed < maxSpeed) {
			speed += dSpeed; 
		}
		updateSpeedText ();
	}

	void updateSpeedText() {
		Debug.Log (dashing);
		if (speed == maxSpeed || dashing) {
			Debug.Log ("max");
			speedText.text = "Speed: MAX\n";
		} else {
			speedText.text = "Speed: " + speed;
		}
	}

	// Out of Boundary 
	public void OutOfBoundaryWarning () {
		if (rb.position.y > maxPos) {
		}
		isOutOfBound = !isOutOfBound;
		if (isOutOfBound) {
			gameText.text = "You'll lose the game \n if you keep on \n flying too far away!";
			timeOutBefore = Time.time;
		} else {
			ClearText (gameText);
		}
	}

	void ClearText(Text text){
		text.text = "";
	}

	// Game over.
	public void GameOver ()
	{
		if (isMoving) {
			targetCamera.GetComponent<AudioSource> ().PlayOneShot (audioDead);
			anim.SetBool ("isDead", true);
			isMoving = false;
			gameText.text = "Game over! \n Your score is: " + count.ToString () + "\n\nPull the trigger to restart.";
		}
	}

	public void changeIsOutOfEdge() {
		isOutOfEdge = !isOutOfEdge;
	}
}
