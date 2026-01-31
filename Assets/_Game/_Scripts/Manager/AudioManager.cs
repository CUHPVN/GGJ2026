using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    public enum SoundType
    {
        None,
        BGM_Main,
        Button_Click,
        Slot_Run,
        Slot_Stop,
        Mouse_Click,
        Game_Over//a

    }
    [System.Serializable]
    public class Sound
    {
        public SoundType type;
        public AudioClip clip;

        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }
    public Sound[] sounds;

    private Dictionary<SoundType, AudioSource> audioMap;

    protected override void Awake()
    {
        base.Awake(); // Gọi logic Singleton

        // Khởi tạo Dictionary
        audioMap = new Dictionary<SoundType, AudioSource>();

        foreach (Sound s in sounds)
        {
            // Tạo AudioSource component
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            // Thêm vào Dictionary để dùng sau này
            // Kiểm tra xem đã có chưa để tránh lỗi trùng lặp
            if (!audioMap.ContainsKey(s.type))
            {
                audioMap.Add(s.type, s.source);
            }
            else
            {
                Debug.LogWarning($"[AudioManager] Trùng lặp SoundType: {s.type}. Chỉ cái đầu tiên được dùng.");
            }
        }
    }

    // Hàm Play nhận vào Enum thay vì string
    public void Play(SoundType type)
    {
        if (type == SoundType.None) return;

        if (audioMap.TryGetValue(type, out AudioSource source))
        {
            source.Play();
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Không tìm thấy âm thanh: {type}");
        }
    }

    // (Tùy chọn) Hàm Stop
    public void Stop(SoundType type)
    {
        if (audioMap.TryGetValue(type, out AudioSource source))
        {
            source.Stop();
        }
    }
}