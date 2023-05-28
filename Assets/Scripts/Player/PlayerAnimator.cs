using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator bodyAnimator;
    private Animator armsAnimator;
    private SpriteRenderer armsRenderer;

    private bool isManagingGun;

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
        if (isManagingGun) return;

        armsAnimator.SetTrigger(K.ACP.landed);
    }

    public void TriggerBodyFlipping()
    {
        bodyAnimator.SetTrigger(K.ACP.flip);
    }

    public void TriggerGunTaking()
    {
        if (isManagingGun) return;

        armsAnimator.SetTrigger(K.ACP.takeGun);
    }

    public void TriggerGunHiding()
    {
        if (isManagingGun) return;

        armsAnimator.SetTrigger(K.ACP.hideGun);
    }

    public void TriggerTeleportation()
    {
        bodyAnimator.SetTrigger(K.ACP.teleport);
    }

    public void SetArmsAlpha(float value)
    {
        Color temp = armsRenderer.color;
        temp.a = value;
        armsRenderer.color = temp;
    }

    public string GetCurrentAnimName(Animators animatorName)
    {
        string clipName = "";

        if (animatorName == Animators.BodyAnimator)
        {
            AnimatorClipInfo[] currClipInfo = bodyAnimator.GetCurrentAnimatorClipInfo(0);
            clipName = currClipInfo[0].clip.name;
            return clipName;
        } else if (animatorName == Animators.ArmsAnimator)
        {
            AnimatorClipInfo[] currClipInfo = armsAnimator.GetCurrentAnimatorClipInfo(0);
            clipName = currClipInfo[0].clip.name;
            return clipName;
        }
        return clipName;
    }

    public void SetIsManagingGun(bool value)
    {
        isManagingGun = value;
    }
}
