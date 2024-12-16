using UnityEngine;
using BackEnd;
using System;
using TMPro;
using UnityEngine.UI;
using BackEnd.Tcp;
public class BackEndServerManager : MonoBehaviour
{
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
        Backend.Match.OnMatchMakingRoomSomeoneInvited += (args) => {
            // TODO
        };
    }
    public void Update()
    {
        Backend.Match.Poll();
    }
}
