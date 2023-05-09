using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator bodyAnimator;
    Animator armsAnimator;
    SpriteRenderer armsRenderer;

    private void Start()
    {
        bodyAnimator = GetComponent<Animator>();
        armsAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        armsRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void SetAnimatorsBools(string boolName, bool value)
    {
        bodyAnimator.SetBool(boolName, value);
        armsAnimator.SetBool(boolName, value);
    }

    public void SetWalkBools(bool value)
    {
        SetAnimatorsBools("Walk", value);
    }

    public void SetHasGunBools(bool value)
    {
        SetAnimatorsBools("HasGun", value);
    }

    public void SetJumpBool(bool value)
    {
        bodyAnimator.SetBool("Jump", value);
    }

    public void TriggerArmsLanding()
    {
        armsAnimator.SetTrigger("Landed");
    }

    public void TriggerBodyFlipping()
    {
        bodyAnimator.SetTrigger("Flip");
    }

    public void SetArmsAlpha(float value)
    {
        Color temp = armsRenderer.color;
        temp.a = value;
        armsRenderer.color = temp;
    }
}
