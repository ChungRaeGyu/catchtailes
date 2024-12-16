using BackEnd;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomManager : MonoBehaviour
{
    [SerializeField] private InputField nickNameInput;
    [SerializeField] private Button inviteBtn;

    public void InviteBtn()
    {
        Backend.Match.InviteUser(nickNameInput.text);
    }

}
