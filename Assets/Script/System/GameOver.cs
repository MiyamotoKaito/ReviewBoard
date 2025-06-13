using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button RetryButton;
    [SerializeField] Button TitleButton;
    void Start()
    {
        Cursor.visible = true;
        RetryButton.onClick.AddListener(() => OnclickRetryStageUI());
        TitleButton.onClick.AddListener(() => OnclickTitleUI());

    }

    private void OnclickRetryStageUI()
    {
        int previousScene = PlayerPrefs.GetInt("LastScene", 0); // •Û‘¶‚³‚ê‚½‘O‚ÌƒV[ƒ“”Ô†
        SceneManager.LoadScene(previousScene);
    }

    private void OnclickTitleUI()
    {
        SceneManager.LoadScene("Title");
    }
}
