using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] GameObject[] startObjects;

    // ��ó���� ���ǹ�.
    // ����Ƽ �����ͻ��� �ƴϸ� �ڵ尡 ���Ե��� �ʴ´�.
#if UNITY_EDITOR
    private void Awake()
    {
        for(int i= 0; i<startObjects.Length; i++)
        {
            GameObject findObject = GameObject.Find(startObjects[i].name);
            if (findObject == null)
            {
                GameObject newOjbect = Instantiate(startObjects[i]);
                string newName = newOjbect.name.Replace("(Clone)", string.Empty);
                newOjbect.name = newName;
            }
        }
    }
#endif

}
