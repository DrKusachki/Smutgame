using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
public class playerMain : MonoBehaviour
{

	public enum states
	{
		standing,
		jumping,
		dashing,
		running,
		attacking,
		falling,
		parry
	}
	public enum direction : int
	{
		left = -1,
		right = 1
	}
	[SerializeField] public playerStats stats;

	private static playerMain _Player;
	public static playerMain Player { get { return _Player;  } }
	bool wasGrounded = false;
	bool justJumped = false;
	bool isDashing = false;
	bool isParry = false;
	private IEnumerator cr;
	bool isAttacking = false;

	

	private states state { get; set; } = states.standing;
	[SerializeField] private states stateCopy;
	
	playerMove pm;
	private void Start()
	{
		if (Player != null && _Player != this)
			Destroy(this);
		else
			_Player = this;
		pm = GetComponent<playerMove>();
		Application.targetFrameRate = 150;
	}

	private void Update()
	{
		stateCopy = state;
		switch (state)
		{
			case states.standing:
				if (Input.GetAxis("Horizontal") != 0)
					state = states.running;
				if (Input.GetAxis("Jump") != 0)
				{
					justJumped = true;
					state = states.jumping;
				}
				if (Input.GetAxis("Fire3") != 0)
					state = states.dashing;
				if (Input.GetAxis("Fire1") != 0)
					state = states.attacking;
				if (pm.isFalling)
					state = states.falling;
				if (Input.GetAxis("Fire2") != 0)
					state = states.parry;
				break;
			case states.running:
				pm.Move();
				pm.animator.SetBool("IsWalking", true);
				if (Input.GetAxis("Horizontal") == 0f)
				{
					pm.animator.SetBool("IsWalking", false);
					state = states.standing;
				}
				if (Input.GetAxis("Jump") != 0)
				{
					justJumped = true;
					pm.animator.SetBool("IsWalking", false);
					state = states.jumping;
				}
				if (Input.GetAxis("Fire3") != 0)
				{
					pm.animator.SetBool("IsWalking", false);
					pm.Stop();
					state = states.dashing;
				}
				if (Input.GetAxis("Fire1") != 0)
					state = states.attacking;
				if (Input.GetAxis("Fire2") != 0f)
				{
					pm.Stop();
					pm.animator.SetBool("IsWalking", false);
					state = states.parry;
				}
				break;

			case states.dashing:
				if (!isDashing)
				{
					StartCoroutine(Dash());
					isDashing = true;
				}
				break;

			case states.jumping:
				if (pm.isGrounded && justJumped)
				{
					pm.Jump();
					justJumped = false;
					pm.animator.SetBool("IsJumping", true);
				}
				if (Input.GetAxis("Horizontal") != 0)
				{
					pm.Move();
				}
				if (GetComponent<Rigidbody2D>().velocity.y < 0)
				{
					pm.animator.SetBool("IsJumping", false);
					pm.animator.SetBool("IsFalling", true);
					state = states.falling;
				}
				else
				if(!wasGrounded && pm.isGrounded)
				{
					pm.animator.SetBool("IsFalling", false);
					pm.animator.SetBool("IsJumping", false);
					state = states.standing;
				}
				if (Input.GetAxis("Fire1") != 0f)
				{
					pm.animator.SetBool("IsFalling", false);
					pm.animator.SetBool("IsJumping", false);
					state = states.attacking;
				}
				break;
			case states.falling:
				if (Input.GetAxis("Horizontal") != 0)
					pm.Move();
				pm.animator.SetBool("IsFalling", true);

				if (Input.GetAxis("Fire1") != 0f)
				{
					state = states.attacking;
					pm.animator.SetBool("IsFalling", false);
					pm.animator.SetBool("IsJumping", false);
				}
				
				if (pm.isGrounded)
				{
					pm.animator.SetBool("IsFalling", false);
					state = states.standing;
				}
				break;
			case states.attacking:
				if (!isAttacking)
				{
					pm.animator.SetBool("IsWalking", false);
					cr = Attack(pm.isGrounded);
					StartCoroutine(cr);
					if (pm.isGrounded) GetComponent<Rigidbody2D>().velocity = Vector3.zero;
					if (!pm.isGrounded)
						pm.Move();
					if (!wasGrounded && pm.isGrounded)
					{
						state = states.standing;
						StopCoroutine(cr);
						pm.animator.SetBool("IsFalling", false);
						pm.animator.SetBool("IsJumping", false);
					}
					else if(wasGrounded && !pm.isGrounded)
					{
						state = states.standing;
					}
				}
				else if (!pm.isGrounded)
				{
					pm.Move();
				}
				break;
			case states.parry:
                if (!isParry)
                {
	                StartCoroutine(Parry());
                    isParry = true;
                }
			break;
		}
		wasGrounded = pm.isGrounded;
	}

	private IEnumerator Dash()
	{
		pm.animator.SetBool("IsDashing", true);
		yield return pm.StartCoroutine(pm.Dash());
		state = states.standing;
		isDashing = false;
		pm.animator.SetBool("IsDashing", false);
		yield return null;
	}

	private IEnumerator Parry()
	{
		yield return pm.Parry();
		state = states.standing;
		isParry = false;
		yield return null;
	}
	private IEnumerator Attack(bool onGround)
	{
		isAttacking = true;
		pm.animator.SetBool("IsAttacking", true);
		var pa = GetComponent<playerAttack>();
		if (Input.GetAxis("Vertical") > 0)
		{
			pm.animator.SetBool("HoldingUp", true);
			pa.attack(new(pm.Facing(), 1));
			Debug.Log("Attacked up!");
		}
		else
		{
			pa.attack(new(pm.Facing(), 0));
		}
		yield return new WaitForSeconds(stats.attackSpeed);
		pm.animator.SetBool("IsAttacking", false);
		pm.animator.SetBool("HoldingUp", false);
		isAttacking = false;
		state = pm.isGrounded ? states.standing : states.falling; 
		yield return null;
	}

}
