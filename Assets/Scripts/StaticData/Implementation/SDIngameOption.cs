using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectS.Define.IngameOption;

namespace ProjectS.SD
{
    [Serializable]
    public class SDIngameOption : StaticData
    {
        public OptionCategory optionCategory;
        public string optionName;
        public string[] optionValue;
        public int defaultOptionIndex;
    }
}
