using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    public enum SoundType
    {
        None,       // Luôn nên có giá trị mặc định
        BGM_Main,   // Nhạc nền
        Player_Jump,// Âm thanh nhảy
        Player_Die, // Âm thanh chết
        UI_Click,   // Tiếng click nút
        Enemy_Hit   // Kẻ địch bị đánh
    }
    [System.Serializable]
    public class Sound
    {
        public SoundType type;       // <-- Thay đổi ở đây (String -> Enum)
        public AudioClip clip;

        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds; // Dùng để config trong Inspector

    // Dictionary để tra cứu nhanh: SoundType -> AudioSource
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