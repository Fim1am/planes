using GameSparks.Api.Responses;
using GameSparks.Core;
using GameSparks.RT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSessionManager : MonoBehaviour
{
    private RealtimeSessionInfo sessionInfo;
    public RealtimeSessionInfo SessionInfo
    {
        get
        {
            return this.sessionInfo;
        }
    }

    private static GameSparksRTUnity gamesparksRTUnity;

    public void JoinOrCreateRTSession(RealtimeSessionInfo _info)
    {
        sessionInfo = _info;    
        gamesparksRTUnity = gameObject.AddComponent<GameSparksRTUnity>();

        GSRequestData mockedResponse = new GSRequestData()
        .AddNumber("port", (double)_info.GetPortID())
        .AddString("host", _info.GetHostURL())
        .AddString("accessToken", _info.GetAccessToken());

        FindMatchResponse response = new FindMatchResponse(mockedResponse);

        gamesparksRTUnity.Configure(response,
            (peerId) => { OnPlayerConnectedToGame(peerId); },
            (peerId) => { OnPlayerDisconnected(peerId); },
            (ready) => { OnRTReady(ready); },
            (packet) => { OnPacketReceived(packet); });


        gamesparksRTUnity.Connect();

    }

    public void RefreshSessionInfo(RealtimeSessionInfo _info)
    {
        sessionInfo = _info;
    }

    private void OnPlayerConnectedToGame(int _peerId)
    {
        
    }

    private void OnPlayerDisconnected(int _peerId)
    {
        
    }

    private void OnRTReady(bool _isReady)
    {
        if (_isReady)
        {
            Debug.Log("GSM| RT Session Connected...");
        }
    }

    private void OnPacketReceived(RTPacket _packet)
    {

        switch (_packet.OpCode)
        {
            case (int)NetworkData.FromServerOpCodes.SHOWMENU:

                Debug.Log("connected to rt");
                FindObjectOfType<MenuCanvas>().ShowMenuPanel();

                break;

            case (int)NetworkData.FromServerOpCodes.STARTGAME:

                RTData planePosData = _packet.Data.GetData(1);

                Vector3 planePos = new Vector3((float)planePosData.GetFloat(1), 0, (float)planePosData.GetFloat(2));

                RTData planeDirData = _packet.Data.GetData(2);

                Vector3 planeDir = new Vector3((float)planeDirData.GetFloat(1), 0, (float)planeDirData.GetFloat(2));

                string displayName = _packet.Data.GetString(3);
                int skinIndex = (int)_packet.Data.GetInt(4);
                int planeId = (int)_packet.Data.GetInt(5);

                GameManager.Instance.SpawnPlane(planeId, true, planePos, planeDir, displayName, skinIndex);

                break;

            case (int)NetworkData.FromServerOpCodes.SERVER_UPDATE:

                int planesCount = (int)_packet.Data.GetInt(1);

                for(uint i = 2; i < planesCount + 2; i++)
                {
                    RTData planeData = _packet.Data.GetData(i);

                    int id = (int)planeData.GetInt(1);

                    RTData posData = planeData.GetData(2);

                    Vector3 pos = new Vector3((float)posData.GetFloat(1), 0, (float)posData.GetFloat(2));

                    RTData dirData = planeData.GetData(3);

                    Vector3 forward = new Vector3((float)dirData.GetFloat(1), 0, (float)dirData.GetFloat(2));

                    GameManager.Instance.UpdatePlane(id, pos, forward);
                    
                }

                break;

        }
    }

    public static void SendPacket(int _opCode, GameSparksRT.DeliveryIntent _intent, RTData _data, int[] _targets)
    {
        gamesparksRTUnity.SendData(_opCode, _intent, _data, _targets);
    }

}

public class RealtimeSessionInfo
{
    private string hostURL;
    private string acccessToken;
    private int portID;
    private string matchID;

    public RealtimeSessionInfo(GameSparks.Api.Messages.MatchFoundMessage _message)
    {
        hostURL = _message.Host;
        acccessToken = _message.AccessToken;
        portID = (int)_message.Port;
        matchID = _message.MatchId;
    }


    public string GetMatchID()
    {
        return this.matchID;
    }

    public int GetPortID()
    {
        return this.portID;
    }

    public string GetAccessToken()
    {
        return this.acccessToken;
    }

    public string GetHostURL()
    {
        return this.hostURL;
    }
}

public class RealtimePlayer
{
    public string displayName;
    public bool isLocal;

    public RealtimePlayer(string _displayName, bool _isLocal)
    {
        isLocal = _isLocal;
        displayName = _displayName;
    }
}
