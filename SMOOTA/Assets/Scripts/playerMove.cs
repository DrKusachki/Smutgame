using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMove : MonoBehaviour
{
	[Header("Moving")]
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] public float moveMod = 1f;
	[Header("Dash")]
	[SerializeField] public float dashDistance = 5f;
	[SerializeField] float dashTime = 0f;
	[Header("Jump")]
	[SerializeField] float jumpStrength = 5f;
	[SerializeField] public float jumpMod = 1f;
	[SerializeField] public Animator animator;
	[HideInInspector] static public playerMove player;
	public enum states
	{
		standing,
		jumping,
		dashing,
		running,
		attacking
	}
	public enum direction : int
	{
		left = -1,
		right = 1
	}
	[HideInInspector] public direction dir = direction.right;
	[HideInInspector] public states state = states.dashing;
	Rigidbody2D rb;
	[HideInInspector] public bool isGrounded => GetComponentInChildren<playerGroundedCheck>().isGrounded;
	bool isWalled = false;


	private void Start()
	{
		player = this;
		rb = this.GetComponent<Rigidbody2D>();
	}

	public int Facing()
    {
		return (int)dir;
    }

	private void Mirror()
	{
		gameObject.GetComponent<SpriteRenderer>().flipX = dir <= 0;
	}

	public void Move()
	{
		rb.velocity = Input.GetAxisRaw("Horizontal") * moveMod * moveSpeed * Vector2.right + Vector2.up * rb.velocity.y;
		dir = Input.GetAxisRaw("Horizontal") == 0 ? dir : (direction)Input.GetAxisRaw("Horizontal");
		Mirror();
	}

	public void Jump()
	{
		rb.AddForce(jumpMod * jumpStrength * Vector3.up, ForceMode2D.Impulse);
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
			isWalled = true;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
			isWalled = false;
	}

	public IEnumerator Dash()
	{
		LayerMask lm = gameObject.layer;
		gameObject.layer = LayerMask.NameToLayer("Invulnerable");
;
		gameObject.GetComponent<BoxCollider2D>().size -= Vector2.up;
        transform.localPosition -= Vector3.up * .5f;
		
		float startpos = transform.position.x;
		float endpos = transform.position.x + ((int)dir) * dashDistance;

		float speed = dashDistance / dashTime;
		for (float f = 0f; f <= 1 && isGrounded && !isWalled; f += .01f)
		{
			transform.position += (int)dir * dashDistance * Vector3.right / 100f;
			yield return new WaitForSeconds(dashTime / 100f);
		}
		rb.velocity = Vector2.zero;
		
		gameObject.GetComponent<BoxCollider2D>().size += Vector2.up;
        transform.localPosition += Vector3.up * .5f;
        
		gameObject.layer = lm;
		yield return null;
	}
}
	