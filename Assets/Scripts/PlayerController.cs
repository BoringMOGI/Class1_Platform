using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform attackPivot;     // ���� �� ����.
    [SerializeField] float attackDistance;      // ���� �� ��ġ.
    [SerializeField] float attackRange;         // ���� ����.
    [SerializeField] float attackRate;          // ���� �ֱ�(�ӵ�).
    [SerializeField] LayerMask enemyMask;       // ���� ���̾�.

    [SerializeField] Movement movement;     // ������ ����.    
    [SerializeField] AudioSource jumpSFX;   // ���� ȿ����.

    const int MAX_JUMP_COUNT = 2;

    int jumpCount = 0;
    bool isAttack = false;
    bool isRight = true;

    new SpriteRenderer renderer;    // ��������Ʈ ������.
    Animator anim;                  // �ִϸ�����.

    void Start()
    {
        jumpCount = MAX_JUMP_COUNT;

        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
            renderer.flipX = true;
        }
        // �������� �����ٸ�.
        else if (horizontal >= 1.0f)
        {
            renderer.flipX = false;
        }

        isRight = (renderer.flipX == false);

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


    public void OnAttack()
    {
        Vector3 dir = isRight ? Vector2.right : Vector2.left;
        dir *= attackDistance;

        // Enemy���̾ �ް� �ִ� ��� �ݸ����� �˻��Ѵ�.
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPivot.position + dir, attackRange, enemyMask);
        foreach(Collider2D collider in hitEnemys)
        {
            Damageable target = collider.GetComponent<Damageable>();
            if(target != null)
            {
                // ������ ���� �޼����� �ۼ��ؼ� ����.
                Damageable.DamageMessage message;
                message.attacker = transform;
                message.attackerName = "�÷��̾�";
                message.amount = 1;

                target.OnDamaged(message);
            }
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

    private void OnDrawGizmosSelected()
    {
        // ����� : �� â�� ������ ���� ��.
        if (attackPivot != null)
        {
            Gizmos.color = Color.red;
            Vector3 dir = isRight ? Vector2.right : Vector2.left;
            dir *= attackDistance;
            Gizmos.DrawWireSphere(attackPivot.position + dir, attackRange);
        }
    }
}
