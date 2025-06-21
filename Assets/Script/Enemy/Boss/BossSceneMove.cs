using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using UnityEngine.UIElements; �� ���̍s���폜

public class BossSceneMove : MonoBehaviour
{
    [SerializeField] private Text enemyText;
    [SerializeField] GameObject clearUIPanel;
    [SerializeField] Button BossButton;

    void Start()
    {
        Cursor.visible = true;
        // ������Ԃł�UI���\��
        if (clearUIPanel != null)
        {
            clearUIPanel.SetActive(false);
        }

        // �{�^���̃N���b�N�C�x���g��ݒ�
        if (BossButton != null)
        {
            BossButton.onClick.AddListener(() => OnclickTitleUI());
        }

        // �e�L�X�g�ݒ�
        if (enemyText != null)
        {
            enemyText.text = "��!!";
        }

        // �����x���UI��\���i�{�X�����񂾉��o�̌�j
        StartCoroutine(ShowUIAfterDelay(0f));
    }

    private IEnumerator ShowUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowCaseUI();
    }

    private void CompleteStage()
    {
        Debug.Log("�X�e�[�W�N���A����");
        PlayerPrefs.Save();
    }

    private void ShowCaseUI()
    {
        if (clearUIPanel != null)
        {
            Debug.Log("�N���AUI�\��");
            clearUIPanel.SetActive(true);
            CompleteStage();
        }
    }

    private void OnclickTitleUI()
    {
        Debug.Log("���̃V�[���ֈړ�");
        Time.timeScale = 1f;
        SceneManager.LoadScene("BossBattle");
    }
}