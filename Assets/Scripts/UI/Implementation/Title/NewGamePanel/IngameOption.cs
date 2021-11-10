using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectS.UI.Title
{
    public class IngameOption : MonoBehaviour
    {
        public Button difficultyTab;
        public Button detailTab;
        public List<IngameOptionSlot> ingameOptionSlots = new List<IngameOptionSlot>();
        private enum 

        /// <summary>
        /// 인게임 옵션 패널을 초기화합니다.
        /// </summary>
        public void Initialize()
        {
            //difficultyTab.onClick.AddListener();
            // 자식 객체의 모든 옵션 슬롯을 초기화하고 가져옵니다.
            foreach(IngameOptionSlot optionSlot in GetComponentsInChildren<IngameOptionSlot>())
            {
                optionSlot.Initialize();
                ingameOptionSlots.Add(optionSlot);
            }

        }

        private void OptionTabClicked()
        {

        }
        /// <summary>
        /// 패널의 내용을 초기 상태로 되돌립니다.
        /// </summary>
        public void ResetPanel()
        {
            foreach(IngameOptionSlot optionSlot in ingameOptionSlots)
            {
                optionSlot.SetOptionValue(optionSlot.sdIngameOption.defaultOptionIndex);
            }
        }
    }
}
