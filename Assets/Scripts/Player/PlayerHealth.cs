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
    PlayerController _player;

    private readonly int _ExplosionsCounter = 7;
    private bool _isAlive = true;

    private void Start()
    {
        _bodyRenderer = GetComponent<SpriteRenderer>();
        _armsRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _gameManager = FindObjectOfType<GameManager>();
        _player = GetComponent<PlayerController>();

        SpawnPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.EnemyBullet) && _isAlive)
        {
            KillPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(K.T.Enemy) && _isAlive)
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

    private void SpawnPlayer()
    {
        Vector3 respawnPosition = _gameManager.GetRespawnPosition(ScenePresist.GetRespawnId());
        gameObject.transform.position = new(respawnPosition.x, respawnPosition.y + 0.5f, respawnPosition.z);
        _isAlive = true;
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2f);

        _gameManager.ResetGameSession();
    }

    private void KillPlayer()
    {
        _isAlive = false;
        _bodyRenderer.enabled = false;
        _armsRenderer.enabled = false;
        gameObject.layer = LayerMask.NameToLayer(K.L.ImmortalPlayer);

        // TODO play death animation (yes, you have to do special anim)

        StartCoroutine(SpawnExplosions());
        _player.SetPlayerInput(false);

        StartCoroutine(RespawnPlayer());
    }

    public bool GetIsAlive()
    {
        return _isAlive;
    }
}
