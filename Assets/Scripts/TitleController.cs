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
        // ������ �ε� �Ϸ� ����
        private bool loadComplete;
        // ��� ������ �ε� �Ϸ� ����
        private bool allLoaded;
        // ���� ������ ����
        private IntroPhase introPhase;
        // Ÿ��Ʋ ��ư �г�
        public UITitleButton titleButtonPanel;

        /// <summary>
        /// loadComplete ������Ƽ
        /// ���� ������ �ε� �Ϸ�� ��� ������ �ε� �Ϸᰡ �ƴ϶�� ���� ������� ����
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
        /// ���� ����� ���� ���� ����
        /// </summary>
        /// <param name="phase">������ ������</param>
        private void OnPhase(IntroPhase phase)
        {
            // ����� ���� ����
            switch (phase)
            {
                // �ε� ����� �����մϴ�
                case IntroPhase.Start:
                    LoadComplete = true;
                    break;
                // ���ø����̼� ������ �����մϴ�.
                case IntroPhase.ApplicationSetting:
                    var gameManager = GameManager.Instance;
                    gameManager.OnApplicationSetting();
                    LoadComplete = true;
                    break;
                // ���� ������ �õ��մϴ�.
                case IntroPhase.Server:

                    LoadComplete = true;
                    break;
                // ��ȹ �����͸� �ҷ��ɴϴ�.
                case IntroPhase.StaticData:
                    // ��ȹ�����͸� �ε��մϴ�.
                    GameManager.SD.Initialize();
                    LoadComplete = true;
                    break;
                
                // ���� �����͸� �ҷ��ɴϴ�.
                case IntroPhase.UserData:

                    LoadComplete = true;
                    break;
                // ���ҽ� ������ �ҷ��ɴϴ�.
                case IntroPhase.Resource:
                    // ��Ʋ�󽺿� �������� �ε��մϴ�.
                    ResourceManager.Instance.Initialize();
                    LoadComplete = true;
                    break;
                // UI������ �����մϴ�.
                case IntroPhase.UI:
                    titleButtonPanel.Initialize();
                    LoadComplete = true;
                    break;
                // �ε� �Ϸ�
                case IntroPhase.Complete:

                    LoadComplete = true;
                    allLoaded = true;
                    break;
            }
        }

        /// <summary>
        /// ����� ���� ������� ����
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
