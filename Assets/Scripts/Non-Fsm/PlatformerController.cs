using UnityEngine;
using System;

public class PlatformerController : MonoBehaviour 
{
	private Animator _animator;
	private Rigidbody2D _rigidbody;

	private Action _state;
	private float _gravityScale;
	
	public float moveSpeed = 3;
	public float jumpPower = 5;

	protected virtual void Awake ()
	{
		_animator = GetComponent<Animator> ();
		_rigidbody = GetComponent<Rigidbody2D> ();

		_state = OnAirStay;
	}

	protected virtual void Update ()
	{
		_state ();

		UpdateAnimation ();
		UpdateFace ();
	}

	// Ground State
	protected virtual void OnGroundEnter ()
	{
		_state = OnGroundStay;
	}

	protected virtual void OnGroundStay ()
	{

	}
	
	protected virtual void OnGroundExit ()
	{
		_state = OnAirEnter;
	}
	
	// Air State
	protected virtual void OnAirEnter ()
	{
		_state = OnAirStay;
	}

	protected virtual void OnAirStay ()
	{

	}
	
	protected virtual void OnAirExit ()
	{
		_state = OnGroundEnter;
	}

	// Ladder State
	protected virtual void OnLadderEnter ()
	{
		_gravityScale = _rigidbody.gravityScale;
		_rigidbody.gravityScale = 0;

		_state = OnLadderStay;
	}
	
	protected virtual void OnLadderStay ()
	{

	}
	
	protected virtual void OnLadderExit ()
	{
		_rigidbody.gravityScale = _gravityScale;
		_gravityScale = 0;

		_state = OnAirEnter;
	}
	
	// Flop | Die State
	protected virtual void OnFlopEnter()
	{
		_animator.SetTrigger ("Flopping");
		
		Collider2D c1 = GetComponent<BoxCollider2D>();
		Collider2D c2 = GetComponent<PolygonCollider2D>();
		
		c1.enabled = false;
		c2.enabled = true;

		_state = OnFlopStay;
	}

	protected virtual void OnFlopStay()
	{
		if (_rigidbody.velocity.y != 0)
			return;

		_rigidbody.gravityScale = 0;
		_rigidbody.Sleep ();

		_state = OnFlopExit;

		Collider2D c1 = GetComponent<BoxCollider2D>();
		Collider2D c2 = GetComponent<PolygonCollider2D>();
		
		c1.enabled = false;
		c2.enabled = false;
	}

	protected virtual void OnFlopExit()
	{
		_state = OnDied;
	}

	protected virtual void OnDied()
	{

	}
	
	// Collision State
	protected virtual void OnCollisionEnter2D(Collision2D c)
	{
		if (_state == OnDied)
			return;
	}

	protected virtual void OnCollisionStay2D(Collision2D c)
	{
		if (_state == OnDied)
			return;

		GameObject obj = c.gameObject;

		if(obj.CompareTag("Platform") && _state == OnAirStay)
		{
			float vy = _rigidbody.velocity.y;
					
			if(Mathf.Abs(vy) < Mathf.Epsilon)
				_state = OnAirExit;
		}
	}

	protected virtual void OnCollisionExit2D(Collision2D c)
	{
		if (_state == OnDied)
			return;

		GameObject obj = c.gameObject;

		if(obj.CompareTag("Platform") && _state == OnGroundStay)
		{
			_state = OnGroundExit;
		}
	}
	
	// Trigger State
	protected virtual void OnTriggerEnter2D(Collider2D c)
	{
		if (_state == OnDied)
			return;

		if(c.gameObject.CompareTag("Ladder"))
		{
			_state = OnLadderEnter;
		}
	}

	protected virtual void OnTriggerStay2D(Collider2D c)
	{
		if (_state == OnDied)
			return;
	}

	protected virtual void OnTriggerExit2D(Collider2D c)
	{
		if (_state == OnDied)
			return;

		if(c.gameObject.CompareTag("Ladder"))
		{
			_state = OnLadderExit;
		}
	}

	public void Move(float horizontal)
	{
		// Calculate Move Velocity
		Vector3 velocity = _rigidbody.velocity;
		velocity.x = moveSpeed * horizontal;
		
		// Apply Move Velocity
		_rigidbody.velocity = velocity;
	}

	public void Move(float horizontal, float vertical)
	{
		// Calculate Move Velocity
		Vector3 velocity = _rigidbody.velocity;
		velocity.x = moveSpeed * horizontal;
		velocity.y = moveSpeed * vertical;
		
		// Apply Move Velocity
		_rigidbody.velocity = velocity;
	}

	public void Jump()
	{
		// Calculate Jump Velocity
		Vector2 velocity = _rigidbody.velocity;
		velocity.y = jumpPower;
		
		// Apply Jump Velocity
		_rigidbody.velocity = velocity;
	}

	public void Die()
	{
		_state = OnFlopEnter;
	}


	private void UpdateAnimation()
	{
		float vx = _rigidbody.velocity.x;

		// Apply Running Animation | Idle
		_animator.SetBool ("Running", vx != 0);
	}

	private void UpdateFace()
	{
		if (_state == OnFlopEnter || _state == OnFlopStay)
			return;

		float vx = _rigidbody.velocity.x;

		// Moving ?
		if (vx == 0)
			return;
		
		// Calculate Face Direction
		Vector3 scale = transform.localScale;
		scale.x = Mathf.Abs (scale.x) * Math.Sign (vx);
		
		// Flip Face Direction
		transform.localScale = scale;
	}
}
