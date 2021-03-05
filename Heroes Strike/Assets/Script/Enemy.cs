using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;

    Animator enemyAnim;
    SpriteRenderer enemySprite;
    CapsuleCollider2D capsuleCollider2D;
    Rigidbody2D rb;
    EnemyAI enemyAI;

    [Header("Enemy Status")]
    public int maxHealth;
    public int coin;
    public float attackRange, attackRate,agroDistance;

    int health;
    public bool isDead;
    Vector3 healthBarOffset = new Vector3(0, 1.3f, 0);

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        enemySprite = GetComponent<SpriteRenderer>();
        enemyAI = GetComponent<EnemyAI>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = transform.position + healthBarOffset;

        if (isDead)
        {
            StartCoroutine(Dead());
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        enemyAnim.SetTrigger("TakeDamage");
        healthBar.UpdateHealth(health);
        if (health <= 0)
        {
            isDead = true;
        }
    }

    IEnumerator Dead()
    {
        enemyAnim.SetBool("isDead", true);
        capsuleCollider2D.isTrigger = true;
        enemyAI.enabled = false;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(.05f);

        enemySprite.color = new Color(Mathf.MoveTowards(enemySprite.color.r, 0f, 1f*Time.deltaTime), Mathf.MoveTowards(enemySprite.color.g, 0f, 1f*Time.deltaTime), Mathf.MoveTowards(enemySprite.color.b, 0f, 1f* Time.deltaTime), Mathf.MoveTowards(enemySprite.color.a, 0f, 1f* Time.deltaTime));
        
        if (enemySprite.color.a == 0)
        {
            GameManager.instance.AddCoin(coin);
            gameObject.SetActive(false);
        }
    }
}
