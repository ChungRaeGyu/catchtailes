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
        Backend.Match.JoinMatchMakingServer(out errorInfo); //매치 서버 접근

        Backend.Match.OnJoinMatchMakingServer = (JoinChannelEventArgs args) =>
        {
            //이벤트 사용방법

            // TODO
        };
    }
    public void MatchEnd()
    {
        Backend.Match.LeaveMatchMakingServer();
    }

}
