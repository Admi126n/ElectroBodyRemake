using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] Explosion explosion;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.StartPlayback();

        StartCoroutine(StartIdleAnim());
    }

    private IEnumerator StartIdleAnim()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        _animator.StopPlayback();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(K.T.Player) || collision.collider.CompareTag(K.T.PlayerBullet))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
