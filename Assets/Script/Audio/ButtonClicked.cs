using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    [SerializeField] private AudioSource m_clickSound;

    public void PlayClick()
    {
        m_clickSound.Play();
    }
}
