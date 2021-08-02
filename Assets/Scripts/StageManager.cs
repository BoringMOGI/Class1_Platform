using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singletone<StageManager>
{
    [SerializeField] Transform player;
    [SerializeField] float moveSpeed;

    Transform[] stages;

    // Start is called before the first frame update
    void Start()
    {
        stages = new Transform[transform.childCount];
        for(int i = 0; i<stages.Length; i++)
        {
            stages[i] = transform.GetChild(i);
        }

        player.position = stages[0].position;   // ������ ������ �÷��̾ ��ġ��Ų��.
    }

    public void OnMoveStage(int index)
    {
        StartCoroutine(Move(index));
    }
    IEnumerator Move(int endIndex)
    {
        int startIndex = 0;

        while (startIndex < endIndex)
        {
            startIndex++;
            Vector3 destination = stages[startIndex].position;
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
    }

}
