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
    [SerializeField] private Button BossButton;

    [Header("�X�e�[�W�N���A���")]
    [SerializeField] private bool stage1Cleared = false;
    [SerializeField] private bool stage2Cleared = false;
    [SerializeField] private bool stage3Cleared = false;

    [Header("�f�o�b�O�ݒ�")]
    [SerializeField] private bool resetProgressOnPlay = false;
    private static bool hasResetThisPlaySession = false;

    private void Start()
    {
#if UNITY_EDITOR
        if (resetProgressOnPlay && !hasResetThisPlaySession)
        {
            ResetStageProgress();
            hasResetThisPlaySession = true;
        }
#endif
        LoadStageProgress();
        UpdateStageButtons();

        Debug.Log($"GameManager Start - Stage1: {stage1Cleared}, Stage2: {stage2Cleared}, Stage3: {stage3Cleared}");
    }

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStaticVariables()
    {
        hasResetThisPlaySession = false;
    }
#endif

    private void UpdateStageButtons()
    {
        // �ʏ�̃X�e�[�W�{�^���̐���
        if (stage1Button != null) stage1Button.interactable = true;
        if (stage2Button != null) stage2Button.interactable = stage1Cleared;
        if (stage3Button != null) stage3Button.interactable = stage2Cleared;

        // BossButton�̕\������
        if (BossButton != null)
        {
            // Stage3���N���A����Ă���ꍇ�̂ݕ\��
            BossButton.gameObject.SetActive(stage3Cleared);
            if (stage3Cleared)
            {
                BossButton.interactable = true;
            }
        }

        Debug.Log($"�{�^����ԍX�V - Stage2: {(stage2Button != null ? stage2Button.interactable.ToString() : "null")}, Stage3: {(stage3Button != null ? stage3Button.interactable.ToString() : "null")}, Boss�\��: {stage3Cleared}");
    }

    public void ClearStage(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                stage1Cleared = true;
                Debug.Log("Stage1�N���A�IStage2���������܂���");
                break;
            case 2:
                stage2Cleared = true;
                Debug.Log("Stage2�N���A�IStage3���������܂���");
                break;
            case 3:
                stage3Cleared = true;
                Debug.Log("Stage3�N���A�IBoss�{�^�����\������܂�");
                break;
        }
        UpdateStageButtons();
        SaveStageProgress();
    }

    private void SaveStageProgress()
    {
        PlayerPrefs.SetInt("Stage1Cleared", stage1Cleared ? 1 : 0);
        PlayerPrefs.SetInt("Stage2Cleared", stage2Cleared ? 1 : 0);
        PlayerPrefs.SetInt("Stage3Cleared", stage3Cleared ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log($"�i�s�󋵕ۑ� - Stage1: {stage1Cleared}, Stage2: {stage2Cleared}, Stage3: {stage3Cleared}");
    }

    private void LoadStageProgress()
    {
        stage1Cleared = PlayerPrefs.GetInt("Stage1Cleared", 0) == 1;
        stage2Cleared = PlayerPrefs.GetInt("Stage2Cleared", 0) == 1;
        stage3Cleared = PlayerPrefs.GetInt("Stage3Cleared", 0) == 1;

        Debug.Log($"�i�s�󋵓ǂݍ��� - Stage1: {stage1Cleared}, Stage2: {stage2Cleared}, Stage3: {stage3Cleared}");
        Debug.Log($"PlayerPrefs���l - Stage1: {PlayerPrefs.GetInt("Stage1Cleared", -1)}, Stage2: {PlayerPrefs.GetInt("Stage2Cleared", -1)}, Stage3: {PlayerPrefs.GetInt("Stage3Cleared", -1)}");
    }

    private void ResetStageProgress()
    {
        PlayerPrefs.SetInt("Stage1Cleared", 0);
        PlayerPrefs.SetInt("Stage2Cleared", 0);
        PlayerPrefs.SetInt("Stage3Cleared", 0);
        PlayerPrefs.Save();

        stage1Cleared = false;
        stage2Cleared = false;
        stage3Cleared = false;

        Debug.Log("�X�e�[�W�i�s�󋵂����Z�b�g���܂���");
    }

    [ContextMenu("Reset Stage Progress")]
    public void ResetStageProgressFromMenu()
    {
        ResetStageProgress();
        LoadStageProgress();
        UpdateStageButtons();
    }

    [ContextMenu("Check PlayerPrefs")]
    public void CheckPlayerPrefs()
    {
        Debug.Log($"���݂�PlayerPrefs - Stage1: {PlayerPrefs.GetInt("Stage1Cleared", -1)}, Stage2: {PlayerPrefs.GetInt("Stage2Cleared", -1)}, Stage3: {PlayerPrefs.GetInt("Stage3Cleared", -1)}");
    }
}