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
    public class CharacterChoice
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
        private SpriteAtlas characterAtlas = SpriteLoader.GetAtlas(AtlasType.CharacterImageAtlas);
        // 캐릭터 순서
        private int index = 0;

        /// <summary>
        /// 캐릭터 선택란을 초기화합니다.
        /// 버튼에 리스너를 달고 첫 번째 캐릭터를 설정합니다.
        /// </summary>
        public void Initialize()
        {
            // 버튼에 리스너를 답니다.
            previousBtn.onClick.AddListener(PreviousCharacter);
            nextBtn.onClick.AddListener(NextCharacter);

            // 첫번째 캐릭터를 설정합니다.
            SetCharacter(index);
            
        }
        /// <summary>
        /// 이전 캐릭터를 설정합니다.
        /// </summary>
        private void PreviousCharacter()
        {
            // 첫 번째 캐릭터라면 마지막 캐릭터를 설정합니다.
            if (--index < 0)
            {
                index = characterAtlas.spriteCount - 1;
                SetCharacter(index);
                return;
            }
            // 아니라면 이전 캐릭터를 설정합니다.
            else
            {
                SetCharacter(index);
            }
        }

        /// <summary>
        /// 다음 캐릭터를 설정합니다.
        /// </summary>
        private void NextCharacter()
        {
            // 마지막 캐릭터라면 첫 번째 캐릭터를 설정합니다.
            if (++index >= characterAtlas.spriteCount)
            {
                SetCharacter(index = 0);
            }
            // 아니라면 다음 캐릭터를 설정합니다.
            else
            {
                SetCharacter(++index);
            }
        }

        /// <summary>
        /// 캐릭터 이미지, 이름, 설명을 설정합니다.
        /// </summary>
        /// <param name="index">설정할 캐릭터 순서를 입력합니다.</param>
        private void SetCharacter(int index)
        {
            characterImage.sprite = SpriteLoader.GetSprite(AtlasType.CharacterImageAtlas, ((CharacterName)index).ToString());

            characterName.text = ((CharacterName)index).ToString();
            descriptionText.text = 
        }
    }
}
