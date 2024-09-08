using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    private AudioSource musicSource;
    [SerializeField] AudioClip[] musicParts;
    [SerializeField] LevelManager levelManager;
    int currentSong = 0;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicParts[0];
        musicSource.Play();
        Invoke("LoopMusic", musicSource.clip.length);
    }



    void LoopMusic(){
        if(currentSong != (int)levelManager.currentWave/2){
            int time = musicSource.timeSamples;
            musicSource.clip = musicParts[(int)levelManager.currentWave/2];
            musicSource.Play();
            musicSource.timeSamples = time;
            currentSong = levelManager.currentWave/2;
        }
        Invoke("LoopMusic", musicSource.clip.length-musicSource.time);
    }
}
