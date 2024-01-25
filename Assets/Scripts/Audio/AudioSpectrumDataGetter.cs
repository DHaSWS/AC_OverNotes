using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 参考サイト
/// https://note.com/logic_magic/n/n47e91a1e65bb
/// </summary>
public class AudioSpectrumDataGetter : MonoBehaviour {
    [SerializeField] public int Resolution = 512;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _dataOffset = 0;
    [SerializeField] private FFTWindow fftWindow = FFTWindow.Triangle;
    [SerializeField] public float[] SpectrumData = null;
    private float[] _data;

    private void Start() {
        var clip = _audioSource.clip;
        _data = new float[clip.channels * clip.samples];
        _audioSource.clip.GetData(_data, _dataOffset);
        SpectrumData = new float[(int)Resolution];
    }

    private void FixedUpdate() {
        Refresh();
    }

    private void Refresh() {
        bool cond = _audioSource.isPlaying && _audioSource.timeSamples < _data.Length;

        if (cond) {
            _audioSource.GetSpectrumData(SpectrumData, 0, fftWindow);
        } else {
            SpectrumData = Enumerable.Repeat<float>(0, (int)Resolution).ToArray();
        }

        for (int i = 1; i < SpectrumData.Length - 1; i++) {
            Debug.DrawLine(new Vector3(i - 1, SpectrumData[i] + 10, 0), new Vector3(i, SpectrumData[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(SpectrumData[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(SpectrumData[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), SpectrumData[i - 1] - 10, 1), new Vector3(Mathf.Log(i), SpectrumData[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(SpectrumData[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(SpectrumData[i]), 3), Color.blue);
        }
    }
}
