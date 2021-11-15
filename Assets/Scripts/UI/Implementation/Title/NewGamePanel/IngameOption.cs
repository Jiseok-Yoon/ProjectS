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
        public Transform difficultyOptionHolder;
        public Transform detailOptionHolder;
        public List<IngameOptionSlot> ingameOptionSlots = new List<IngameOptionSlot>();
        private enum OptionTabType{ Difficulty, Detail}

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

            difficultyTab.onClick.AddListener(() => ChangeOptionTab(OptionTabType.Difficulty));
            detailTab.onClick.AddListener(() => ChangeOptionTab(OptionTabType.Detail));

            ChangeOptionTab(OptionTabType.Difficulty);
        }
        /// <summary>
        /// 클릭된 탭으로 전환합니다.
        /// </summary>
        /// <param name="type">클릭된 탭 정보</param>
        private void ChangeOptionTab(OptionTabType type)
        {
            switch (type)
            {
                case OptionTabType.Difficulty:
                    difficultyOptionHolder.gameObject.SetActive(true);
                    detailOptionHolder.gameObject.SetActive(false);
                    break;
                case OptionTabType.Detail:
                    difficultyOptionHolder.gameObject.SetActive(false);
                    detailOptionHolder.gameObject.SetActive(true);
                    break;
            }
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
        /// <summary>
        /// 현재 옵션들을 게임 매니저의 옵션 데이터에 저장합니다.
        /// </summary>
        public void OptionSave()
        {
            // 인게임 옵션 데이터를 생성합니다.
            var ingameOptionData = new IngameOptionData();

            foreach(IngameOptionSlot slot in ingameOptionSlots)
            {
                ingameOptionData.SetOptionValue(slot.optionName, slot.GetOptionValue());
            }

            // 게임 매니저에 등록합니다.
            GameManager.IngameOption = ingameOptionData;
            
        }
    }
}
