/// <summary>
/// Class with all strings needed in code. Here are all clips names, layers names, etc.
/// </summary>
static class K
{
    /// <summary>
    /// Class with layers names.
    /// </summary>
    public static class L
    {
        public static readonly string Ground = "Ground";
        public static readonly string Teleporters = "Teleporters";
        public static readonly string InactiveTeleporters = "InactiveTeleporters";
        public static readonly string Bullets = "Bullets";
    }

    /// <summary>
    /// Class with animations names.
    /// </summary>
    public static class A
    {
        public static readonly string NoGunFlip = "NoGunFlip";
        public static readonly string GunFlip = "GunFlip";
        public static readonly string NoGunExitTeleport = "NoGunExitTeleport";
        public static readonly string NoGunTeleport = "NoGunTeleport";
        public static readonly string GunExitTeleport = "GunExitTeleport";
        public static readonly string GunTeleport = "GunTeleport";
        public static readonly string ArmsTakeGun = "ArmsTakeGun";
        public static readonly string ArmsHideGun = "ArmsHideGun";
        public static readonly string ArmsGun = "ArmsGun";
        public static readonly string ArmsNoGunIdle = "ArmsNoGunIdle";
        public static readonly string ArmsNoGunWalk = "ArmsNoGunWalk";
    }

    /// <summary>
    /// Class with tags.
    /// </summary>
    public static class T
    {
        public static readonly string Teleporter = "Teleporter";
        public static readonly string ExitTeleporter = "ExitTeleporter";
        public static readonly string InactiveTeleporter = "InactiveTeleporter";
        public static readonly string Player = "Player";
        public static readonly string Chip = "Chip";
        public static readonly string Ammo = "Ammo";
        public static readonly string Bullet = "Bullet";
        public static readonly string Cannon = "Cannon";
        public static readonly string Ground = "Ground";
    }

    /// <summary>
    /// Class with animations controllers' parameters names.
    /// </summary>
    public static class ACP
    {
        public static readonly string Jump = "Jump";
        public static readonly string Walk = "Walk";
        public static readonly string HasGun = "HasGun";
        public static readonly string Landed = "Landed";
        public static readonly string Flip = "Flip";
        public static readonly string TakeGun = "TakeGun";
        public static readonly string HideGun = "HideGun";
        public static readonly string Explode = "Explode";
        public static readonly string Teleport = "Teleport";
        public static readonly string WalkSpeed = "WalkSpeed";
    }
}

public enum Animators
{
    BodyAnimator = 0,
    ArmsAnimator = 1
}
