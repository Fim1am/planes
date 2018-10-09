using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{   
    public RealtimePlayer RTPlayer {get; private set;}

    public int SkinIndex { get; private set; }

    public int PlaneId { get; private set; }

	void Start ()
    {
		
	}

    public void InitPlane(int _planeId, bool _isLocal,string _displayName, int _skinIndex, Vector3 _position, Vector3 _direction)
    {
        Debug.Log(_displayName);
        RTPlayer = new RealtimePlayer(_displayName, _isLocal);
        PlaneId = _planeId;
        SkinIndex = _skinIndex;
        SetTransform(_direction, _position);
    }

    public void SetTransform(Vector3 _forward, Vector3 _position)
    {
        transform.position = _position;
        transform.forward = _forward;
    }

}
