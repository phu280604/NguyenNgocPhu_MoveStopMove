using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringCollection
{
    #region --- Fields ---

    #region -- Save data --
    public static readonly string ROOT_JSON_DATA = Application.persistentDataPath;
    public static readonly string VISUAL_DATA = "VisualData";
    public static readonly string LEVEL_DATA = "LevelData";
    #endregion

    #region -- Name effect --
    public static readonly string BONUS_NONE = "none effect";
    public static readonly string BONUS_RANGE = "base range";
    public static readonly string BONUS_RANGE_AFTER_ELIMINATING = "range after eliminating enemy";
    public static readonly string BONUS_MAX_SPEED = "max speed";
    public static readonly string BONUS_MAX_SPEED_AFTER_ELIMINATING = "max speed after eliminating enemy";
    #endregion

    #region -- Name unit --
    public static readonly string PLAYER_NAME = "Player";
    public static readonly string BOT_NAME = "Bot";
    #endregion

    #endregion
}
