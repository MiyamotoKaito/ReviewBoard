using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossDeath : MonoBehaviour
{
    [Header("フラッシュエフェクト設定")]
    public GameObject flashPanel; // 白いパネル（Canvas上のImage）
    public float flashDuration = 1.5f; // フラッシュの時間

    [Header("移行先シーン")]
    public string gameClearSceneName = "GameClear";

    private Image flashImage;
    private bool isTriggered = false;

    void Start()
    {
        // フラッシュパネルの初期設定
        if (flashPanel != null)
        {
            flashImage = flashPanel.GetComponent<Image>();
            if (flashImage != null)
            {
                // 最初は透明にしておく
                Color c = flashImage.color;
                c.a = 0f;
                flashImage.color = c;
                flashPanel.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (GameObject.FindWithTag("GameClear") != null && !isTriggered)
        {
            TriggerGameClear();
        }
        
    }

    public void TriggerGameClear()
    {

        if (isTriggered)
        {
            Debug.Log("既に発動済みのため処理をスキップします");
            return;
        }

        isTriggered = true;
        Debug.Log("フラッシュエフェクトを開始します");

        StartCoroutine(FlashAndTransition());
    }

    private IEnumerator FlashAndTransition()
    {
        Debug.Log("FlashAndTransition コルーチンが開始されました");
        Debug.Log($"現在のTime.timeScale: {Time.timeScale}");

        if (flashImage == null)
        {
            Debug.LogWarning("flashImageが設定されていません。すぐにシーン移行します");
            SceneManager.LoadScene(gameClearSceneName);
            yield break;
        }

        Debug.Log("フラッシュエフェクト開始");

        // フラッシュエフェクト開始
        float elapsedTime = 0f;
        Color startColor = flashImage.color;
        Color endColor = new Color(1f, 1f, 1f, 1f); // 白色（完全不透明）

        // フェードイン（白くなる）- unscaledDeltaTimeを使用
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Time.timeScaleの影響を受けない
            float progress = elapsedTime / flashDuration;

            // イージングカーブ（急激に白くなる）
            progress = progress * progress;

            flashImage.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }

        // 完全に白くなったことを確認
        flashImage.color = endColor;
        Debug.Log("フラッシュエフェクト完了。シーン移行準備中...");

        // 少し待ってからシーン移行 - WaitForSecondsRealtimeを使用
        yield return new WaitForSecondsRealtime(0.1f); // Time.timeScaleの影響を受けない

        // ゲームクリアシーンに移行
        Debug.Log($"シーン '{gameClearSceneName}' に移行します");
        SceneManager.LoadScene(gameClearSceneName);
    }
}

// 使用例：特定のオブジェクトがインスタンス化されたときに発動
public class ObjectInstantiationDetector : MonoBehaviour
{
    [Header("監視設定")]
    public GameObject targetPrefab; // 監視するプレハブ
    public bossDeath gameClearTrigger; // ゲームクリアトリガー

    private bool hasDetected = false;

    void Update()
    {
        if (hasDetected) return;

        // 特定のタグを持つオブジェクトが生成されたかチェック
        GameObject[] objects = GameObject.FindGameObjectsWithTag("ClearObject");

        if (objects.Length > 0)
        {
            hasDetected = true;
            Debug.Log("クリアオブジェクトが検出されました！");

            // ゲームクリアを発動
            if (gameClearTrigger != null)
            {
                gameClearTrigger.TriggerGameClear();
            }
        }
    }
}

// オブジェクト生成時に自動で通知する方法
public class InstantiatedObject : MonoBehaviour
{
    void Start()
    {
        // このオブジェクトがインスタンス化された瞬間に実行
        bossDeath trigger = FindObjectOfType<bossDeath>();
        if (trigger != null)
        {
            trigger.TriggerGameClear();
        }
    }
}