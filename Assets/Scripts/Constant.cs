using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    public static readonly int NUM_TO_SHOW_RATE = 5;
    public static readonly float POINT_TO_THREE_STAR = 5f;
    public static readonly float POINT_TO_TWO_STAR = 4.5f;
   // public static readonly int POINT_TO_ONE_STAR = 5;

    public static readonly int MAX_LEVEL = 60;
    public static readonly int MAX_ROUND = 10;
    public static readonly float HEIGHT_DRAG_OBJECT = 2f;
    public static readonly float HEIGHT_DRAG_OBJECT_MAX = 2f;
    public static readonly float TIME_CHECK_TARGET_NAV_PATH = 0.2f;
    public static readonly int LEVEL_BONUS_MONEY = 200;

    public static float DELAY_BLINK_GLASS = 0.2f;

    public static readonly string TAG_FLOOR = "Floor";
    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_BONE = "Bone";

    public static readonly string ANIM_IDLE = "Idle";
    public static readonly string ANIM_IDLE_GUN = "IdleGun";
    public static readonly string ANIM_WALK = "Walk";
    public static readonly string ANIM_RUN = "Run";

    //  public static readonly string ANIM_RUN = "";
    public static readonly string ANIM_JUMP_DOWN = "JumpDown";
    public static readonly string ANIM_JUMP = "Jump";
    public static readonly string ANIM_END_JUMP = "EndJump";
    public static readonly string ANIM_GUN_ATTACK = "GunAttack";
    public static readonly string ANIM_ATTACK = "Attack";
    public static readonly string ANIM_SHIELD = "Shield";
    public static readonly string ANIM_VICTORY = "Victory";

    public static readonly int NUM_SHOW_RATE = 4;
    public static readonly int COST_PER_UPDATE = 200;
    public static readonly int EXP_PER_ADS = 800;



    //--------------------PlayerPref
    public static readonly string KEY_MONEY = "Money";
    public static readonly string KEY_KEY = "Key";
    public static readonly string KEY_BONUS = "Bonus";
    public static readonly string KEY_LEVEL = "Level";
    public static readonly string KEY_HAT = "Hat";
    public static readonly string KEY_SKIN = "Skin";
    public static readonly string KEY_CURRENT_ITEM_BONUS = "CurrentItemBonus";
    public static readonly string KEY_CURRENT__TYPE_ITEM_BONUS = "CurrentTypeBonus";
    public static readonly string KEY_SAVE_BED = "BED";
    public static readonly string KEY_SAVE_BIG_CABINET = "BIG_CABINET";
    public static readonly string KEY_SAVE_SMALL_CABINET = "SMALL_CABINET";
    public static readonly string KEY_SAVE_CARPET = "CARPET";
    public static readonly string KEY_SAVE_PICTURE = "PICTURE";
    public static readonly string KEY_SAVE_SKIN = "ListSkins";

    public static readonly float TIME_CHECK_REWARD = 1;



    // public static readonly string SCENE_PLAY

    //  public static readonly float RATE_BIG_DIAMOND_SPIN = 8000;

#if UNITY_ANDROID
    public static readonly string ADMOB_BANNER = "d2e158f58b4ca7f9";
    public static readonly string ADMOB_INTERSTITIAL = "74e3413049e43f2a";
    public static readonly string ADMOB_REWARD = "49c91d8204a5bbf5";
    public const string MAX_SDK_KEY = "ZoNyqu_piUmpl33-qkoIfRp6MTZGW9M5xk1mb1ZIWK6FN9EBu0TXSHeprC3LMPQI7S3kTc1-x7DJGSV8S-gvFJ";

#elif UNITY_IOS
    public static readonly string ADMOB_BANNER = "a56b4eb069ae90ee";
    public static readonly string ADMOB_INTERSTITIAL = "a26bb1414e3f2041";
    public static readonly string ADMOB_REWARD = "958ce153bf292dd1";
    public const string MAX_SDK_KEY = "ZoNyqu_piUmpl33-qkoIfRp6MTZGW9M5xk1mb1ZIWK6FN9EBu0TXSHeprC3LMPQI7S3kTc1-x7DJGSV8S-gvFJ";
#endif

}
