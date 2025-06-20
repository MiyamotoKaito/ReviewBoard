using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("�X�e�[�W�{�^��")]
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;

    [Header("�X�e�[�W�N���A���")]
    [SerializeField] private bool stage1Cleared = false;
    [SerializeField] private bool stage2Cleared = false;

    [Header("�f�o�b�O�ݒ�")]
    [SerializeField] private bool resetProgressOnPlay = true; // �G�f�B�^�Ńv���C���Ƀ��Z�b�g���邩�ǂ���

    private void Start()
    {
#if UNITY_EDITOR
        // �v���C���[�h�J�n���̂݃��Z�b�g�i�ŏ���1�񂾂��j
        if (resetProgressOnPlay && !HasResetThisSession())
        {
            ResetStageProgress();
            MarkResetForThisSession();
        }
#endif
        LoadStageProgress();
        UpdateStageButtons();
    }

    // �X�e�[�W�{�^���̗L��/�������X�V
    private void UpdateStageButtons()
    {
        if (stage1Button != null) stage1Button.interactable = true;
        if (stage2Button != null) stage2Button.interactable = stage1Cleared;
        if (stage3Button != null) stage3Button.interactable = stage2Cleared;
    }

    // �X�e�[�W�N���A���ɌĂяo�����\�b�h
    public void ClearStage(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                stage1Cleared = true;
                break;
            case 2:
                stage2Cleared = true;
                break;
        }

        UpdateStageButtons();
        SaveStageProgress();
    }

    // �X�e�[�W�i�s�󋵂�ۑ�
    private void SaveStageProgress()
    {
        PlayerPrefs.SetInt("Stage1Cleared", stage1Cleared ? 1 : 0);
        PlayerPrefs.SetInt("Stage2Cleared", stage2Cleared ? 1 : 0);
        PlayerPrefs.Save();
    }

    // �X�e�[�W�i�s�󋵂�ǂݍ���
    private void LoadStageProgress()
    {
        stage1Cleared = PlayerPrefs.GetInt("Stage1Cleared", 0) == 1;
        stage2Cleared = PlayerPrefs.GetInt("Stage2Cleared", 0) == 1;
    }

    // �X�e�[�W�i�s�󋵂����Z�b�g
    private void ResetStageProgress()
    {
        PlayerPrefs.DeleteKey("Stage1Cleared");
        PlayerPrefs.DeleteKey("Stage2Cleared");
        PlayerPrefs.DeleteKey("Stage3Cleared"); // �����I�ɒǉ������\�����l��
        PlayerPrefs.Save();

        stage1Cleared = false;
        stage2Cleared = false;

        Debug.Log("�X�e�[�W�i�s�󋵂����Z�b�g���܂���");
    }

    // ���̃Z�b�V�����Ń��Z�b�g�ς݂��`�F�b�N
    private bool HasResetThisSession()
    {
        return SessionState.GetBool("StageProgressReset", false);
    }

    // ���̃Z�b�V�����Ń��Z�b�g�ς݂Ƃ��ă}�[�N
    private void MarkResetForThisSession()
    {
        SessionState.SetBool("StageProgressReset", true);
    }

    // �C���X�y�N�^�[������Ăяo����悤��
    [ContextMenu("Reset Stage Progress")]
    public void ResetStageProgressFromMenu()
    {
        ResetStageProgress();
        LoadStageProgress();
        UpdateStageButtons();
    }
}