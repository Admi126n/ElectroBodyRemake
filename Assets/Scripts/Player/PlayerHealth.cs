using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Explosion explosion;

    private SpriteRenderer _bodyRenderer;
    private SpriteRenderer _armsRenderer;
    private GameManager _gameManager;
    private PlayerInput _playerInput;

    private readonly int _ExplosionsCounter = 7;

    private void Start()
    {
        _bodyRenderer = GetComponent<SpriteRenderer>();
        _armsRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _gameManager = FindObjectOfType<GameManager>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.EnemyBullet))
        {
            KillPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(K.T.Enemy))
        {
            KillPlayer();
        }
    }


    private IEnumerator SpawnExplosions()
    {
        for (int i = 0; i < _ExplosionsCounter; i++)
        {
            float xPos = transform.position.x + Random.Range(-0.3f, 0.3f);
            float yPos = transform.position.y + Random.Range(-1f, 0.7f);
            Vector3 explosionPos = new(xPos, yPos, 0f);

            Instantiate(explosion, explosionPos, transform.rotation, transform);

            yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
        }
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2f);

        _gameManager.ResetGameSession();
    }

    private void KillPlayer()
    {
        _bodyRenderer.enabled = false;
        _armsRenderer.enabled = false;
        gameObject.layer = LayerMask.NameToLayer(K.L.ImmortalPlayer);

        // TODO play death animation (yes, you have to do special anim)

        StartCoroutine(SpawnExplosions());

        _playerInput.enabled = false;
        StartCoroutine(RespawnPlayer());
    }

}
