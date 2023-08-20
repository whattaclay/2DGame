using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudioManager : MonoBehaviour
    {
        public static PlayerAudioManager instance { get; private set;}
        private AudioSource _source;
        [SerializeField] private AudioClip[] footSteps;

        private void Awake()
        {
            instance = this;
            _source = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip sound)
        {
            _source.PlayOneShot(sound);
        }
        public void FootStep()
        {
            int randInt = Random.Range(0, footSteps.Length);
            _source.PlayOneShot(footSteps[randInt]);
        }
    }
}