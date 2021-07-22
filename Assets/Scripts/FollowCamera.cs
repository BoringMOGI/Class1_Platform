using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;      // Ÿ��.
    [SerializeField] Vector3 offset;        // ����.
    [SerializeField] float moveSpeed;       // ī�޶��� �ӵ�.

    // Update() ȣ�� �Ŀ� LateUpdate()�� �Ҹ���.
    // ������ Ÿ���� Update������ ��ġ�� �����ϰ�
    // �� �Ŀ� ���� ���󰡾��ϱ� ������.
    void LateUpdate()
    {
        // �� ��ġ�� Ÿ���� ��ġ�� �Ѵ�.

        transform.position = target.position + offset;

        /*
        transform.position = Vector3.Lerp(
            transform.position, 
            target.position + offset, 
            moveSpeed * Time.deltaTime);
        */
    }
}
