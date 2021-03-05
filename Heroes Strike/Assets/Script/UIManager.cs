using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject shopMenu;
    public Image[] playerHealth;
    public Text coinText,healthUpgradeCost,attackUpgradeCost;
    public int attackUpgradeIndex = 0;
    GameManager gm;

    public static UIManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        gm = FindObjectOfType<GameManager>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerHealth();
        UpdateCoin();
        SetUpgradeCost();
    }

    void UpdatePlayerHealth()
    {
        for (int i = 0; i < GameManager.instance.characterStatus.currentHealth; i++)
        {
            playerHealth[i].gameObject.SetActive(true);
        }
    }

    void UpdateCoin()
    {
        coinText.text = GameManager.instance.coinHeld.ToString();
    }

    public void OpenShop()
    {
        if(!shopMenu.activeInHierarchy)
        {
            shopMenu.SetActive(true);
        }
    }

    public void CloseShop()
    {
        if (shopMenu.activeInHierarchy)
        {
            shopMenu.SetActive(false);
        }
    }

    void SetUpgradeCost()
    {
        if(GameManager.instance.characterStatus.maxHealth >= GameManager.instance.healthUpgradeCost.Length)
        {
            healthUpgradeCost.text = "Max";
        }
        else
        {
            healthUpgradeCost.text = GameManager.instance.healthUpgradeCost[GameManager.instance.characterStatus.maxHealth - 1].ToString();
        }
        if(attackUpgradeIndex >= GameManager.instance.attackUpgradeCost.Length)
        {
            attackUpgradeCost.text = "Max";
        }
        else
        {
            attackUpgradeCost.text = GameManager.instance.attackUpgradeCost[attackUpgradeIndex].ToString();
        }
    }

    public void UpgradeHealth()
    {
        if(healthUpgradeCost.text != "Max")
        {
            int index = gm.characterStatus.maxHealth - 1;
            float cost = gm.healthUpgradeCost[index];
            if (gm.coinHeld >= cost)
            {
                gm.IncreaseMaxHealth();
                gm.coinHeld -= cost;
            }
            else
            {
                return;
            }
        }
    }

    public void UpgradeAttack()
    {
        if(attackUpgradeCost.text != "Max")
        {
            float cost = gm.attackUpgradeCost[attackUpgradeIndex];
            if (gm.coinHeld >= cost)
            {
                gm.IncreaseAttackDamage();
                gm.coinHeld -= cost;
                attackUpgradeIndex++;
            }
            else
            {
                return;
            }
        }
    }
}
