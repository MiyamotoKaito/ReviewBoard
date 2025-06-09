using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]GameObject DeathgameObject;
    public Slider healthSlider;
    public float leftLimit = -3f;        // 左端のX座標
    public float rightLimit = 3f;        // 右端のX座標
    public float patrolDuration = 2f;    // 片道にかかる時間（秒）

    private float timer = 0f;
    private bool movingRight = true;

    void Start()
    {
        
    }
    void Update()
    {
        Move();
        clear();
    }

    private void Move()
    {
        timer += Time.deltaTime;

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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punch"))
        {
            Debug.Log("当たった");
            SpriteRenderer Colors = GetComponent<SpriteRenderer>();
            if (Colors != null)
            {
                Debug.Log("色を変更中！");
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

    private void clear()
    {
        if (healthSlider.value == 0f)
        {
            Destroy(gameObject);
            Time.timeScale = 0f;
        }

    }

    private void OnDestroy()
    {
        Instantiate(DeathgameObject, transform.position, Quaternion.identity);
    }
}
