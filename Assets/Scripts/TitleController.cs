using ProjectS.Define;
using ProjectS.Resouce;
using ProjectS.UI.Title;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProjectS.Define.Title;

namespace ProjectS
{
    public class TitleController : MonoBehaviour
    {
        // 페이즈 로드 완료 여부
        private bool loadComplete;
        // 모든 페이즈 로드 완료 여부
        private bool allLoaded;
        // 현재 페이즈 상태
        private IntroPhase introPhase;
        // 타이틀 버튼 패널
        public UITitleButton titleButtonPanel;

        /// <summary>
        /// loadComplete 프로퍼티
        /// 현재 페이즈 로드 완료시 모든 페이즈 로드 완료가 아니라면 다음 페이즈로 변경
        /// </summary>
        public bool LoadComplete
        {
            get => loadComplete;
            set
            {
                loadComplete = value;
                if (loadComplete && !allLoaded)
                {
                    NextPhase();
                }
            }
        }

        public void Initialize()
        {
            OnPhase(introPhase);
        }

        /// <summary>
        /// 현재 페이즈에 대한 로직 실행
        /// </summary>
        /// <param name="phase">진행할 페이즈</param>
        private void OnPhase(IntroPhase phase)
        {
            // 페이즈별 로직 실행
            switch (phase)
            {
                // 로딩 페이즈를 시작합니다
                case IntroPhase.Start:
                    LoadComplete = true;
                    break;
                // 어플리케이션 세팅을 설정합니다.
                case IntroPhase.ApplicationSetting:
                    var gameManager = GameManager.Instance;
                    gameManager.OnApplicationSetting();
                    LoadComplete = true;
                    break;
                // 서버 연결을 시도합니다.
                case IntroPhase.Server:

                    LoadComplete = true;
                    break;
                // 기획 데이터를 불러옵니다.
                case IntroPhase.StaticData:
                    // 기획데이터를 로드합니다.
                    GameManager.SD.Initialize();
                    LoadComplete = true;
                    break;
                
                // 유저 데이터를 불러옵니다.
                case IntroPhase.UserData:

                    LoadComplete = true;
                    break;
                // 리소스 파일을 불러옵니다.
                case IntroPhase.Resource:
                    // 아틀라스와 프리팹을 로드합니다.
                    ResourceManager.Instance.Initialize();
                    LoadComplete = true;
                    break;
                // UI관리를 시작합니다.
                case IntroPhase.UI:
                    titleButtonPanel.Initialize();
                    LoadComplete = true;
                    break;
                // 로딩 완료
                case IntroPhase.Complete:

                    LoadComplete = true;
                    allLoaded = true;
                    break;
            }
        }

        /// <summary>
        /// 페이즈를 다음 페이즈로 변경
        /// </summary>
        private void NextPhase()
        {
            StartCoroutine(WaitForSeconds());
            IEnumerator WaitForSeconds()
            {
                yield return new WaitForSeconds(.1f);

                LoadComplete = false;
                OnPhase(++introPhase);
            }
        }
    }
}
