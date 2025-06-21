using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject BossBattleGameObject; // BossBattleゲームオブジェクトを追加
    public Slider healthSlider;
    private bool isBattleStarted = false;
    private bool isDead = false; // 死亡フラグを追加（重複処理防止）

    void Start()
    {
        Debug.Log("Boss Start - 初期HP: " + healthSlider.value);
        Debug.Log("BossBattleGameObject設定状況: " + (BossBattleGameObject != null ? "設定済み" : "未設定"));
        // DialogManager3を削除したので、バトルを自動開始
        isBattleStarted = true;
        Debug.Log("バトル自動開始");
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        // HPの状態を常に監視（DialogManager3を削除したのでisBattleStartedチェックを削除）
        if (healthSlider.value <= 0f)
        {
            Debug.Log("HP が 0 以下になりました。現在のHP: " + healthSlider.value);
            Clear();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch") && !isDead)
        {
            Debug.Log("Punch が Boss に当たりました");
            SpriteRenderer Colors = GetComponent<SpriteRenderer>();
            if (Colors != null)
            {
                StartCoroutine(FlashRed(Colors));
            }
        }
    }

    private IEnumerator FlashRed(SpriteRenderer Colors)
    {
        Color originalcolor = Colors.color;
        Colors.color = new Color(1f, 0.3f, 0.3f, originalcolor.a);
        yield return new WaitForSeconds(0.1f);
        Colors.color = originalcolor;
    }

    private void Clear()
    {
        Debug.Log("Clear メソッド呼び出し - HP: " + healthSlider.value + ", isDead: " + isDead);

        if (healthSlider.value <= 0f && !isDead)
        {
            Debug.Log("死亡処理開始");
            isDead = true;
            Die();
        }
        else
        {
            Debug.Log("死亡処理スキップ - 条件を満たしていません");
        }
    }

    private void Die()
    {
        Debug.Log("Die メソッド開始");

        // Punchタグのオブジェクトを削除
        GameObject[] handhit = GameObject.FindGameObjectsWithTag("Punch");
        Debug.Log("削除するPunchオブジェクト数: " + handhit.Length);
        foreach (GameObject hit in handhit)
        {
            Destroy(hit);
        }

        // BossBattleゲームオブジェクトを呼び出す（アクティブにする）
        if (BossBattleGameObject != null)
        {
            Debug.Log("BossBattleGameObject をアクティブにします");
            BossBattleGameObject.SetActive(true);
            Debug.Log("BossBattleゲームオブジェクトを呼び出しました");
        }
        else
        {
            Debug.LogError("BossBattleGameObjectが設定されていません！Inspectorで設定してください");
        }

        // 自身を破棄
        Debug.Log("Boss オブジェクトを破棄します");
        Destroy(gameObject);
    }

    public void StartBattle()
    {
        isBattleStarted = true;
        Debug.Log("ボス戦開始！isBattleStarted = " + isBattleStarted);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            float oldHP = healthSlider.value;
            healthSlider.value -= 10f;
            Debug.Log("HP減少: " + oldHP + " → " + healthSlider.value);
        }
    }
}