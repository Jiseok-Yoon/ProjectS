using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 프로젝트에서 사용되는 상수나 열거형 등을 정의
/// </summary>
namespace ProjectS.Define
{
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
        public enum CharacterName { character1, character2 }
    }

    public class StaticData
    {
        public const string SDPath = "Assets/StaticData";
        public const string SDExcelPath = "Assets/StaticData/Excel";
        public const string SDJsonPath = "Assets/StaticData/Json";
    }
}

