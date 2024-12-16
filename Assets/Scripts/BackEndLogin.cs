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
        // 회원가입 로직
        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            //나중에 안내문구로 교체해버리기
            Debug.Log("회원가입에 성공했습니다");
        }
        else
        {
            Debug.Log("회원가입에 실패했습니다");
        }
    }
    public bool CustomLogin(string id, string pw)
    {
        // Step 3. 로그인 구현하기 로직
        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            Debug.Log("로그인 성공했습니다");
            return true;
        }
        else
        {
            Debug.Log("로그인 실패했습니다");
            return false;
        }
    }

    public void UpdateNickname(string nickname)
    {
        // Step 4. 닉네임 변경 구현하기 로직
        Debug.Log("닉네임 변경을 요청합니다.");

        var bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            Debug.Log("닉네임 변경에 성공했습니다 : " + bro);
        }
        else
        {
            Debug.LogError("닉네임 변경에 실패했습니다 : " + bro);
        }
    }
}
