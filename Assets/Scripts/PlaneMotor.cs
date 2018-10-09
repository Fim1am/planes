using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMotor : MonoBehaviour
{
    private float turningSpeed = 5f;

    public Vector3 TargetDirection { get; set;}
    public Vector3 TargetPosition { get; set; }

    private Transform selfTransform;
	
	void Start ()
    {
        selfTransform = transform;
	}
	
	
	private void FixedUpdate ()
    {
        if(Vector3.Angle(selfTransform.forward, TargetDirection ) > 1)
            selfTransform.forward = Vector3.Lerp(selfTransform.forward, TargetDirection, turningSpeed * Time.fixedDeltaTime);
        

        if(Vector3.Distance(selfTransform.position, TargetPosition) > .1f)
            selfTransform.position = Vector3.Lerp(selfTransform.position, TargetPosition, Time.fixedDeltaTime);
    }
}
