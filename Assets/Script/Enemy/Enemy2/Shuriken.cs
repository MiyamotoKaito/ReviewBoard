using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float m_moveSpeed;
    [SerializeField] float m_rotateSpeed;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.down * m_moveSpeed, ForceMode2D.Impulse);
        transform.Rotate(0f, 0f, m_rotateSpeed * Time.deltaTime);
    }
}
