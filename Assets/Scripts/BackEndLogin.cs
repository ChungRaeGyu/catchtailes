using UnityEngine;
using BackEnd;
public class BackEndLogin
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
        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            //���߿� �ȳ������� ��ü�ع�����
            Debug.Log("ȸ�����Կ� �����߽��ϴ�");
        }
        else
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�");
        }
    }
    public bool CustomLogin(string id, string pw)
    {
        // Step 3. �α��� �����ϱ� ����
        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("�α��� �����߽��ϴ�");
            return true;
        }
        else
        {
            Debug.Log("�α��� �����߽��ϴ�");
            return false;
        }
    }

    public void UpdateNickname(string nickname)
    {
        // Step 4. �г��� ���� �����ϱ� ����
        Debug.Log("�г��� ������ ��û�մϴ�.");

        var bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            Debug.Log("�г��� ���濡 �����߽��ϴ� : " + bro);
        }
        else
        {
            Debug.LogError("�г��� ���濡 �����߽��ϴ� : " + bro);
        }
    }
}
