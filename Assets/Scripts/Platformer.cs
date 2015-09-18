using UnityEngine;
using System.Collections;

public class Platformer : MonoBehaviour
{
	public Animator animator;
	public Rigidbody2D rigidbody2D;
	public Collider2D groundCollider;

	public float moveSpeed = 3;
	public float jumpPower = 5;

	public bool canMoveOnJump = true;

	void Update ()
	{
		if(IsGrounded && Input.GetKeyDown (KeyCode.Space))
		{
			OnJumping();
		}

		if(IsFalling)
		{
			OnFalling();
		}

		if(IsClimbing && Input.GetButton ("Vertical"))
		{
			OnClimbing();
		}

		// Facing
		if(canMoveOnJump || IsGrounded)
		{
			if(Input.GetButton ("Horizontal"))
			{
				OnFacing();
				OnRunning();
			}
			else
			{
				OnIdle();
			}
		}
	}

	bool IsClimbing 
	{
		get;
		set;
	}

	bool IsFalling
	{
		get { return rigidbody2D.velocity.y < 0; }
	}

	bool IsGrounded
	{
		get { return rigidbody2D.velocity.y == 0; }
	}

	void OnIdle()
	{
		animator.SetBool ("Running", false);

		Vector3 velocity = rigidbody2D.velocity;
		velocity.x = 0;

		rigidbody2D.velocity = velocity;
	}

	void OnClimbing()
	{
		float vertical = Input.GetAxis("Vertical");
		Vector3 velocity = rigidbody2D.velocity;
		
		velocity.y = moveSpeed * vertical;
		rigidbody2D.velocity = velocity;
	}

	void OnRunning()
	{
		animator.SetBool ("Running", true);

		float horizontal = Input.GetAxis("Horizontal");
		Vector3 velocity = rigidbody2D.velocity;
		
		velocity.x = moveSpeed * horizontal;
		rigidbody2D.velocity = velocity;
	}
	
	void OnFacing()
	{
		// H = -1.0....0.0....1.0
		float horizontal = Input.GetAxis("Horizontal");
		
		// Sign = -1 or 1 only
		float sign = Mathf.Sign(horizontal);
		
		Vector3 scale = transform.localScale;
		scale.x = Mathf.Abs(scale.x) * sign;
		
		transform.localScale = scale;
	}

	void OnJumping()
	{
		Vector3 velocity = rigidbody2D.velocity;
		velocity.y = jumpPower;
		
		rigidbody2D.velocity = velocity;

		groundCollider.enabled = false;
	}

	void OnFalling()
	{
		groundCollider.enabled = true;
	}


	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Ladder")
		{
			Vector3 velocity = rigidbody2D.velocity;
			velocity.y = 0;
			
			rigidbody2D.velocity = velocity;
		}
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if(c.tag == "Ladder")
		{
			IsClimbing = true;
			rigidbody2D.gravityScale = 0;

			Vector3 velocity = rigidbody2D.velocity;

			if(velocity.y < 0)
				velocity.y = 0;
			
			rigidbody2D.velocity = velocity;
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if(c.tag == "Ladder")
		{
			IsClimbing = false;
			rigidbody2D.gravityScale = 1;
		}
	}
}
