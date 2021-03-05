using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Portal.isCanLoad)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        StartCoroutine(LoadToScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadToScene(int sceneToLoad)
    {
        anim.SetTrigger("Load");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneToLoad);
        Portal.isCanLoad = false;
    }
}
