using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ��(���) ���� Ŭ����.

public class TitleManager : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("NewGame");
        SceneManager.LoadScene("WorldMap");
    }
    public void OpenOption()
    {
        Debug.Log("OpenOption");
    }
    public void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}
