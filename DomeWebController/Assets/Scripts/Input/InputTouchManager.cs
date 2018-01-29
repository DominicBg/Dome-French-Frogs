using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputTouchManager : MonoBehaviour {

    public Button ActionButton;


	// Use this for initialization
	void Start () {

        ActionButton.onClick.AddListener(PressActionButton);


    }
	


    public void PressActionButton()
    {
        NetworkObjectManager[] NetworkObjectsArray = GameObject.FindObjectsOfType<NetworkObjectManager>() as NetworkObjectManager[];

        for (int i = 0; i < NetworkObjectsArray.Length; i++)
        {
            if(NetworkObjectsArray[i].isLocalPlayer)
            {
                Debug.Log("sent");
                NetworkObjectsArray[i].CmdPressActionButton();
            }
        }
    }
}
