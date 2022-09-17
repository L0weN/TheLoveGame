using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardScript : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D myBody;
    public Transform attackPoint;
    public LayerMask warrior;
    public GameObject player;
    public float attackRange = 0.5f;
    private bool right = true;
    private bool canMove = true;
    private bool canTakeDamage = true;
    private bool canAttack = true;
    private float time;
    private int maxHealth = 250;
    public int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        Invoke("MoveTime", 3f);
    }

    void Update()
    {
        if (canMove)
        {
            if (right)
            {
                Move(1);
                ChangeDirection(4);
                anim.SetBool("Walk", true);
            }
            else
            {
                Move(-1);
                ChangeDirection(-4);
                anim.SetBool("Walk", true);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    void MoveTime()
    {
        if (right)
        {
            right = false;
        }
        else
        {
            right = true;
        }
        time = Random.Range(2f, 4f);
        Invoke("MoveTime", time);
    }

    void Move(int speed)
    {
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
    }
    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    public void Attack()
    {
        if (canAttack)
        {
            anim.SetBool("Attack", true);
            canTakeDamage = false;
            canMove = false;
            canAttack = false;
            StartCoroutine(endAttack());
        }
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentHealth -= damage;
            anim.SetTrigger("Hurt");
            if(currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        canTakeDamage = false;
        canMove = false;
        canAttack = false;
        anim.SetBool("Dead", true);
        StartCoroutine(endDie());
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    IEnumerator endAttack()
    {
        yield return new WaitForSeconds(0.3f);
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, warrior);
        foreach (Collider2D enemy in hitEnemy)
        {
            if (enemy.tag == "Player")
            {
                player.GetComponent<PlayerMovement>().PlayerTakeDamage(20);
            }
            
        }
        yield return new WaitForSeconds(0.7f);
        canTakeDamage = true;
        canMove = true;
        anim.SetBool("Attack", false);
        yield return new WaitForSeconds(5f);
        canAttack = true;
    }

    IEnumerator endDie()
    {
        yield return new WaitForSeconds(1.3f);
        this.gameObject.SetActive(false);
    }
}
