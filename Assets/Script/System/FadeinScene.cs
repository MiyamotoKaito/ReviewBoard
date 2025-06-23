using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeIn : MonoBehaviour
{
    [Header("フェード設定")]
    public Image fadeImage; // 黒いパネル（Canvas上のImage）
    public float fadeInDuration = 2f; // フェードイン時間
    public Color fadeColor = Color.black; // フェード色（黒）

    void Start()
    {
        StartFadeIn();
    }

    void StartFadeIn()
    {
        if (fadeImage == null)
        {
            Debug.LogWarning("fadeImage が設定されていません");
            return;
        }

        // 最初は完全に不透明（黒画面）
        fadeColor.a = 1f;
        fadeImage.color = fadeColor;
        fadeImage.gameObject.SetActive(true);

        // フェードイン開始
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;
        Color startColor = fadeColor; // 完全不透明
        Color endColor = fadeColor;   // 完全透明
        endColor.a = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // timeScaleの影響を受けない
            float progress = elapsedTime / fadeInDuration;

            // スムーズなイージング
            progress = Mathf.SmoothStep(0f, 1f, progress);

            fadeImage.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }

        // 完全に透明にして非表示
        fadeImage.color = endColor;
        fadeImage.gameObject.SetActive(false);
    }
}