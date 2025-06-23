using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.ObjectModel;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image m_hpGauge;
    [SerializeField] private Image m_health;
    [SerializeField] private Image m_burn;
    public float duration = 0.5f;
    public float debugDamageRate = 0.2f;
    public float currentRate = 1f;

    public void Start()
    {
        m_health.gameObject.SetActive(true);
        m_burn.gameObject.SetActive(true);
        SetGauge(1.0f); // ここで fillAmount を変更
        m_health.gameObject.SetActive(false);
        m_burn.gameObject.SetActive(false);
        PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
        if (currentRate <= debugDamageRate)
        {
            SceneManager.LoadScene("GameOver");
        }

        // Time.timeScaleによってHPバーの表示・非表示を切り替え
        if (Time.timeScale == 1f)
        {
            m_hpGauge.gameObject.SetActive(true);
            m_health.gameObject.SetActive(true);
            m_burn.gameObject.SetActive(true);
        }
        else if (Time.timeScale == 0f)
        {
            m_hpGauge.gameObject.SetActive(false);
            m_health.gameObject.SetActive(false);
            m_burn.gameObject.SetActive(false);
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