using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource gameMusicSource;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("SFX")]
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip failClip;
    [SerializeField] private AudioClip btnClick;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        PlayBackgroundMusic();
    }

    // ---------------- MUSIC ----------------
    public void PlayBackgroundMusic()
    {
        if (backgroundSource.isPlaying) return;

        backgroundSource.clip = backgroundMusic;
        backgroundSource.loop = true;
        backgroundSource.Play();
    }

    public void StopBackgroundMusic()
    {
        backgroundSource.Stop();
    }

    // ---------------- SFX ----------------
    public void PlayCoin()
    {
        PlaySFX(coinClip);
    }

    public void PlayJump()
    {
        PlaySFX(jumpClip);
    }

    public void PlayFail()
    {
        PlaySFX(failClip);
    }

    public void ButtonClick()
    {
        PlaySFX(btnClick);
    }

    void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        gameMusicSource.PlayOneShot(clip);
    }
}
