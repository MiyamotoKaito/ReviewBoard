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

    // WebGL用のセッション管理（改善版）
    private const string SESSION_KEY = "GameSession";
    private const string SESSION_VALID_KEY = "SessionValid";
    private static string currentSessionId;

    // シングルトンパターンでGameManagerの重複を防ぐ
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        // シングルトンパターンの実装
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        // シーン遷移時にGameManagerを保持（必要に応じて）
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
        // WebGLビルドの場合、ブラウザリロード検出（改善版）
        CheckForBrowserReload();
#endif

        LoadStageProgress();
        UpdateStageButtons();

        Debug.Log($"GameManager Start - Stage1: {stage1Cleared}, Stage2: {stage2Cleared}, Stage3: {stage3Cleared}");
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    private void CheckForBrowserReload()
    {
        // セッション有効性フラグをチェック
        bool isSessionValid = PlayerPrefs.GetInt(SESSION_VALID_KEY, 0) == 1;
        
        // 新しいセッションIDを生成
        string newSessionId = System.DateTime.Now.Ticks.ToString();
        
        // 前回のセッションIDを取得
        string savedSessionId = PlayerPrefs.GetString(SESSION_KEY, "");
        
        // 真のブラウザリロードかどうかを判定（より厳密に）
        bool isTrueBrowserReload = string.IsNullOrEmpty(savedSessionId) || !isSessionValid;
        
        if (isTrueBrowserReload)
        {
            Debug.Log("真のブラウザリロードを検出 - ステージ進行状況をリセット");
            ResetStageProgress();
        }
        else
        {
            Debug.Log("通常のシーン遷移を検出 - 進行状況を保持");
        }
        
        // 現在のセッションIDを保存し、セッションを有効に設定
        currentSessionId = newSessionId;
        PlayerPrefs.SetString(SESSION_KEY, currentSessionId);
        PlayerPrefs.SetInt(SESSION_VALID_KEY, 1);
        PlayerPrefs.Save();
    }
#endif

    private void OnApplicationFocus(bool hasFocus)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGLでフォーカスを失った場合の処理を改善
        if (!hasFocus)
        {
            Debug.Log("アプリケーションがフォーカスを失いました");
            // 即座にセッションを無効化せず、遅延処理で判定
            StartCoroutine(DelayedSessionInvalidation());
        }
        else
        {
            Debug.Log("アプリケーションがフォーカスを取得しました");
            // フォーカスを取り戻した場合はセッションを有効に戻す
            PlayerPrefs.SetInt(SESSION_VALID_KEY, 1);
            PlayerPrefs.Save();
        }
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    private System.Collections.IEnumerator DelayedSessionInvalidation()
    {
        // 短時間待機してからセッション無効化を判定
        yield return new WaitForSeconds(0.5f);
        
        // まだフォーカスが戻っていない場合のみセッションを無効化
        if (!Application.isFocused)
        {
            PlayerPrefs.SetInt(SESSION_VALID_KEY, 0);
            PlayerPrefs.Save();
            Debug.Log("セッションを無効化しました");
        }
    }
#endif

    private void OnApplicationPause(bool pauseStatus)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGLでポーズ状態になった場合の処理を改善
        if (pauseStatus)
        {
            Debug.Log("アプリケーションがポーズされました");
            // 即座に無効化せず、少し待つ
            StartCoroutine(DelayedSessionInvalidation());
        }
        else
        {
            Debug.Log("アプリケーションのポーズが解除されました");
            PlayerPrefs.SetInt(SESSION_VALID_KEY, 1);
            PlayerPrefs.Save();
        }
#endif
    }

    // シーン遷移時にセッションを維持するメソッド
    public void OnSceneTransition()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // 通常のシーン遷移であることを明示的に記録
        PlayerPrefs.SetInt(SESSION_VALID_KEY, 1);
        PlayerPrefs.Save();
        Debug.Log("シーン遷移: セッションを有効に保持");
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
        // ボタンがnullでないかチェックしてから操作
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
        Debug.Log($"セッション状態 - SessionValid: {PlayerPrefs.GetInt(SESSION_VALID_KEY, -1)}, SessionID: {PlayerPrefs.GetString(SESSION_KEY, "none")}");
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}