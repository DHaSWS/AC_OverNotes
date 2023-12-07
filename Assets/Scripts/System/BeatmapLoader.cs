using OverNotes;
using OverNotes.System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeatmapLoader : MonoBehaviour 
{
    [SerializeField] private AudioGetter audioGetter;

    public void CheckBeatmap()
    {
        OverNotesSystem system = OverNotesSystem.Instance;
        if (system.BeatmapPath == null)
        {
            Debug.LogError("BeatmapPath is not set");
        }

        if(audioGetter == null)
        {
            Debug.LogError("AudioGetter is not set");
        }

        if (system.Beatmaps.Count == 0)
        {
            LoadBeatmap();
        }
    }

    private void LoadBeatmap()
    {
        string[] folders = Directory.GetDirectories(OverNotesSystem.Instance.BeatmapPath);

        foreach (string folder in folders)
        {
            LoadFolder(folder);
        }

        audioGetter.GetAudioClip();
    }

    private void LoadFolder(string folderPath)
    {
        string[] files = Directory.GetFiles(folderPath);
        BeatmapData data = new BeatmapData();

        foreach (string file in files)
        {
            LoadFile(file, data);
        }

        OverNotesSystem.Instance.Beatmaps.Add(data);
    }

    private void LoadFile(string filePath, BeatmapData data)
    {
        string extension = Path.GetExtension(filePath);
        string fileName = Path.GetFileNameWithoutExtension(filePath);

        switch (extension)
        {
            case ".mp3":
                {
                    data.audioFilePath = filePath;
                }
                break;
            case ".ogg":
                {
                    data.audioFilePath = filePath;
                }
                break;
            case ".ontd":
                {
                    if (fileName == "info")
                        LoadInfo(filePath, data);

                    if (fileName == "bpm")
                        LoadBpm(filePath, data);
                }
                break;
            case ".oncd":
                {
                    LoadChart(filePath, data);
                }
                break;
        }
    }

    private void LoadInfo(string filePath, BeatmapData beatmapData)
    {
        string infoData = File.ReadAllText(filePath);
        string[] splitData = infoData.Split('\n');

        beatmapData.title = splitData[0];
        beatmapData.artist = splitData[1];
        beatmapData.offset = double.Parse(splitData[2]) + 1.0d;
    }

    private void LoadBpm(string filePath, BeatmapData beatmapData)
    {
        string bpmData = File.ReadAllText(filePath);
        string[] splitData = bpmData.Split('\n');

        foreach (string line in splitData)
        {
            BPMParam param = new BPMParam();
            string[] splitLine = line.Split('\t');

            param.time = double.Parse(splitLine[0]);
            param.value = double.Parse(splitLine[1]);

            beatmapData.bpms.Add(param);
        }
    }

    private void LoadChart(string _path, BeatmapData beatmapData)
    {
        string chartData = File.ReadAllText(_path);
        string[] splitData = chartData.Split('\n');

        ChartInfo chartInfo = new ChartInfo();

        chartInfo.diffucult = (ChartDifficult)int.Parse(splitData[0]);
        chartInfo.level = splitData[1];
        chartInfo.creator = splitData[2];

        for (int i = 0; i < splitData.Length; i++)
        {
            if (i < 3)
            {
                continue;
            }

            NoteParam noteParam = new NoteParam();

            string[] splitLine = splitData[i].Split('\t');
            noteParam.beatTime = double.Parse(splitLine[0]) + 1.0d;
            noteParam.beatEndTime = double.Parse(splitLine[1]) + 1.0d;
            noteParam.column = int.Parse(splitLine[2]);

            chartInfo.maxCombo += 1;

            if(noteParam.beatTime < noteParam.beatEndTime)
            {
                chartInfo.maxCombo += 1;
            }

            chartInfo.notes.Add(noteParam);
        }

        beatmapData.charts.Add(chartInfo);
    }
}
