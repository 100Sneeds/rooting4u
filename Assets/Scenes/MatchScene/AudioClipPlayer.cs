using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using System.Linq;

public class AudioClipPlayer : MonoBehaviour
{
    private static string[] AUDIO_CLIP_FILE_NAMES = new string[]
    {
        "Easy1",
        "Easy2",
        //"Easy3",
        //"Medium1",
        //"Medium2",
        //"Medium3",
        //"Hard1",
        //"Hard2",
    };

    private Dictionary<string, AudioClip> audioClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    async void Start()
    {
        this.audioClips = await this.LoadAudioClips(AUDIO_CLIP_FILE_NAMES);
        this.audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioClipByName(string audioClipName)
    {
        var audioClip = this.GetAudioClip(audioClipName);
        this.audioSource.PlayOneShot(audioClip);
    }

    private AudioClip GetAudioClip(string audioClipName)
    {
        return this.audioClips.GetValueOrDefault(audioClipName);
    }

    private async Task<Dictionary<string, AudioClip>> LoadAudioClips(string[] fileNames)
    {
        string audioClipDirectory = Path.Combine(Application.dataPath, "Sounds");
        string[] paths = AUDIO_CLIP_FILE_NAMES.Select((fileName) => Path.Combine(audioClipDirectory, fileName + ".ogg")).ToArray();

        var audioClips = new Task<AudioClip>[paths.Length];
        for (int pathIndex = 0; pathIndex < paths.Length; pathIndex++) {
            var path = paths[pathIndex];
            var audioClip = LoadAudioClip(path, AudioType.OGGVORBIS);
            audioClips[pathIndex] = audioClip;
        }

        var audioClipDictionary = new Dictionary<string, AudioClip>();
        for (int fileNameIndex = 0; fileNameIndex < paths.Length; fileNameIndex++)
        {
            var fileName = fileNames[fileNameIndex];
            var audioClip = await audioClips[fileNameIndex];
            audioClipDictionary.Add(fileName, audioClip);
        }

        return audioClipDictionary;
    }

    private async Task<AudioClip> LoadAudioClip(string path, AudioType audioType)
    {
        AudioClip audioClip = null;
        int retryDelayMs = 5;
        var builder = new UriBuilder(path) { Scheme = Uri.UriSchemeFile };
        var uri = builder.ToString();
        using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(uri, audioType))
        {
            webRequest.SendWebRequest();

            // wrap tasks in try/catch, otherwise it'll fail silently
            try
            {
                while (!webRequest.isDone)
                {
                    await Task.Delay(retryDelayMs);
                    retryDelayMs *= 2; // exponential backoff
                    if (retryDelayMs >= 8000)
                    {
                        throw new Exception("Timeout error: Failed to load audio clip after 8 seconds");
                    }
                }

                bool isConnectionError = webRequest.result == UnityWebRequest.Result.ConnectionError;
                bool isProtocolError = webRequest.result == UnityWebRequest.Result.ProtocolError;

                if (isConnectionError || isProtocolError)
                {
                    Debug.Log($"{webRequest.error}");
                }
                else
                {
                    audioClip = DownloadHandlerAudioClip.GetContent(webRequest);
                }
            }
            catch (Exception err)
            {
                Debug.Log($"{err.Message}, {err.StackTrace}");
            }
        }

        return audioClip;
    }
}
