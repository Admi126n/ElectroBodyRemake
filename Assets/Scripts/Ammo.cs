using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Script used with ammo tile map
/// </summary>
public class Ammo : MonoBehaviour
{
    private Tilemap ammoTilemap;
    private PlayerGunController playerGunController;

    private void Start()
    {
        ammoTilemap = GetComponent<Tilemap>();
        playerGunController = FindObjectOfType<PlayerGunController>();
    }

    /// <summary>
    /// On trigger enter, when player collide with tile map, script deletes ammo from its tilemap.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.player))
        {
            // remove picked up ammo from tilemap
            Vector3Int playerPosition = ammoTilemap.WorldToCell(collision.gameObject.transform.position);
            ammoTilemap.SetTile(new(playerPosition.x, playerPosition.y - 1, playerPosition.z), null);
            playerGunController.PickUpAmmo();
        }
    }
}
