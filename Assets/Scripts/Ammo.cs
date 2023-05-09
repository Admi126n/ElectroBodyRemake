using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ammo : MonoBehaviour
{
    Tilemap test;
    PlayerGunController playerGunController;

    private void Start()
    {
        test = GetComponent<Tilemap>();
        playerGunController = FindObjectOfType<PlayerGunController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3Int x = test.WorldToCell(collision.gameObject.transform.position);
            test.SetTile(x, null);
            playerGunController.PickUpAmmo();
        }
    }
}
