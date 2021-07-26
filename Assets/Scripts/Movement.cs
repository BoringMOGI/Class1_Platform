using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform groundChecker;   // ���� üũ�� ��ġ ����.
    [SerializeField] LayerMask groundMask;      // ���� üũ ����ũ.
    [SerializeField] float checkerDistance;     // üũ �Ÿ�.

    [SerializeField] Rigidbody2D rigid;
    
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float knockBackTime;

    float horizontal; // ��, �� ����.

    public bool isGround { get; private set; } // ���� ���� �� ���� ��.
    public bool isMove
    {
        get
        {
            return horizontal != 0f;
        }
    }
    public float velocityY
    {
        get
        {
            return rigid.velocity.y;
        }
    }

    bool isKnockBack = false;
    bool isMovement = true;

    private void Update()
    {
        CheckGround();
        Move();
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
        if(!isKnockBack && isMovement)
            rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);

        horizontal = 0f;
    }

    public void OnStart()
    {
        isMovement = true;
    }
    public void OnStop()
    {
        isMovement = false;
    }

    public void OnMove(float horizontal)
    {
        this.horizontal = horizontal;
    }
    public void OnJump()
    {
        // ���� �������� 10��ŭ�� ���� ���϶�.
        rigid.velocity = new Vector2(rigid.velocity.x, 0.0f);
        rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
    }

    Coroutine knockBackCoroutine;

    public void OnKnockBack(Vector2 dir, float power)
    {
        // ������ �ڷ�ƾ�� ���ư��� �ִٸ� Stop��Ų��.
        if (knockBackCoroutine != null)
            StopCoroutine(knockBackCoroutine);

        // ���ο� �ڷ�ƾ�� �����Ų��.
        knockBackCoroutine = StartCoroutine(KnockBack(dir, power));
    }
    IEnumerator KnockBack(Vector2 dir, float power)
    {
        isKnockBack = true;
        rigid.velocity = new Vector2(0, rigid.velocity.y);  // x�� ���ǵ带 0����.
        rigid.AddForce(dir * power, ForceMode2D.Impulse);   // x�� �������� ���� ���Ѵ�.

        yield return new WaitForSeconds(knockBackTime);

        isKnockBack = false;
    }

}
