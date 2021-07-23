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
    bool isAlive;

    private void Start()
    {
        horizontal = 1f;
        isAlive = true;

        // Ű = headPivot�� Enemy�� ��(pivot) ������ �Ÿ�.
        height = Vector2.Distance(headPivot.position, transform.position);
    }

    void Update()
    {
        if (!isAlive)
            return;

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


    public void OnDamaged(Damageable.DamageMessage message)
    {
        // ��¦��.
        StartCoroutine(OnBlink());

        Vector2 dir = (transform.position.x <= message.attacker.position.x) ? Vector2.left : Vector2.right;
        movement.OnKnockBack(dir, 2);
    }
    IEnumerator OnBlink()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        renderer.color = Color.white;
    }

    public void OnDead()
    {
        // ���� �׾���.
        anim.SetTrigger("onDead");

        isAlive = false;
        gameObject.layer = LayerMask.NameToLayer("Dead");   // ���̾ Dead�� �ٲ۴�.
        StartCoroutine(OnDisappear());
    }

    IEnumerator OnDisappear()
    {
        yield return new WaitForSeconds(3f);
        float time = 1f;

        while(true)
        {
            yield return null; // 1������ ���ڴ�.

            Color originColor = renderer.color;
            originColor.a = time / 1f;
            renderer.color = originColor;

            time -= Time.deltaTime;
            if (time <= 0.0f)
                break;
        }

        renderer.color = new Color(0f, 0f, 0f, 0f);
        gameObject.SetActive(false);
    }
}
