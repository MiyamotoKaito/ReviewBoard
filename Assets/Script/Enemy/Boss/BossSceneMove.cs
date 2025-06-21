using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using UnityEngine.UIElements; ← この行を削除

public class BossSceneMove : MonoBehaviour
{
    [SerializeField] private Text enemyText;
    [SerializeField] GameObject clearUIPanel;
    [SerializeField] Button BossButton;

    void Start()
    {
        Cursor.visible = true;
        // 初期状態ではUIを非表示
        if (clearUIPanel != null)
        {
            clearUIPanel.SetActive(false);
        }

        // ボタンのクリックイベントを設定
        if (BossButton != null)
        {
            BossButton.onClick.AddListener(() => OnclickTitleUI());
        }

        // テキスト設定
        if (enemyText != null)
        {
            enemyText.text = "ｶｯ!!";
        }

        // 少し遅れてUIを表示（ボスが死んだ演出の後）
        StartCoroutine(ShowUIAfterDelay(0f));
    }

    private IEnumerator ShowUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowCaseUI();
    }

    private void CompleteStage()
    {
        Debug.Log("ステージクリア処理");
        PlayerPrefs.Save();
    }

    private void ShowCaseUI()
    {
        if (clearUIPanel != null)
        {
            Debug.Log("クリアUI表示");
            clearUIPanel.SetActive(true);
            CompleteStage();
        }
    }

    private void OnclickTitleUI()
    {
        Debug.Log("次のシーンへ移動");
        Time.timeScale = 1f;
        SceneManager.LoadScene("BossBattle");
    }
}