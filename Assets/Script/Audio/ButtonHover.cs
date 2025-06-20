using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_hoverSound;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_hoverSound != null)
        {
            m_audioSource.PlayOneShot(m_hoverSound);
        }
    }
}