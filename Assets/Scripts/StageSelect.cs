using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    private void OnMouseDown()
    {
        int index = transform.GetSiblingIndex(); // ���� ���° �ڽ�����?
        StageManager.Instance.OnMoveStage(index);
    }
}
