using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SelectButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button stage1Button;
    [SerializeField] Button stage2Button;
    [SerializeField] Button stage3Button;
    void Start()
    {
        stage1Button.onClick.AddListener(() => Stage1UI());
        stage2Button.onClick.AddListener(() => Stage2UI());
        stage3Button.onClick.AddListener(() => Stage3UI());
    }

    // Update is called once per frame


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
