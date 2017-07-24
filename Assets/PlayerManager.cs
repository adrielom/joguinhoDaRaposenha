using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance;
	public int direction;
	public bool isGrounded, attachedToTheWall;
	public float speed, jumpSpeed;
	public GameObject raycastForward, raycastDownwards;


	void Start() {
		if (instance == null) {
			instance = this;
		}

		isGrounded = true;

	}
	
	void FixedUpdate () {
		Movement();
		Raycasting();
	}

	public void Movement() {

		if (attachedToTheWall == false) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed *direction, gameObject.GetComponent<Rigidbody2D>().velocity.y);
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			Jumping();
		}
			
		if (isGrounded == false && attachedToTheWall == true) {
			GetComponent<Rigidbody2D>().gravityScale = .2f;
		}
		else {
			GetComponent<Rigidbody2D>().gravityScale = 1f;
		}

		if (direction == -1) {
			transform.eulerAngles = new Vector2(0, 180);
		}
		else {
			transform.eulerAngles = new Vector2(0, 0);
		}

	}

	public void Jumping() {

		if (isGrounded == true) {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
		}

		if (isGrounded == true && attachedToTheWall == true) {
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);

		}
		else if (isGrounded == false && attachedToTheWall == true){
			GetComponent<Rigidbody2D>().velocity = new Vector2 (jumpSpeed *direction, gameObject.GetComponent<Rigidbody2D>().velocity.y);
		}

		if (attachedToTheWall == true) {
			direction *= -1;
		}
	}

	public void Raycasting() {

		Debug.DrawLine (transform.position, raycastForward.transform.position, Color.red);
		Debug.DrawLine (transform.position, raycastDownwards.transform.position, Color.red);

		RaycastHit2D hitDownwards = Physics2D.Linecast(transform.position, raycastDownwards.transform.position, 1 << LayerMask.GetMask("Player"));
		RaycastHit2D hitForwards = Physics2D.Linecast(transform.position, raycastForward.transform.position, 1 << LayerMask.GetMask("Player"));
		if (hitDownwards.collider != null) {
			isGrounded = true;
			Debug.Log(hitDownwards.collider.gameObject.name);
		}
		else {
			isGrounded = false;
		}


	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Wall") {
			attachedToTheWall = true;
		}
	}

	void OnCollisionExit2D (Collision2D col) {
		if (col.gameObject.tag == "Wall") {
			attachedToTheWall = false;
		}
	}

	void OnMouseDown (){
		Jumping();
	}


}
