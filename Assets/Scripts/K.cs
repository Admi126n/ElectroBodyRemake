using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class with all strings needed in code. Here are all clips names, layers names, etc.
/// </summary>
static class K
{
    /// <summary>
    /// Class with layers names
    /// </summary>
    public static class L
    {
        public static readonly string ground = "Ground";
    }

    /// <summary>
    /// Class with animations names
    /// </summary>
    public static class A
    {
        public static readonly string bodyNoGunFlip = "BodyNoGunFlip";
        public static readonly string bodyGunFlip = "BodyGunFlip";
        public static readonly string bodyNoGunExitTeleport = "BodyNoGunExitTeleport";
        public static readonly string bodyNoGunTeleport = "BodyNoGunTeleport";
        public static readonly string bodyGunExitTeleport = "BodyGunExitTeleport";
        public static readonly string bodyGunTeleport = "BodyGunTeleport";
        public static readonly string armsTakeGun = "ArmsTakeGun";
        public static readonly string armsHideGun = "ArmsHideGun";
        public static readonly string armsGun = "ArmsGun";
        public static readonly string armsNoGunIdle = "ArmsNoGunIdle";
        public static readonly string armsNoGunWalk = "ArmsNoGunWalk";
    }

    /// <summary>
    /// Class with tags
    /// </summary>
    public static class T
    {
        public static readonly string teleporter = "Teleporter";
        public static readonly string exitTeleporter = "ExitTeleporter";
        public static readonly string inactiveTeleporter = "InactiveTeleporter";
        public static readonly string player = "Player";
        public static readonly string chip = "Chip";
        public static readonly string Ammo = "Ammo";
    }

    /// <summary>
    /// Class with animations controllers' parameters names
    /// </summary>
    public static class ACP
    {
        public static readonly string jump = "Jump";
        public static readonly string walk = "Walk";
        public static readonly string hasGun = "HasGun";
        public static readonly string landed = "Landed";
        public static readonly string flip = "Flip";
        public static readonly string takeGun = "TakeGun";
        public static readonly string hideGun = "HideGun";
        public static readonly string explode = "Explode";
        public static readonly string teleport = "Teleport";
        public static readonly string WalkSpeed = "WalkSpeed";
    }
}

public enum Animators
{
    BodyAnimator = 0,
    ArmsAnimator = 1
}
