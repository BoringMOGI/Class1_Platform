using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class GameDoor : MonoBehaviour
{
    Animation anim;

    void Start()
    {
        // ���� �Ŵ������� �̺�Ʈ ���.
        GameManager.Instance.OnOpenDoor += OnOpenDoor;
        anim = GetComponent<Animation>();
    }

    void OnOpenDoor()
    {
        anim.Play();
    }
}
