using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour 
{

	public enum State
	{
		AtStart, AtEnd, ToStart, ToEnd
	}

	public float moveSpeed;
	public float delay = 2;

	public Vector3 end;
	
	private State state;
	private Vector3 start;
	
	void Start () 
	{
		state = State.ToEnd;
		start = transform.position;
	}
	
	void Update ()
	{
		if(state == State.ToEnd)
		{
			Vector3 current = transform.position;
			Vector3 direction = (end - current).normalized;
			float distance = Vector3.Distance(current, end);

			transform.Translate(direction * moveSpeed * Time.deltaTime);

			if(distance <= 0.1f)
			{
				transform.localPosition = end;
				state = State.AtEnd;

				Invoke ("FlipState", delay);
			}
		}
		else if(state == State.ToStart)
		{
			Vector3 current = transform.position;
			Vector3 direction = (start - current).normalized;
			float distance = Vector3.Distance(current, start);

			transform.Translate(direction * moveSpeed * Time.deltaTime);
			
			if(distance <= 0.1f)
			{
				transform.localPosition = start;
				state = State.AtStart;
				
				Invoke ("FlipState", delay);
			}
		}

	}
	
	void FlipState()
	{
		if (state == State.AtStart || state == State.ToStart)
			state = State.ToEnd;
		else
			state = State.ToStart;
	}
}
