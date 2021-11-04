using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectS.UI.Title
{
    /// <summary>
    /// NewGame패널을 컨트롤할 클래스입니다.
    /// </summary>
    public class UINewGame : UITitlePanelBase
    {
        public CharacterChoice characterChoice;
        public IngameOption ingameOption;

        public Button decideButton;
        public Button backButton;
        public Button closeButton;

        // 현재 캐릭터 선택중인지 인게임 옵션 조정중인지 나타냅니다.
        private enum NewGamePhase { CharacterChoice, IngameOption }

        private NewGamePhase phase;
        /// <summary>
        /// 새 게임 패널을 초기화합니다.
        /// 버튼에 리스너들을 달고 캐릭터 선택창과 인게임 옵션 창을 초기화합니다.
        /// </summary>
        public void Initialize()
        {
            phase = NewGamePhase.CharacterChoice;
            characterChoice.Initialize();
            decideButton.onClick.AddListener(DecideButtonOnClicked);
            backButton.onClick.AddListener(BackButtonOnClicked);
            closeButton.onClick.AddListener(CloseButtonOnClicked);
            
        }

        /// <summary>
        /// 결정 버튼을 클릭했을시 호출됩니다.
        /// </summary>
        private void DecideButtonOnClicked()
        {
            switch (phase)
            {
                // 캐릭터 선택 창에서 결정을 누르면 게임 옵션창으로 갑니다.
                case NewGamePhase.CharacterChoice:
                    OnPhase(phase = NewGamePhase.IngameOption);
                    break;
                // 게임 옵션창에서 결정을 누르면 게임을 시작합니다.
                case NewGamePhase.IngameOption:
                    StartGame();
                    break;
            }
        }
        /// <summary>
        /// 뒤로가기 버튼을 클릭했을시 호출됩니다.
        /// </summary>
        private void BackButtonOnClicked()
        {
            switch (phase)
            {
                // 캐릭터 선택창에서 뒤로가기를 누르면 패널을 닫습니다.
                case NewGamePhase.CharacterChoice:
                    Close();
                    break;
                // 게임 옵션창에서 뒤로가기를 누르면 캐릭터선택창으로 갑니다.
                case NewGamePhase.IngameOption:
                    OnPhase(phase = NewGamePhase.CharacterChoice);
                    break;
            }
        }
        /// <summary>
        /// 닫기 버튼을 클릭했을시 호출됩니다.
        /// </summary>
        private void CloseButtonOnClicked()
        {
            Close();
        }
        /// <summary>
        /// 패널을 닫고 캐릭터 선택창과 게임 옵션 창을 초기화합니다.
        /// </summary>
        private void Close()
        {
            ClosePanel();
            characterChoice.ClearPanel();
            ingameOption.ClearPanel();
        }
        
        /// <summary>
        /// 새 게임의 현재 페이즈를 설정합니다.
        /// </summary>
        /// <param name="phase">설정할 페이즈</param>
        private void OnPhase(NewGamePhase phase)
        {
            switch (phase)
            {
                case NewGamePhase.CharacterChoice:
                    characterChoice.gameObject.SetActive(true);
                    ingameOption.gameObject.SetActive(false);
                    break;
                case NewGamePhase.IngameOption:
                    ingameOption.gameObject.SetActive(true);
                    characterChoice.gameObject.SetActive(false);
                    break;
            }
        }

        /// <summary>
        /// 캐릭터와 옵션을 저장하고 게임을 시작합니다.
        /// </summary>
        private void StartGame()
        {

        }
    }


}
