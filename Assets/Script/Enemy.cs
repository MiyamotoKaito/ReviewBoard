using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;         // 移動速度
    public float leftLimit = -3f;        // 左端のX座標
    public float rightLimit = 3f;        // 右端のX座標

    private bool movingRight = true;

    void Update()
    {
        // 移動処理
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }
}
