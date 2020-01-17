using UnityEngine;

namespace TimB
{
    public static class ConstsLibrary
    {

        public static readonly Color32 proColor = new Color32(255, 255, 255, 255);//Base color
        public static readonly Color32 redFlashColor = new Color32(227, 15, 15, 255);//Red Flash Color

        public const float halfOfSecondTimer = 0.5f;
        public const float oneSecondTimer = 1f;
        public const float twoSecondTimer = 2f;

        #region Save Load Pefs Names
        //System Values
        public const string soundEffectVolumePrefs = "soundEffectVolume";
        public const string musicVolumePrefs = "musicVolume";
        public const string mutedPrefs = "muted";
        #endregion
    }
}

