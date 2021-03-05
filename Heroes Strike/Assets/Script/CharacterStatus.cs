using System.Collections;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public int attackDamage;
    public int criticalChance;
    public bool isInvulnerable;

    public static bool isDead = false;
    public static CharacterStatus instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);        
    }

    public void TakeDamage()
    {
        if (isInvulnerable)
        {
            GetComponent<Animator>().SetTrigger("Defence2");
            return;
        }
        
        if(currentHealth > 0)
        {
            SfxManager.instance.PlaySFX(2);
            UIManager.instance.playerHealth[currentHealth - 1].gameObject.SetActive(false);
            currentHealth--;
            StartCoroutine(TakeDamageAnimation());

            if(currentHealth <= 0)
            {
                isDead = true;
                Die();
            }
        }
    }

    void Die()
    {
        GetComponent<Animator>().SetBool("Death", true);
    }

    IEnumerator TakeDamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for(int i = 0; i < 3; i++)
        {
            isInvulnerable = true;

            foreach(SpriteRenderer sr in srs)
            {
                Color color = sr.color;
                color.a = 0;
                sr.color = color;
            }

            yield return new WaitForSeconds(.1f);

            foreach(SpriteRenderer sr in srs)
            {
                Color color = sr.color;
                color.a = 1f;
                sr.color = color;
            }

            yield return new WaitForSeconds(.1f);
        }

        isInvulnerable = false;
    }
}
