using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange;

    public int noOfAttacks = 0;
    float delayBetweenAttack = .9f;
    float lastAttackTimer = 0f;
    float attackRate = 2f;
    float nextAttackTime = 0f;
    bool isDefence;
    int damage;
    int criticalChance;

    public DamageText damageText;
    PlayerMovement player;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(!PauseMenu.isGamePaused && !UIManager.instance.shopMenu.activeInHierarchy)
        {
            if (Time.time - lastAttackTimer > delayBetweenAttack)
            {
                noOfAttacks = 0;
            }

            if (Input.GetButtonDown("Fire1") && !isDefence && Time.time>=nextAttackTime)
            {
                lastAttackTimer = Time.time;
                noOfAttacks++;
                if (noOfAttacks == 1)
                {
                    player.isAttacking = true;
                    anim.SetBool("Attack1", true);
                    Attack();
                }
                noOfAttacks = Mathf.Clamp(noOfAttacks, 0, 3);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                isDefence = true;
            }
            if (Input.GetButtonUp("Fire2"))
            {
                isDefence = false;
            }
            Defence();
        }
    }

    void Attack()
    {
        damage = GameManager.instance.characterStatus.attackDamage;
        criticalChance = GameManager.instance.characterStatus.criticalChance;

        int dmg = Random.Range(damage - 2, damage + 2);
        int critical = Random.Range(0, 100);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in enemies)
        {
            SfxManager.instance.PlaySFX(1);

            if(enemy.GetComponent<Enemy>() != null)
            {
                if (!enemy.GetComponent<Enemy>().isDead)
                {
                    if (critical < criticalChance)
                    {
                        dmg *= 2;
                        Instantiate(damageText, enemy.transform.position, enemy.transform.rotation).SetCriticalDamageText(dmg);
                    }
                    else
                    {
                        Instantiate(damageText, enemy.transform.position, enemy.transform.rotation).SetDamageText(dmg);
                    }
                    enemy.GetComponent<Enemy>().TakeDamage(dmg);
                }
            }
            else
            {
                if (!enemy.GetComponent<Boss>().isDead)
                {
                    if (critical < criticalChance)
                    {
                        dmg *= 2;
                        Instantiate(damageText, enemy.transform.position, enemy.transform.rotation).SetCriticalDamageText(dmg);
                    }
                    else
                    {
                        Instantiate(damageText, enemy.transform.position, enemy.transform.rotation).SetDamageText(dmg);
                    }
                    enemy.GetComponent<Boss>().TakeDamage(dmg);
                }
            }           
        }
    }

    public void Attack1()
    {
        if (noOfAttacks >= 2)
        {
            anim.SetBool("Attack2", true);
            Attack();
        }
        else
        {
            player.isAttacking = false;
            anim.SetBool("Attack1", false);
            nextAttackTime = Time.time + 1f / attackRate;
            noOfAttacks = 0;
        }
    }

    public void Attack2()
    {
        if (noOfAttacks >= 3)
        {
            anim.SetBool("Attack3", true);
            Attack();
        }
        else
        {
            player.isAttacking = false;
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", false);
            nextAttackTime = Time.time + 1f / attackRate;
            noOfAttacks = 0;
        }
    }

    public void Attack3()
    {
        player.isAttacking = false;
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);
        nextAttackTime = Time.time + 1f / attackRate;
        noOfAttacks = 0;
    }

    public void Defence()
    {
        anim.SetBool("isDefence", isDefence);
        GameManager.instance.characterStatus.isInvulnerable = isDefence;
        player.isDefending = isDefence;
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
