using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
