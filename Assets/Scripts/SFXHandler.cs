using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXHandler : MonoBehaviour
{
    [SerializeField] string sfxName;

    private void Start()
    {
        Button button = GetComponent<Button>();                 // �����Լ� Button �˻�
        if(button == null)                                      // ��ư�� ���� ���.
        {
            Debug.Log(name + " is not have button!");           // �α� ���.
            enabled = false;                                    // ��ũ���� ����.
            return;
        }

        button.onClick.AddListener(OnPlaySFX);                  // ��ư Ŭ�� �̺�Ʈ onClick�� �̺�Ʈ ���.
    }

    public void OnPlaySFX()
    {
        SoundManager.Instance.PlaySFX(sfxName);
    }
}
