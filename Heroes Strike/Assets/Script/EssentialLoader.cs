using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject sceneLoader;
    public GameObject uiManager;
    public GameObject player;
    public GameObject gameManager;
    public GameObject sfx;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<SceneLoader>() == null)
        {
            Instantiate(sceneLoader).GetComponent<SceneLoader>();
        }

        if (FindObjectOfType<UIManager>() == null)
        {
            Instantiate(uiManager).GetComponent<UIManager>();
        }

        if(SfxManager.instance == null)
        {
            SfxManager.instance = Instantiate(sfx).GetComponent<SfxManager>();
        }
        
        if(CharacterStatus.instance == null)
        {
            CharacterStatus.instance = Instantiate(player).GetComponent<CharacterStatus>();
        }

        if(GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameManager).GetComponent<GameManager>();
        }
    }
}
