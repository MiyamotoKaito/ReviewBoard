using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private float HandSpeed = 1.0f;
    [SerializeField] private GameObject HandHitPrefab;

    private Rigidbody2D rb ;
    private Vector2 spawnPosition ;
        
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandMove();

    }

    public void HandMove()
    {
        rb.AddForce(Vector2.up * HandSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            spawnPosition = collision.transform.position;
            Instantiate(HandHitPrefab, spawnPosition, Quaternion.identity); // è’ìÀà íuÇ…HandHitprefabÇê∂ê¨
        }
        Destroy(gameObject);

    }



}
