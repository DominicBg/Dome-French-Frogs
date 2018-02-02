using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputTouchManager : MonoBehaviour
{

    public Button ActionButtonLeft,ActionButtonTop;
    public JoystickScript JoyStick;


    


    // Use this for initialization
    void Start()
    {
        ActionButtonLeft.onClick.AddListener(PressActionButtonLeft);
        ActionButtonTop.onClick.AddListener(PressActionButtonTop);
    }



    public void PressActionButtonLeft()
    {
        if (DomeNetworkManager.GetNetworkClient() != null)
            DomeNetworkManager.GetNetworkClient().CmdPressActionButtonLeft();
    }

    public void PressActionButtonTop()
    {
        if (DomeNetworkManager.GetNetworkClient() != null)
            DomeNetworkManager.GetNetworkClient().CmdPressActionButtonTop();
    }
}
