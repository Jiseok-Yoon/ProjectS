using ProjectS.Define;
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
        public UINewGame newGamePanel;
        public UILoadGame loadGamePanel;
        public UIHelp helpPanel;
        public UIAchievements achievementsPanel;
        // 타이틀 버튼과 버튼의 타입을 가질 딕셔너리입니다.
        public Dictionary<Button, TitleButtonType> buttons = new Dictionary<Button, TitleButtonType>();

        /// <summary>
        /// 각 버튼에 클릭 리스너를 달아주며 초기화합니다.
        /// </summary>
        public void Initialize()
        {
            var buttonArray = gameObject.GetComponentsInChildren<Button>();

            for (int i = 0; i < buttonArray.Length; ++i)
            {
                buttons.Add(buttonArray[i], (TitleButtonType)i);
            }

            foreach(Button btn in buttons.Keys)
            {
                btn.onClick.AddListener(() => OpenPanel(buttons[btn]));
            }
        }

        /// <summary>
        /// 클릭한 버튼에 따라 해당 패널을 엽니다.
        /// </summary>
        /// <param name="type">클릭한 버튼의 TitleButtonType</param>
        public void OpenPanel(TitleButtonType type)
        {
            switch (type)
            {
                case TitleButtonType.newGame:
                    newGamePanel.OpenPanel();
                    break;
                case TitleButtonType.loadGame:
                    loadGamePanel.OpenPanel();
                    break;
                case TitleButtonType.help:
                    helpPanel.OpenPanel();
                    break;
                case TitleButtonType.achievements:
                    achievementsPanel.OpenPanel();
                    break;
            }

        }
    }
}
