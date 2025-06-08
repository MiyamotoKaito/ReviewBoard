using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;         // �ړ����x
    public float leftLimit = -3f;        // ���[��X���W
    public float rightLimit = 3f;        // �E�[��X���W

    private bool movingRight = true;

    void Update()
    {
        // �ړ�����
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
