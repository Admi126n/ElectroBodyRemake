using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Player Sounds")]
    [SerializeField] AudioClip landingClip;
    [SerializeField] AudioClip jumpingClip;
    [SerializeField] AudioClip walkingClip;

    [Header("Weapon Sounds")]
    [SerializeField] AudioClip ammoPickedUp;

    [Header("Other Sounds")]
    [SerializeField] AudioClip explosion;

    float landingVolume = 1f;
    float jumpingVolume = 1f;
    float walkingVolume = 1f;

    static AudioPlayer instance;

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

    public void PlayFootStepClip(Vector3 position)
    {
        PlayClip(walkingClip, position, walkingVolume);
    }

    public void PlayAmmoPickedUpClip(Vector3 position)
    {
        PlayClip(ammoPickedUp, position, 1f);
    }

    public void PlayExplosionClip(Vector3 position)
    {
        PlayClip(explosion, position, 1f);
    }
}
