using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour 
{
	private enum State
	{
		Closed, Closing, Opening, Opened
	}

	public Rigidbody2D door;
	public float height;
	public float moveSpeed;

	private State state;
	private float y;

	void Start () 
	{
		state = State.Closed;
		y = door.position.y;
	}

	void Update ()
	{
		if(state == State.Closing)
		{
			door.velocity = Vector2.down * moveSpeed;
			
			float current = door.position.y;
			if(current <= y)
			{
				door.velocity = Vector2.zero;
				state = State.Closed;
			}
		}
		else if(state == State.Opening)
		{
			door.velocity = Vector2.up * moveSpeed;
			
			float current = door.position.y;
			if(current >= y + height)
			{
				door.velocity = Vector2.zero;
				state = State.Opened;
			}
		}
	}

	void FlipState()
	{
		if (state == State.Closed || state == State.Closing)
			state = State.Opening;
		else
			state = State.Closing;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.tag == "Door Trigger")
			FlipState ();
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if (c.tag == "Door Trigger")
			FlipState ();
	}
}
