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
        // GameManager�����݂���ꍇ�A�V�[���J�ڂł��邱�Ƃ�ʒm
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.OnSceneTransition();
        }

        // �V�[���J�ڂ����s
        SceneManager.LoadScene("Title");
    }
}