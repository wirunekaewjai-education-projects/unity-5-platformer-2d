using UnityEngine;

public class Door : Tween 
{
	void Reset ()
	{
		autoPlay = false;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (!c.CompareTag ("Player"))
			return;
		
		FlipDoor ();
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if (!c.CompareTag ("Player"))
			return;
		
		FlipDoor ();
	}

	void FlipDoor()
	{
		MoveTo ((currentIndex == 0) ? 1 : 0);
	}
}
