using ProjectS.SD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectS.UI.Title
{
    public class IngameOptionSlot : MonoBehaviour
    {
        private string optionName;
        public string OptionName { get =>optionName; set => optionName = value; }
        private TextMeshProUGUI optionText;
        public Button nextButton;
        public Button previousButton;
        private SDIngameOption sdIngameOption;
        public SDIngameOption SdIngameOption { get => sdIngameOption; set =>sdIngameOption = value; }
        private int currentOptionIndex;
        public int CurrentOptionIndex { get => currentOptionIndex; set => currentOptionIndex = value; }

        /// <summary>
        /// 옵션 슬롯을 초기화합니다.
        /// </summary>
        public void Initialize()
        {
            // 옵션의 이름을 가져옵니다.
            optionName = transform.parent.name;
            optionText = GetComponentInChildren<TextMeshProUGUI>();
            // 옵션 이름에 해당하는 SDIngameOption을 불러옵니다.
            sdIngameOption = GameManager.SD.sdIngameOption.Find(_ => _.optionName == optionName);
            // 기본 옵션으로 세팅합니다.
            SetOptionValue(sdIngameOption.defaultOptionIndex);
        }

        /// <summary>
        /// 옵션을 인덱스에 따라 설정합니다.
        /// </summary>
        /// <param name="index">옵션의 인덱스</param>
        public void SetOptionValue(int index)
        {
            currentOptionIndex = index;
            // 인덱스가 옵션 길이보다 크다면 처음 옵션으로 설정합니다.
            if (index >= sdIngameOption.optionValue.Length)
            {
                SetOptionValue(0);
                return;
            }
            // 인덱스가 0보다 작다면 마지막 옵션으로 설정합니다.
            if (index < 0)
            {
                SetOptionValue(sdIngameOption.optionValue.Length - 1);
                return;
            }

            // 옵션 텍스트에 값을 설정합니다.
            optionText.text = sdIngameOption.optionValue[currentOptionIndex];
        }
        public string GetOptionValue()
        {
            return optionText.text;
        }
    }
}
