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

    [SerializeField] float godModeTime;     // ���� �ð� Ÿ��.

    const int MAX_JUMP_COUNT = 2;

    int jumpCount = 0;
    bool isAttack = false;          // ���� ���� ���� ��.
    bool isRight = true;            // ���� ����.
    bool isDamaged = false;         // ���� �޾��� ��.

    bool isOnGoal = false;          // ���� �� �� �ִ°�?
    bool isStopControl = false;     // ������ �� ���°�?

    new SpriteRenderer renderer;    // ��������Ʈ ������.
    Animator anim;                  // �ִϸ�����.

    Inventory inventory;            // �κ��丮 Ŭ����.

    void Start()
    {
        jumpCount = MAX_JUMP_COUNT;

        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetBool("isAlive", true);

        inventory = new Inventory();        
    }

    void Update()
    {
        if (isStopControl)
            return;

        // Ư�� ���� ���Ϸ� �������� �״´�.
        if (IsDeadLine())
        {
            OnDead();
            return;
        }

        Move();
        Jump();
        Attack();
        Goal();

        anim.SetBool("isGround", movement.isGround);
        anim.SetBool("isMove", movement.isMove);
        anim.SetFloat("velocityY", movement.velocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            isOnGoal = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            isOnGoal = false;
        }
    }

    void Move()
    {
        if (isAttack || isDamaged)
            return;

        float horizontal = Input.GetAxisRaw("Horizontal"); // ��:-1f, x:0f, ��:1f.

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
        if(Input.GetKeyDown(KeyCode.Space) && !isDamaged && !isAttack && jumpCount > 0)
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
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttack && !isDamaged && movement.isGround)
        {
            anim.SetTrigger("onAttack"); // onAttack�� Ʈ���� �ϰڴ�.
            isAttack = true;
        }
    }
    void Goal()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && isOnGoal)
        {
            GameManager.Instance.OnStageClear();
            isStopControl = true;
            gameObject.SetActive(false);
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
    public void OnDamaged(Damageable.DamageMessage message)
    {
        Transform attaacker = message.attacker;

        // �����ڰ� �����ʿ� �ִٸ�(������ x���� ������ ũ�ٸ�) ���� �������� ���ư���.
        Vector2 dir = (attaacker.position.x >= transform.position.x) ? Vector2.left : Vector2.right;
        dir += Vector2.up;

        isDamaged = true;
        isAttack = false;

        movement.OnStop();               // movement ������ ���� ����.
        movement.OnKnockBack(dir, 4f);   // ������ �˹� ���Ѷ�.

        StartCoroutine("NoMovement");
        StartCoroutine("GodMode");
    }
    public void OnDead()
    {
        // �� ������Ʈ�� ����.
        // gameObject.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Dead");

        anim.SetBool("isAlive", false);
        anim.SetTrigger("onDead");

        StopAllCoroutines();            // ��� �������� �ڷ�ƾ ��Ȱ��ȭ (��, �̸����� ȣ���� ��)
        renderer.enabled = true;        // ������ Ȱ��ȭ.

        Time.timeScale = 0.3f;          // �ð��� �帧�� 0.5��� �����϶�. (Default : 1)
        Invoke("OnFinishDead", 1.0f);   // N�� �ڿ� �ش� �Լ��� �����϶�.
    }

    void OnFinishDead()
    {
        Time.timeScale = 1f;
    }
    bool IsDeadLine()
    {
        return transform.position.y <= -4f;
    }

    
    public void OnAddItem(ItemObject.ITEM item)
    {
        inventory.Add(item);
        ItemInfoUI.Instance.OnUpdateItem(item, inventory.Count(item));
    }

    IEnumerator GodMode()
    {
        gameObject.layer = LayerMask.NameToLayer("GodMode");
        float time = Time.time + godModeTime;
        float visibleTime = 0.0f;

        while (Time.time < time)
        {
            if(visibleTime <= Time.time)
            {
                renderer.enabled = !renderer.enabled;
                visibleTime = Time.time + 0.1f;
            }
                        
            yield return null;
        }

        renderer.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    IEnumerator NoMovement()
    {
        yield return new WaitForSeconds(0.1f);

        while (!movement.isGround)
            yield return null;

        isDamaged = false;
        movement.OnStart();
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
