using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Difficulty
{
    Easy,
    Medium,
    Hard,
    None
}

public class Constant : MonoBehaviour
{
    public enum GameEntity
    {
        PLAYER,
        PEDESTRIAN_GIRL_A,
        PEDESTRIAN_GIRL_B,
        PEDESTRIAN_GIRL_C,
        PEDESTRIAN_GIRL_D,
        PEDESTRIAN_GIRL_E,
        PEDESTRIAN_BOY_A,
        PEDESTRIAN_BOY_B,
        PEDESTRIAN_BOY_C,
        PEDESTRIAN_BOY_D,
        PEDESTRIAN_BOY_E,
        PEDESTRIAN_BOY_F,
        ZOMBIE_A,
        ZOMBIE_B,
        ZOMBIE_C,
        BULLET,
        MAFIA_A,
        MAFIA_B,
        MAFIA_C,
        COP_A,
        COP_B,
        COP_C,
        ASSASSIN,
        ASSASSIN_A,
        ASSASSIN_B,
        ASSASSIN_C,
        MARINES_A,
        MARINES_B
    } 

    public static class UtilityData
    {
        public static bool IsInternetAvailable => Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        public static bool isMenuTransition = false;
    }

    
    public class LAYERS
    {
        public const string ENVTERRAIN = "EnvTerrain";
    }

    public enum TAGS_ENUM
    {
        Player,
        Pedestrian,
        Enemy,
    }

    public class TAGS
    {
        public const string PEDESTRIAN = "Pedestrian";
        public const string PEDESTRIANPOINT = "PedestrianPoint";
        public const string PLAYER = "Player";
        public const string PEDESTRIAN_REMOVER = "PedestrianRemover";
        public const string SEA_TRIGGER = "SeaTrigger";
        public const string ENEMY = "Enemy";
        public const string HEAD = "Head";
        public const string MISSION_OBJECTS = "MissionObjects";
    }


    public static bool isMainMenu = true;
    public static bool isFirstPlay = true;

    public class Levels
    {
        public const int totalLevels = 10;
    }

    public class Scenes
    {
        public const string gameplayScene = "day";
        public const string maineMenu = "MainMenu";
    }



    public class ANIMATOR_STRINGS
    {
        public const string SPEED = "Forward";
        public const string TURN = "Turn";

        public const string CROUCH = "Crouch";
        public const string ONGROUND = "OnGround";

        public const string RIFLE = "IsRifle";
        public const string PISTOL = "IsPistol";
        public const string MELEE = "IsMelee";

        public const string EQUIP = "Equip";
        public const string AIM = "Aim";
        public const string SCARED = "Scared";

        public const string KICK_COUNT = "KickCount";
        public const string PUNCH_COUNT = "PunchCount";

        public const string PUNCH = "Punch";
        public const string KICK = "Kick";

        public const string ATTACK = "Attack";
        public const string ISHIT = "IsHit";

        public const string COMBO_INDEX = "ComboIndex";
        public const string IS_FIGHTING = "IsFighting";
    }

    public static class GameplayData
    {
        public static int currentLevel = 1;
        public static int totalLevels = 10;

        public static bool allLevelsUnlocked = false;

        public static bool IsLastLevel => currentLevel >= totalLevels;
    }
}

