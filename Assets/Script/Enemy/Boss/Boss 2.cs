using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class Boss2 : MonoBehaviour
{
    [SerializeField] GameObject DeathgameObject;
    [SerializeField] GameObject BossAttackObject;
    [SerializeField] GameObject Boss2AttackObject;
    [SerializeField] float AttackTimer = 1.0f;
    [SerializeField] float AttackCooldown = 2f;
    [SerializeField] int currentStageNumber = 4; // 追加：現在のステージ番号

    // 速度関連の変数を追加
    [SerializeField] float normalPatrolDuration = 2.0f;    // 通常時の片道時間
    [SerializeField] float fastPatrolDuration = 1.0f;      // 高速時の片道時間（HP150以下）
    [SerializeField] float normalAttackCooldown = 2f;      // 通常時の攻撃間隔
    [SerializeField] float fastAttackCooldown = 1f;        // 高速時の攻撃間隔（HP150以下）

    public Slider healthSlider;            //HPバー
    public float leftLimit = -9.0f;        // 左端のX座標
    public float rightLimit = 9.0f;        // 右端のX座標
    public float patrolDuration = 2.0f;    // 現在の片道時間
    private float timer = 0f;
    private bool movingRight = true;
    private bool isBattleStarted = false;
    private bool isEnraged = false;        // 怒り状態フラグを追加

    void Start()
    {
        patrolDuration = normalPatrolDuration;
        AttackCooldown = normalAttackCooldown;
    }

    void Update()
    {
        if (!isBattleStarted)
        {
            return;
        }

        CheckEnrageCondition(); // HP150以下のチェック
        Move();
        Clear();
    }

    private void CheckEnrageCondition()
    {
        if (!isEnraged && healthSlider.value <= 150f)
        {
            isEnraged = true;
            patrolDuration = fastPatrolDuration;
            AttackCooldown = fastAttackCooldown;
            Debug.Log("ボスが怒り状態になりました！攻撃パターンが変化します！");
        }
    }

    private void Move()
    {
        timer += Time.deltaTime;
        AttackTimer += Time.deltaTime;
        float t = timer / patrolDuration;
        // 緩急をつけて補間（0〜1 → 0〜1）
        float easedT = Mathf.SmoothStep(0f, 1f, t);
        // 線形補間
        float newX = Mathf.Lerp(
            movingRight ? leftLimit : rightLimit,
            movingRight ? rightLimit : leftLimit,
            easedT
        );
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        // 往復切り替え
        if (timer >= patrolDuration)
        {
            timer = 0f;
            movingRight = !movingRight;
        }

        // 攻撃パターンをHP状態によって変更
        if (AttackTimer > AttackCooldown)
        {
            Vector2 force = new Vector2(transform.position.x, 18f);

            if (isEnraged)
            {
                // HP150以下の場合：Boss2AttackObjectを使用
                Instantiate(Boss2AttackObject, force, Quaternion.identity);
                Debug.Log("強化攻撃を発動！");
            }
            else
            {
                // 通常時：BossAttackObjectを使用
                Instantiate(BossAttackObject, force, Quaternion.identity);
            }

            AttackTimer = 0f;//タイマーリセット
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
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
        if (healthSlider.value == 0f)
        {
            Die();
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
    }

    private void Die()
    {
        GameObject[] handhit = GameObject.FindGameObjectsWithTag("Punch"); //"Punch" タグが付いたすべてのGameObjectを配列として取得
        foreach (GameObject hit in handhit)
        {
            Destroy(hit);
        }
        Instantiate(DeathgameObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void CompleteStage()
    {
        // PlayerPrefsでステージクリア状態を保存
        PlayerPrefs.SetInt($"Stage{currentStageNumber}Cleared", 1);
        PlayerPrefs.Save();
        Debug.Log($"ステージ{currentStageNumber}をクリアしました！");
    }

    public void StartBattle()
    {
        isBattleStarted = true;
    }
}