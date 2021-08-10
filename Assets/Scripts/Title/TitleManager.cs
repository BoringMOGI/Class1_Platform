using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    enum TITLE_BUTTON
    {
        NewGame,
        Continue,
        Option,
        Exit,

        Count,
    }

    [SerializeField] ButtonSwitch[] buttons;

    private void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.BGM.Forest1);        // ����� ���.

        bool isSavedData = DataManager.IsSavedData;
        buttons[(int)TITLE_BUTTON.Continue].Switch(isSavedData);
    }

    void OnNewGameAsDelete()
    {
        DataManager.DeleteAll();
        DataManager.SaveAll();
        SceneMover.Instance.MoveScene("WorldMap");
    }

    public void NewGame()
    {
        if(DataManager.IsSavedData)
        {
            // ���̺� ���� ����. ������ ������ �����.
            PopupManager.Instance.ShowPopup(
                "�� ���� ����",
                "������ �����͸� �����մϴ�.\n���� �����Ͻðڽ��ϱ�?",
                new PopupManager.ButtonHandle("�ƴϿ�", "��"),
                (index) => {  
                    if(index == 1)
                        OnNewGameAsDelete();
                });
        }
        else
        {
            DataManager.SaveAll();
            SceneMover.Instance.MoveScene("WorldMap");
        }                
    }
    public void Continue()
    {
        PopupManager.Instance.ShowPopup("�̾��ϱ�", "������ �̾��մϴ�.",
            new PopupManager.ButtonHandle("GO!"),
            (index) => {

                SceneMover.Instance.MoveScene("WorldMap");
            });
    }
    public void OpenOption()
    {
        SceneMover.Instance.OpenOption();
    }
    public void ExitGame()
    {
        PopupManager.Instance.ShowPopup("���� ����", "������ �����Ͻðڽ��ϱ�?",
            new PopupManager.ButtonHandle("��", "��", "���Կ�"),
            (index) => {

                Debug.Log("���õ� ��ư�� : " + index);
            });
    }
}
