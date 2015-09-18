using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	private Camera camera;
	private BoxCollider2D collider2D;
	private Rigidbody2D rigidbody2D;

	public Transform target;
	public float speed;

	// Use this for initialization
	void Start () 
	{
		camera = GetComponent<Camera> ();
		collider2D = GetComponent<BoxCollider2D> ();
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateBound ();
		UpdatePosition ();
	}

	void UpdatePosition()
	{
		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = target.position;

		Vector3 direction = targetPosition - currentPosition;
		direction.z = 0;

		rigidbody2D.velocity = direction * speed;
	}

	void UpdateBound()
	{
		int width = Screen.width;
		int height = Screen.height;
		
		float aspect = (float) width / height;
		float size = camera.orthographicSize;
		
		float boundWidth = size * aspect;
		Vector2 boundSize = collider2D.size;
		boundSize.x = boundWidth * 2f;
		
		collider2D.size = boundSize;
	}
}
