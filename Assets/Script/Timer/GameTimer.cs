using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text m_timeText;
    float m_gameTimer = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timer();

    }

    private void Timer()
    {
        m_gameTimer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(m_gameTimer / 60f);
        int seconds = Mathf.FloorToInt(m_gameTimer % 60f);

        m_timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
