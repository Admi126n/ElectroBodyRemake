using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceShield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateForceShield());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ActivateForceShield()
    {
        yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));

        foreach (Transform child in transform)
        {
            Animator childAnimator = child.GetComponent<Animator>();
            childAnimator.SetBool(K.ACP.ActivateForceShield, true);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
