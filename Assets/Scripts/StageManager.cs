using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singletone<StageManager>
{
    [SerializeField] Transform player;
    [SerializeField] float moveSpeed;

    Transform[] stages;
    int playerIndex = 0;

    bool isMoving;          // �����̴� ���ΰ�?

    // Start is called before the first frame update
    void Start()
    {
        stages = new Transform[transform.childCount];
        for(int i = 0; i<stages.Length; i++)
        {
            stages[i] = transform.GetChild(i);
        }

        playerIndex = PlayerData.Instance.lastStage;
        player.position = stages[playerIndex].position;   // ������ ������ �÷��̾ ��ġ��Ų��.
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnMoveStage(int index)
    {
        if (isMoving)
            return;

        // ���� ���� ����.
        if(playerIndex == index)
        {
            // ���������� �̵�.
            isMoving = true;
            PlayerData.Instance.lastStage = playerIndex;
            //DataManager.SaveAll();
            SceneMover.Instance.MoveScene("Game");
        }
        else
            StartCoroutine(Move(index));
    }
    IEnumerator Move(int endIndex)
    {
        isMoving = true;

        bool isForward = endIndex >= playerIndex;

        while (playerIndex != endIndex)
        {
            playerIndex += isForward ? 1 : -1;

            Vector3 destination = stages[playerIndex].position;
            //Vector3 direction = destination - player.position;

            while(true)
            {
                //player.Translate(direction * moveSpeed * Time.deltaTime);
                player.position = Vector3.MoveTowards(player.position, destination, moveSpeed * Time.deltaTime);

                // ������������ �Ÿ�.
                float distance = Vector3.Distance(player.position, destination);
                if(distance <= 0.02f)
                {
                    player.position = destination;
                    break;
                }

                yield return null;
            }

            yield return null;
        }

        isMoving = false;
    }

}
