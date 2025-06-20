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
    [SerializeField] private bool stage3Cleared = false;

    private void Start()
    {
        UpdateStageButtons();
    }

    // �X�e�[�W�{�^���̗L��/�������X�V
    private void UpdateStageButtons()
    {
        // �X�e�[�W1�͏�ɗL��
        stage1Button.interactable = true;

        // �X�e�[�W2�̓X�e�[�W1�N���A��ɗL��
        stage2Button.interactable = stage1Cleared;

        // �X�e�[�W3�̓X�e�[�W2�N���A��ɗL��
        stage3Button.interactable = stage2Cleared;
    }

    // �X�e�[�W�N���A���ɌĂяo�����\�b�h
    public void ClearStage(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                stage1Cleared = true;
                Debug.Log("�X�e�[�W1�N���A�I");
                break;
            case 2:
                stage2Cleared = true;
                Debug.Log("�X�e�[�W2�N���A�I");
                break;
            case 3:
                stage3Cleared = true;
                Debug.Log("�X�e�[�W3�N���A�I");
                break;
        }

        UpdateStageButtons();

        // �f�[�^��ۑ��i����N�������L���ɂ���j
        SaveStageProgress();
    }

    // �X�e�[�W�i�s�󋵂�ۑ�
    private void SaveStageProgress()
    {
        PlayerPrefs.SetInt("Stage1Cleared", stage1Cleared ? 1 : 0);
        PlayerPrefs.SetInt("Stage2Cleared", stage2Cleared ? 1 : 0);
        PlayerPrefs.SetInt("Stage3Cleared", stage3Cleared ? 1 : 0);
        PlayerPrefs.Save();
    }

    // �X�e�[�W�i�s�󋵂�ǂݍ���
    private void LoadStageProgress()
    {
        stage1Cleared = PlayerPrefs.GetInt("Stage1Cleared", 0) == 1;
        stage2Cleared = PlayerPrefs.GetInt("Stage2Cleared", 0) == 1;
        stage3Cleared = PlayerPrefs.GetInt("Stage3Cleared", 0) == 1;
    }

    private void Awake()
    {
        LoadStageProgress();
    }
}