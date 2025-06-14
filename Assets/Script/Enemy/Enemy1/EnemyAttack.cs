using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float TargetAttackSpeed = 0f;
    private Rigidbody2D rb;
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Move();
        Destroy(gameObject,10f);
        if (Time.timeScale == 0f)
        {
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        
    }

    private void Move()
    {
        rb.AddForce(Vector2.down * TargetAttackSpeed, (ForceMode2D.Impulse));
        transform.Rotate(0f, 0f, 30f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
