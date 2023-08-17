using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Environment
{
    public class BossDoor : MonoBehaviour
    {
        private TotemManager _totemManager;
        [SerializeField] private int firstFlameAmount;
        [SerializeField] private int doorOpenAmount;
        [SerializeField] private GameObject firstFlame;
        [SerializeField] private GameObject secondFlame;
        [SerializeField] private TextMeshProUGUI bossDoorText;
        private bool _isReadyToOpen;
        public UnityEvent <string,float> onFlameEnable;
        private const float TimeForActivateFlame = 3f;
        private float _timeCounter;

        private void Awake()
        {
            _totemManager = FindObjectOfType<TotemManager>();
            firstFlame.SetActive(false);
            secondFlame.SetActive(false);
            bossDoorText.enabled = false;
        }
        private void Update()
        {
            FlamesEnableChecker();
        }
        private string BossDoorText()
        {
            var str = "";
            if (_totemManager.TotemsActivated < doorOpenAmount)
            {
                str = $"Осталось активировать тотемов: {doorOpenAmount - _totemManager.TotemsActivated}.";
            }
            else
            {
                str = $"Нажмите 'R', чтобы войти в комату Босса этажа.";
            }
            return str;
        }
        private void FlamesEnableChecker()
        {
            if (_totemManager.TotemsActivated >= firstFlameAmount && !firstFlame.activeSelf)
            {
                if (_timeCounter<=0f)
                {
                    onFlameEnable.Invoke("BossDoorCamera",5f);
                }
                _timeCounter += Time.deltaTime;
                if (TimeForActivateFlame < _timeCounter)
                {
                    firstFlame.SetActive(true);
                    _timeCounter = 0f;
                }
            }
            if (_totemManager.TotemsActivated >= doorOpenAmount && !secondFlame.activeSelf)
            {
                if (_timeCounter<=0f)
                {
                    onFlameEnable.Invoke("BossDoorCamera",5f);
                }
                _timeCounter += Time.deltaTime;
                if (TimeForActivateFlame < _timeCounter)
                {
                    secondFlame.SetActive(true);
                    _isReadyToOpen = true;
                    _timeCounter = 0f;
                }
            }
        }
        private void OnTriggerStay2D(Collider2D col)
        { 
            if (!col.CompareTag("Player")) return;
            bossDoorText.enabled = true;
            bossDoorText.text = BossDoorText();
            if(!_isReadyToOpen) return;
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Switch Scene Logic
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            bossDoorText.enabled = false;
        }
    }
}