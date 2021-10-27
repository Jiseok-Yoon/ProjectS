using ProjectS;
using ProjectS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectS
{
    /// <summary>
    /// 게임 전반의 데이터와 흐름을 매니징 하는 클래스
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        /// <summary>
        /// 게임 매니저가 초기화 되고 실행됩니다.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            // 타이틀 컨트롤러를 찾아 초기화하고 로딩을 시작합니다.
            var titleController = FindObjectOfType<TitleController>();
            titleController.Initialize();
        }

        /// <summary>
        /// 앱을 기본적으로 설정합니다.
        /// </summary>
        public void OnApplicationSetting()
        {
            // 수직동기화 끄기
            QualitySettings.vSyncCount = 0;
            // 렌더 프레임을 60으로 설정
            Application.targetFrameRate = 60;
            // 앱 실행 중 장시간 대기 시에도 화면이 꺼지지 않게
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}
