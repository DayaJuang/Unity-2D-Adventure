using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterStatus characterStatus;
    public static GameManager instance;

    public float coinHeld;
    int maxHealth = 10;
    int currentMaxHealth = 3;
    float healthUpgradeBaseCost = 50;
    float attackUpgradeBaseCost = 100;
    public float[] healthUpgradeCost,attackUpgradeCost;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        characterStatus = FindObjectOfType<CharacterStatus>();
        healthUpgradeCost = new float[maxHealth];
        attackUpgradeCost = new float[7];
        SetHealthUpgradeCost();
        SetAttackUpgradeCost();
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseMaxHealth()
    {
        characterStatus.maxHealth++;
        characterStatus.currentHealth = characterStatus.maxHealth;
    }

    public void IncreaseAttackDamage()
    {
        characterStatus.attackDamage += 3;
    }

    public void AddCoin(int coin)
    {
        coinHeld += coin;
    }

    void SetHealthUpgradeCost()
    {
        for(int i = currentMaxHealth - 1; i < healthUpgradeCost.Length; i++)
        {
            healthUpgradeCost[i] = healthUpgradeBaseCost;
            healthUpgradeBaseCost = Mathf.FloorToInt(healthUpgradeBaseCost*1.75f);
            
        }
    }
    void SetAttackUpgradeCost()
    {
        for (int i = 0; i < attackUpgradeCost.Length; i++)
        {
            attackUpgradeCost[i] = attackUpgradeBaseCost;
            attackUpgradeBaseCost = Mathf.FloorToInt(attackUpgradeBaseCost * 1.5f);

        }
    }
}
