using BackEnd;
using BackEnd.Tcp;
using UnityEngine;


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

        Backend.Match.OnJoinMatchMakingServer = (JoinChannelEventArgs args) =>
        {
            //�̺�Ʈ �����

            // TODO
        };
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
    }

}
