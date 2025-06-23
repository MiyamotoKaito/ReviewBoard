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
        _titlebutton.onClick.AddListener(() => Onclick());
    }

    private void Onclick()
    {
        SceneManager.LoadScene("Title");
    }
}
