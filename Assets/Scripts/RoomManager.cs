using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Camera _mainCamera;
    GameObject targerPosition;
    public int speed = 6;
    bool moveEnabled = false;

    private void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();

        targerPosition = new();
    }

    private void Update()
    {
        if (moveEnabled)
        {
            _mainCamera.transform.position = Vector3.Lerp(_mainCamera.gameObject.transform.position,
                targerPosition.transform.position,
                speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.Player) && !collision.isTrigger)
        {
            targerPosition.transform.position = new(gameObject.transform.position.x, gameObject.transform.position.y, -10f);
            moveEnabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.Player) && !collision.isTrigger)
        {
            moveEnabled = false;
        }
    }
}
