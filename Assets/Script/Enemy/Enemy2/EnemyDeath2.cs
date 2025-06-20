using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyDeath2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text enemyText;
    [SerializeField] GameObject clearUIPanel;
    [SerializeField] Button NextStageButton;
    [SerializeField] Button TitleButton;
    [SerializeField] int currentStageNumber = 2; // 追加：現在のステージ番号
    void Start()
    {
        CompleteStage();
        //敵を倒したら次のステージのボタンとタイトルボタンを表示
        if (enemyText != null)
        {
            enemyText.text = "天晴！！";
        }
        TitleButton.onClick.AddListener(() => OnclickTitleUI());

    }

    // Update is called once per frame
    void Update()
    {
        ShowCaseUI();
    }

    private void CompleteStage()
    {
        // PlayerPrefsでステージクリア状態を保存
        PlayerPrefs.SetInt($"Stage{currentStageNumber}Cleared", 1);
        PlayerPrefs.Save();

        Debug.Log($"ステージ{currentStageNumber}をクリアしました！");
    }

    /// <summary>
    /// 敵を倒したら黒帯を表示するメソッド
    /// </summary>
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
