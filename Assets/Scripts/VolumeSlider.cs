using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private string _vcaPath = "vca:/MasterVolume";
    
    private VCA _masterVca;

    private void Awake()
    {
        _masterVca = RuntimeManager.GetVCA(_vcaPath);
    }

    private void Start()
    {
        float volume;
        float finalVolume;
        _masterVca.getVolume(out volume, out finalVolume);
        _slider.value = volume * _slider.maxValue;
    }
   
    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetMasterVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetMasterVolume);
    }

    public void SetMasterVolume(float input)
    {
        float ratio = input / _slider.maxValue;
        ratio = Mathf.Clamp01(ratio);
        _masterVca.setVolume(ratio);
    }
}