using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Text enemyText;
    [SerializeField] GameObject clearUIPanel;
    [SerializeField] Button TitleButton;
    [SerializeField] int currentStageNumber = 1; // 現在のステージ番号

    void Start()
    {
        CompleteStage();
        if (enemyText != null)
        {
            enemyText.text = "ぐはっ！！";
        }
        TitleButton.onClick.AddListener(() => OnclickTitleUI());
    }

    void Update()
    {
        ShowCaseUI();
    }

    private void CompleteStage()
    {
        // 直接PlayerPrefsに保存する方法
        PlayerPrefs.SetInt($"Stage{currentStageNumber}Cleared", 1);
        PlayerPrefs.Save();
        Debug.Log($"ステージ{currentStageNumber}をクリアしました！");

        // 保存されたかを確認
        int savedValue = PlayerPrefs.GetInt($"Stage{currentStageNumber}Cleared", 0);
        Debug.Log($"保存確認 - Stage{currentStageNumber}Cleared: {savedValue}");
    }

    private void ShowCaseUI()
    {
        if (clearUIPanel != null)
        {
            clearUIPanel.SetActive(true);
        }
    }

    private void OnclickTitleUI()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StageSelect");
    }
}