using UnityEngine;
using System.Collections;

public class BotControl : MonoBehaviour 
{
	private Animator animator;
	private Rigidbody2D rigidbody;

	private bool running;

	public float moveSpeed = 3;
	public float left, right;

	void Awake ()
	{
		animator = GetComponent<Animator> ();
		rigidbody = GetComponent<Rigidbody2D> ();

		running = true;
		animator.SetBool ("Running", true);
	}

	void Update () 
	{
		if (!running)
			return;

		float current = transform.position.x;
		float face = Mathf.Sign(transform.localScale.x);
		if(face > 0)
		{
			rigidbody.velocity = Vector2.right * moveSpeed;

			if(current >= right)
			{
				running = false;
				rigidbody.velocity = Vector3.zero;
				animator.SetBool ("Running", false);

				Invoke("OnFacing", 2);
			}
		}
		else
		{
			rigidbody.velocity = Vector2.left * moveSpeed;
			
			if(current <= left)
			{
				running = false;
				rigidbody.velocity = Vector3.zero;
				animator.SetBool ("Running", false);

				Invoke("OnFacing", 2);
			}
		}

	}

	void OnFacing()
	{
		Vector3 scale = transform.localScale;
		scale.x *= -1f;
		
		transform.localScale = scale;

		running = true;
		animator.SetBool ("Running", true);
	}
}
