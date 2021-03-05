using UnityEngine;

public class Portal : MonoBehaviour
{
    public static bool isCanLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            isCanLoad = true;
        }
    }
}
