using FMODUnity;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter _emitter;

    public static MusicHandler Instance { get; private set; }
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartMusic();
    }

    public void StartMusic()
    {
        _emitter.Play();
    }

    public void StopMusic()
    {
        _emitter.Stop();
    }
}
