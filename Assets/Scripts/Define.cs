using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʈ���� ���Ǵ� ����� ������ ���� ����
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

}

