using UnityEngine;
using BackEnd;
using System;
using TMPro;
using UnityEngine.UI;
using BackEnd.Tcp;
public class BackEndServerManager : MonoBehaviour
{
    public TMP_InputField ID;
    public TMP_InputField PW;
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
        Debug.Log("1");
        if(BackEndLogin.Instance.CustomLogin(ID.text, PW.text))
        {
            Debug.Log("2");    
            BackEndMatchingServer.Instance.JoinMatch();
        }
    }

    public void MatchEndBtn()
    {
        BackEndMatchingServer.Instance.MatchEnd();
    }
    public void Update()
    {
        Backend.Match.Poll();
    }
}
