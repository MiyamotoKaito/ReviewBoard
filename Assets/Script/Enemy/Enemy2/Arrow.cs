using DG.Tweening;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private bool hasStartedAnimation = false;
    private bool shouldHideOnTimeScaleZero = false; // TimeScale0�̎��ɔ�\���ɂ��邩�̃t���O

    private void Start()
    {
        // ��������TimeScale���`�F�b�N
        if (Time.timeScale == 0f)
        {
            // TimeScale �� 0 �̎��ɐ������ꂽ�ꍇ�͕\�������܂�
            gameObject.SetActive(true);
            shouldHideOnTimeScaleZero = false;
        }
        else
        {
            // TimeScale �� 1 �̎��ɐ������ꂽ�ꍇ�͔�\������X�^�[�g
            gameObject.SetActive(false);
            shouldHideOnTimeScaleZero = true;
        }
    }


    void Update()
    {
        //TimeScale�� 1 �ɂȂ������Ɉ�x�����A�j���[�V�������J�n
        if (!hasStartedAnimation && Time.timeScale == 1f)
        {
            hasStartedAnimation = true;
            StartArrowAnimation();
        }

        // TimeScale �� 0 �ɂȂ������\���i�������ATimeScale0�Ő������ꂽ�ꍇ�͏����j
        else if (Time.timeScale == 0f && shouldHideOnTimeScaleZero)
        {
            gameObject.SetActive(false);
        }
    }

    private void StartArrowAnimation()
    {
        gameObject.SetActive(true);
        shouldHideOnTimeScaleZero = true; // �A�j���[�V�����J�n��͒ʏ�̓���
        var sr = GetComponent<SpriteRenderer>();
        sr.DOFade(0f, 0.1f).SetLoops(-1, LoopType.Yoyo);
        Destroy(gameObject, 0.5f);
    }
}
