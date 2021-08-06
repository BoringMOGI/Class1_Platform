using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PoolManager<SoundManager, SFXObject>
{
    public enum BGM
    {
        Forest1,
    }

    [SerializeField] AudioSource audioSource;                   // ����Ŀ.
    [SerializeField] AudioClip[] bgms;                          // ������� ����� ����.
    [SerializeField] AudioClip[] sfxs;                          // ȿ���� ����� ����.

    private const string KEY_BGM_VOLUMNE = "bgmVolumn";         // ����� ũ�� Ű ��.
    private const string KEY_SFX_VOLUMNE = "sfxVolumn";         // ȿ���� ũ�� Ű ��.
    private const string KEY_SOUND_MANAGER = "SoundManager";    // ���� �Ŵ��� Ʈ����.

    private bool isSaved;                 // ���� ����.

    private float bgmVolume;              // ����� ũ��.
    private float sfxVolume;              // ȿ���� ũ��.

    public float BgmVolumn => bgmVolume;  // ����� ũ�� ������Ƽ (�б� ����)
    public float SfxVolumn => sfxVolume;  // ȿ���� ũ�� ������Ƽ (�б� ����)

    private void Start()
    {
        DataManager.OnSave += OnSave;
        DataManager.OnLoad += OnLoad;

        OnLoad();

        // ���ʿ� ������ �Ǿ��� ��.
        if (isSaved == false)
        {
            // �ʱ� ��.
            bgmVolume = GameData.MAX_BGM_VOLUMN;
            sfxVolume = GameData.MAX_SFX_VOLUMN;
        }

        audioSource.volume = bgmVolume;             // ����Ŀ ���� ����.
        DontDestroyOnLoad(gameObject);              // ���� �ε尡 �Ǿ Destory���� ����.
    }


    public void PlayBGM(BGM bgm)
    {
        PlayBGM(bgm.ToString());
    }
    public void PlayBGM(string bgmName)
    {
        for (int i = 0; i < bgms.Length; i++)       // ��� BGM �迭�� ��ȸ.
        {
            if(bgms[i].name == bgmName)             // i��° BGM�� �̸��� ���ٸ�
            {
                audioSource.clip = bgms[i];         // audioSource(����Ŀ)�� clip(CD)�� ����.
                audioSource.Play();                 // ��� ��ư.
                break;
            }
        }
    }

    public void PlaySFX(string sfxName)
    {
        for (int i = 0; i < sfxs.Length; i++)
        {
            if(sfxs[i].name == sfxName)                             // ��û�ϴ� �̸��� ���� ȿ������ ���� ���.
            {
                SFXObject sfx = GetPool();                          // ȿ���� �������� ����ҿ��� ������.
                sfx.PlaySFX(sfxs[i], sfxVolume);                    // ȿ���� ���.
                break;
            }
        }
    }


    public void OnChangedVolumnBGM(float volume)
    {
        bgmVolume = volume;
        audioSource.volume = bgmVolume / GameData.MAX_BGM_VOLUMN;
    }
    public void OnChangedVolumnSFX(float volume)
    {
        sfxVolume = volume;
    }

    public void OnSave()
    {
        Debug.Log("Sound Manager Saved!!");

        DataManager.SetFloat(KEY_BGM_VOLUMNE, bgmVolume);
        DataManager.SetFloat(KEY_SFX_VOLUMNE, sfxVolume);
        DataManager.SetBool(KEY_SOUND_MANAGER, true);
    }
    public void OnLoad()
    {
        bgmVolume = DataManager.GetFloat(KEY_BGM_VOLUMNE);
        sfxVolume = DataManager.GetFloat(KEY_SFX_VOLUMNE);
        isSaved   = DataManager.GetBool(KEY_SOUND_MANAGER);
    }
}
