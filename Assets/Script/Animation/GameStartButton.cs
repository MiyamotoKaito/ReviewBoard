using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;//シーン切り替えに必要

public class GameStartButton : MonoBehaviour
{
    [SerializeField] Button SelectButton;
    [SerializeField] Button TutorialButton;
    [SerializeField] Image fadePanel; // フェードアウト用
    [SerializeField] float fadeDuration = 1f;
    void Start()
    {
        TutorialButton.onClick.AddListener(() => FadeAndLoad("Tutorial"));
        SelectButton.onClick.AddListener(() => FadeAndLoad("GameSelect"));

        // 最初は透明
        fadePanel.color = new Color(0, 0, 0, 0);
        fadePanel.gameObject.SetActive(true); //← SetActive(true) にしておかないとフェードできない
    }

    private void FadeAndLoad(string sceneName)
    {
        fadePanel.DOFade(1f, fadeDuration).SetUpdate(true).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
}
