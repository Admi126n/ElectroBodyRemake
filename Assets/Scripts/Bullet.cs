using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRigidbody;
    PlayerController playerController;

    float bulletSpeed;

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();

        bulletSpeed = -playerController.transform.localScale.x;
    }

    void Update()
    {
        bulletRigidbody.velocity = new(bulletSpeed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
