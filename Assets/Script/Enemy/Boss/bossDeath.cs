using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossDeath : MonoBehaviour
{
    [Header("�t���b�V���G�t�F�N�g�ݒ�")]
    public GameObject flashPanel; // �����p�l���iCanvas���Image�j
    public float flashDuration = 1.5f; // �t���b�V���̎���

    [Header("�ڍs��V�[��")]
    public string gameClearSceneName = "GameClear";

    private Image flashImage;
    private bool isTriggered = false;

    void Start()
    {
        // �t���b�V���p�l���̏����ݒ�
        if (flashPanel != null)
        {
            flashImage = flashPanel.GetComponent<Image>();
            if (flashImage != null)
            {
                // �ŏ��͓����ɂ��Ă���
                Color c = flashImage.color;
                c.a = 0f;
                flashImage.color = c;
                flashPanel.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (GameObject.FindWithTag("GameClear") != null && !isTriggered)
        {
            TriggerGameClear();
        }
        
    }

    public void TriggerGameClear()
    {

        if (isTriggered)
        {
            Debug.Log("���ɔ����ς݂̂��ߏ������X�L�b�v���܂�");
            return;
        }

        isTriggered = true;
        Debug.Log("�t���b�V���G�t�F�N�g���J�n���܂�");

        StartCoroutine(FlashAndTransition());
    }

    private IEnumerator FlashAndTransition()
    {
        Debug.Log("FlashAndTransition �R���[�`�����J�n����܂���");
        Debug.Log($"���݂�Time.timeScale: {Time.timeScale}");

        if (flashImage == null)
        {
            Debug.LogWarning("flashImage���ݒ肳��Ă��܂���B�����ɃV�[���ڍs���܂�");
            SceneManager.LoadScene(gameClearSceneName);
            yield break;
        }

        Debug.Log("�t���b�V���G�t�F�N�g�J�n");

        // �t���b�V���G�t�F�N�g�J�n
        float elapsedTime = 0f;
        Color startColor = flashImage.color;
        Color endColor = new Color(1f, 1f, 1f, 1f); // ���F�i���S�s�����j

        // �t�F�[�h�C���i�����Ȃ�j- unscaledDeltaTime���g�p
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Time.timeScale�̉e�����󂯂Ȃ�
            float progress = elapsedTime / flashDuration;

            // �C�[�W���O�J�[�u�i�}���ɔ����Ȃ�j
            progress = progress * progress;

            flashImage.color = Color.Lerp(startColor, endColor, progress);
            yield return null;
        }

        // ���S�ɔ����Ȃ������Ƃ��m�F
        flashImage.color = endColor;
        Debug.Log("�t���b�V���G�t�F�N�g�����B�V�[���ڍs������...");

        // �����҂��Ă���V�[���ڍs - WaitForSecondsRealtime���g�p
        yield return new WaitForSecondsRealtime(0.1f); // Time.timeScale�̉e�����󂯂Ȃ�

        // �Q�[���N���A�V�[���Ɉڍs
        Debug.Log($"�V�[�� '{gameClearSceneName}' �Ɉڍs���܂�");
        SceneManager.LoadScene(gameClearSceneName);
    }
}

// �g�p��F����̃I�u�W�F�N�g���C���X�^���X�����ꂽ�Ƃ��ɔ���
public class ObjectInstantiationDetector : MonoBehaviour
{
    [Header("�Ď��ݒ�")]
    public GameObject targetPrefab; // �Ď�����v���n�u
    public bossDeath gameClearTrigger; // �Q�[���N���A�g���K�[

    private bool hasDetected = false;

    void Update()
    {
        if (hasDetected) return;

        // ����̃^�O�����I�u�W�F�N�g���������ꂽ���`�F�b�N
        GameObject[] objects = GameObject.FindGameObjectsWithTag("ClearObject");

        if (objects.Length > 0)
        {
            hasDetected = true;
            Debug.Log("�N���A�I�u�W�F�N�g�����o����܂����I");

            // �Q�[���N���A�𔭓�
            if (gameClearTrigger != null)
            {
                gameClearTrigger.TriggerGameClear();
            }
        }
    }
}

// �I�u�W�F�N�g�������Ɏ����Œʒm������@
public class InstantiatedObject : MonoBehaviour
{
    void Start()
    {
        // ���̃I�u�W�F�N�g���C���X�^���X�����ꂽ�u�ԂɎ��s
        bossDeath trigger = FindObjectOfType<bossDeath>();
        if (trigger != null)
        {
            trigger.TriggerGameClear();
        }
    }
}