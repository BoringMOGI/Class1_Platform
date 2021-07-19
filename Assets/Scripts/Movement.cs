using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform groundChecker;   // ���� üũ�� ��ġ ����.
    [SerializeField] LayerMask groundMask;      // ���� üũ ����ũ.
    [SerializeField] float checkerDistance;     // üũ �Ÿ�.

    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Transform imagePivot;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;

    [SerializeField] AudioSource jumpSFX;

    public bool isGround { get; private set; } // ���� ���� �� ���� ��.
    bool isAttack;
    bool isJump
    {
        get
        {
            return rigid.velocity.y != 0;
        }
    }

    void Start()
    {
        isGround = true;
        Debug.Log("Movement Start");
    }

    void Update()
    {
        // Ư�� ���� ���Ϸ� �������� �״´�.
        if (IsDeadLine())
        {
            OnDead();
            return;
        }

        CheckGround();
        Move();
        Jump();
        Attack();
    }

    void CheckGround()
    {
        // Ư�� ��ġ���� �Ʒ� �������� ����(����)�� ���.
        RaycastHit2D hit = Physics2D.Raycast(groundChecker.position, Vector2.down, checkerDistance, groundMask);
        Debug.DrawRay(groundChecker.position, Vector2.down * checkerDistance, Color.red);

        isGround = hit;
    }
    void Move()
    {
        // Horizontal : ����
        // ����Ƽ�� �⺻ ���� Ű�� ��,�� ���� ���� -1.0 ~ 1.0 ������ ��.
        //float horizontal = Input.GetAxis("Horizontal");

        // GetAxisRow : 1 or 0 or -1.
        float horizontal = 0.0f;
        
        // �������� �ƴ� �� �̵� Ű�� �޾ƿ´�.
        if(!isAttack)
            horizontal = Input.GetAxisRaw("Horizontal");

        bool isRun = (horizontal != 0);

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

        rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);

        anim.SetBool("isRun", isRun);
        anim.SetFloat("velocityY", rigid.velocity.y); // ������ٵ��� Y�� �ӵ� ���� ����.
    }
    void Jump()
    {
        // ���� �����̽� �ٸ� ������ ��. ���� �� ���� ��. ���� ���� �ƴ� ��.
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !isAttack)
        {
            // ���� �������� 10��ŭ�� ���� ���϶�.
            rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);

            // ���� ȿ���� ���.
            jumpSFX.Play();
        }

        anim.SetBool("isGround", isGround); // �ִϸ������� �Ķ���� isGround�� ����.
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttack && isGround && !isJump)
        {
            anim.SetTrigger("onAttack"); // onAttack�� Ʈ���� �ϰڴ�.
            isAttack = true;

            // 1�ʵڿ� OnFinish�� �ҷ���.
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
