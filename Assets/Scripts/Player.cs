using UnityEngine;
using System.Collections;

public class Player : PlatformerController 
{
	protected override void OnGroundStay ()
	{
		base.OnGroundStay ();
		Move (Input.GetAxisRaw ("Horizontal"));

		if(Input.GetKeyDown (KeyCode.Space))
		{
			Jump();
		}
	}

	protected override void OnAirStay ()
	{
		base.OnAirStay ();
		Move (Input.GetAxisRaw ("Horizontal"));
	}

	protected override void OnLadderStay ()
	{
		base.OnLadderStay ();

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);

		if(Input.GetKeyDown (KeyCode.Space))
		{
			Jump();
		}
	}

	protected override void OnCollisionEnter2D (Collision2D c)
	{
		base.OnCollisionEnter2D (c);

		if(!c.gameObject.CompareTag("Bot"))
			return;

		Vector3 direction = c.relativeVelocity.normalized;
		float angle = Vector3.Angle (Vector3.down, direction);
		
		if (angle < 45f)
		{
			Jump();
		}
		else
		{
			Die ();

			Invoke("RestartLevel", 3);
		}
	}

	void RestartLevel()
	{
		int level = Application.loadedLevel;
		Application.LoadLevel (level);
	}
}
