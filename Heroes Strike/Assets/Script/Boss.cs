using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public string bossName;
    public int maxHealth;
    public int currentHealth;
    public bool isDead = false;

    public LayerMask playerLayer;
    public Transform attackPoint;

    public Text nameText;
    public GameObject DialogText;
    public HealthBar healthBar;

    public float attackRange = 1f;
    Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        nameText.text = bossName;
        healthBar.SetMaxHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.UpdateHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            isDead = true;
            anim.SetBool("isDeath", true);

            StartCoroutine(Dead());
        }
    }

    public void Attack()
    {
        Collider2D players = Physics2D.OverlapCircle(attackPoint.transform.position, attackRange, playerLayer);

        if (players != null)
        {
            players.GetComponent<CharacterStatus>().TakeDamage();
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1f);

        DialogText.GetComponentInChildren<Text>().text = "Im...Impossible!";
        DialogText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        GetComponent<Animator>().SetBool("isDeath", false);
        DialogText.gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("Died");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
