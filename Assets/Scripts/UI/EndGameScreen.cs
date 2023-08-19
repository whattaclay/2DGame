using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class EndGameScreen : MonoBehaviour
    {
        public UnityEvent onEndGame;

        public void InvokeEndGameEvent()
        {
            onEndGame.Invoke();
        }
    }
}