using UnityEngine;
using System.Collections;

public class HitListener : MonoBehaviour 
{
	public GameObject callback;

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.layer == LayerMask.NameToLayer("Jump Through"))
		{
			callback.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
			c.gameObject.SendMessageUpwards("OnJumping", SendMessageOptions.DontRequireReceiver);

			gameObject.SetActive(false);
		}
	}
}
