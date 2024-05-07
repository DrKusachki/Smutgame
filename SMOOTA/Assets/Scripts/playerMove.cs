using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] float jumpStrength = 5f;
	[SerializeField] public float moveMod = 1f;
	[SerializeField] public float jumpMod = 1f;
	[SerializeField] public float dashDistance = 5f;
	[SerializeField] Animator animator;
	[HideInInspector] static public playerMove player;
	public enum states
	{
		standing,
		jumping,
		dashing,
		running
	}
	public enum direction : int
	{
		left = -1,
		right = 1
	}
	[HideInInspector] public direction dir = direction.right;
	[HideInInspector] public states state = states.dashing;
	Rigidbody2D rb;
	bool isGrounded = false;
	bool isWalled = false;

	private void Start()
	{
		player = this;
		rb = this.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (rb.velocity.magnitude > 0.001f && state != states.dashing)
		{
			if (!isGrounded)
				animator.SetBool("IsFalling", true);
			else if (state == states.running)
				animator.SetBool("IsWalking", true);
		}
		else
		{
			animator.SetBool("IsWalking", false);
			animator.SetBool("IsFalling", false);
		} 
		if (Input.GetKeyDown("space") && state != states.dashing && isGrounded)
			jump();

		if (state != states.dashing)
			move();

		if (rb.velocity.magnitude < 0.001f && state != states.dashing)
			state = states.standing;

		if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
		{
			StartCoroutine(dash());
		}
		mirror();
	}

	private void mirror()
	{
		gameObject.GetComponent<SpriteRenderer>().flipX = dir > 0 ? false : true;
	}

	private void move()
	{
		rb.velocity = Vector2.right * Input.GetAxisRaw("Horizontal") * moveSpeed * moveMod + Vector2.up * rb.velocity.y;
		if (Input.GetAxisRaw("Horizontal") == 1)
			dir = direction.right;
		else if (Input.GetAxisRaw("Horizontal") == -1)
			dir = direction.left;
		if(Input.GetAxisRaw("Horizontal") != 0)
			state = states.running;
	}

	private void jump()
	{
		rb.AddForce(Vector3.up * jumpStrength * jumpMod, ForceMode2D.Impulse); 
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
			isGrounded = true;
		
		if (collision.gameObject.tag == "Wall")
			isWalled = true;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
			isGrounded = false;
	
		if (collision.gameObject.tag == "Wall")
			isWalled = false;
	}

	IEnumerator dash()
	{
		LayerMask lm = gameObject.layer;
		state = states.dashing;
		animator.SetBool("IsDashing", true);
		gameObject.layer = LayerMask.NameToLayer("Invulnerable");
		float startpos = transform.position.x;
		float endpos = transform.position.x + ((int)dir) * dashDistance;
        transform.localPosition -= Vector3.up * .5f;
        gameObject.GetComponent<CapsuleCollider2D>().size -= Vector2.up;
 		for (float f = 0f; f < 1; f += .01f)
		{
			if (!isGrounded)
				break;
			if (isWalled)
				break;
			transform.position = Vector3.right * Mathf.Lerp(startpos, endpos, f) + Vector3.up * transform.position.y;
			yield return new WaitForSeconds((dashDistance / 100) * .0075f);
		}
		gameObject.GetComponent<CapsuleCollider2D>().size += Vector2.up;
        transform.localPosition += Vector3.up * .5f;
        gameObject.layer = lm;
		state = states.standing;
		animator.SetBool("IsDashing", false);
		yield return null;
	}
}
	