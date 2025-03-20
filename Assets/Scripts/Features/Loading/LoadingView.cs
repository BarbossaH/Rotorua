using Cysharp.Threading.Tasks;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : MonoBehaviour
{
    private Image _bgImg;
    private TextMeshProUGUI _loadingTMP;
    private Slider _slider;

    private float _currentValue = 0f;

    private void Awake()
    {
        _bgImg = transform.Find("BG_Loading").GetComponent<Image>();
        _loadingTMP = transform.Find("LoadingTMP").GetComponent<TextMeshProUGUI>();
        _slider = transform.Find("Slider").GetComponent<Slider>();
        _loadingTMP.text = "Win";
    }

    private void OnEnable()
    {
        SceneLoaderManager.Instance.OnSceneLoading += UpdateProgress;
    }
    private void OnDisable()
    {
        SceneLoaderManager.Instance.OnSceneLoading -= UpdateProgress;
    }

    private void UpdateProgress(float percentage, string des)
    {
        UpdateSliderAsync(percentage, des).Forget();
    }
    private async UniTask UpdateSliderAsync(float targetValue, string des)
    {
        while (_currentValue < targetValue)
        {
            _currentValue += Time.deltaTime;
            _slider.value = _currentValue;

            //await UniTask.Delay(50);
            await UniTask.Yield();
        }

        _slider.value = targetValue;
    }
}
