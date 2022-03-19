using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum UIMessageType
{
    OK_Message,Yes_No_Message,Timed_Message
}

public delegate void SimpleDelegate();

public class GameLoopUI : MonoBehaviour
{
    public static GameLoopUI instance;

    public GameObject messageBox,okBox,YesNoBox;
    public Text titleString, messageString, okBtnString, yesBtnString, noBtnString;

    public Button yesBtn, noBtn, okBtn;

    private void Awake()
    {
        instance = this;
    }

    void CloseMessage()
    {
        this.messageBox.SetActive(false);
    }


    public void ShowMessage(string message, string title= "Message",
        UIMessageType messageMode = UIMessageType.OK_Message, SimpleDelegate yesBtnEvent = null,SimpleDelegate noBtnEvent = null, SimpleDelegate okBtnEvent = null,
        string yesBtnName = "Yes",string noBtnName = "No",string okBtnName = "Ok",
        float waitTime = 2f)
    {
        this.titleString.text = title;
        this.messageString.text = message;

        this.yesBtnString.text = yesBtnName;
        this.noBtnString.text = noBtnName;
        this.okBtnString.text = okBtnName;

        this.yesBtn.onClick.RemoveAllListeners();
        this.noBtn.onClick.RemoveAllListeners();
        this.okBtn.onClick.RemoveAllListeners();
        
        UnityAction yes = new UnityAction(yesBtnEvent);
        UnityAction no = new UnityAction(yesBtnEvent);
        UnityAction ok = new UnityAction(yesBtnEvent);

        if (yes != null)
            this.yesBtn.onClick.AddListener(yes);

        if (no != null)
            this.noBtn.onClick.AddListener(no);

        if (ok != null)
            this.okBtn.onClick.AddListener(ok);

        this.okBox.SetActive(messageMode.Equals(UIMessageType.OK_Message));
        this.YesNoBox.SetActive(messageMode.Equals(UIMessageType.Yes_No_Message));

        this.messageBox.SetActive(true);

        if (messageMode.Equals(UIMessageType.Timed_Message))
            Invoke("HideMessage", waitTime);
    }

    public void HideMessage()
    {
        this.messageBox.SetActive(false);
    }
}
