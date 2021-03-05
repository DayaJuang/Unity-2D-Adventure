using UnityEngine;
using UnityEngine.UI;

public class Shopkeeper : MonoBehaviour
{
    public Image bubbleText;
    Animator anim;
    bool canOpenShop;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckPlayer();

        if (canOpenShop)
        {
            bubbleText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.M))
            {

                anim.SetTrigger("isOpenShop");
                UIManager.instance.OpenShop();
            }
        }
        else
        {
            bubbleText.gameObject.SetActive(false);
        }
    }

    void CheckPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if(distance <= 1f)
        {
            canOpenShop = true;
        }
        else
        {
            canOpenShop = false;
        }
    }
}
