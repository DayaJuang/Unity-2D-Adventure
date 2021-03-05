using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTitle : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
