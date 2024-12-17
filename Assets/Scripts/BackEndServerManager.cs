using UnityEngine;
using BackEnd;
using System;
using TMPro;
using UnityEngine.UI;
using BackEnd.Tcp;
using UnityEngine.SceneManagement;
public class BackEndServerManager : MonoBehaviour
{
    private static BackEndServerManager _instance;

    public static BackEndServerManager Instance;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField] TMP_InputField ID;
    [SerializeField] TMP_InputField PW;
    [SerializeField] TMP_InputField nickName;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject MatchingPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        var bro = Backend.Initialize(); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        }
        Event();

    }

    public void SignUpBtn()
    {
        BackEndLogin.Instance.CustomSignUp(ID.text, PW.text);
        ID.text = "";
        PW.text = "";
    }

    public void LoginBtn() 
    {
        if (ID.text == null || PW.text == null || nickName.text == null) return;
        if(BackEndLogin.Instance.CustomLogin(ID.text, PW.text))
        {
            BackEndLogin.Instance.UpdateNickname(nickName.text);

            BackEndMatchingServer.Instance.JoinMatch();

        }
    }

    public void MatchEndBtn()
    {
        BackEndMatchingServer.Instance.MatchEnd();
    }
    
    public void CreateRoomBtn()
    {
        BackEndMatchingServer.Instance.CreateMatchRoom();
    }

    public void OnInviteEvent()
    {

    }

    public void Event()
    {
        
        //매치 입장시 
        Backend.Match.OnJoinMatchMakingServer = (JoinChannelEventArgs args) =>
        {
            if (args.ErrInfo == ErrorInfo.Success)
            {
                Debug.Log("매치 입장완료");
                loginPanel.SetActive(false);
                MatchingPanel.SetActive(true);
            }
            else
            {
                Debug.Log(args.ErrInfo.Reason);
            }

            //이벤트 사용방법
            //로그인 패널 끄고 
            // TODO
        };
        //방 만들기
        Backend.Match.OnMatchMakingRoomCreate = (MatchMakingInteractionEventArgs args) =>
        {
            SceneManager.LoadScene((int)Scene.WAITINGROOM);
            // TODO 캐릭터 생성과 대기실로 이동
        };
        //방 떠남
        Backend.Match.OnLeaveMatchMakingServer = (LeaveChannelEventArgs args) =>
        {
            // TODO 모든 패널 끄고 로그인 창으로
        };
        //초대받았을때
        Backend.Match.OnMatchMakingRoomSomeoneInvited += (args) => {
            Debug.Log("초대받음");
            Debug.Log($"{args.RoomId}방 아이디\n{args.RoomToken}방 토큰\n{args.InviteUserInfo}");

            // TODO
        };
    }
    public void Update()
    {
        Backend.Match.Poll();

    }
}
