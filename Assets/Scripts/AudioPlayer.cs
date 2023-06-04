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
    [SerializeField] AudioClip chipPickedUp;

    private readonly float _Volume = 1f;

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
        PlayClip(landing, position, _Volume);
    }

    public void PlayJumpingClip(Vector3 position)
    {
        PlayClip(jumping, position, _Volume);
    }

    public void PlayFootStepClip(Vector3 position)
    {
        PlayClip(walking, position, _Volume);
    }

    public void PlayTeleportingClip(Vector3 position)
    {
        PlayClip(teleporting, position, _Volume);
    }

    public void PlayAmmoPickedUpClip(Vector3 position)
    {
        PlayClip(ammoPickedUp, position, _Volume);
    }

    public void PlayChipPickedUpClip(Vector3 position)
    {
        PlayClip(chipPickedUp, position, _Volume);
    }

    public void PlayExplosionClip(Vector3 position)
    {
        PlayClip(explosion, position, _Volume);
    }
}
