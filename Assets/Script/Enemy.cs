using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float leftLimit = -3f;        // 左端のX座標
    public float rightLimit = 3f;        // 右端のX座標
    public float patrolDuration = 2f;    // 片道にかかる時間（秒）

    private float timer = 0f;
    private bool movingRight = true;

    void Update()
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
}
