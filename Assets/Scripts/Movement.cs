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
        rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);
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
}
