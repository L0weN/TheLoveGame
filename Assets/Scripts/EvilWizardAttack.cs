using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardAttack : MonoBehaviour
{
    public SpriteRenderer sprite;
    public GameObject evilWizard;
    private bool attack = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CanAttack()
    {
        if (attack)
        {
            sprite.enabled = true;
            evilWizard.GetComponent<EvilWizardScript>().Attack();
            attack = false;
            StartCoroutine(endAttack());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CanAttack();
        }
        
    }

    IEnumerator endAttack()
    {
        yield return new WaitForSeconds(1f);
        sprite.enabled = false;
        yield return new WaitForSeconds(5f);
        attack = true;
    }
}
