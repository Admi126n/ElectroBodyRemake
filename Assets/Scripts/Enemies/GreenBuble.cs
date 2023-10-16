using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GreenBuble : MonoBehaviour
{
    [SerializeField] float sleepTime = 3f;

    private Animator _animator;
    private float _randomSleep;

    void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(ActivateBuble());
    }

    private IEnumerator ActivateBuble()
    {
        
        while (true)
        {
            _animator.SetTrigger("ActivateBuble");

            _randomSleep = Random.Range(sleepTime - 1f, sleepTime + 1f);
            yield return new WaitForSeconds(_randomSleep);
        }
    }
}
