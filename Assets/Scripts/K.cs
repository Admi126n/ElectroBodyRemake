/// <summary>
/// Class with all strings needed in code. Here are all clips names, layers names, etc.
/// </summary>
static class K
{
    public static readonly string GameVersion = "Beta v1.1";

    /// <summary>
    /// Class with layers names.
    /// </summary>
    public static class L
    {
        public static readonly string Player = "Player";
        public static readonly string Ground = "Ground";
        public static readonly string Teleporter = "Teleporter";
        public static readonly string InactiveTeleporter = "InactiveTeleporter";
        public static readonly string ImmortalPlayer = "ImmortalPlayer";
        public static readonly string IgnoreRaycast = "Ignore Raycast";
        public static readonly string Platform = "Platform";
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
        public static readonly string Player = "Player";
        public static readonly string Ammo = "Ammo";
        public static readonly string PlayerBullet = "PlayerBullet";
        public static readonly string Chip = "Chip";
        public static readonly string ExitTeleporter = "ExitTeleporter";
        public static readonly string Ground = "Ground";
        public static readonly string Enemy = "Enemy";
        public static readonly string EnemyWall = "EnemyWall";
        public static readonly string EnemyBullet = "EnemyBullet";
        public static readonly string Room = "Room";
        public static readonly string Platform = "Platform";
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
        public static readonly string IsTeleporting = "IsTeleporting";
        public static readonly string SensorDestroyed = "SensorDestroyed";
        public static readonly string ActivateForceShield = "ActivateForceShield";
    }

    public static class Ammo
    {
        public static readonly int Weapon1 = 20;  // 5  20
        public static readonly int Weapon2 = 35;  // 10 35
        public static readonly int Weapon3 = 60;  // 15 60
        public static readonly int Weapon4 = 70;  // 20 70
        public static readonly int Weapon5 = 85;  // 25 85
    }

    public static class LevelName
    {
        public static readonly string mainMenu = "MainMenu";
        public static readonly string testLevel = "TestLevel";
        public static readonly string level1 = "Level_1";
        public static readonly string level2 = "Level_2";
    }
}

public enum Animators
{
    BodyAnimator = 0,
    ArmsAnimator = 1
}
