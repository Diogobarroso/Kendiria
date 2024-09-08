using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    private AudioSource musicSource;
    [SerializeField] AudioClip[] musicParts;
    [SerializeField] LevelManager levelManager;
    int currentSong = -1;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        LoopMusic();
    }



    void LoopMusic(){
        if(currentSong != (int)levelManager.currentWave/2){
            int time = musicSource.timeSamples;
            musicSource.clip = musicParts[(int)levelManager.currentWave/2];
            musicSource.Play();
            musicSource.timeSamples = time;
            if(currentSong >= 0){
                musicParts[currentSong].UnloadAudioData();
            }
            currentSong = levelManager.currentWave/2;
            if(currentSong+1 <3){
                musicParts[currentSong+1].LoadAudioData();
            }
        }
        Invoke("LoopMusic", musicSource.clip.length-musicSource.time);
    }
}
