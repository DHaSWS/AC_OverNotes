using OverNotes;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AudioGetter : MonoBehaviour
{
    [SerializeField] public bool isAudioGenerated = false;
    public void GetAudioClip()
    {
        StartCoroutine(GenerateMusic());
    }

    private IEnumerator GenerateMusic()
    {
        foreach(BeatmapData data in SystemData.beatmaps)
        {
            string audioPath = data.audioFilePath;
            string extension = Path.GetExtension(audioPath);
            AudioType type = AudioType.UNKNOWN;
            switch (extension)
            {
                case ".mp3":
                    {
                        type = AudioType.MPEG;
                    }
                    break;
                case ".ogg":
                    {
                        type = AudioType.OGGVORBIS;
                    }
                    break;
                default:
                    {
                        Debug.LogError("This extension is not supported");
                    }
                    break;
            }

            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(
              "file://" + audioPath, type);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                data.clip = DownloadHandlerAudioClip.GetContent(www);
                data.clip.name = Path.GetFileNameWithoutExtension(audioPath);
            }
        }
        Debug.Log("Generated music");
    }
}
