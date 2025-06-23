using UnityEngine;
using UnityEngine.UI;

public class ClickUI : MonoBehaviour
{
    [SerializeField] private Image m_mouseUI;
    [SerializeField] private Text m_space;
    void Update()
    {
        // Time.timeScale‚É‚æ‚Á‚ÄUI‚Ì•\¦E”ñ•\¦‚ğØ‚è‘Ö‚¦
        if (Time.timeScale == 0f)
        {
            m_mouseUI.gameObject.SetActive(false);
            m_space.gameObject.SetActive(false);
        }
        else if (Time.timeScale == 1f)
        {
            m_mouseUI.gameObject.SetActive(true);
            m_space.gameObject.SetActive(true);
        }
    }
}