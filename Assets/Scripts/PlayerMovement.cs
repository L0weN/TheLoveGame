using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 6f;
    private float jump = 6f;
    private float dashPower = 10f;
    private float scaleX = 5f;
    public float attackRange = 0.5f;
    
    
    private bool isGrounded;
    private bool jumped;
    private bool rightMove = true;
    private bool canDash = true;
    private bool canMove = true;
    private bool canAttack = true;
    private bool canDoubleAttack = true;
    private bool canTakeDamage = true;
    private bool canHeal = true;
    private int currentHealth, currentMana;

    private GameObject player;
    public Transform groundCheckPosition, attackPoint;
    public LayerMask groundLayer, enemyLayer;
    private Rigidbody2D myBody;
    private Animator anim;
    public SkillMenuScript skillMenu;
    public Image lost;
    

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = 100;
        currentMana = 100;
    }

    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
        PlayerAttack();
        PlayerDoubleAttack();
        Heal();
    }

    void FixedUpdate()
    {
        PlayerWalk();
    }
    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (jumped)
        {
            
            if (h > 0)
            {
                myBody.velocity = new Vector2(3 * speed / 4, myBody.velocity.y);
                ChangeDirection(scaleX);
                rightMove = true;
            }
            else if (h < 0)
            {
                myBody.velocity = new Vector2(3 * -speed / 4, myBody.velocity.y);
                ChangeDirection(-scaleX);
                rightMove = false;
            }
            else
            {
                myBody.velocity = new Vector2(0f, myBody.velocity.y);
            }
            anim.SetFloat("Speed", Mathf.Abs((float)myBody.velocity.x));
        }
        else
        {
            if (canMove)
            {
                if (h > 0)
                {
                    myBody.velocity = new Vector2(speed, myBody.velocity.y);
                    ChangeDirection(scaleX);
                    rightMove=true;
                    SoundManagerScript.PlaySound("run");
                    if (Input.GetKey(KeyCode.C))
                    {
                        PlayerDash(dashPower);
                    }
                    if (isGrounded)
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            jumped = true;

                            myBody.velocity = new Vector2(myBody.velocity.x, jump * 3 / 2);
                            anim.SetBool("Jump", true);
                            SoundManagerScript.StopSound();
                            SoundManagerScript.PlaySound("jump");

                        }
                    }
                }
                else if (h < 0)
                {
                    myBody.velocity = new Vector2(-speed, myBody.velocity.y);
                    ChangeDirection(-scaleX);
                    rightMove = false;
                    SoundManagerScript.PlaySound("run");
                    if (Input.GetKey(KeyCode.C))
                    {
                        PlayerDash(-dashPower);
                    }
                    if (isGrounded)
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            jumped = true;

                            myBody.velocity = new Vector2(myBody.velocity.x, jump * 3 / 2);
                            anim.SetBool("Jump", true);
                            SoundManagerScript.StopSound();
                            SoundManagerScript.PlaySound("jump");

                        }
                    }

                }
                else
                {
                    myBody.velocity = new Vector2(0f, myBody.velocity.y);
                    SoundManagerScript.StopSound();
                }
                anim.SetFloat("Speed", Mathf.Abs((float)myBody.velocity.x));
            }
        }
            
    }
    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                jumped = true;
                
                myBody.velocity = new Vector2(myBody.velocity.x, jump);
                anim.SetBool("Jump", true);
                SoundManagerScript.StopSound();
                SoundManagerScript.PlaySound("jump");

            }
        }
    }
    
    void PlayerDash(float dash)
    {
        if (currentMana >= 5)
        {
            if (canDash)
            {
                canMove = false;
                canAttack = false;
                canDash = false;
                myBody.velocity = new Vector2(dash, myBody.velocity.y);
                anim.SetBool("Dash", true);
                SoundManagerScript.StopSound();
                SoundManagerScript.PlaySound("slide");
                player.GetComponent<Player>().ManaReduction(5);
                skillMenu.canSlide = false;
                StartCoroutine(endDash());
            }
        }
    }

    void PlayerAttack()
    {
        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canAttack = false;
                canMove = false;
                anim.SetBool("Attack", true);
                StartCoroutine(detectEnemy(25));
                SoundManagerScript.StopSound();
                SoundManagerScript.PlaySound("attack");
                if (rightMove)
                {
                    myBody.velocity = new Vector2(1f, myBody.velocity.y);
                }
                else
                {
                    myBody.velocity = new Vector2(-1f, myBody.velocity.y);
                }
                StartCoroutine(endAttack());
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void PlayerDoubleAttack()
    {
        if(currentMana >= 10)
        {
            if (canDoubleAttack)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    canAttack = false;
                    canMove = false;
                    canDoubleAttack = false;
                    anim.SetBool("DoubleAttack", true);
                    StartCoroutine(detectEnemy(50));
                    SoundManagerScript.StopSound();
                    SoundManagerScript.PlaySound("doubleattack");
                    player.GetComponent<Player>().ManaReduction(10);
                    if (rightMove)
                    {
                        myBody.velocity = new Vector2(2f, myBody.velocity.y);
                    }
                    else
                    {
                        myBody.velocity = new Vector2(-2f, myBody.velocity.y);
                    }
                    skillMenu.canDoubleAttack = false;
                    StartCoroutine(endDoubleAttack());
                }
            }
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            canAttack = false;
            canDoubleAttack = false;
            canMove = false;
            canTakeDamage = false;
            anim.SetBool("TakeDamage", true);
            player.GetComponent<Player>().TakeDamage(damage);
            if (rightMove)
            {
                myBody.velocity = new Vector2(-2f, 1f);
            }
            else
            {
                myBody.velocity = new Vector2(2f, 1f);
            }
            StartCoroutine(endTakeDamage());
        }
    }

    void Heal()
    {
        if (canHeal)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                player.GetComponent<Player>().Heal(25);
                skillMenu.canHeal = false;
                canHeal = false;
                StartCoroutine(endHeal());
            }
        }
    }

    public void PlayerDie()
    {
        canAttack = false;
        canDoubleAttack=false;
        canMove = false;
        canTakeDamage = false;
        StartCoroutine(endDie());
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        if (currentHealth <= 0)
        {
            lost.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }

    public void SetMana(int mana)
    {
        currentMana = mana;
    }

    
    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    IEnumerator endDash()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Dash", false);
        canMove = true;
        canAttack = true;
        canDoubleAttack = true;
        yield return new WaitForSeconds(5f);
        canDash = true;
    }

    IEnumerator endAttack()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attack", false);
        canAttack = true;
        canMove = true;
    }

    IEnumerator endDoubleAttack()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("DoubleAttack", false);
        canAttack = true;
        canMove = true;
        yield return new WaitForSeconds(3f);
        canDoubleAttack = true;
    }

    IEnumerator endTakeDamage()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("TakeDamage", false);
        canAttack = true;
        canDoubleAttack=true;
        canMove = true;
        canTakeDamage = true;
        if (currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    IEnumerator endDie()
    {
        anim.SetBool("Die", true);
        yield return new WaitForSeconds(0.9f);
        Time.timeScale = 0f;
    }

    IEnumerator endHeal()
    {
        yield return new WaitForSeconds(15f);
        canHeal = true;
    }

    IEnumerator detectEnemy(int attackDamage)
    {
        yield return new WaitForSeconds(0.3f);
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemy)
        {
            if(enemy.tag == "Bandit")
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            if(enemy.tag == "EvilWizard")
            {
                enemy.GetComponent<EvilWizardScript>().TakeDamage(attackDamage);
            }
        }
    }

}
