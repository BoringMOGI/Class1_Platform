using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;

    public static event System.Action OnExit;

    SoundManager soundManager = null;
    
    void Start()
    {
        soundManager = SoundManager.Instance;

        bgmSlider.maxValue = GameData.MAX_BGM_VOLUMN;   // �����̴� ���� �ִ� ���� ���� ������ ������ ����.
        sfxSlider.maxValue = GameData.MAX_SFX_VOLUMN;   // �����̴� ���� �ִ� ���� ���� ������ ������ ����.

        bgmSlider.value = soundManager.BgmVolumn;       // �����̴� ���� �ʱ� ���� ���� �Ŵ������� ������ ����.
        sfxSlider.value = soundManager.SfxVolumn;       // �����̴� ���� �ʱ� ���� ���� �Ŵ������� ������ ����.
    }

    public void OnChangedVolumeBGM(float volumn)
    {
        soundManager.OnChangedVolumnBGM(volumn);
    }
    public void OnChangedVolumeSFX(float volumn)
    {
        soundManager.OnChangedVolumnSFX(volumn);
    }

    public void OnExitOption()
    {
        OnExit?.Invoke();           // â�� ������ ��ϵ� �̺�Ʈ ȣ��.
        OnExit = null;              // ��ϵ� �̺�Ʈ ��� ����.

        soundManager.OnSave();      // �ɼ� ����.

        // ���� ���� �����.
        SceneManager.UnloadSceneAsync("Option");
    }
}
