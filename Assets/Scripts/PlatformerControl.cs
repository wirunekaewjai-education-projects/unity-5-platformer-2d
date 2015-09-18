using UnityEngine;
using System.Collections;

public class PlatformerControl : MonoBehaviour 
{
	public Animator animator;
	public Rigidbody2D rigidbody;
	public Collider2D groundCollider;

	public float moveSpeed = 3;
	public float jumpPower = 5;
	
	public bool moveOnJump = true;
	
	public bool IsClimbing 
	{
		get;
		set;
	}
	
	public bool IsFalling
	{
		get { return rigidbody.velocity.y < 0; }
	}
	
	public bool IsGrounded
	{
		get { return rigidbody.velocity.y == 0; }
	}


	// Update is called once per frame
	void Update () 
	{
		if(IsGrounded || moveOnJump)
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

		if(IsClimbing && Input.GetButton ("Vertical"))
		{
			OnClimbing();
		}

		if(IsGrounded && Input.GetKeyDown (KeyCode.Space))
		{
			OnJumping();
		}

		if(IsFalling)
		{
			OnFalling();
		}
	}

	void OnIdle()
	{
		animator.SetBool ("Running", false);
		
		Vector3 velocity = rigidbody.velocity;
		velocity.x = 0;
		
		rigidbody.velocity = velocity;
	}

	void OnRunning()
	{
		float h = Input.GetAxisRaw ("Horizontal");

		animator.SetBool ("Running", h != 0);

		Vector3 velocity = rigidbody.velocity;
		velocity.x = moveSpeed * h;

		rigidbody.velocity = velocity;
	}

	void OnFacing()
	{
		float h = Input.GetAxisRaw ("Horizontal");

		if (h == 0)
			return;

		Vector3 scale = transform.localScale;
		scale.x = Mathf.Abs (scale.x) * h;

		transform.localScale = scale;
	}

	
	void OnJumping()
	{
		Vector3 velocity = rigidbody.velocity;
		velocity.y = jumpPower;
		
		rigidbody.velocity = velocity;
		
		groundCollider.enabled = false;
	}
	
	void OnFalling()
	{
		groundCollider.enabled = true;
	}
	
	void OnClimbing()
	{
		float vertical = Input.GetAxis("Vertical");
		Vector3 velocity = rigidbody.velocity;
		
		velocity.y = moveSpeed * vertical;
		rigidbody.velocity = velocity;
	}
	
	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Ladder")
		{
			Vector3 velocity = rigidbody.velocity;
			velocity.y = 0;
			
			rigidbody.velocity = velocity;
		}
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if(c.tag == "Ladder")
		{
			IsClimbing = true;
			rigidbody.gravityScale = 0;
			
			Vector3 velocity = rigidbody.velocity;
			
			if(velocity.y < 0)
				velocity.y = 0;
			
			rigidbody.velocity = velocity;
		}
	}
	
	void OnTriggerExit2D(Collider2D c)
	{
		if(c.tag == "Ladder")
		{
			IsClimbing = false;
			rigidbody.gravityScale = 1;
		}
	}
}
