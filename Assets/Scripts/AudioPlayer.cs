using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Player Sounds")]
    [SerializeField] AudioClip landingClip;
    [SerializeField] AudioClip jumpingClip;
    [SerializeField] AudioClip walkingClip;

    float landingVolume = 1f;
    float jumpingVolume = 1f;
    float walkingVolume = 1f;

    static AudioPlayer instance;

    private bool playWalkingClip = true;

    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void PlayClip(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayLandingClip(Vector3 position)
    {
        PlayClip(landingClip, position, landingVolume);
    }

    public void PlayJumpClip(Vector3 position)
    {
        PlayClip(jumpingClip, position, jumpingVolume);
    }

    public void PlayWalkClip(Vector3 position)
    {
        if (playWalkingClip)
        {
            PlayClip(walkingClip, position, walkingVolume);
            playWalkingClip = false;
            StartCoroutine(StopPlaying());
        }
    }

    IEnumerator StopPlaying()
    {
        yield return new WaitForSeconds(0.3f);
        playWalkingClip = true;
    }
}
