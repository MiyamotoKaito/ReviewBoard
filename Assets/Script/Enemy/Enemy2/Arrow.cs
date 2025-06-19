using DG.Tweening;
using UnityEngine;

public class Arrow: MonoBehaviour
{
    private bool isStarted = false;
    void Update()
    {   
        if (!isStarted && Time.timeScale == 1f)
        {
            gameObject.SetActive(true);
            var sr = GetComponent<SpriteRenderer>();
            sr.DOFade(0f, 0.1f).SetLoops(-1, LoopType.Yoyo);
            Destroy(gameObject, 0.5f);
        }

        else if (Time.timeScale == 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
