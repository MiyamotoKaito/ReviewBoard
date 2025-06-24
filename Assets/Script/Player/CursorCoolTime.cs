using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CursorCoolTime : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] float _coolTime;
    [SerializeField] float _Value;
    private bool _isOnCoolDown = false;
    private Tween _coolDownTween; // Tweenの参照を保持

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
        else if (!_isOnCoolDown) // クールダウン中でない場合のみリセット
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

        // 既存のTweenがあればキルしてから新しいものを開始
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
        Debug.Log("クールダウン完了");
        _Value = 0f;
        _image.gameObject.SetActive(false);
        _isOnCoolDown = false;
        _coolDownTween = null;
    }

    // シーン遷移時やオブジェクト破棄時の処理
    private void OnDestroy()
    {
        // Tweenをキルしてクールダウンを強制完了
        if (_coolDownTween != null)
        {
            _coolDownTween.Kill();
        }

        // 状態をリセット
        if (_isOnCoolDown)
        {
            CompleteCoolDown();
        }
    }

    // 手動でクールダウンをリセットするメソッド（必要に応じて）
    public void ResetCoolDown()
    {
        if (_coolDownTween != null)
        {
            _coolDownTween.Kill();
        }
        CompleteCoolDown();
    }

    // シーン遷移前に呼び出すメソッド
    public void OnSceneTransition()
    {
        if (_isOnCoolDown)
        {
            ResetCoolDown();
        }
    }
}