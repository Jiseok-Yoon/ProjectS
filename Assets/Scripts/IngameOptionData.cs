using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectS
{
    /// <summary>
    /// 인게임 옵션 설정을 저장하는 클래스입니다.
    /// </summary>
    public class IngameOptionData
    {
        // 옵션 이름과 옵션 값을 들고 있을 딕셔너리
        public Dictionary<string, string> options = new Dictionary<string, string>();

        /// <summary>
        /// 딕셔너리에 기본 옵션 이름과 기본 옵션 값을 넣어줍니다.
        /// </summary>
        public IngameOptionData()
        {
            var sdIngameOption = GameManager.SD.sdIngameOption;
            for (int i = 0; i < sdIngameOption.Count; ++i)
            {
                options.Add(sdIngameOption[i].optionName, sdIngameOption[i].optionValue[sdIngameOption[i].defaultOptionIndex]);
            }

        }

        // 옵션값을 설정합니다.
        public void SetOptionValue(string OptionName, string Value)
        {
            if (options[OptionName] == null)
                return;

            options[OptionName] = Value;
        }
        // 옵션 이름을 받아 해당하는 옵션의 값을 돌려줍니다.
        public string GetOptionValue(string optionName)
        {
            if (options == null)
                return null;

            return options[optionName];
        }
        // 옵션 이름을 받아 해당하는 옵션의 값의 인덱스를 돌려줍니다.
        public int GetOptionValueIndex(string optionName)
        {
            if (options == null)
                return -1;
            var optionValueList = GameManager.SD.sdIngameOption.Find(_ => _.optionName == optionName).optionValue.ToList();
            return optionValueList.FindIndex(_ => _ == options[optionName]);
        }
    }
}
