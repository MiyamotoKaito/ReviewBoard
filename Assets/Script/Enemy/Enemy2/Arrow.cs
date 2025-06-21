using DG.Tweening;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private bool hasStartedAnimation = false;
    private bool shouldHideOnTimeScaleZero = false; // TimeScale0の時に非表示にするかのフラグ

    private void Start()
    {
        // 生成時はTimeScaleをチェック
        if (Time.timeScale == 0f)
        {
            // TimeScale が 0 の時に生成された場合は表示したまま
            gameObject.SetActive(true);
            shouldHideOnTimeScaleZero = false;
        }
        else
        {
            // TimeScale が 1 の時に生成された場合は非表示からスタート
            gameObject.SetActive(false);
            shouldHideOnTimeScaleZero = true;
        }
    }


    void Update()
    {
        //TimeScaleが 1 になった時に一度だけアニメーションを開始
        if (!hasStartedAnimation && Time.timeScale == 1f)
        {
            hasStartedAnimation = true;
            StartArrowAnimation();
        }

        // TimeScale が 0 になったら非表示（ただし、TimeScale0で生成された場合は除く）
        else if (Time.timeScale == 0f && shouldHideOnTimeScaleZero)
        {
            gameObject.SetActive(false);
        }
    }

    private void StartArrowAnimation()
    {
        gameObject.SetActive(true);
        shouldHideOnTimeScaleZero = true; // アニメーション開始後は通常の動作
        var sr = GetComponent<SpriteRenderer>();
        sr.DOFade(0f, 0.1f).SetLoops(-1, LoopType.Yoyo);
        Destroy(gameObject, 0.5f);
    }
}
