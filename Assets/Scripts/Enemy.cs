using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private float localScale_X = 1.4f;
    private float localScale_Y = 1.4f;

    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public GameObject bandit,warrior;
    public AIPath aiPath;

    public GameObject Apple, Fish, Steak, Chicken, ChickenLeg, Peach, MelonWater, Strawberry;

    private bool isDead = false;
    private bool canTakeDamage = true;
    private bool canAttack = true;
    
    void Start()
    {
        currentHealth = maxHealth;
        aiPath.canMove = false;
    }

    void Update()
    {
        ChangeDirection();
    }

    void ChangeDirection()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-localScale_X, localScale_Y, 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(localScale_X, localScale_Y, 1f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            if (!isDead)
            {
                currentHealth -= damage;
                anim.SetTrigger("Hurt");
                aiPath.canMove = false;
                canAttack = false;
                StopCoroutine(endAttack());
                anim.SetBool("Attack", false);
                anim.SetBool("Run", false);
                anim.SetBool("isDead", false);
                StartCoroutine(disableTakeDamage());
                if (currentHealth <= 0)
                {
                    Die();
                    isDead = true;
                }
            }
        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            aiPath.canMove = false;
            anim.SetBool("Attack", true);
            StartCoroutine(endAttack());
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);   
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!aiPath.canMove)
        {
            if (collision.tag == "Player")
            {
                aiPath.canMove = true;
                anim.SetBool("Run", true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (aiPath.canMove)
        {
            if (collision.tag == "Player")
            {
                aiPath.canMove = false;
                anim.SetBool("Run", false);
            }
        }
    }

    private void Die()
    {
        anim.SetBool("isDead", true);
        this.enabled = false;
        aiPath.canMove = false;
        StartCoroutine(disableEnemy());
    }

    IEnumerator disableTakeDamage()
    {
        yield return new WaitForSeconds(1f);
        if (!aiPath.canMove)
        {
            aiPath.canMove = true;
            anim.SetBool("Run", true);
        }
        canAttack = true;
    }

    IEnumerator endAttack()
    {
        
        yield return new WaitForSeconds(0.5f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            warrior.GetComponent<PlayerMovement>().PlayerTakeDamage(10);
        }
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("Attack", false);
        anim.SetBool("Run", false);
        canAttack = true;
    }

    IEnumerator disableEnemy()
    {
        yield return new WaitForSeconds(1f);
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1f);
        bandit.SetActive(false);
        Vector3 collect = transform.position;
        collect.y = transform.position.y + 2f;
        int collect_number = Random.Range(0, 8);

        if(collect_number == 0)
        {
            Instantiate(Apple, collect, Quaternion.identity);
        }
        if(collect_number == 1)
        {
            Instantiate(Fish, collect, Quaternion.identity);
        }
        if (collect_number == 2)
        {
            Instantiate(Peach, collect, Quaternion.identity);
        }
        if (collect_number == 3)
        {
            Instantiate(Steak, collect, Quaternion.identity);
        }
        if (collect_number == 4)
        {
            Instantiate(MelonWater, collect, Quaternion.identity);
        }
        if (collect_number == 5)
        {
            Instantiate(Chicken, collect, Quaternion.identity);
        }
        if (collect_number == 6)
        {
            Instantiate(Strawberry, collect, Quaternion.identity);
        }
        if (collect_number == 7)
        {
            Instantiate(ChickenLeg, collect, Quaternion.identity);
        }
    }
}
