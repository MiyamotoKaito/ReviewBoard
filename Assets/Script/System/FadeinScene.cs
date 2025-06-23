using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeIn : MonoBehaviour
{
    [Header("�t�F�[�h�ݒ�")]
    public Image fadeImage; // �����p�l���iCanvas���Image�j
    public float fadeInDuration = 2f; // �t�F�[�h�C������
    public Color fadeColor = Color.black; // �t�F�[�h�F�i���j

    void Start()
    {
        StartFadeIn();
    }

    void StartFadeIn()
    {
        if (fadeImage == null)
        {
            Debug.LogWarning("fadeImage ���ݒ肳��Ă��܂���");
            return;
        }

        // �ŏ��͊��S�ɕs�����i����ʁj
        fadeColor.a = 1f;
        fadeImage.color = fadeColor;
        fadeImage.gameObject.SetActive(true);

        // �t�F�[�h�C���J�n
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;
        Color startColor = fadeColor; // ���S�s����
        Color endColor = fadeColor;   // ���S����
        endColor.a = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // timeScale�̉e�����󂯂Ȃ�
            float progress = elapsedTime / fadeInDuration;

            // �X���[�Y�ȃC�[�W���O
            progress = Mathf.SmoothStep(0f, 1f, progress);

            fadeImage.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }

        // ���S�ɓ����ɂ��Ĕ�\��
        fadeImage.color = endColor;
        fadeImage.gameObject.SetActive(false);
    }
}