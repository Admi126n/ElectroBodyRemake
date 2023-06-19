using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Explosion explosion;

    private SpriteRenderer _bodyRenderer;
    private SpriteRenderer _armsRenderer;

    private readonly int _ExplosionsCounter = 7;

    private void Start()
    {
        _bodyRenderer = GetComponent<SpriteRenderer>();
        _armsRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.Enemy))
        {
            Debug.Log("You died");

            _bodyRenderer.enabled = false;
            _armsRenderer.enabled = false;

            StartCoroutine(SpawnExplosions());
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


}
