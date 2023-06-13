using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] AudioClip shootingClip;
    [SerializeField] Sprite bulletSprite;
    [SerializeField] bool isDestroyable;
    [SerializeField] Sprite destroyedCannon;
    [SerializeField] Explosion explosion;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroyable && (collision.CompareTag(K.T.Bullet)))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            _boxCollider.enabled = false;
            _spriteRenderer.sprite = destroyedCannon;
        }
    }
}
