using UnityEngine;

namespace Common.MonoServices
{
    //this interface must exist, or it cannot be registered into ioc container
    public interface IAudioService { void PlayAudio(AudioClip audioClip); }
    public class AudioService : MonoBehaviour, IMonoService, IAudioService
    {
        [SerializeField] private AudioSource sfxSource;


        public void PlayAudio(AudioClip audioClip)
        {
            sfxSource.Play();
        }
    }
}