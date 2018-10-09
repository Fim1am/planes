using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{

    [SerializeField]
    private Joystick joystick;
	
	public Joystick GetJoystick()
    {
        joystick.gameObject.SetActive(true);

        return joystick;
    }
}
