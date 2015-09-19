using UnityEngine;
using System.Collections;

public class Bot : PlatformerController 
{
	protected override void OnCollisionEnter2D (Collision2D c)
	{
		base.OnCollisionEnter2D (c);

		if (!c.gameObject.CompareTag ("Player"))
			return;

		Vector3 direction = c.relativeVelocity.normalized;
		float angle = Vector3.Angle (Vector3.up, direction);

		if (angle > 45f)
			return;

		Tween tween = GetComponent<Tween> ();
		tween.enabled = false;

		Die ();
	}
}
