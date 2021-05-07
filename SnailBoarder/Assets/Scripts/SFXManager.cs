using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public AudioSource skateboard;
    public AudioSource railgrind;
    public AudioSource jump;
    public AudioSource land;

    public float minPitch = 0.85f;
    public float maxPitch = 1.15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySkateboard(float pitch)
    {
        if (!skateboard.isPlaying)
        {
            skateboard.Play();
        }
        else
        {
            skateboard.UnPause();
        }
        //update pitch
        skateboard.pitch = Mathf.Lerp(minPitch, maxPitch, pitch);
    }

    public void PauseSkateboard()
    {
        skateboard.Pause();
    }

    public void PlayRailgrind()
    {
        //PlayJump();
        railgrind.Play();
    }

    public void StopRailgrind()
    {
        railgrind.Stop();
        //PlayJump();
    }

    public void PlayJump()
    {
        jump.Play();
    }

    public void PlayJumpDelayed(float time)
    {
        jump.PlayDelayed(time);
    }

    public void PlayLand()
    {
        land.Play();
    }
    
    public void PlayLandDelayed(float time)
    {
        land.PlayDelayed(time);
    }
}
