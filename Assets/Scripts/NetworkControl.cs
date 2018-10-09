using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.RT;

public static class NetworkControl
{
    public enum ToServerOpCodes
    {
        STARTGAME_REQUEST = 200,
        SEND_PLANE_DIR = 201
    }

    public enum FromServerOpCodes
    {
        STARTGAME = 100,
        SHOWMENU = 101,
        SERVER_UPDATE = 103
    }

    public static void StartGameRequest(string _displayName, int _skinIndex = 1)
    {
        RTData rtData = new RTData();

        rtData.SetString(1, _displayName);
        rtData.SetInt(2, _skinIndex);

        RTSessionManager.SendPacket((int)ToServerOpCodes.STARTGAME_REQUEST, GameSparksRT.DeliveryIntent.RELIABLE, rtData, new[] { 0 });
    }

    public static void SendPlaneDirection(Vector3 _direction)
    {
        RTData rtData = new RTData();

        rtData.SetFloat(1, _direction.x);
        rtData.SetFloat(2, _direction.z);

        RTSessionManager.SendPacket((int)ToServerOpCodes.SEND_PLANE_DIR, GameSparksRT.DeliveryIntent.UNRELIABLE, rtData, new[] { 0 });
    }
}
