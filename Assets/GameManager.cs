using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

    private static bool hasResetThisPlaySession = false; // static�ϐ��ŊǗ�

    private void Start()
    {
#if UNITY_EDITOR
        // �v���C���[�h�J�n���̂݃��Z�b�g�istatic�ϐ��ŊǗ��j
        if (resetProgressOnPlay && !hasResetThisPlaySession)
        {
            ResetStageProgress();
            hasResetThisPlaySession = true;
        }
#endif
        LoadStageProgress();
        UpdateStageButtons();
    }

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStaticVariables()
    {
        // �v���C���[�h�J�n����static�ϐ������Z�b�g
        hasResetThisPlaySession = false;
    }
#endif

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

        Debug.Log($"�ǂݍ��݌��� - Stage1Cleared: {stage1Cleared}, Stage2Cleared: {stage2Cleared}");
    }

    // �X�e�[�W�i�s�󋵂����Z�b�g
    private void ResetStageProgress()
    {
        // PlayerPrefs�𖾎��I��0�ɐݒ肵�Ă���Save
        PlayerPrefs.SetInt("Stage1Cleared", 0);
        PlayerPrefs.SetInt("Stage2Cleared", 0);
        PlayerPrefs.SetInt("Stage3Cleared", 0);
        PlayerPrefs.Save();

        // �ϐ����m����false�ɐݒ�
        stage1Cleared = false;
        stage2Cleared = false;

        Debug.Log("�X�e�[�W�i�s�󋵂����Z�b�g���܂���");
        Debug.Log($"���Z�b�g��̒l�m�F - Stage1: {PlayerPrefs.GetInt("Stage1Cleared", -1)}, Stage2: {PlayerPrefs.GetInt("Stage2Cleared", -1)}");
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