using UnityEngine;
using System.Collections;

public class PlatformerControl : MonoBehaviour 
{
	private Animator animator;
	private Rigidbody2D rigidbody;

	public Collider2D groundCollider;

	public float moveSpeed = 3;
	public float jumpPower = 5;
	
	public bool moveOnJump = true;
	
	public bool IsDied 
	{
		get;
		set;
	}

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

	void Awake ()
	{
		animator = GetComponent<Animator> ();
		rigidbody = GetComponent<Rigidbody2D> ();
	}


	// Update is called once per frame
	void Update () 
	{
		if(IsFalling)
		{
			OnFalling();
		}

		if (IsDied)
			return;

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

	void OnHit()
	{
		IsDied = true;
		animator.SetBool ("Died", true);
		
		rigidbody.gravityScale = 0;
		rigidbody.Sleep ();
		
		Collider2D[] cs = GetComponents<Collider2D> ();
		foreach(Collider2D c in cs)
		{
			c.enabled = false;
		}

		Invoke ("OnDied", 3);
	}

	void OnDied()
	{
		Restarter.Restart ();
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

	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Bot")
		{
			OnHit();
		}
	}
}
