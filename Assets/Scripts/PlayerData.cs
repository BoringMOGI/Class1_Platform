using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singletone<PlayerData>
{
    public int lastStage = 0;
    public bool[] isUnlockStages;  // ���������� Ǯ�ȴ°�?
    public bool[] isStageClears;   // ���������� Ŭ�����ߴ°�?

    protected new void Awake()
    {
        base.Awake();

        isUnlockStages = new bool[GameData.MAX_STAGE_COUNT];
        isStageClears = new bool[GameData.MAX_STAGE_COUNT];

        DataManager.OnSave += OnSave;
        DataManager.OnLoad += OnLoad;

        OnLoad();
        DontDestroyOnLoad(gameObject);
    }

    void OnSave()
    {
        DataManager.SetInt("lastStage", lastStage);
        for(int i = 0; i< GameData.MAX_STAGE_COUNT; i++)
        {
            // �������� Ŭ���� ����.
            string key = string.Concat("isStageClears", i);
            DataManager.SetBool(key, isStageClears[i]);

            // �������� ��� ����.
            key = string.Concat("isUnlockStages", i);
            DataManager.SetBool(key, isUnlockStages[i]);
        }
    }
    void OnLoad()
    {
        lastStage = DataManager.GetInt("lastStage");
        for(int i = 0; i< GameData.MAX_STAGE_COUNT; i++)
        {
            string key = string.Concat("isStageClears", i);
            isStageClears[i] = DataManager.GetBool(key);

            key = string.Concat("isUnlockStages", i);
            isUnlockStages[i] = DataManager.GetBool(key);
        }
    }
}
