using UnityEngine;


namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemyAudioManager : MonoBehaviour
    {
        private AudioSource _source;
        [SerializeField] private AudioClip[] footSteps;
        private const float FootStepSoundFixer = 0.8f;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }
        public void PlaySound(AudioClip sound)
        {
            _source.volume = 1f;
            _source.PlayOneShot(sound);
        }
        public void FootStep()
        {
            _source.volume = FootStepSoundFixer;
            int randInt = Random.Range(0, footSteps.Length);
            _source.PlayOneShot(footSteps[randInt]);
        }
    }
}