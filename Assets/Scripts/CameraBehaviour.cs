using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	private Transform target, selfTransform;

	private float yOffset = 5f;
	
	void Start ()
	{
		selfTransform = transform;
	}
	
	
	void LateUpdate () 
	{
		if (target != null)
		{
			selfTransform.position = target.position + (Vector3.up * yOffset);
		}
		else
		{
			target = GameObject.FindWithTag("Player").transform;
		}
	}
}
