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
    void Start()
    {
        //�G��|�����玟�̃X�e�[�W�̃{�^���ƃ^�C�g���{�^����\��
        if (enemyText != null)
        {
            enemyText.text = "�V���I�I";
        }
        NextStageButton.onClick.AddListener(() => OnclickNextStageUI());
        TitleButton.onClick.AddListener(() => OnclickTitleUI());

    }

    // Update is called once per frame
    void Update()
    {
        ShowCaseUI();
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

    private void OnclickNextStageUI()
    {
        Time.timeScale = 1f;
        int NextStage = SceneManager.GetActiveScene().buildIndex + 2;
        SceneManager.LoadScene(NextStage);
    }

    private void OnclickTitleUI()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
