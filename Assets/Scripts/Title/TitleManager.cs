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
            Debug.Log("��¥ ����ϱ�?");

            PopupManager.Instance.ShowPopup(
                "�� ���� ����",
                "������ �����͸� �����մϴ�.\n���� �����Ͻðڽ��ϱ�?",
                "�׷���", "�Ⱦ��",
                (index) => {  
                    if(index == 0)
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
        SceneMover.Instance.MoveScene("WorldMap");
    }
    public void OpenOption()
    {
        SceneMover.Instance.OpenOption();
    }
    public void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}
