using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D c)
	{
		if (!enabled)
			return;

		Vector3 position = transform.position;

		PlayerPrefs.SetInt("Saved", 1);
		PlayerPrefs.SetInt("LevelIndex", Application.loadedLevel);

		PlayerPrefs.SetFloat("CheckpointX", position.x);
		PlayerPrefs.SetFloat("CheckpointY", position.y);

		print("Saved");

		enabled = false;
	}
}
