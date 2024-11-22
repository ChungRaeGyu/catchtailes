using UnityEngine;
using BackEnd;
using BackEnd.Tcp;
public class BackEndLogin : MonoBehaviour
{
    private static BackEndLogin _instance;
    public static BackEndLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackEndLogin();
            }
            return _instance;
        }
    }

    public void CustomSignUp(string id, string pw)
    {
        // ȸ������ ����
        Debug.Log("ȸ�������� ��û�մϴ�.");
        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�");
        }
        else
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�");
        }
    }
    public void CustomLogin(string id, string pw)
    {
        // Step 3. �α��� �����ϱ� ����
        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("�α��� �����߽��ϴ�");
            ErrorInfo errorInfo;
            Backend.Match.JoinMatchMakingServer(out errorInfo); //��ġ ���� ����
        }
        else
        {
            Debug.Log("�α��� �����߽��ϴ�");
        }
    }

    public void UpdateNickname(string nickname)
    {
        // Step 4. �г��� ���� �����ϱ� ����
    }
    public void Update()
    {
        Backend.Match.Poll();
    }
}
