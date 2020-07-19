using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip loseMusic;

    // Cached Components
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeMusic(levelMusic);
    }

    public void ChangeToLoseMusic()
    {
        ChangeMusic(loseMusic);
    }

    private void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();
    }
}
