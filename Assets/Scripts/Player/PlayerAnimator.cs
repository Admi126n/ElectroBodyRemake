using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator bodyAnimator;
    private Animator armsAnimator;
    private SpriteRenderer armsRenderer;

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
        SetAnimatorsBools(K.ACP.walk, value);
    }

    public void SetHasGunBools(bool value)
    {
        SetAnimatorsBools(K.ACP.hasGun, value);
    }

    public void SetJumpBool(bool value)
    {
        bodyAnimator.SetBool(K.ACP.jump, value);
    }

    public void TriggerArmsLanding()
    {
        armsAnimator.SetTrigger(K.ACP.landed);
    }

    public void TriggerBodyFlipping()
    {
        bodyAnimator.SetTrigger(K.ACP.flip);
    }

    public void TriggerGunTaking()
    {
        armsAnimator.SetTrigger(K.ACP.takeGun);
    }

    public void TriggerGunHiding()
    {
        armsAnimator.SetTrigger(K.ACP.hideGun);
    }

    public void SetArmsAlpha(float value)
    {
        Color temp = armsRenderer.color;
        temp.a = value;
        armsRenderer.color = temp;
    }
}
