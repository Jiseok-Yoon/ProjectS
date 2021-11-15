using ProjectS.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static ProjectS.Define.Character;
using static ProjectS.Define.Resource;

namespace ProjectS.UI.Title
{
    public class CharacterChoice : MonoBehaviour
    {

        // 캐릭터 이름 표시
        public TextMeshProUGUI characterName;
        // 캐릭터 이미지 표시
        public Image characterImage;
        // 캐릭터 설명 표시
        public TextMeshProUGUI descriptionText;
        // 이전, 다음 버튼
        public Button previousBtn;
        public Button nextBtn;

        // 캐릭터 이미지 아틀라스
        private SpriteAtlas characterAtlas;
        // 캐릭터 순서
        private int currentIndex;

        /// <summary>
        /// 캐릭터 선택란을 초기화합니다.
        /// 버튼에 리스너를 달고 첫 번째 캐릭터를 설정합니다.
        /// </summary>
        public void Initialize()
        {
            // 활성화상태로 시작합니다.
            gameObject.SetActive(true);
            characterAtlas = SpriteLoader.GetAtlas(AtlasType.CharacterImageAtlas);
            // 버튼에 리스너를 답니다.
            previousBtn.onClick.AddListener(PreviousCharacter);
            nextBtn.onClick.AddListener(NextCharacter);

            // 첫번째 캐릭터로 설정합니다.
            SetCharacter(currentIndex = 0);
            
        }
        /// <summary>
        /// 이전 캐릭터를 설정합니다.
        /// </summary>
        private void PreviousCharacter()
        {
            // 캐릭터 아틀라스가 없다면 리턴합니다.
            if (characterAtlas == null)
            {
                Debug.LogError("캐릭터 정보를 불러올 수 없습니다.");
                return;
            }

            // 첫 번째 캐릭터라면 마지막 캐릭터를 설정합니다.
            if (currentIndex <= 0)
            {
                currentIndex = characterAtlas.spriteCount - 1;
                SetCharacter(currentIndex);
                return;
            }
            // 아니라면 이전 캐릭터를 설정합니다.
            else
            {
                SetCharacter(--currentIndex);
            }
        }

        /// <summary>
        /// 다음 캐릭터를 설정합니다.
        /// </summary>
        private void NextCharacter()
        {
            // 캐릭터 아틀라스가 없다면 리턴합니다.
            if (characterAtlas == null)
            {
                Debug.LogError("캐릭터 정보를 불러올 수 없습니다.");
                return;
            }

            // 마지막 캐릭터라면 첫 번째 캐릭터를 설정합니다.
            if (currentIndex >= characterAtlas.spriteCount - 1)
            {
                SetCharacter(currentIndex = 0);
            }
            // 아니라면 다음 캐릭터를 설정합니다.
            else
            {
                SetCharacter(++currentIndex);
            }
        }

        /// <summary>
        /// 캐릭터 이미지, 이름, 설명을 설정합니다.
        /// </summary>
        /// <param name="index">설정할 캐릭터 순서를 입력합니다.</param>
        private void SetCharacter(int index)
        {
            // 캐릭터 이미지를 변경합니다.
            characterImage.sprite = SpriteLoader.GetSprite(AtlasType.CharacterImageAtlas, ((CharacterName)index).ToString());
            // 캐릭터 이름을 변경합니다.
            characterName.text = ((CharacterName)index).ToString();
            // 캐릭터 설명을 변경합니다.
            var sdString = GameManager.SD.sdString;
            descriptionText.text = sdString.Find(_ => _.index == CHARACTER_DESCRIPTION_SECTOR + index).kr;
        }
        /// <summary>
        /// 패널의 내용을 초기 상태로 되돌립니다.
        /// </summary>
        public void ResetPanel()
        {
            // 첫번째 캐릭터로 설정합니다.
            SetCharacter(currentIndex = 0);
        }

        /// <summary>
        /// 선택된 캐릭터를 게임 매니저의 플레이어 데이터에 저장합니다.
        /// </summary>
        public void SaveCharacter()
        {

        }
    }
}
