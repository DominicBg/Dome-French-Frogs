using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputTouchManager : MonoBehaviour
{

    public Button ActionButton;


    // Use this for initialization
    void Start()
    {
        ActionButton.onClick.AddListener(PressActionButton);
    }



    public void PressActionButton()
    {
        if (DomeNetworkManager.GetNetworkClient() != null)
            DomeNetworkManager.GetNetworkClient().CmdPressActionButton();
    }
}
