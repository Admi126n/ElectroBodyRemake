using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _bodyAnimator;
    private Animator _armsAnimator;

    public bool IsManagingGun { set; get; }

    public bool PlayerIsFlipping
    {
        get
        {
            string name = GetCurrentAnimName(Animators.BodyAnimator);
            return (name == K.A.NoGunFlip || name == K.A.GunFlip);
        }
    }

    public bool PlayerIsTeleporting
    {
        get
        {
            string name = GetCurrentAnimName(Animators.BodyAnimator);
            return (name == K.A.NoGunTeleport || name == K.A.NoGunExitTeleport || name == K.A.GunTeleport || name == K.A.GunExitTeleport);
        }
    }

    private void Start()
    {
        _bodyAnimator = GetComponent<Animator>();
        _armsAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    private void SetAnimatorsBools(string boolName, bool value)
    {
        _bodyAnimator.SetBool(boolName, value);
        _armsAnimator.SetBool(boolName, value);
    }

    public void SetWalkBools(bool value)
    {
        SetAnimatorsBools(K.ACP.Walk, value);
    }

    public void SetHasGunBools(bool value)
    {
        SetAnimatorsBools(K.ACP.HasGun, value);
    }

    public void SetWalkSpeed(float value)
    {
        _bodyAnimator.SetFloat(K.ACP.WalkSpeed, value);
        _armsAnimator.SetFloat(K.ACP.WalkSpeed, value);
    }

    public void SetJumpBool(bool value)
    {
        _bodyAnimator.SetBool(K.ACP.Jump, value);
    }

    public void SetIsTeleportingBool(bool value)
    {
        _bodyAnimator.SetBool(K.ACP.IsTeleporting, value);
    }

    public void TriggerArmsLanding()
    {
        if (IsManagingGun) return;

        _armsAnimator.SetTrigger(K.ACP.Landed);
    }

    public void TriggerBodyFlipping()
    {
        _bodyAnimator.SetTrigger(K.ACP.Flip);
    }

    public void TriggerGunTaking()
    {
        if (IsManagingGun) return;

        _armsAnimator.SetTrigger(K.ACP.TakeGun);
    }

    public void TriggerGunHiding()
    {
        if (IsManagingGun) return;

        _armsAnimator.SetTrigger(K.ACP.HideGun);
    }

    public void TriggerTeleportation()
    {
        // if (_isTeleporting) return;

        _bodyAnimator.SetTrigger(K.ACP.Teleport);
    }

    public bool GetIsTeleportingBool()
    {
        return _bodyAnimator.GetBool(K.ACP.IsTeleporting);
    }

    public string GetCurrentAnimName(Animators animatorName)
    {
        string clipName = "";

        if (animatorName == Animators.BodyAnimator)
        {
            AnimatorClipInfo[] currClipInfo = _bodyAnimator.GetCurrentAnimatorClipInfo(0);
            clipName = currClipInfo[0].clip.name;
            return clipName;
        } else if (animatorName == Animators.ArmsAnimator)
        {
            AnimatorClipInfo[] currClipInfo = _armsAnimator.GetCurrentAnimatorClipInfo(0);
            clipName = currClipInfo[0].clip.name;
            return clipName;
        }
        return clipName;
    }
}
