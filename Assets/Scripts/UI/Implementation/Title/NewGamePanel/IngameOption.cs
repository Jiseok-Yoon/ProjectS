using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ProjectS.Define.IngameOption;

namespace ProjectS.UI.Title
{
    public class IngameOption : MonoBehaviour
    {
        public Button difficultyTabButton;
        public Button detailTabButton;
        public Transform difficultyOptionHolder;
        public Transform detailOptionHolder;
        private List<IngameOptionSlot> ingameOptionSlots = new List<IngameOptionSlot>();
        private enum OptionTabType{ Difficulty, Detail}
        private IngameOptionSlot difficultyOptionSlot;
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

                // 전체 난이도 옵션이라면 참조를 가져오고 리스너를 달아줍니다.
                if (optionSlot.OptionName == OptionTabType.Difficulty.ToString())
                {
                    difficultyOptionSlot = optionSlot;
                    difficultyOptionSlot.nextButton.onClick.AddListener(NextDifficultyOptionValue);
                    difficultyOptionSlot.previousButton.onClick.AddListener(PreviousDifficultyOptionValue);
                    continue;
                }

                // 버튼에 리스너를 답니다.
                optionSlot.nextButton.onClick.AddListener(()=> { NextOptionValue(optionSlot); });
                optionSlot.previousButton.onClick.AddListener(() => { PreviousOptionValue(optionSlot); });
            }

            difficultyTabButton.onClick.AddListener(() => ChangeOptionTab(OptionTabType.Difficulty));
            detailTabButton.onClick.AddListener(() => ChangeOptionTab(OptionTabType.Detail));

            ChangeOptionTab(OptionTabType.Difficulty);
        }
        /// <summary>
        /// 전체 난이도의 다음 버튼이 눌렸을 때 실행됩니다.
        /// </summary>
        private void NextDifficultyOptionValue()
        {
            // 변경할 옵션의 인덱스를 설정합니다.
            int Index = difficultyOptionSlot.CurrentOptionIndex + 1;

            // 사용자 정의 설정 옵션값으로 설정되지 않도록 합니다.
            if (Index >= difficultyOptionSlot.SdIngameOption.optionValue.Length - 1)
                Index = 0;

            // 난이도 슬롯들에 적용해줍니다.
            var difficultySlots = ingameOptionSlots.FindAll(_ => _.SdIngameOption.optionCategory == OptionCategory.Difficulty);
            foreach(IngameOptionSlot slot in difficultySlots)
            {
                slot.SetOptionValue(Index);
            }
        }
        /// <summary>
        /// 전체 난이도의 이전 버튼이 눌렸을 때 실행됩니다.
        /// </summary>
        private void PreviousDifficultyOptionValue()
        {
            // 변경할 옵션의 인덱스를 설정합니다.
            int Index = difficultyOptionSlot.CurrentOptionIndex -1;
            // 사용자 정의 설정 옵션값으로 설정되지 않도록 합니다.
            if (Index < 0)
                Index = difficultyOptionSlot.SdIngameOption.optionValue.Length - 2;

            // 난이도 슬롯들에 적용해줍니다.
            var difficultySlots = ingameOptionSlots.FindAll(_ => _.SdIngameOption.optionCategory == OptionCategory.Difficulty);
            foreach (IngameOptionSlot slot in difficultySlots)
            {
                slot.SetOptionValue(Index);
            }
        }
        private void NextOptionValue(IngameOptionSlot slot)
        {
            // 난이도 탭의 옵션을 조정하는 경우 전체 난이도를 사용자 설정으로 바꿔줍니다.
            if (slot.SdIngameOption.optionCategory == OptionCategory.Difficulty)
            {
                difficultyOptionSlot.SetOptionValue(difficultyOptionSlot.SdIngameOption.optionValue.Length - 1);
            }
            slot.SetOptionValue(++(slot.CurrentOptionIndex));
        }
        private void PreviousOptionValue(IngameOptionSlot slot)
        {
            // 난이도 탭의 옵션을 조정하는 경우 전체 난이도를 사용자 설정으로 바꿔줍니다.
            if (slot.SdIngameOption.optionCategory == OptionCategory.Difficulty)
            {
                difficultyOptionSlot.SetOptionValue(difficultyOptionSlot.SdIngameOption.optionValue.Length - 1);
            }
            slot.SetOptionValue(--(slot.CurrentOptionIndex));
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
                optionSlot.SetOptionValue(optionSlot.SdIngameOption.defaultOptionIndex);
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
                ingameOptionData.SetOptionValue(slot.OptionName, slot.GetOptionValue());
            }

            // 게임 매니저에 등록합니다.
            GameManager.IngameOption = ingameOptionData;
            
        }
    }
}
