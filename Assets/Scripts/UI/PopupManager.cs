using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singletone<PopupManager>
{
    public enum BUTTON_TYPE
    {
        Confirm,        // Ȯ��. ��ư 1��.
        Choice,         // ����. ��ư 2��.
        Triple,         // 3����.
    }

    public struct ButtonHandle
    {
        public string[] buttonNames;
        public BUTTON_TYPE type;

        public ButtonHandle(string one, string two, string three)
        {
            buttonNames = new string[3];
            buttonNames[0] = one;
            buttonNames[1] = two;
            buttonNames[2] = three;
            type = BUTTON_TYPE.Triple;
        }

        public ButtonHandle(string one, string two)
            : this(one, two, string.Empty)
        {
            type = BUTTON_TYPE.Choice;
        }

        public ButtonHandle(string one)
            : this(one, string.Empty, string.Empty)
        {
            type = BUTTON_TYPE.Confirm;
        }
    }


    [SerializeField] Transform panel;       // �г�.
    [SerializeField] Text titleText;        // Ÿ��Ʋ �ؽ�Ʈ.
    [SerializeField] Text context;          // ���� �ؽ�Ʈ.
    [SerializeField] Button[] buttons;      // ��ư �迭.

    System.Action<int> OnCallback;          // �ݹ�.

    private void Start()
    {
        SwitchPopup(false);
        DontDestroyOnLoad(gameObject);
    }

    private void SwitchPopup(bool isOn)
    {
        panel.gameObject.SetActive(isOn);
    }

    public void OnSelectedPopup(int index)
    {
        OnCallback(index);
        SwitchPopup(false);
    }


    public void ShowPopup(string title, string context, ButtonHandle handle, System.Action<int> OnCallback)
    {
        titleText.text = title;
        this.context.text = context;

        int buttonCount = (int)handle.type;       // ���� ���� ��ư ����.
        for(int i = 0; i<buttons.Length; i++)     // ��ü ��ư �迭�� ��ȸ.
        {
            buttons[i].gameObject.SetActive(i <= buttonCount);          // ��ư�� ���� �Ҵ�.
            buttons[i].gameObject.GetComponentInChildren<Text>().text = handle.buttonNames[i];      // ��ư�� �ؽ�Ʈ ����.
        }

        this.OnCallback = OnCallback;
        SwitchPopup(true);
    }
}
