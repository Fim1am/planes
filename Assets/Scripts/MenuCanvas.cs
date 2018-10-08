using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject loading_Panel, menu_Panel;

	// Use this for initialization
	void Start ()
    {
		
	}

    public void ShowMenuPanel()
    {
        loading_Panel.SetActive(false);

        menu_Panel.SetActive(true);
    }
	
}
