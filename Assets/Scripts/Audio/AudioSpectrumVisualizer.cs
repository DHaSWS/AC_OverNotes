using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrumVisualizer : MonoBehaviour {
    [SerializeField] private AudioSpectrumDataGetter _dataGetter;
    [SerializeField] private GameObject _barPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private List<GameObject> _spectrumBars;
    [SerializeField] private int _barMaxHeight;
    [SerializeField] private int _barCount;
    private int _dataCount => _dataGetter.SpectrumData.Length;
    private int _barStep;

    /// <summary>
    /// Set spectrum bar
    /// </summary>
    private void SetSpectrumBar() {
        for (int i = 0; i < _barCount; i++) {
            GameObject obj = Instantiate(_barPrefab, _content);
            _spectrumBars.Add(obj);
        }
    }

    /// <summary>
    /// Set address
    /// </summary>
    private void SetAddress() {
        _barStep = Mathf.RoundToInt(_dataCount / _barCount);
    }

    private void Start() {
        SetAddress();
        SetSpectrumBar();
    }

    /// <summary>
    /// Update spectrum bar
    /// </summary>
    /// <param name="index">Index</param>
    /// <param name="spectrumBar">Game object</param>
    private void OnUpdateSpectrumBar(int index, GameObject spectrumBar) {
        RectTransform rectTransform = spectrumBar.GetComponent<RectTransform>();
        Vector2 sizeDelta = rectTransform.sizeDelta;
        float spectrumData = Mathf.Log(_dataGetter.SpectrumData[_barStep * index] + 1) * 5;
        sizeDelta.y = _barMaxHeight * spectrumData;
        rectTransform.sizeDelta = sizeDelta;
    }

    private void FixedUpdate() {
        for (int i = 0; i < _barCount; i++) {
            GameObject bar = _spectrumBars[i];
            OnUpdateSpectrumBar(i, bar);
        }
    }

}
