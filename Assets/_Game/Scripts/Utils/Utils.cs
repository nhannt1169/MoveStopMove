public static class Utils
{
    public static string playerTag = "Player";
    public static string botTag = "Bot";
    public enum PoolType { throwable_axe = 0, throwable_boomerang = 1, throwable_dagger = 2, throwable_hammer = 3 };

    public static string animIdle = "IsIdle";
    public static string animMove = "IsMove";
    public static string animJump = "jump";
    public static string animThrow = "IsAttack";
    public static string animDie = "IsDead";

    public enum LevelIdx { level1 = 0, level2 = 1, level3 = 2 }
    public enum ItemStatus { locked = 0, unlocked = 1, equipped = 2 };

    public enum WeaponThrowType { straight = 0, spinning = 1, returning = 2 }
    public enum CharacterStatus { idle = 0, attacking = 1, waiting = 2, walking = 3, dead = 4 }

    public static string errorNotEnoughMoney = "Insufficient coins!";

    public enum ItemType { weapon = 0, hair = 1, pants = 2 }
}
