using UnityEngine;
using BackEnd;
using TMPro;
using BackEnd.Tcp;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.GPUSort;
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
    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject matchingPanel;
    [SerializeField] GameObject inviteCanvas;
    [SerializeField] TMP_Text inviter;

    SessionId roomId;
    string roomName;
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
    public void InviteAcceptBtn()
    {
        BackEndMatchingServer.Instance.AcceptInviteBtn(roomId, roomName);
    }
    public void InviteDeclineBtn()
    {
        BackEndMatchingServer.Instance.DeclineInviteBtn(roomId, roomName);
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
                matchingPanel.SetActive(true);
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

        //�ʴ� ��û
        Backend.Match.OnMatchMakingRoomInvite = (MatchMakingInteractionEventArgs args) => {
            Debug.Log(args.ErrInfo);
            // TODO
        };
        //�ʴ�޾�����
        Backend.Match.OnMatchMakingRoomSomeoneInvited += (args) => {
            Debug.Log("�ʴ����");
            Debug.Log($"{args.RoomId}�� ���̵�\n{args.RoomToken}�� ��ū\n{args.InviteUserInfo}");
            roomId = args.RoomId;
            roomName = args.RoomToken;

            // TODO �ʴ� ��û�� ���� ���� ���� ��ư �����ֱ�
            inviteCanvas.SetActive(true);
            inviter.text = args.InviteUserInfo.m_nickName;
        };

        //����/����
        Backend.Match.OnMatchMakingRoomInviteResponse = (MatchMakingInteractionEventArgs args) => {
            // TODO
            Debug.Log(args.ErrInfo);
            Debug.Log(args.Reason);
            inviteCanvas.SetActive(false);
        };
    }
    public void Update()
    {
        Backend.Match.Poll();

    }
}
