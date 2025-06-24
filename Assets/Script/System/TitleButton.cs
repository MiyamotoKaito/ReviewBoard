using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [SerializeField] Button _titlebutton;

    void Start()
    {
        _titlebutton.onClick.AddListener(() => OnTitleButtonClick());
    }

    public void OnTitleButtonClick()
    {
        // GameManagerが存在する場合、シーン遷移であることを通知
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.OnSceneTransition();
        }

        // シーン遷移を実行
        SceneManager.LoadScene("Title");
    }
}