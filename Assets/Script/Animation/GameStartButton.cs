using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;//�V�[���؂�ւ��ɕK�v

public class GameStartButton : MonoBehaviour
{
    [SerializeField] Button SelectButton;
    [SerializeField] Button TutorialButton;
    [SerializeField] Image fadePanel; // �t�F�[�h�A�E�g�p
    [SerializeField] float fadeDuration = 1f;
    void Start()
    {
        TutorialButton.onClick.AddListener(() => FadeAndLoad("Tutorial"));
        SelectButton.onClick.AddListener(() => FadeAndLoad("GameSelect"));

        // �ŏ��͓���
        fadePanel.color = new Color(0, 0, 0, 0);
        fadePanel.gameObject.SetActive(true); //�� SetActive(true) �ɂ��Ă����Ȃ��ƃt�F�[�h�ł��Ȃ�
    }

    private void FadeAndLoad(string sceneName)
    {
        fadePanel.DOFade(1f, fadeDuration).SetUpdate(true).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
}
