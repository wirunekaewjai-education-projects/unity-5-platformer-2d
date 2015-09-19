using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public Transform target;

	private Vector3 offset;

	void Start () 
	{
		offset = transform.position;
	}

	void Update ()
	{
		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = target.position;

		Vector3 lerp = Vector3.Lerp (currentPosition, targetPosition, 0.1f);
		transform.position = lerp + offset;
	}
}
