using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_hoverSound;
    [SerializeField] private Image m_hoverImage;

    private bool m_pressed = false;

    private void Start()
    {
        if (!m_pressed && m_hoverImage != null)
        {
            m_hoverImage.gameObject.SetActive(false);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_hoverSound != null)
        {
            m_audioSource.PlayOneShot(m_hoverSound);
            m_pressed = true;
        }

        if (m_hoverImage != null)
        {
            m_hoverImage.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //�J�[�\�������ꂽ��t���O�����Z�b�g
        m_pressed = false;

        // �J�[�\�������ꂽ��Image���\��
        if (m_hoverImage != null)
        {
            m_hoverImage.gameObject.SetActive(false);
        }
    }
}