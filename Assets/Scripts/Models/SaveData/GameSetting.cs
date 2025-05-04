using System;

namespace Models
{
    public class GameSettings
    {
        public float MusicVolume { get; set; }
        public float SFXVolume { get; set; }
        public bool MutedMusic { get; set; }
        public bool MutedSFX { get; set; }
        public string Language { get; set; }

        public LanguageEnum GetLanguageEnum() => Enum.TryParse(Language, out LanguageEnum result) ? result : LanguageEnum.English;
    }
}