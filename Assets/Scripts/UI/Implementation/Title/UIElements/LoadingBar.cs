using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectS.UI.Title
{
    public class LoadingBar : MonoBehaviour
    {
        // 현재 로딩 상태 설명
        public TextMeshProUGUI loadStateDesc;
        // 로딩 상태 게이지
        public Image loadFillGauge;
        /// <summary>
        /// 로딩 상태 설명을 설정합니다.
        /// </summary>
        /// <param name="loadState">현재 로딩 상태 설명</param>
        public void SetLoadStateDescription(string loadState)
        {
            loadStateDesc.text = loadState;
        }

        /// <summary>
        /// 로딩 바 애니메이션 처리
        /// </summary>
        /// <param name="loadPer">로딩 정도</param>
        /// <returns></returns>
        public IEnumerator LoadGaugeUpdate(float loadPer)
        {
            // ui의 fillAmount 값이랑 현재 로딩 퍼센테이지 값이랑 같지 않다면 반복
            while (!Mathf.Approximately(loadFillGauge.fillAmount, loadPer))
            {
                loadFillGauge.fillAmount = Mathf.Lerp(loadFillGauge.fillAmount, loadPer, Time.deltaTime * 2f);
                yield return null;
            }
        }
    }
}
