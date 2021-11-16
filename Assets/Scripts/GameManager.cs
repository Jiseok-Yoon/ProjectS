using ProjectS;
using ProjectS.DB;
using ProjectS.Define;
using ProjectS.SD;
using ProjectS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectS
{
    /// <summary>
    /// 게임 전반의 데이터와 흐름을 매니징 하는 클래스
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        public float loadProgress;

        /// <summary>
        /// 게임 매니저가 초기화 되고 실행됩니다.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            // 타이틀 컨트롤러를 찾아 초기화하고 로딩을 시작합니다.
            var titleController = FindObjectOfType<TitleController>();
            titleController.Initialize();

            
            // 게임 매니저가 파괴되지 않게 합니다.
            DontDestroyOnLoad(this);
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

        /// <summary>
        /// 기획 데이터를 들고 있을 필드
        /// </summary>
        // private 필드 인스펙터 노출을 위한 직렬화.
        [SerializeField]
        private StaticDataModule sd = new StaticDataModule();
        public static StaticDataModule SD => Instance.sd;

        /// <summary>
        /// 캐릭터 정보
        /// </summary>
        [SerializeField]
        private BoCharacter boCharacter;
        public static BoCharacter BoCharacter
        {
            get => Instance.boCharacter;
            set => Instance.boCharacter = value;
        }


        /// <summary>
        /// 인게임 옵션의 정보를 들고있을 필드
        /// </summary>
        [SerializeField]
        private IngameOptionData ingameOption;
        public static IngameOptionData IngameOption
        {
            get => Instance.ingameOption;
            set => Instance.ingameOption = value;
        }



        /// <summary>
        /// 씬을 비동기로 로드하는 기능
        /// 다른 씬 간의 전환에 사용 (ex: Title -> InGame)
        /// </summary>
        /// <param name="sceneName">로드할 씬의 이름을 갖는 열거형</param>
        /// <param name="loadCoroutien">씬 전환 시 로딩 씬에서 미리 처리할 작업</param>
        /// <param name="loadComplete">씬 전환 완료 후 실행할 기능</param>
        public void LoadScene(SceneType sceneName, IEnumerator loadCoroutine = null, Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());

            // 씬을 전환할 때 ex) Title -> Ingame 전환을 할 때 한 번에 전환하는 것이 아니라
            // 중간에 로딩 씬을 이용, 최종적으로 Title -> Loading -> Ingame

            // 코루틴 -> 유니티에서 특정 작업을 비동기로 실행할 수 있게 해주는 기능
            //           (비동기처럼 실행이 되는데.. 실제로 비동기는 아님)

            // LoadScene 메서드에서만 사용가능한 로컬함수 선언
            IEnumerator WaitForLoad()
            {
                // 로딩 진행상태를 나타냄 (0~1)
                loadProgress = 0;

                // 비동기로 로딩 씬으로 전환 (비동기로 사용하는 이유는 씬 전환시 화면이 멈추지 않게 하기 위해서)
                yield return SceneManager.LoadSceneAsync(SceneType.Loading.ToString());

                // 로딩 씬으로 전환 완료 후에 아래 로직이 들어옴

                // 내가 변경하고자하는 씬을 추가
                var asyncOper = SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive);
                // 방금 추가한 씬을 비활성화
                // 이유? 2개의 씬이 활성화되어있다면 따로 컨트롤하지 않는 이상 어떤 씬이 화면에 보일지 모름
                // 실제 변경하고자 하는 씬을 추가해둔 다음 사용자에게는 로딩 씬을 보여주고
                // 실제 변경하고자 하는 씬에 필요한 리소스등을 부르는 작업을 하기 위해서
                asyncOper.allowSceneActivation = false;

                // 변경하고자 하는 씬에 필요한 작업이 존재한다면 실행
                if (loadCoroutine != null)
                {
                    // 해당 작업이 완료될 때 까지 대기
                    yield return StartCoroutine(loadCoroutine);
                }

                // 위에 작업이 완료된 후에 아래 로직이 실행됌


                // 비동기로 로드한 씬이 활성화가 완료되지 않았다면 특정 작업을 반복
                while (!asyncOper.isDone)
                {
                    // loadProgress 값을 이용해서 사용자한테 로딩바를 통해 진행 상태를 알려줌

                    if (loadProgress >= .9f)
                    {
                        loadProgress = 1f;

                        // 로딩바가 마지막까지 차는 것을 확인하기 위해 1초 정도 대기
                        yield return new WaitForSeconds(1f);

                        // 변경하고자 하는 씬을 다시 활성화
                        // (isDone은 씬이 활성상태가 아니라면 progress가 1이 되어도 true가 안됌)
                        asyncOper.allowSceneActivation = true;
                    }
                    else
                        loadProgress = asyncOper.progress;

                    // 코루틴 내에서 반복문 사용 시 로직을 한 번 실행 후, 메인 로직을 실행 할 수 있게 yield return
                    yield return null;
                }

                // 위의 반복 작업이 다 끝난 후 아래 로직 실행

                // 로딩 씬에서 다음 씬에 필요한 작업을 전부 수행했으므로 로딩씬을 비활성화 시킴
                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());

                // 모든 작업이 완료되었으므로 모든 작업 완료 후 실행시킬 로직이 있다면 실행
                loadComplete?.Invoke();
            }
        }

    }
}
