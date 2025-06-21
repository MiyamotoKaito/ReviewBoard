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
    [SerializeField] int currentStageNumber = 1; // ���݂̃X�e�[�W�ԍ�

    void Start()
    {
        CompleteStage();
        if (enemyText != null)
        {
            enemyText.text = "���͂��I�I";
        }
        TitleButton.onClick.AddListener(() => OnclickTitleUI());
    }

    void Update()
    {
        ShowCaseUI();
    }

    private void CompleteStage()
    {
        // ����PlayerPrefs�ɕۑ�������@
        PlayerPrefs.SetInt($"Stage{currentStageNumber}Cleared", 1);
        PlayerPrefs.Save();
        Debug.Log($"�X�e�[�W{currentStageNumber}���N���A���܂����I");

        // �ۑ����ꂽ�����m�F
        int savedValue = PlayerPrefs.GetInt($"Stage{currentStageNumber}Cleared", 0);
        Debug.Log($"�ۑ��m�F - Stage{currentStageNumber}Cleared: {savedValue}");
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