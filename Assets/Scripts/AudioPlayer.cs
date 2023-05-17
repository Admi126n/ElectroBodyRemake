using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Player Sounds")]
    [SerializeField] AudioClip landing;
    [SerializeField] AudioClip jumping;
    [SerializeField] AudioClip walking;
    [SerializeField] AudioClip teleporting;

    [Header("Weapon Sounds")]
    [SerializeField] AudioClip ammoPickedUp;

    [Header("Other Sounds")]
    [SerializeField] AudioClip explosion;

    private readonly float volume = 1f;

    static AudioPlayer audioPlayerInstance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (audioPlayerInstance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            audioPlayerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void PlayClip(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayLandingClip(Vector3 position)
    {
        PlayClip(landing, position, volume);
    }

    public void PlayJumpingClip(Vector3 position)
    {
        PlayClip(jumping, position, volume);
    }

    public void PlayFootStepClip(Vector3 position)
    {
        PlayClip(walking, position, volume);
    }

    public void PlayTeleportingClip(Vector3 position)
    {
        PlayClip(teleporting, position, volume);
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
