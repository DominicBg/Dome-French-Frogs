using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkObjectManager : NetworkBehaviour
{

    [SyncVar]
    public uint ID;

    [SyncVar]
    public string CustomizationString;

    public GameObject Player = null;

    public void Start()
    {
        ID = netId.Value;

        JoystickScript.OnChangeJoystick.AddListener(

            (Vector2 JoystickInput) =>
            {
                if (isLocalPlayer)
                    CmdSendVectorData(JoystickInput);
            }

            );

    }

    public void FixedUpdate()
    {

    }


    [Command]
    public void CmdSendVectorData(Vector3 _data)
    {

    }

    [Command]
    public void CmdSendStringData(string _data)
    {

    }

    [Command]
    public void CmdPressActionButtonLeft()
    {
    }

    [Command]
    public void CmdPressActionButtonTop()
    {

    }


}
