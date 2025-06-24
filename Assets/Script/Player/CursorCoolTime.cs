using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CursorCoolTime : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] float _coolTime;
    [SerializeField] float _Value;
    private bool _isOnCoolDown = false;
    private Tween _coolDownTween; // Tween�̎Q�Ƃ�ێ�

    void Start()
    {
        _image.gameObject.SetActive(false);
        _Value = 0f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isOnCoolDown)
        {
            StartCoolDown();
        }
        else if (!_isOnCoolDown) // �N�[���_�E�����łȂ��ꍇ�̂݃��Z�b�g
        {
            _image.fillAmount = 0f;
        }
    }

    private void StartCoolDown()
    {
        _isOnCoolDown = true;
        _Value = 0f;
        _image.gameObject.SetActive(true);
        _image.fillAmount = 0f;

        // ������Tween������΃L�����Ă���V�������̂��J�n
        if (_coolDownTween != null)
        {
            _coolDownTween.Kill();
        }

        _coolDownTween = _image.DOFillAmount(1f, _coolTime).SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                _Value = _image.fillAmount;
            })
            .OnComplete(() =>
            {
                CompleteCoolDown();
            });
    }

    private void CompleteCoolDown()
    {
        Debug.Log("�N�[���_�E������");
        _Value = 0f;
        _image.gameObject.SetActive(false);
        _isOnCoolDown = false;
        _coolDownTween = null;
    }

    // �V�[���J�ڎ���I�u�W�F�N�g�j�����̏���
    private void OnDestroy()
    {
        // Tween���L�����ăN�[���_�E������������
        if (_coolDownTween != null)
        {
            _coolDownTween.Kill();
        }

        // ��Ԃ����Z�b�g
        if (_isOnCoolDown)
        {
            CompleteCoolDown();
        }
    }

    // �蓮�ŃN�[���_�E�������Z�b�g���郁�\�b�h�i�K�v�ɉ����āj
    public void ResetCoolDown()
    {
        if (_coolDownTween != null)
        {
            _coolDownTween.Kill();
        }
        CompleteCoolDown();
    }

    // �V�[���J�ڑO�ɌĂяo�����\�b�h
    public void OnSceneTransition()
    {
        if (_isOnCoolDown)
        {
            ResetCoolDown();
        }
    }
}