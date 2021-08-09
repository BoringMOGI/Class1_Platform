using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    [SerializeField] Collider2D selectCollider;
    [SerializeField] GameObject lockStage;
    [SerializeField] GameObject lockImage;

    int stageIndex;

    private void Start()
    {
        stageIndex = transform.GetSiblingIndex(); // ���� ���° �ڽ�����?
        bool isUnlock = PlayerData.Instance.isUnlockStages[stageIndex];

        selectCollider.enabled = isUnlock;
        lockStage.SetActive(!isUnlock);
        lockImage.SetActive(!isUnlock);
    }

    private void OnMouseUpAsButton()              // ���� ���� �������� ������ ���� ��.
    {
        StageManager.Instance.OnMoveStage(stageIndex);
    }
}
