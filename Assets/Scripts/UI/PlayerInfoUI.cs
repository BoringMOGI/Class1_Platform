using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui���� Ŭ����.

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] Text playerText;
    [SerializeField] Image[] hpImages;

    public void UpdateHp(int amount)
    {
        for(int i = 0; i<hpImages.Length; i++)
        {
            bool isEnable = i < amount;
            hpImages[i].enabled = isEnable;
        }
    }
    public void SetName(string name)
    {
        playerText.text = name;
    }
}
