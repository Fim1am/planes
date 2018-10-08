using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Core;
using GameSparks.Api.Messages;

public class GameSparksManager : MonoBehaviour 
{
	void Start ()
	{
		GS.GameSparksAvailable += (b) =>
		{
			if(b)
				DeviceAuthorization();
		};

        MatchFoundMessage.Listener = MatchFound;
    }

	private void DeviceAuthorization()  
	{
        new GameSparks.Api.Requests.DeviceAuthenticationRequest()
            .Send((_response) =>
            {
                if (!_response.HasErrors)
                {
                    Matchmaking();
                }
            });

	}

    public void Matchmaking()
    {
        new GameSparks.Api.Requests.MatchmakingRequest()
        .SetMatchShortCode("SIMPLE_MATCH")
        .SetSkill(0)
        .Send((m_response) => {

            if (!m_response.HasErrors)
            {
                Debug.Log("matchmaking is successful");
            }
            else
            {
                Debug.Log(m_response.Errors.JSON);
            }
        });
    }

    private void MatchFound(MatchFoundMessage _message)
    {
        RealtimeSessionInfo info = new RealtimeSessionInfo(_message);

        FindObjectOfType<RTSessionManager>().JoinOrCreateRTSession(info);
    }

}
