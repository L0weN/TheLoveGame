using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMeatScript : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            this.gameObject.SetActive(false);
            player.GetComponent<Player>().Heal(10);
        }
    }
}
