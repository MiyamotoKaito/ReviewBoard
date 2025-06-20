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

    [Header("ステージクリア状態")]
    [SerializeField] private bool stage1Cleared = false;
    [SerializeField] private bool stage2Cleared = false;

    [Header("デバッグ設定")]
    [SerializeField] private bool resetProgressOnPlay = true; // エディタでプレイ時にリセットするかどうか

    private static bool hasResetThisPlaySession = false; // static変数で管理

    private void Start()
    {
#if UNITY_EDITOR
        // プレイモード開始時のみリセット（static変数で管理）
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
        // プレイモード開始時にstatic変数をリセット
        hasResetThisPlaySession = false;
    }
#endif

    // ステージボタンの有効/無効を更新
    private void UpdateStageButtons()
    {
        if (stage1Button != null) stage1Button.interactable = true;
        if (stage2Button != null) stage2Button.interactable = stage1Cleared;
        if (stage3Button != null) stage3Button.interactable = stage2Cleared;
    }

    // ステージクリア時に呼び出すメソッド
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

    // ステージ進行状況を保存
    private void SaveStageProgress()
    {
        PlayerPrefs.SetInt("Stage1Cleared", stage1Cleared ? 1 : 0);
        PlayerPrefs.SetInt("Stage2Cleared", stage2Cleared ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ステージ進行状況を読み込み
    private void LoadStageProgress()
    {
        stage1Cleared = PlayerPrefs.GetInt("Stage1Cleared", 0) == 1;
        stage2Cleared = PlayerPrefs.GetInt("Stage2Cleared", 0) == 1;

        Debug.Log($"読み込み結果 - Stage1Cleared: {stage1Cleared}, Stage2Cleared: {stage2Cleared}");
    }

    // ステージ進行状況をリセット
    private void ResetStageProgress()
    {
        // PlayerPrefsを明示的に0に設定してからSave
        PlayerPrefs.SetInt("Stage1Cleared", 0);
        PlayerPrefs.SetInt("Stage2Cleared", 0);
        PlayerPrefs.SetInt("Stage3Cleared", 0);
        PlayerPrefs.Save();

        // 変数も確実にfalseに設定
        stage1Cleared = false;
        stage2Cleared = false;

        Debug.Log("ステージ進行状況をリセットしました");
        Debug.Log($"リセット後の値確認 - Stage1: {PlayerPrefs.GetInt("Stage1Cleared", -1)}, Stage2: {PlayerPrefs.GetInt("Stage2Cleared", -1)}");
    }

    // インスペクターからも呼び出せるように
    [ContextMenu("Reset Stage Progress")]
    public void ResetStageProgressFromMenu()
    {
        ResetStageProgress();
        LoadStageProgress();
        UpdateStageButtons();
    }
}