using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singletone<SoundManager>
{
    [SerializeField] AudioSource audioSource;                   // ����Ŀ.
    [SerializeField] AudioClip[] bgms;                          // ���� ����� ���� �迭.

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
        if(isSaved == false)
        {
            // �ʱ� ��.
            bgmVolume = GameData.MAX_BGM_VOLUMN;
            sfxVolume = GameData.MAX_SFX_VOLUMN;
        }

        DontDestroyOnLoad(gameObject);              // ���� �ε尡 �Ǿ Destory���� ����.
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

    public void OnChangedVolumnBGM(float volume)
    {
        bgmVolume = volume;
        audioSource.volume = bgmVolume / GameData.MAX_BGM_VOLUMN;
    }
    public void OnChangedVolumnSFX(float volume)
    {
        sfxVolume = volume;
    }

    void OnSave()
    {
        DataManager.SetFloat(KEY_BGM_VOLUMNE, bgmVolume);
        DataManager.SetFloat(KEY_SFX_VOLUMNE, sfxVolume);
        DataManager.SetBool(KEY_SOUND_MANAGER, true);
    }
    void OnLoad()
    {
        bgmVolume = DataManager.GetFloat(KEY_BGM_VOLUMNE);
        sfxVolume = DataManager.GetFloat(KEY_SFX_VOLUMNE);
        isSaved   = DataManager.GetBool(KEY_SOUND_MANAGER);
    }
}
