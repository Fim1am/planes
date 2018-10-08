using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject loading_Panel, menu_Panel;

    [SerializeField]
    private InputField displayName_InputField;

	// Use this for initialization
	void Start ()
    {
		
	}

    public void ShowMenuPanel()
    {
        loading_Panel.SetActive(false);

        menu_Panel.SetActive(true);
    }

    public void StartGameButton()
    {
        NetworkData.StartGameRequest(displayName_InputField.text == string.Empty ? "Player" : displayName_InputField.text, 5);
    }
	
}
