using UnityEngine;
using Zenject;

namespace Runtime.Core.Misc
{
    public class AudioPlayer : IInitializable
    {
        private readonly Camera _camera;
        private AudioSource _audioSource;

        public AudioPlayer(Camera camera)
        {
            _camera = camera;
        }

        public void Initialize()
        {
            _audioSource = _camera.GetComponent<AudioSource>();
        }

        public void Play(AudioClip clip)
        {
            Play(clip, 1f);
        }

        public void Play(AudioClip clip, float volume)
        {
            _audioSource.PlayOneShot(clip, volume);
        }
    }
}