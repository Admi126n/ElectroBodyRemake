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
    [SerializeField] AudioClip bullet_1_clip;
    [SerializeField] AudioClip bullet_2_clip;
    [SerializeField] AudioClip bullet_3_clip;
    [SerializeField] AudioClip bullet_4_clip;
    [SerializeField] AudioClip bullet_5_clip;
    [SerializeField] AudioClip ammoPickedUp;

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

    public void PlayBulletClip(int weapon, Vector3 position)
    {
        switch (weapon)
        {
            case 1:
                PlayClip(bullet_1_clip, position, 1f);
                break;
            case 2:
                PlayClip(bullet_2_clip, position, 1f);
                break;
            case 3:
                PlayClip(bullet_3_clip, position, 1f);
                break;
            case 4:
                PlayClip(bullet_4_clip, position, 1f);
                break;
            case 5:
                PlayClip(bullet_5_clip, position, 1f);
                break;
        }
    }

    public void PlayAmmoPickedUpClip(Vector3 position)
    {
        PlayClip(ammoPickedUp, position, 1f);
    }
}
