using BackEnd;
using BackEnd.Tcp;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BackEndMatchingServer 
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
        Backend.Match.JoinMatchMakingServer(out errorInfo); //��ġ ���� ����
    }
    public void MatchEnd()
    {
        Backend.Match.LeaveMatchMakingServer();
        Backend.Match.OnLeaveMatchMakingServer = (LeaveChannelEventArgs args) =>
        {
            // TODO ��� �г� ���� �α��� â����
        };
    }
    public void CreateMatchRoom()
    {
        Backend.Match.CreateMatchRoom();

        Backend.Match.OnMatchMakingRoomCreate = (MatchMakingInteractionEventArgs args) =>
        {
            SceneManager.LoadScene((int)Scene.WAITINGROOM);   
            // TODO ĳ���� ������ ���Ƿ� �̵�
        };
    }

}
