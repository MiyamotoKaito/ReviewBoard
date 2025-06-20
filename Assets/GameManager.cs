using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [Header("ステージボタン")]
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;
    [SerializeField] private Button BossButton;

    [Header("ステージクリア状態")]
    [SerializeField] private bool stage1Cleared = false;
    [SerializeField] private bool stage2Cleared = false;
    [SerializeField] private bool stage3Cleared = false;

    [Header("デバッグ設定")]
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
        // 通常のステージボタンの制御
        if (stage1Button != null) stage1Button.interactable = true;
        if (stage2Button != null) stage2Button.interactable = stage1Cleared;
        if (stage3Button != null) stage3Button.interactable = stage2Cleared;

        // BossButtonの表示制御
        if (BossButton != null)
        {
            // Stage3がクリアされている場合のみ表示
            BossButton.gameObject.SetActive(stage3Cleared);
            if (stage3Cleared)
            {
                BossButton.interactable = true;
            }
        }

        Debug.Log($"ボタン状態更新 - Stage2: {(stage2Button != null ? stage2Button.interactable.ToString() : "null")}, Stage3: {(stage3Button != null ? stage3Button.interactable.ToString() : "null")}, Boss表示: {stage3Cleared}");
    }

    public void ClearStage(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                stage1Cleared = true;
                Debug.Log("Stage1クリア！Stage2が解放されました");
                break;
            case 2:
                stage2Cleared = true;
                Debug.Log("Stage2クリア！Stage3が解放されました");
                break;
            case 3:
                stage3Cleared = true;
                Debug.Log("Stage3クリア！Bossボタンが表示されます");
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
        Debug.Log($"進行状況保存 - Stage1: {stage1Cleared}, Stage2: {stage2Cleared}, Stage3: {stage3Cleared}");
    }

    private void LoadStageProgress()
    {
        stage1Cleared = PlayerPrefs.GetInt("Stage1Cleared", 0) == 1;
        stage2Cleared = PlayerPrefs.GetInt("Stage2Cleared", 0) == 1;
        stage3Cleared = PlayerPrefs.GetInt("Stage3Cleared", 0) == 1;

        Debug.Log($"進行状況読み込み - Stage1: {stage1Cleared}, Stage2: {stage2Cleared}, Stage3: {stage3Cleared}");
        Debug.Log($"PlayerPrefs生値 - Stage1: {PlayerPrefs.GetInt("Stage1Cleared", -1)}, Stage2: {PlayerPrefs.GetInt("Stage2Cleared", -1)}, Stage3: {PlayerPrefs.GetInt("Stage3Cleared", -1)}");
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

        Debug.Log("ステージ進行状況をリセットしました");
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
        Debug.Log($"現在のPlayerPrefs - Stage1: {PlayerPrefs.GetInt("Stage1Cleared", -1)}, Stage2: {PlayerPrefs.GetInt("Stage2Cleared", -1)}, Stage3: {PlayerPrefs.GetInt("Stage3Cleared", -1)}");
    }
}