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
        // �г� Ȧ��
        public Transform PanelHolder;
        // �ε� ��
        public LoadingBar loadingBar;
        // �ε� �� �ִϸ��̼� ó���� ���� �ڷ�ƾ
        private Coroutine loadGaugeUpdateCoroutine;

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
            PanelHolder.gameObject.SetActive(false);
            loadingBar.gameObject.SetActive(true);
            OnPhase(introPhase);
        }

        /// <summary>
        /// ���� ����� ���� ���� ����
        /// </summary>
        /// <param name="phase">������ ������</param>
        private void OnPhase(IntroPhase phase)
        {
            loadingBar.SetLoadStateDescription($"Loading {phase.ToString()}");
            // �ε����� fillAmount�� ���� ���� �ε� ������ �ۼ�Ʈ�� ���� ������ ������ �ȵƴٸ�
            // ���� �ڷ�ƾ�� ��������..
            // �̹� �������� �ڷ�ƾ�� �� ���۽�Ű�� ������ �߻��ϹǷ�
            // �ڷ�ƾ�� �����Ѵٸ� ���� �Ŀ� ���� ����� �ε� ������ �ۼ�Ʈ�� �Ѱ� �ڷ�ƾ�� �ٽ� �����ϰ� �Ѵ�.
            if (loadGaugeUpdateCoroutine != null)
            {
                StopCoroutine(loadGaugeUpdateCoroutine);
                loadGaugeUpdateCoroutine = null;
            }

            // ����� ����� ��ü ������ �Ϸᰡ �ƴ϶��
            if (phase != IntroPhase.Complete)
            {
                // ���� �ε� �ۼ��������� ���Ѵ�.
                var loadPer = (float)phase / (float)IntroPhase.Complete;
                // ���� �ۼ��������� �ε��ٿ� ����
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
                    allLoaded = true;
                    LoadComplete = true;
                    
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
