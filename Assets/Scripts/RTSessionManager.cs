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

    private GameSparksRTUnity gamesparksRTUnity;

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

                FindObjectOfType<MenuCanvas>().ShowMenuPanel();

                break;

            case (int)NetworkData.FromServerOpCodes.STARTGAME:

                break;

        }
    }
}

public class RealtimeSessionInfo
{
    private string hostURL;
    private string acccessToken;
    private int portID;
    private string matchID;
    private List<RealtimePlayer> playerList = new List<RealtimePlayer>();

    public RealtimeSessionInfo(GameSparks.Api.Messages.MatchFoundMessage _message)
    {
        hostURL = _message.Host;
        acccessToken = _message.AccessToken;
        portID = (int)_message.Port;
        matchID = _message.MatchId;

        foreach (var p in _message.Participants)
        {
            playerList.Add(new RealtimePlayer(p.DisplayName, p.Id, (int)p.PeerId));
        }
    }

    public List<RealtimePlayer> GetPlayerList()
    {
        return playerList;
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
    public string id;
    public int peerId;
    public bool isOnline, isLocal;

    public RealtimePlayer(string _displayName, string _id, int _peerId, bool _isLocal)
    {
        isLocal = _isLocal;
        displayName = _displayName;
        id = _id;
        peerId = _peerId;
    }

    public RealtimePlayer(string _displayName, string _id, int _peerId)
    {
        isLocal = false;
        displayName = _displayName;
        id = _id;
        peerId = _peerId;
    }
}
