using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 프로젝트에서 사용되는 상수나 열거형 등을 정의
/// </summary>
namespace ProjectS.Define
{
    public enum IntroPhase { Start, ApplicationSetting, Server, StaticData, UserData, Resource, UI, Complete }

    public enum TitleButtonType { newGame, loadGame, help, achievements };
}

