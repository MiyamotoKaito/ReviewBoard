using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.ObjectModel;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image m_health;
    [SerializeField] private Image m_burn;

    public float duration = 0.5f;
    public float debugDamageRate = 0.2f;
    public float currentRate = 1f;

    public void Start()
    {
        m_health.gameObject.SetActive(false); m_burn.gameObject.SetActive(false);
        SetGauge(1.0f);
        PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {


        if (currentRate <= debugDamageRate)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (Time.timeScale == 1f)
        {
            m_health.gameObject.SetActive(true); m_burn.gameObject.SetActive(true);
        }
    }
    public void SetGauge(float targetRate)
    {
        m_health.DOFillAmount(targetRate, duration).OnComplete(() =>
        {
            m_burn.DOFillAmount(targetRate, duration * 0.5f).SetDelay(0.5f);
        });
        currentRate = targetRate;
    }

    public void TakeDamage(float rate)
    {
        SetGauge(currentRate - rate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            TakeDamage(debugDamageRate);
        }
    }

}
