using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform headPivot;
    [SerializeField] float wallDistance;
    [SerializeField] LayerMask groundMask;

    [SerializeField] Movement movement;
    [SerializeField] Animator anim;
    [SerializeField] new SpriteRenderer renderer;

    float horizontal;  // �̵� ����.
    float height;      // Ű.

    bool isRight
    {
        get
        {
            return horizontal >= 0;
        }
    }

    private void Start()
    {
        horizontal = 1f;

        // Ű = headPivot�� Enemy�� ��(pivot) ������ �Ÿ�.
        height = Vector2.Distance(headPivot.position, transform.position);
    }

    void Update()
    {
        CheckWall();
        CheckFliff();
        Move();
    }

    void CheckWall()
    {
        Vector2 direction = isRight ? Vector2.right : Vector2.left;
        if(Physics2D.Raycast(headPivot.position, direction, wallDistance, groundMask))
        {
            Debug.Log("�տ� ���� �ִ�!!");
            horizontal *= -1f;
        }
    }
    void CheckFliff()
    {
        Vector2 pivot = headPivot.position;
        Vector2 dir = isRight ? Vector2.right : Vector2.left;
        dir *= wallDistance;
        pivot += dir;

        Debug.DrawRay(pivot, Vector2.down * (height + .5f), Color.red);
        if(!Physics2D.Raycast(pivot, Vector2.down, height + .5f, groundMask))
        {
            Debug.Log("��! �����̴�.");
            horizontal *= -1f;
        }
    }
    void Move()
    {
        renderer.flipX = !isRight;               // �������� ������ �� �¿� ���� ���Ѷ�.
        movement.OnMove(horizontal);             // movemnet���� ������ �����ؼ� ��������.
        anim.SetBool("isMove", movement.isMove); // animator���� isMove �Ķ���͸� �����ض�.
    }
}
