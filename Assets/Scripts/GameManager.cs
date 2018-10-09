using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [SerializeField]
    private GameObject gameCanvas, menuCanvas;

    [SerializeField]
    private Plane plane_Prefab;

    private List<Plane> activePlanes = new List<Plane>();

	private void Awake ()
    {
        if(Instance == null)
            Instance = this;
	}

    public void SpawnPlane(int _planeId, bool _isLocal, Vector3 _position, Vector3 _direction, string _displayName, int _skinIndex)
    {

        Plane plane = Instantiate(plane_Prefab);

        if(_isLocal)
        {
            gameCanvas.SetActive(true);

            menuCanvas.SetActive(false);
        }

        plane.InitPlane(_planeId, _isLocal,_displayName, _skinIndex, _position, _direction);

        activePlanes.Add(plane);
    }

    public void UpdatePlane(int _id, Vector3 _pos, Vector3 _forward)
    {
        Plane plane = activePlanes.Find(p => p.PlaneId == _id);

        if (plane != null)
        {
            plane.SetTransform(_forward, _pos);
        }else
        {
            Debug.Log("dont have a plane with id " + _id);
        }
    }
	
}
