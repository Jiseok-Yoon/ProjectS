using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectS.UI.Title
{
    public class UITitleButton : MonoBehaviour
    {
        public GraphicRaycaster gr;
        private enum ButtonType{ newGame, loadGame, help, achievements };

        private UINewGame newGamePanel;
        private UILoadGame loadGamePanel;
        private UIHelp helpPanel;
        private UIAchievements achievementsPanel;

        private List<Button> buttons;

        public void Initialize()
        {
            for (int i = 0; i <buttons.Count; ++i)
            {
                buttons[i].onClick.AddListener(() => OpenPanel(i));
            }
        }

        public void OpenPanel(int index)
        {
            ButtonType Type;
            
        }
    }
}
