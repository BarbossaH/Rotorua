namespace Managers
{
    using System;
    using Common;
    using UnityEngine;

    // [AutoRegisterManager]
    public class AudioListenerManager : MonoBehaviour
    {
        public static AudioListenerManager Instance;
        private AudioListener currentListener;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            EventCenterManager.Instance.RegisterEvent(GameEvents.SceneSwitching, OnSwitchingScene);
            EventCenterManager.Instance.RegisterEvent(GameEvents.SceneSwitched, OnSceneSwitched);
        }

        private void OnDestroy()
        {
            EventCenterManager.Instance.UnregisterEvent(GameEvents.SceneSwitching, OnSwitchingScene);
            EventCenterManager.Instance.UnregisterEvent(GameEvents.SceneSwitched, OnSceneSwitched);
        }

        private void OnSceneSwitched(object[] obj)
        {
            AutoDetectAndSet();
        }

        private void OnSwitchingScene(object[] obj)
        {
            DisableCurrentListener();
        }

        private void SetActiveListener(AudioListener newListener)
        {
            if (newListener == null)
            {
                Debug.LogWarning("Trying to set null to audio listener");
                return;
            }

            if (currentListener != null && currentListener != newListener)
            {
                currentListener.enabled = false;
            }

            currentListener = newListener;
            currentListener.enabled = true;
        }

        private void DisableCurrentListener()
        {
            if (currentListener != null)
            {
                currentListener.enabled = false;
                currentListener = null;
            }
        }

        private void AutoDetectAndSet()
        {
            AudioListener[] ls = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
            if (ls.Length == 0)
            {
                Debug.Log("Not found listener");
                return;
            }

            for (int i = 0; i < ls.Length; i++)
            {
                if (i == 0)
                {
                    SetActiveListener(ls[0]);
                    // Debug.Log("Find a listener");
                }
                else { ls[i].enabled = false; }
            }
        }
    }

}