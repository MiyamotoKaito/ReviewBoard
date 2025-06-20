using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource clickSound;

    public void PlayClickSound()
    {
        clickSound.Play();
    }
}
