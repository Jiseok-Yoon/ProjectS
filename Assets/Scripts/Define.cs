using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 프로젝트에서 사용되는 상수나 열거형 등을 정의
/// </summary>
namespace ProjectS.Define
{
    /// <summary>
    /// 게임에 사용되는 씬 종류
    /// </summary>
    public enum SceneType { Title, Ingame, Loading }

    public class Title
    {
        public enum IntroPhase { Start, ApplicationSetting, Server, StaticData, UserData, Resource, UI, Complete }

        public enum TitleButtonType { newGame, loadGame, help, achievements };

    }

    public class Resource
    {
        public enum AtlasType { CharacterImageAtlas }
    }

    public class Character
    {
        public const int CHARACTER_DESCRIPTION_SECTOR = 1001;
    }

    public class StaticData
    {
        public const string SDPath = "Assets/StaticData";
        public const string SDExcelPath = "Assets/StaticData/Excel";
        public const string SDJsonPath = "Assets/StaticData/Json";
    }

    public class IngameOption
    {
        public enum OptionCategory { Difficulty, Detail }

    }
}

