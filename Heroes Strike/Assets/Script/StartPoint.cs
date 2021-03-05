using UnityEngine;

public class StartPoint : MonoBehaviour
{
    CharacterStatus player;

    private void Awake()
    {
        player = FindObjectOfType<CharacterStatus>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
        {
            player.transform.position = gameObject.transform.position;
        }
    }
}
