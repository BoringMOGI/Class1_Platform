using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singletone<PopupManager>
{
    [SerializeField] Transform panel;       // �г�.
    [SerializeField] Text titleText;        // Ÿ��Ʋ �ؽ�Ʈ.
    [SerializeField] Text context;          // ���� �ؽ�Ʈ.
    [SerializeField] Button leftButton;     // ���� ��ư.
    [SerializeField] Button rightButton;    // ������ ��ư.

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
    public void ShowPopup(string title, string context, string left, string right, System.Action<int> OnCallback)
    {
        titleText.text = title;
        this.context.text = context;
        leftButton.GetComponentInChildren<Text>().text = left;
        rightButton.GetComponentInChildren<Text>().text = right;                

        this.OnCallback = OnCallback;
        SwitchPopup(true);
    }
}
