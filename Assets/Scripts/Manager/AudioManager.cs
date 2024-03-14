using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    AudioSource _audioSource;

    [SerializeField] MusicClip[] musicLibrary;

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
        var clip = musicLibrary.First(x => x.musicName == music);
        _audioSource.clip = clip.musicFile;
        _audioSource.Play();
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
