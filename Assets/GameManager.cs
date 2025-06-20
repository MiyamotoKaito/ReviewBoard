using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
#if UNITY_EDITOR
        // プレイモード開始時のみリセット（最初の1回だけ）
        if (resetProgressOnPlay && !HasResetThisSession())
        {
            ResetStageProgress();
            MarkResetForThisSession();
        }
#endif
        LoadStageProgress();
        UpdateStageButtons();
    }

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
    }

    // ステージ進行状況をリセット
    private void ResetStageProgress()
    {
        PlayerPrefs.DeleteKey("Stage1Cleared");
        PlayerPrefs.DeleteKey("Stage2Cleared");
        PlayerPrefs.DeleteKey("Stage3Cleared"); // 将来的に追加される可能性も考慮
        PlayerPrefs.Save();

        stage1Cleared = false;
        stage2Cleared = false;

        Debug.Log("ステージ進行状況をリセットしました");
    }

    // このセッションでリセット済みかチェック
    private bool HasResetThisSession()
    {
        return SessionState.GetBool("StageProgressReset", false);
    }

    // このセッションでリセット済みとしてマーク
    private void MarkResetForThisSession()
    {
        SessionState.SetBool("StageProgressReset", true);
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