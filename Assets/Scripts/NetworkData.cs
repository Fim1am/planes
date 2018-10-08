using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.RT;

public static class NetworkData
{
    public enum ToServerOpCodes
    {
        STARTGAME_REQUEST = 200
    }

    public enum FromServerOpCodes
    {
        STARTGAME = 100,
        SHOWMENU = 101
    }

    public static void StartGameRequest(string _displayName, int _skinIndex = 1)
    {
        RTData rtData = new RTData();

        rtData.SetString(1, _displayName);
        rtData.SetInt(2, _skinIndex);

        RTSessionManager.SendPacket((int)ToServerOpCodes.STARTGAME_REQUEST, GameSparksRT.DeliveryIntent.RELIABLE, rtData, new[] { 0 });
    }
}
