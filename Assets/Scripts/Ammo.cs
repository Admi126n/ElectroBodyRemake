using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ammo : MonoBehaviour
{
    Tilemap ammoTilemap;
    PlayerGunController playerGunController;

    private void Start()
    {
        ammoTilemap = GetComponent<Tilemap>();
        playerGunController = FindObjectOfType<PlayerGunController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType() == typeof(BoxCollider2D))
        {
            Vector3Int playerPosition = ammoTilemap.WorldToCell(collision.gameObject.transform.position);
            ammoTilemap.SetTile(playerPosition, null);
            ammoTilemap.SetTile(new(playerPosition.x, playerPosition.y - 1, playerPosition.z), null);  // needed when player jumps on the ammo
            playerGunController.PickUpAmmo();
        }
    }
}
