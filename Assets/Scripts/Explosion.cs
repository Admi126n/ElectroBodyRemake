using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioPlayer _audioPlayer;

    private void Start()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _audioPlayer.PlayExplosionClip(transform.position);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
