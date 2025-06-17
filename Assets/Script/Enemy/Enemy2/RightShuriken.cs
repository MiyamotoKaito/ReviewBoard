using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightShuriken : MonoBehaviour
{
    [SerializeField] float m_moveSpeed;
    [SerializeField] float m_rotateSpeed;

    Rigidbody2D rb;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    public void Update()
    {
        Vector2 force = new Vector2(-1f, -1f).normalized * m_moveSpeed;
        rb.AddForce(force, ForceMode2D.Impulse);
        transform.Rotate(0f, 0f, -m_rotateSpeed * Time.deltaTime);
    }
}
