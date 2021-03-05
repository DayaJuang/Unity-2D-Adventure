using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource[] sfx;

    public static SfxManager instance;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFX(int soundToPlay)
    {
        if(soundToPlay < sfx.Length)
        {
            sfx[soundToPlay].Play();
        }
    }
}
