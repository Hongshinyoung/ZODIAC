using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private Dictionary<string, AudioClip> soundList = new Dictionary<string, AudioClip>();
    private List<string> loadedSounds = new List<string>();

    private Queue<AudioSource> audioSourcePool = new Queue<AudioSource>();
    [SerializeField] private int poolSize = 10;

    private float currentVolume = 1f;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject gameobj = new GameObject();
            gameobj.transform.SetParent(transform);
            AudioSource audioSource = gameobj.AddComponent<AudioSource>();
            audioSource.gameObject.SetActive(false);
            audioSourcePool.Enqueue(audioSource);
        }
    }


    public AudioClip LoadSound(string soundName)
    {
        if (!soundList.TryGetValue(soundName, out AudioClip value))
        {
            AudioClip clip = ResourceManager.Instance.LoadAsset<AudioClip>(soundName, eAssetType.Sound);
            if (clip != null)
            {
                soundList.Add(soundName, clip);
            }
        }
        return soundList[soundName];
    }

    public void LoadSceneSounds(List<string> loadsound)
    {
        foreach (var sound in loadedSounds)
        {
            soundList.Remove(sound);
        }

        loadedSounds.Clear();

        foreach (var sound in loadsound)
        {
            LoadSound(sound);
            loadedSounds.Add(sound);
        }
    }

    public void UnloadSound(string soundName)
    {
        if (soundList.ContainsKey(soundName))
        {
            soundList.Remove(soundName);
        }
    }

    public void PlaySound(string soundName, float volume, bool loop = false)
    {
        AudioClip playAudio = LoadSound(soundName);
        if (playAudio == null) return;

        if (audioSourcePool.Count > 0)
        {
            AudioSource audioSource = audioSourcePool.Dequeue();
            audioSource.gameObject.SetActive(true);

            // 오디오 설정 및 재생
            audioSource.clip = playAudio;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.Play();

            // 재생 종료 후 풀로 반환
            StartCoroutine(ReturnToPool(audioSource));
        }
    }

    private System.Collections.IEnumerator ReturnToPool(AudioSource audioSource)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        audioSource.clip = null; // 클립 초기화
        audioSource.gameObject.SetActive(false);
        audioSourcePool.Enqueue(audioSource);
    }

    public void SetVolume(float volume)
    {
        currentVolume = Mathf.Clamp(volume, 0f, 1f); // 볼륨 값 제한 (0~1)
        AudioListener.volume = currentVolume; // 전역 볼륨 설정

        // 만약 특정 AudioSource의 볼륨을 개별적으로 관리해야 한다면 여기서 처리
        foreach (var audioSource in audioSourcePool)
        {
            if (audioSource.gameObject.activeSelf) // 활성화된 AudioSource만 볼륨 업데이트
            {
                audioSource.volume = currentVolume;
            }
        }
    }

}