using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public AudioSource backgroundTrack;
    public AudioSource backgroundTrackTournament;
    public AudioSource time;
    //public AudioSource gameTrack;

    private void Awake()
    {
        // Older way to setup singletons
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (backgroundTrack)
            backgroundTrack.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLevelAudio()
    {
        if (backgroundTrack && backgroundTrackTournament)
        {
            backgroundTrack.Stop();
            backgroundTrackTournament.Play();
        }
        else if (backgroundTrack)
        {
            backgroundTrack.Stop();
            backgroundTrack.Play();
        }
    }

    public void EndLevelAudio()
    {
        time.Play();
        if (backgroundTrack && backgroundTrackTournament)
        {
            backgroundTrackTournament.Stop();
            backgroundTrack.Play();
        }
        else if (backgroundTrack)
        {
            backgroundTrack.Stop();
            backgroundTrack.Play();
        }
    }
}
