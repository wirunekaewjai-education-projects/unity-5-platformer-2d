using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour 
{

	void OnTriggerExit2D(Collider2D c)
	{
		int level = Application.loadedLevel;
		Application.LoadLevel (level);
	}
}
