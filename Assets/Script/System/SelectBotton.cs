using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SelectButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject selectUIPanel;
    [SerializeField] Button stage1Button;
    [SerializeField] Button stage2Button;
    [SerializeField] Button stage3Button;
    void Start()
    {
        stage1Button.onClick.AddListener(() => Stage1UI());
        stage1Button.onClick.AddListener(() => Stage2UI());
        stage1Button.onClick.AddListener(() => Stage3UI());
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
    }

    private void ShowUI()
    {
        if (selectUIPanel != null)
        {
            selectUIPanel.SetActive(true);
        }
    }

    private void Stage1UI()
    {
        SceneManager.LoadScene("Stage1");
    }
    private void Stage2UI()
    {
        SceneManager.LoadScene("Stage2");
    }

    private void Stage3UI()
    {
        SceneManager.LoadScene("Stage3");
    }
}
