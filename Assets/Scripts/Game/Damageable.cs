using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public struct DamageMessage
    {
        public Transform attacker;  // ������.
        public string attackerName; // �������� �̸�.
        public int amount;          // ��.
    }

    // �ǰ� ���� Ŭ����.
    [SerializeField] Transform infoPivot;                      // HP, �̸�, ����â�� ������.
    [SerializeField] int maxHp;

    [SerializeField] UnityEvent<DamageMessage> OnDamagedEvent; // �ŰԺ��� int���� �Լ�.
    [SerializeField] UnityEvent OnDeadEvent;                   // �ŰԺ��� ���� �Լ�.
    

    int hp;

    public Transform InfoPivot => infoPivot;
    public int MaxHp => maxHp;
    public int Hp => hp;

    void Start()
    {
        hp = maxHp;

        if(infoPivot != null)
            HpManager.Instance.Create(this);
    }

    public void OnDamaged(DamageMessage message)
    {
        Debug.Log(string.Format("{0}���Լ� {1}�� �������� ����", 
            message.attackerName,
            message.amount
            ));

        OnDamagedEvent?.Invoke(message);    // ������ ��ϵ� �̺�Ʈ ȣ��.
        hp -= message.amount;               // �� ü�� ����.
        if (hp <= 0)
        {
            hp = 0;
            OnDead();
        }
    }
    void OnDead()
    {
        OnDeadEvent?.Invoke();       // �̺�Ʈ ��������Ʈ�� Null�� �ƴϸ� ȣ���϶�.
    }
}
