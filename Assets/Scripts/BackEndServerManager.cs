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

        var bro = Backend.Initialize(); // �ڳ� �ʱ�ȭ

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 204 Success
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 400�� ���� �߻�
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
        
        //��ġ ����� 
        Backend.Match.OnJoinMatchMakingServer = (JoinChannelEventArgs args) =>
        {
            if (args.ErrInfo == ErrorInfo.Success)
            {
                Debug.Log("��ġ ����Ϸ�");
                loginPanel.SetActive(false);
                MatchingPanel.SetActive(true);
            }
            else
            {
                Debug.Log(args.ErrInfo.Reason);
            }

            //�̺�Ʈ �����
            //�α��� �г� ���� 
            // TODO
        };
        //�� �����
        Backend.Match.OnMatchMakingRoomCreate = (MatchMakingInteractionEventArgs args) =>
        {
            SceneManager.LoadScene((int)Scene.WAITINGROOM);
            // TODO ĳ���� ������ ���Ƿ� �̵�
        };
        //�� ����
        Backend.Match.OnLeaveMatchMakingServer = (LeaveChannelEventArgs args) =>
        {
            // TODO ��� �г� ���� �α��� â����
        };
        //�ʴ�޾�����
        Backend.Match.OnMatchMakingRoomSomeoneInvited += (args) => {
            Debug.Log("�ʴ����");
            Debug.Log($"{args.RoomId}�� ���̵�\n{args.RoomToken}�� ��ū\n{args.InviteUserInfo}");

            // TODO
        };
    }
    public void Update()
    {
        Backend.Match.Poll();

    }
}
