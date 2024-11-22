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
        // 회원가입 로직
        Debug.Log("회원가입을 요청합니다.");
        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다");
        }
        else
        {
            Debug.Log("회원가입에 실패했습니다");
        }
    }
    public void CustomLogin(string id, string pw)
    {
        // Step 3. 로그인 구현하기 로직
        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("로그인 성공했습니다");
            ErrorInfo errorInfo;
            Backend.Match.JoinMatchMakingServer(out errorInfo); //매치 서버 접근
        }
        else
        {
            Debug.Log("로그인 실패했습니다");
        }
    }

    public void UpdateNickname(string nickname)
    {
        // Step 4. 닉네임 변경 구현하기 로직
    }
    public void Update()
    {
        Backend.Match.Poll();
    }
}
