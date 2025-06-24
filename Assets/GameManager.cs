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

    // WebGL�p�̃Z�b�V�����Ǘ��i���P�Łj
    private const string SESSION_KEY = "GameSession";
    private const string SESSION_VALID_KEY = "SessionValid";
    private static string currentSessionId;

    // �V���O���g���p�^�[����GameManager�̏d����h��
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        // �V���O���g���p�^�[���̎���
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        // �V�[���J�ڎ���GameManager��ێ��i�K�v�ɉ����āj
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
#if UNITY_EDITOR
        if (resetProgressOnPlay && !hasResetThisPlaySession)
        {
            ResetStageProgress();
            hasResetThisPlaySession = true;
        }
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL�r���h�̏ꍇ�A�u���E�U�����[�h���o�i���P�Łj
        CheckForBrowserReload();
#endif

        LoadStageProgress();
        UpdateStageButtons();

        Debug.Log($"GameManager Start - Stage1: {stage1Cleared}, Stage2: {stage2Cleared}, Stage3: {stage3Cleared}");
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    private void CheckForBrowserReload()
    {
        // �Z�b�V�����L�����t���O���`�F�b�N
        bool isSessionValid = PlayerPrefs.GetInt(SESSION_VALID_KEY, 0) == 1;
        
        // �V�����Z�b�V����ID�𐶐�
        string newSessionId = System.DateTime.Now.Ticks.ToString();
        
        // �O��̃Z�b�V����ID���擾
        string savedSessionId = PlayerPrefs.GetString(SESSION_KEY, "");
        
        // �^�̃u���E�U�����[�h���ǂ����𔻒�i��茵���Ɂj
        bool isTrueBrowserReload = string.IsNullOrEmpty(savedSessionId) || !isSessionValid;
        
        if (isTrueBrowserReload)
        {
            Debug.Log("�^�̃u���E�U�����[�h�����o - �X�e�[�W�i�s�󋵂����Z�b�g");
            ResetStageProgress();
        }
        else
        {
            Debug.Log("�ʏ�̃V�[���J�ڂ����o - �i�s�󋵂�ێ�");
        }
        
        // ���݂̃Z�b�V����ID��ۑ����A�Z�b�V������L���ɐݒ�
        currentSessionId = newSessionId;
        PlayerPrefs.SetString(SESSION_KEY, currentSessionId);
        PlayerPrefs.SetInt(SESSION_VALID_KEY, 1);
        PlayerPrefs.Save();
    }
#endif

    private void OnApplicationFocus(bool hasFocus)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL�Ńt�H�[�J�X���������ꍇ�̏��������P
        if (!hasFocus)
        {
            Debug.Log("�A�v���P�[�V�������t�H�[�J�X�������܂���");
            // �����ɃZ�b�V�����𖳌��������A�x�������Ŕ���
            StartCoroutine(DelayedSessionInvalidation());
        }
        else
        {
            Debug.Log("�A�v���P�[�V�������t�H�[�J�X���擾���܂���");
            // �t�H�[�J�X�����߂����ꍇ�̓Z�b�V������L���ɖ߂�
            PlayerPrefs.SetInt(SESSION_VALID_KEY, 1);
            PlayerPrefs.Save();
        }
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    private System.Collections.IEnumerator DelayedSessionInvalidation()
    {
        // �Z���ԑҋ@���Ă���Z�b�V�����������𔻒�
        yield return new WaitForSeconds(0.5f);
        
        // �܂��t�H�[�J�X���߂��Ă��Ȃ��ꍇ�̂݃Z�b�V�����𖳌���
        if (!Application.isFocused)
        {
            PlayerPrefs.SetInt(SESSION_VALID_KEY, 0);
            PlayerPrefs.Save();
            Debug.Log("�Z�b�V�����𖳌������܂���");
        }
    }
#endif

    private void OnApplicationPause(bool pauseStatus)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL�Ń|�[�Y��ԂɂȂ����ꍇ�̏��������P
        if (pauseStatus)
        {
            Debug.Log("�A�v���P�[�V�������|�[�Y����܂���");
            // �����ɖ����������A�����҂�
            StartCoroutine(DelayedSessionInvalidation());
        }
        else
        {
            Debug.Log("�A�v���P�[�V�����̃|�[�Y����������܂���");
            PlayerPrefs.SetInt(SESSION_VALID_KEY, 1);
            PlayerPrefs.Save();
        }
#endif
    }

    // �V�[���J�ڎ��ɃZ�b�V�������ێ����郁�\�b�h
    public void OnSceneTransition()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // �ʏ�̃V�[���J�ڂł��邱�Ƃ𖾎��I�ɋL�^
        PlayerPrefs.SetInt(SESSION_VALID_KEY, 1);
        PlayerPrefs.Save();
        Debug.Log("�V�[���J��: �Z�b�V������L���ɕێ�");
#endif
    }

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStaticVariables()
    {
        hasResetThisPlaySession = false;
        currentSessionId = null;
        instance = null;
    }
#endif

    private void UpdateStageButtons()
    {
        // �{�^����null�łȂ����`�F�b�N���Ă��瑀��
        if (stage1Button != null)
        {
            stage1Button.interactable = true;
        }

        if (stage2Button != null)
        {
            stage2Button.interactable = stage1Cleared;
        }

        if (stage3Button != null)
        {
            stage3Button.interactable = stage2Cleared;
        }

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
        Debug.Log($"�Z�b�V������� - SessionValid: {PlayerPrefs.GetInt(SESSION_VALID_KEY, -1)}, SessionID: {PlayerPrefs.GetString(SESSION_KEY, "none")}");
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}