using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] Explosion explosion;

    private Animator _animator;
    private BoxCollider2D _collider;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _animator.StartPlayback();

        StartCoroutine(StartIdleAnim());
    }

    private IEnumerator StartIdleAnim()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        _animator.StopPlayback();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(K.T.PlayerBullet))
        {
            _animator.SetBool(K.ACP.SensorDestroyed, true);
            _collider.enabled = false;
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
