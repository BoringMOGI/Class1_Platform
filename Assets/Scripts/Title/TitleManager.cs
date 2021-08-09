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

        bool isNewGame = true;

        // ������ �ε�...
        isNewGame = false;
        buttons[(int)TITLE_BUTTON.Continue].Switch(!isNewGame);
    }

    public void NewGame()
    {
        SceneMover.Instance.MoveScene("WorldMap");
    }
    public void Continue()
    {

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
