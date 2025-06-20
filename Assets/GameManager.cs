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
    [SerializeField] private bool stage3Cleared = false;

    private void Start()
    {
        UpdateStageButtons();
    }

    // ステージボタンの有効/無効を更新
    private void UpdateStageButtons()
    {
        // ステージ1は常に有効
        stage1Button.interactable = true;

        // ステージ2はステージ1クリア後に有効
        stage2Button.interactable = stage1Cleared;

        // ステージ3はステージ2クリア後に有効
        stage3Button.interactable = stage2Cleared;
    }

    // ステージクリア時に呼び出すメソッド
    public void ClearStage(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                stage1Cleared = true;
                Debug.Log("ステージ1クリア！");
                break;
            case 2:
                stage2Cleared = true;
                Debug.Log("ステージ2クリア！");
                break;
            case 3:
                stage3Cleared = true;
                Debug.Log("ステージ3クリア！");
                break;
        }

        UpdateStageButtons();

        // データを保存（次回起動時も有効にする）
        SaveStageProgress();
    }

    // ステージ進行状況を保存
    private void SaveStageProgress()
    {
        PlayerPrefs.SetInt("Stage1Cleared", stage1Cleared ? 1 : 0);
        PlayerPrefs.SetInt("Stage2Cleared", stage2Cleared ? 1 : 0);
        PlayerPrefs.SetInt("Stage3Cleared", stage3Cleared ? 1 : 0);
        PlayerPrefs.Save();
    }

    // ステージ進行状況を読み込み
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