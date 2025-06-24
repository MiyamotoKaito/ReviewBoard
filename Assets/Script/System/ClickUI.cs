using UnityEngine;
using UnityEngine.UI;

public class ClickUI : MonoBehaviour
{
    [SerializeField] private Image m_mouseUI;
    [SerializeField] private Image m_coolDown;
    [SerializeField] private Text m_space;

    private bool m_lastTimeScaleState = true; // 前回のTimeScaleの状態を記録

    void Start()
    {
        // 初期状態を設定
        UpdateUIVisibility();
    }

    void Update()
    {
        // TimeScaleの変化をチェック
        bool currentTimeScaleState = Time.timeScale > 0f;

        // 状態が変化した場合のみUIを更新
        if (currentTimeScaleState != m_lastTimeScaleState)
        {
            UpdateUIVisibility();
            m_lastTimeScaleState = currentTimeScaleState;
        }
    }

    private void UpdateUIVisibility()
    {
        bool shouldShow = Time.timeScale > 0f;

        // null チェックを追加して安全性を向上
        if (m_coolDown != null)
            m_coolDown.gameObject.SetActive(shouldShow);

        if (m_mouseUI != null)
            m_mouseUI.gameObject.SetActive(shouldShow);

        if (m_space != null)
            m_space.gameObject.SetActive(shouldShow);
    }

    // シーン開始時に強制的にUIを再初期化
    private void OnEnable()
    {
        // フレーム遅延でUI状態を更新（他のコンポーネントの初期化を待つ）
        Invoke(nameof(UpdateUIVisibility), 0.1f);
    }
}