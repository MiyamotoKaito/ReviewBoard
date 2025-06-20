using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Enemy2 : MonoBehaviour
{
    [SerializeField] GameObject DeathgameObject;
    [SerializeField] GameObject EnemyAttackObject;
    [SerializeField] float AttackTimer = 1.0f;
    [SerializeField] float AttackCooldown = 2f;

    public Slider healthSlider;            //HPバー
    public float leftLimit = -9.0f;        // 左端のX座標
    public float rightLimit = 9.0f;        // 右端のX座標
    public float patrolDuration = 2.0f;    // 片道にかかる時間（秒）
    private float timer = 0f;

    private bool movingRight = true;
    private bool isBattleStarted = false;

    void Start()
    {

    }
    void Update()
    {
        if (!isBattleStarted)
        {
            return;
        }
        Move();
        Clear();

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

        if (AttackTimer > AttackCooldown)
        {
            Instantiate(EnemyAttackObject, transform.position, Quaternion.identity);
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
                StartCoroutine(FlashRed(Colors));//当たると赤くなる
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

    public void StartBattle()
    {
        isBattleStarted = true;
    }
}
