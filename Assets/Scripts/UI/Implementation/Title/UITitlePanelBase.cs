using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectS.UI.Title
{
    /// <summary>
    /// 버튼 패널을 제외하고 타이틀에 존재하는 패널들의 베이스가 되는 클래스입니다.
    /// 캔버스그룹 컴포넌트를 요구합니다.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UITitlePanelBase : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        public CanvasGroup CanvasGroup 
        {
            get
            {
                if (canvasGroup == null)
                    canvasGroup = GetComponent<CanvasGroup>();
                return canvasGroup;
            } 
        }

        protected virtual void Start()
        {
            // 비활성화 상태로 시작합니다.
            ClosePanel();
        }
        /// <summary>
        /// 패널을 엽니다.
        /// </summary>
        public void OpenPanel()
        {
            SetCanvasGroup(true);

        }
        /// <summary>
        /// 패널을 닫습니다.
        /// </summary>
        public void ClosePanel()
        {
            SetCanvasGroup(false);

        }
        /// <summary>
        /// 패널 활성화 상태에 따라 캔버스 그룹을 설정합니다.
        /// </summary>
        /// <param name="isActive">활성화 여부</param>
        private void SetCanvasGroup(bool isActive)
        {
            CanvasGroup.alpha = Convert.ToInt32(isActive);
            CanvasGroup.interactable = isActive;
            CanvasGroup.blocksRaycasts = isActive;
        }
    }
}
