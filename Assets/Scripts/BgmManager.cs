using UnityEngine;

public class BgmManager : MonoBehaviour
{
    [SerializeField] GlobalEvent reachTargetEvent;
    [SerializeField] AudioSource reachTargetAudio, playerDeathAudio;
    public static BgmManager Instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.GetComponent<AudioSource>();
        PlayMusic();

        EventManager.Instance.AddListener(reachTargetEvent, () => reachTargetAudio.Play());

        EventManager.Instance.AddListener(GlobalEvent.PlayerRoasted, () => playerDeathAudio.Play());
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}