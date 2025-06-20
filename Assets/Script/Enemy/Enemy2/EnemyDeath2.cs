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
    [SerializeField] int currentStageNumber = 2; // �ǉ��F���݂̃X�e�[�W�ԍ�
    void Start()
    {
        CompleteStage();
        //�G��|�����玟�̃X�e�[�W�̃{�^���ƃ^�C�g���{�^����\��
        if (enemyText != null)
        {
            enemyText.text = "�V���I�I";
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

    /// <summary>
    /// �G��|�����獕�т�\�����郁�\�b�h
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
