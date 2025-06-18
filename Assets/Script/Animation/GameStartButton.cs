using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//シーン切り替えに必要

public class GameStartButton: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(Push);
    }   

    // Update is called once per frame

    private void Push()
    {
        SceneManager.LoadScene("GameSelect");
    }
}
