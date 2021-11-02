using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectS.UI.Title
{
    /// <summary>
    /// NewGame패널을 컨트롤할 클래스입니다.
    /// </summary>
    public class UINewGame : UITitlePanelBase
    {
        public CharacterChoice CharacterChoice;
        public Transform IngameOption;

        public Button DecideButton;
        public Button BackButton;
        public Button CloseButton;


    }
}
