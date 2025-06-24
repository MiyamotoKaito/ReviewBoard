using UnityEngine;
using UnityEngine.UI;

public class ClickUI : MonoBehaviour
{
    [SerializeField] private Image m_mouseUI;
    [SerializeField] private Image m_coolDown;
    [SerializeField] private Text m_space;

    private bool m_lastTimeScaleState = true; // �O���TimeScale�̏�Ԃ��L�^

    void Start()
    {
        // ������Ԃ�ݒ�
        UpdateUIVisibility();
    }

    void Update()
    {
        // TimeScale�̕ω����`�F�b�N
        bool currentTimeScaleState = Time.timeScale > 0f;

        // ��Ԃ��ω������ꍇ�̂�UI���X�V
        if (currentTimeScaleState != m_lastTimeScaleState)
        {
            UpdateUIVisibility();
            m_lastTimeScaleState = currentTimeScaleState;
        }
    }

    private void UpdateUIVisibility()
    {
        bool shouldShow = Time.timeScale > 0f;

        // null �`�F�b�N��ǉ����Ĉ��S��������
        if (m_coolDown != null)
            m_coolDown.gameObject.SetActive(shouldShow);

        if (m_mouseUI != null)
            m_mouseUI.gameObject.SetActive(shouldShow);

        if (m_space != null)
            m_space.gameObject.SetActive(shouldShow);
    }

    // �V�[���J�n���ɋ����I��UI���ď�����
    private void OnEnable()
    {
        // �t���[���x����UI��Ԃ��X�V�i���̃R���|�[�l���g�̏�������҂j
        Invoke(nameof(UpdateUIVisibility), 0.1f);
    }
}