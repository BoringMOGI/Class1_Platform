using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T>
{
    void Setup(System.Action<T> OnReturnPool);
}

public class PoolManager<TargetObject, PoolObject> : Singletone<TargetObject>
    where TargetObject : MonoBehaviour
    where PoolObject : MonoBehaviour, IPool<PoolObject>  // ���׸� �ڷ��� PoolObject�� MonoBehaviour�� ����ϰ� �־�� �Ѵ�.
{
    [SerializeField] PoolObject poolPrefab;     // Ǯ���� ������Ʈ ������.
    [SerializeField] int initCount;             // �ʱ� ���� ����.

    Stack<PoolObject> storage;                  // �����.

    private new void Awake()
    {
        base.Awake();

        storage = new Stack<PoolObject>();      // stack ���� ��ü ����.
        for (int i = 0; i < initCount; i++)
            CreatePool();
    }

    private void CreatePool()
    {
        PoolObject pool = Instantiate(poolPrefab, transform);
        pool.Setup(OnReturnPool);
        pool.gameObject.SetActive(false);
        storage.Push(pool);
    }

    protected PoolObject GetPool()
    {
        if (storage.Count <= 0)
            CreatePool();

        storage.Peek().gameObject.SetActive(true);
        return storage.Pop();
    }

    private void OnReturnPool(PoolObject pool)
    {
        pool.gameObject.SetActive(false);                       // ���ƿ� pool�� ����.
        storage.Push(pool);                                     // ����ҿ� push�Ѵ�.
    }
}
