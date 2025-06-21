using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button stage1Button;
    [SerializeField] Button stage2Button;
    [SerializeField] Button stage3Button;
    [SerializeField] Button BossButton;
    [SerializeField] Button TitleButton;
    [SerializeField] Image fadePanel; // �t�F�[�h�A�E�g�p
    [SerializeField] float fadeDuration = 1f;
    void Start()
    {
        stage1Button.onClick.AddListener(() => FadeAndLoad("Stage1"));
        stage2Button.onClick.AddListener(() => FadeAndLoad("Stage2"));
        stage3Button.onClick.AddListener(() => FadeAndLoad("Stage3"));
        BossButton.onClick.AddListener(() => FadeAndLoad("Boss"));
        TitleButton.onClick.AddListener(() => FadeAndLoad("Title"));

        // �ŏ��͓���
        fadePanel.color = new Color(0, 0, 0, 0);
        fadePanel.gameObject.SetActive(true); //�� SetActive(true) �ɂ��Ă����Ȃ��ƃt�F�[�h�ł��Ȃ�
    }

    private void FadeAndLoad(string sceneName)
    {
        //�t�F�[�h�A�E�g
        fadePanel.DOFade(1f, fadeDuration).SetUpdate(true).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
}
