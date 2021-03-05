using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    bool isAbleToTake;

    // Update is called once per frame
    void Update()
    {
        if (isAbleToTake)
        {
            isAbleToTake = false;
            SfxManager.instance.PlaySFX(0);
            GameManager.instance.AddCoin(value);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isAbleToTake = true;
        }
    }
}
