using Character;
using UnityEngine;

namespace UI
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject gameMenu;
        [SerializeField] private GameObject buttons;
        [SerializeField] private GameObject player;
        private CharacterController2D _controller;
        private Rigidbody2D _rb;

        private void Awake()
        {
            gameMenu.SetActive(false);
            _controller = player.GetComponent<CharacterController2D>();
            _rb = player.GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || !buttons.activeSelf) return;
            if (!gameMenu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
                gameMenu.SetActive(true);
                _controller.enabled = false;
                _rb.isKinematic = true;
                _rb.velocity = Vector2.zero;
            }
            else
            {
                BackToGame();
            }
        }
        public void BackToGame()
        {
            Cursor.lockState = CursorLockMode.Locked;
            gameMenu.SetActive(false);
            _controller.enabled = true;
            _rb.isKinematic = false;
        }
    }
}