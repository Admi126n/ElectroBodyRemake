using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] Sprite inactiveRespawn;
    [SerializeField] Sprite activeRespawn;

    [Header("ID")]
    [SerializeField] int id;

    private GameManager _gameManager;
    private SpriteRenderer _spriteRenderer;
    private PlayerGunController _playerGun;
    private AudioPlayer _audioPlayer;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerGun = FindObjectOfType<PlayerGunController>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.Player))
        {
            _spriteRenderer.sprite = activeRespawn;

            if (id != ScenePresist.GetRespawnId())
            {
                _gameManager.ResetRespawn(ScenePresist.GetRespawnId());
                _audioPlayer.PlayRespawnSetClip(transform.position);
                _playerGun.ResetAmmo();
            }

            ScenePresist.SetRespawnId(id);
        }
    }

    public void RestRespawn()
    {
        _spriteRenderer.sprite = inactiveRespawn;
    }
    
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public int GetId()
    {
        return id;
    }
}
