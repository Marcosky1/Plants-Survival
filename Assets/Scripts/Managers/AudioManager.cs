using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource sfx;

    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;
        sfx.volume = volume;
    }

    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }
}

