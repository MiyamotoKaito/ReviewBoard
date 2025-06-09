using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeath : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text enemyText;
    void Start()
    {
        if (enemyText != null)
        {
            enemyText.text = "ÇÆÇÕÇ¡ÅIÅI";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
