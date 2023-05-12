using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bulletRigidbody;
    PlayerController playerController;

    readonly float bulletSpeed = 8;
    float bulletDirection;

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();

        transform.localScale = new(-playerController.transform.localScale.x, transform.localScale.y, transform.localScale.z);
        bulletDirection = -playerController.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        bulletRigidbody.velocity = new(bulletDirection, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        // TODO: play explosion animation and sound(in place of bullet) and then destroy object
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
