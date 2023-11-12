using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

//ToDo: Create audio system to inject audio plays ids
//ToDo: Make Pool for instance sound objects

public class AudioPlayer : MonoBehaviour, IAudioPlayer
{
    [SerializeField] private string[] _soundIds = default;
    [SerializeField] private AudioMixer _mixer = null;
    [SerializeField] private string _goSoundId = "SoundInstance";

    private Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();
    private GameObject _audioObject = null;

    public void Start()
    {
        var getter = new AddressableAsyncAssetGetter<AudioClip>();
        foreach (var clipId in _soundIds)
        {
            getter.LoadResource(clipId, CompleteLoadAudioClip);
        }
    }

    private void CompleteLoadAudioClip(AudioClip res)
    {
        _clips[res.name] = res;

        if (_audioObject == null)
        {
            var getter = new AddressableAsyncAssetGetter<GameObject>();
            getter.LoadResource(_goSoundId, CompleteLoadAudioInstance);
        }
    }

    void CompleteLoadAudioInstance(GameObject audioObject)
    {
        if (this == null || gameObject == null || audioObject == null)
            return;

        audioObject.name = $"{nameof(AudioPlayer)}.{gameObject.name}";
        var _audio = audioObject.GetComponent<AudioSource>();
        _audio.outputAudioMixerGroup = _mixer.outputAudioMixerGroup;
        _audioObject = audioObject;
    }

    public void PlaySound(string audioId)
    {
        if (_audioObject == null)
        {
            Debug.LogError($"[{nameof(AudioPlayer)}] Don't loaded audio object with id {_soundIds}");
            return;
        }

        //ToDo: remake to pool object Get
        if (_clips.TryGetValue(audioId, out AudioClip clip))
        {
            var audio = Instantiate(_audioObject).GetComponent<AudioSource>();
            audio.clip = clip;
            audio.Play();

            DestroyAfterSound(audio.gameObject, clip.length).Forget();
        }
    }

    private async UniTaskVoid DestroyAfterSound(GameObject soundGO, float duration)
    {
        await UniTask.WaitForSeconds(duration + 0.1f, true);

        //ToDo: remake to pool object Release
        Destroy(soundGO);
    }
}