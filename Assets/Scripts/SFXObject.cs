using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AudioSource��� ���۳�Ʈ�� ������ �ش� ��ũ��Ʈ�� Add �� �� ����.
// �ش� ��ũ��Ʈ�� �����ϴ� �� AudioSource ���۳�Ʈ�� Remove�� �� ����.
[RequireComponent(typeof(AudioSource))]
public class SFXObject : MonoBehaviour, IPool<SFXObject>
{
    AudioSource speaker;
    System.Action<SFXObject> OnReturnPool;

    public void Setup(Action<SFXObject> OnReturnPool)
    {
        this.OnReturnPool = OnReturnPool;               // ����ҷ� ���ư��� ��������Ʈ ����.
        speaker = GetComponent<AudioSource>();          // �� ������Ʈ���� AudioSource ���۳�Ʈ �˻� �� ����.
    }
    public void PlaySFX(AudioClip clip, float volumn)
    {
        speaker.clip = clip;                            // �� ����Ŀ �ȿ� �ŰԺ��� clip�� ����.
        speaker.volume = volumn;                        // ����Ŀ�� ������ ����.
        speaker.Play();                                 // ȿ���� ���.

        StartCoroutine(CheckPlaying());                 // �ڷ�ƾ ȣ��.
    }

    

    IEnumerator CheckPlaying()
    {
        while (speaker.isPlaying)                       // ����Ŀ�� ��� ���� ��� ��.
            yield return null;

        //Destroy(gameObject);                          // �� �ڽ��� ����.
        OnReturnPool?.Invoke(this);                     // �� �ڽ��� ����ҷ� ��ȯ.
    }
}
