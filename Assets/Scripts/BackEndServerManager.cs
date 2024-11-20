using UnityEngine;
using BackEnd;
using System;
using TMPro;
using UnityEngine.UI;
public class BackEndServerManager : MonoBehaviour
{
    public TMP_InputField ID;
    public TMP_InputField PW;

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
    }

    public void SignUpBtn()
    {
        BackEndLogin.Instance.CustomSignUp(ID.text, PW.text);
        ID.text = "";
        PW.text = "";
    }

    public void LoginBtn() 
    {
        BackEndLogin.Instance.CustomLogin(ID.text, PW.text);
    }
}
