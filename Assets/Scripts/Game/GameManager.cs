using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>
{
    [SerializeField] Transform stageParent;
    [SerializeField] ItemObject.ITEM whatIsKey;

    [SerializeField] UnityEvent StageClear;
    [SerializeField] UnityEvent StageFail;

    public event System.Action OnOpenDoor;              // ���� ������ �̺�Ʈ.

    int keyCount = 0;

    protected new void Awake()      // �θ� Ŭ������ Awake�� ������.
    {
        base.Awake();               // �θ� Ŭ���� Singletone�� Awake ȣ��.

        int stageNumber = PlayerData.Instance.lastStage;
        for(int i = 0; i<stageParent.childCount; i++)
        {
            Transform stage = stageParent.GetChild(i);    // i��° �ڽ� ������Ʈ ����.
            stage.gameObject.SetActive(i == stageNumber); // stageNumber ��° ������Ʈ�� �Ҵ�.
        }
    }

    void Start()
    {
        ItemObject[] allItems = FindObjectsOfType<ItemObject>();
        foreach(ItemObject item in allItems)
        {
            if (item.ItemType == whatIsKey)
                keyCount++;
        }
    }

    public void OnGetKey(ItemObject.ITEM item)
    {
        if(item == whatIsKey)
        {
            keyCount -= 1;
            if (keyCount <= 0)
            {
                OnOpenDoor?.Invoke();           // �� ���� �̺�Ʈ ȣ��.
            }
        }
    }

    public void OnStageClear()
    {
        StageClear?.Invoke();

        PlayerData player = PlayerData.Instance;            // �÷��̾� ���� ���� ����.
        int currentStage = player.lastStage;                // ���� �������� �ѹ�.

        player.isStageClears[currentStage] = true;          // ���� ���������� Ŭ���� ���� ����.

        if(currentStage < GameData.MAX_STAGE_COUNT - 1)     // ���� ���������� ������ ���������� �ƴ϶��.
            player.isUnlockStages[currentStage + 1] = true; // ���� ���������� ���.

        DataManager.SaveAll();
    }
    public void OnStageFail()
    {
        StageFail?.Invoke();
    }

    public void OnRetry()
    {   
        SceneMover.Instance.MoveScene("Game");
    }
    public void OnWorldMap()
    {   
        SceneMover.Instance.MoveScene("WorldMap");
    }
}
