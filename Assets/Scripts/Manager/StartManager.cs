using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    void Start()
    {
        SceneMover.Instance.MoveScene("Title");

#if UNITY_STANDALONE_WIN

        Debug.Log("������ ���� �ڵ�");

#elif UNITY_ANDROID

        Debug.Log("�ȵ���̵��");

#endif

    }
}
