using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
	
	//public Vector3 AngularVelocity 
	//{
	//	get 
	//	{
	//		return (axis * magnitude) / Time.deltaTime;
	//	}
	//}
	
	Transform selfTransform;

    private float turningSpeed = 5f;

    private Vector3 movementDir;

	//private float speed = 2f;


	//private float angularTransition = 3f, inclineTransition = 5f;

	//private float maxSideRotation = 50f, inclineOffset = 2.5f;
	
	
	//Quaternion lastRotation;

	//float magnitude;
	//Vector3 axis;
 
	private Joystick InputJoystick { get; set; }
	
	void Start ()
	{
        selfTransform = transform;

        //lastRotation = selfTransform.rotation;

        InputJoystick = FindObjectOfType<GameCanvas>().GetJoystick();
    }
	
	
	void Update ()
	{
		if (InputJoystick.Direction.magnitude > 0.01f)
		{
			movementDir = new Vector3(InputJoystick.Direction.x, 0, InputJoystick.Direction.y);

            NetworkControl.SendPlaneDirection(movementDir);
		}

        selfTransform.forward = Vector3.Lerp(selfTransform.forward, movementDir, turningSpeed * Time.fixedDeltaTime);

        selfTransform.position = Vector3.Lerp(selfTransform.position, selfTransform.position + selfTransform.forward, Time.fixedDeltaTime);

        //Quaternion deltaRotation = transform.rotation * Quaternion.Inverse (lastRotation);
        //deltaRotation.ToAngleAxis(out magnitude, out axis);

        //lastRotation = transform.rotation;

        //float angVel = -AngularVelocity.y;

        //selfTransform.position += selfTransform.forward * speed * Time.deltaTime;

        //selfTransform.rotation = Quaternion.Lerp(selfTransform.rotation, Quaternion.LookRotation(movementDir, selfTransform.up), angularTransition * Time.fixedDeltaTime);
        //selfTransform.rotation = Quaternion.Lerp(selfTransform.rotation, Quaternion.Euler(new Vector3(selfTransform.rotation.eulerAngles.x, selfTransform.rotation.eulerAngles.y, Mathf.Clamp(angVel * inclineOffset, - maxSideRotation, maxSideRotation))), inclineTransition * Time.fixedDeltaTime);

        //selfTransform.position = new Vector3(selfTransform.position.x, 0, selfTransform.position.z);		
    }
	
}
