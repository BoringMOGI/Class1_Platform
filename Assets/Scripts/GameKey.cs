using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKey : MonoBehaviour
{
    public event System.Action OnGet;

    // ������Ʈ�� Destory�Ǿ��� �� ȣ��Ǵ� �Լ�.
    private void OnDestroy()
    {
        OnGet?.Invoke();
    }
}
