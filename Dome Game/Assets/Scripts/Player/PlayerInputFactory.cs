using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EInputType
{
    GAMECONTROLLER = 0,
    NETWORK = 1
}

public static class PlayerInputFactory
{

    public static PlayerInput GetInput(EInputType inputType, Player player)
    {
        switch (inputType)
        {
            case EInputType.GAMECONTROLLER:
                return new PlayerGameControllerInput(player);
            case EInputType.NETWORK:
                return new PlayerNetworkInput(player);
            default:
                return new PlayerGameControllerInput(player);
        }
    }

}
