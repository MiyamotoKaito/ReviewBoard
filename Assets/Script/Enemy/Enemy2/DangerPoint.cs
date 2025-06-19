using DG.Tweening;
using UnityEngine;

public class BlinkWithDOTween : MonoBehaviour
{
    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
