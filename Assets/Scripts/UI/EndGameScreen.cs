using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace UI
{
    public class EndGameScreen : MonoBehaviour
    {
        public UnityEvent onEndGame;
        private AudioMixer _audioMixer;

        private void Awake()
        {
            _audioMixer = FindObjectOfType<AudioMixer>();
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void OnEnable()
        {
            _audioMixer.SetFloat("Master", 0.1f);
        }
        public void InvokeEndGameEvent()
        {
            onEndGame.Invoke();
        }
    }
}