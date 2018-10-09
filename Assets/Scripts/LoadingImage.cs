using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingImage : MonoBehaviour
{
	private Transform selfTransform;

	private float rotationSpeed = 35f;
	
	void Start ()
	{
		selfTransform = transform;
	}
	
	
	void FixedUpdate () 
	{
		transform.Rotate(Vector3.back * rotationSpeed * Time.fixedDeltaTime);
	}
}
