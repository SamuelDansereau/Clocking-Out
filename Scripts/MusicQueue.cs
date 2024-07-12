using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicQueue : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musicClips;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = musicClips[0];
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (musicSource.isPlaying == false)
        {
            musicSource.clip = musicClips[1];
            musicSource.Play();
        }
    }
}
