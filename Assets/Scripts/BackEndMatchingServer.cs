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

}
