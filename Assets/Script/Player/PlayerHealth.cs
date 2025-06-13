using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.ObjectModel;

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
        SetGauge(1.0f);
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
            Debug.Log("aaa");
            TakeDamage(debugDamageRate);
        }
    }

}
