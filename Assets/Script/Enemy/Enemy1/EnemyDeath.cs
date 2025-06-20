using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyDeath : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text enemyText;
    [SerializeField] GameObject clearUIPanel;
    [SerializeField] Button TitleButton;
    [SerializeField] int currentStageNumber = 1; // �ǉ��F���݂̃X�e�[�W�ԍ�
    void Start()
    {
        CompleteStage();
        if (enemyText != null)
        {
            enemyText.text = "���͂��I�I";
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
        // PlayerPrefs�ŃX�e�[�W�N���A��Ԃ�ۑ�
        PlayerPrefs.SetInt($"Stage{currentStageNumber}Cleared", 1);
        PlayerPrefs.Save();

        Debug.Log($"�X�e�[�W{currentStageNumber}���N���A���܂����I");
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
