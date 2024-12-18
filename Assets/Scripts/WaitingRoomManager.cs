using BackEnd;
using BackEnd.Tcp;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomManager : MonoBehaviour
{
    [SerializeField] private InputField nickNameInput;
    [SerializeField] private Button inviteBtn;

    public void InviteBtn()
    {
        Debug.Log("�ʴ��ư����");
        Backend.Match.InviteUser(nickNameInput.text);

    }
}
