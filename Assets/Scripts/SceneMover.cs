using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMover : Singletone<SceneMover>
{
    [SerializeField] Image blindImage;      // ������.
    
    void Start()
    {
        blindImage.enabled = false;
        DontDestroyOnLoad(gameObject);
    }

    bool isMoving;      // �� �ε� ����.
    bool isOpenOption;  // �ɼ� â ���� ����.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenOption();
        }
    }

    public void MoveScene(string sceneName)
    {
        // �̵� �߿��� ��û�� ���´�.
        if (isMoving)
            return;

        isMoving = true;
        StartCoroutine(MoveTo(sceneName));      // �� �̵� �ڷ�ƾ ȣ��.
    }
    public void OpenOption()
    {
        if (!isOpenOption)
        {
            isOpenOption = true;
            OptionManager.OnExit += () => { isOpenOption = false; };
            SceneManager.LoadScene("Option", LoadSceneMode.Additive);
        }
    }

    IEnumerator MoveTo(string sceneName)
    {
        yield return StartCoroutine(FadeOut()); // FadeOut �ڷ�ƾ�� ���������� ��ٸ���.
        SceneManager.LoadScene(sceneName);

        bool isLoading = true;

        // �̺�Ʈ ���. �ε��� ������ ������ ȣ��.
        // ���ٽ����� �̺�Ʈ�� ���.
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {
            isLoading = false;
        };

        // isLoading�� true�� ��� ���� ���.
        while (isLoading)
            yield return null;

        // �� �ε��� ����.
        yield return StartCoroutine(FadeIn());  // FadeIn �ڷ�ƾ�� ���������� ��ٸ���.
        isMoving = false;
    }

    IEnumerator FadeOut()           // ȭ���� ���� ����������.
    {
        blindImage.enabled = true;
        blindImage.color = new Color(0, 0, 0, 0);   // ������, ������ 0.

        const float fadeTime = 1.0f;
        float time = 0.0f;

        while(true)
        {
            time += Time.deltaTime;
            if (time >= fadeTime)
                break;

            blindImage.color = new Color(0, 0, 0, time / fadeTime);
            yield return null;
        }

        blindImage.color = Color.black;
    }
    IEnumerator FadeIn()        // ȭ���� ���� �������.
    {
        blindImage.enabled = true;
        blindImage.color = new Color(0, 0, 0, 0);   // ������, ������ 0.

        const float fadeTime = 1.0f;
        float time = fadeTime;

        while (true)
        {
            time -= Time.deltaTime;
            if (time <= 0.0f)
                break;

            blindImage.color = new Color(0, 0, 0, time / fadeTime);
            yield return null;
        }

        blindImage.color = new Color(0, 0, 0, 0);
        blindImage.enabled = false;
    }
    
}
