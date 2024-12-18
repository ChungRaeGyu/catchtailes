using BackEnd;
using BackEnd.Tcp;
using System.Collections;
using UnityEngine;


public class BackEndMatchingServer : MonoBehaviour
{
    private static BackEndMatchingServer _instance = null;

    public static BackEndMatchingServer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackEndMatchingServer();
            }

            return _instance;
        }
    }
    public void JoinMatch()
    {
        ErrorInfo errorInfo;
        Backend.Match.JoinMatchMakingServer(out errorInfo); //매치 서버 접근
    }
    public void MatchEnd()
    {
        Backend.Match.LeaveMatchMakingServer();

    }
    public void CreateMatchRoom()
    {
        Backend.Match.CreateMatchRoom();
    }

    public void AcceptInviteBtn(SessionId roomId, string roomToken)
    {
        Backend.Match.AcceptInvitation(roomId, roomToken);
        BackEndServerManager.Instance.inviteCheck = true;
    }
    public void DeclineInviteBtn(SessionId roomId, string roomToken)
    {
        Backend.Match.DeclineInvitation(roomId, roomToken);
        BackEndServerManager.Instance.inviteCheck = false;

    }



}
