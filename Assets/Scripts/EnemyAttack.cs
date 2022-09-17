using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject enemy;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("Attack", 0.3f);
        }
    }
    void Attack()
    {
        enemy.GetComponent<Enemy>().Attack();
    }
    
}
