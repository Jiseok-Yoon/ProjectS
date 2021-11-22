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
        // 패널 홀더
        public Transform PanelHolder;
        // 로딩 바
        public LoadingBar loadingBar;
        // 로딩 바 애니메이션 처리에 사용될 코루틴
        private Coroutine loadGaugeUpdateCoroutine;

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
            PanelHolder.gameObject.SetActive(false);
            loadingBar.gameObject.SetActive(true);
            OnPhase(introPhase);
        }

        /// <summary>
        /// 현재 페이즈에 대한 로직 실행
        /// </summary>
        /// <param name="phase">진행할 페이즈</param>
        private void OnPhase(IntroPhase phase)
        {
            loadingBar.SetLoadStateDescription($"Loading {phase.ToString()}");
            // 로딩바의 fillAmount가 아직 실제 로딩 게이지 퍼센트로 값이 끝까지 보간이 안됐다면
            // 아직 코루틴이 실행중임..
            // 이미 실행중인 코루틴을 또 시작시키면 오류가 발생하므로
            // 코루틴이 존재한다면 멈춘 후에 새로 변경된 로딩 게이지 퍼센트를 넘겨 코루틴을 다시 시작하게 한다.
            if (loadGaugeUpdateCoroutine != null)
            {
                StopCoroutine(loadGaugeUpdateCoroutine);
                loadGaugeUpdateCoroutine = null;
            }

            // 변경된 페이즈가 전체 페이즈 완료가 아니라면
            if (phase != IntroPhase.Complete)
            {
                // 현재 로드 퍼센테이지를 구한다.
                var loadPer = (float)phase / (float)IntroPhase.Complete;
                // 구한 퍼센테이지를 로딩바에 적용
                loadGaugeUpdateCoroutine = StartCoroutine(loadingBar.LoadGaugeUpdate(loadPer));
            }
            else
            {
                loadingBar.loadFillGauge.fillAmount = 1f;
                StartCoroutine(WaitForSeconds());
                IEnumerator WaitForSeconds()
                {
                    yield return new WaitForSeconds(.5f);
                    PanelHolder.gameObject.SetActive(true);
                    loadingBar.gameObject.SetActive(false);
                }

            }

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
                    allLoaded = true;
                    LoadComplete = true;
                    
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
