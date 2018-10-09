using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{   
    public RealtimePlayer RTPlayer {get; private set;}

    public int SkinIndex { get; private set; }

    public int PlaneId { get; private set; }

    private PlaneMotor motor;

	private void OnEnable ()
    {
        motor = GetComponent<PlaneMotor>(); //TODO
	}

    public void InitPlane(int _planeId, bool _isLocal,string _displayName, int _skinIndex, Vector3 _position, Vector3 _direction)
    {
        if(_isLocal)
        {
            gameObject.AddComponent<PlaneController>();
        }

        RTPlayer = new RealtimePlayer(_displayName, _isLocal);
        PlaneId = _planeId;
        SkinIndex = _skinIndex;
        SetTransform(_direction, _position);
    }

    public void SetTransform(Vector3 _forward, Vector3 _position)
    {
        if(motor == null)
            motor = GetComponent<PlaneMotor>();

        motor.TargetPosition = _position;
         motor.TargetDirection = _forward;
    }

}
