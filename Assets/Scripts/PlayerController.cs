using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Movement movement;     // ������ ����.
    [SerializeField] Animator anim;         // �ִϸ�����.
    [SerializeField] Transform imagePivot;  // �̹����� �Ǻ�.
    [SerializeField] AudioSource jumpSFX;   // ���� ȿ����.

    const int MAX_JUMP_COUNT = 2;
    
    bool isAttack;
    int jumpCount;

    void Start()
    {
        Debug.Log("Movement Start");
        jumpCount = MAX_JUMP_COUNT;
    }

    void Update()
    {
        // Ư�� ���� ���Ϸ� �������� �״´�.
        if (IsDeadLine())
        {
            OnDead();
            return;
        }

        Move();
        Jump();
        Attack();

        anim.SetBool("isGround", movement.isGround);
        anim.SetBool("isMove", movement.isMove);
        anim.SetFloat("velocityY", movement.velocityY);
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // ��:-1f, x:0f, ��:1f.
        if (isAttack)
            horizontal = 0f;

        // ������ �����ٸ�.
        if (horizontal <= -1.0f)
        {
            imagePivot.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
        // �������� �����ٸ�.
        else if (horizontal >= 1.0f)
        {
            imagePivot.eulerAngles = Vector3.zero;
        }

        movement.OnMove(horizontal);
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isAttack && jumpCount > 0)
        {
            movement.OnJump();
            jumpCount -= 1;
        }

        // ���� �������� ������.
        if(movement.velocityY < 0.0f && movement.isGround)
        {
            jumpCount = MAX_JUMP_COUNT;
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttack && movement.isGround)
        {
            anim.SetTrigger("onAttack"); // onAttack�� Ʈ���� �ϰڴ�.
            isAttack = true;
        }
    }
    public void OnFinishAttack()
    {
        isAttack = false;
    }

    bool IsDeadLine()
    {
        return transform.position.y <= -4f;
    }

    // �ӽ÷� �׾��� ��.
    void OnDead()
    {
        // �� ������Ʈ�� ����.
        gameObject.SetActive(false);
    }
}
