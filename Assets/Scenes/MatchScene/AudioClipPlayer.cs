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
        "Backbeat"
    };

    [Serializable]
    public struct NamedAudioClip
    {
        public string name;
        public AudioClip audioClip;
    }
    public NamedAudioClip[] namedAudioClips;

    private Dictionary<string, AudioClip> audioClips;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.audioClips = this.GetAudioClipsFromNamedArray(namedAudioClips);
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

    public void PlayAudioClipByName(string audioClipName, float volume)
    {
        var audioClip = this.GetAudioClip(audioClipName);
        this.audioSource.PlayOneShot(audioClip, volume);
    }

    private Dictionary<string, AudioClip> GetAudioClipsFromNamedArray(NamedAudioClip[] namedAudioClips)
    {
        var audioClips = new Dictionary<string, AudioClip>();
        foreach (var namedAudioClip in namedAudioClips)
        {
            audioClips.Add(namedAudioClip.name, namedAudioClip.audioClip);
        }
        return audioClips;
    }

    private AudioClip GetAudioClip(string audioClipName)
    {
        return this.audioClips.GetValueOrDefault(audioClipName);
    }
}
