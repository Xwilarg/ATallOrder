using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    AudioSource _audioSource;

    [SerializeField] MusicClip[] musicLibrary;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        PlayMusic(SongName.Exploration);
    }

    public void PlayMusic(SongName music)
    {

        foreach(MusicClip mc in musicLibrary)
        {
            if(mc.musicName == music)
            {
                _audioSource.clip = mc.musicFile;
                _audioSource.Play();
                break;
            }
        }
    }

    [System.Serializable]
    public struct MusicClip
    {
        public SongName musicName;
        public AudioClip musicFile;
    }

    public enum SongName
    {
        Menu,
        Exploration,
        Combat
    }

}
