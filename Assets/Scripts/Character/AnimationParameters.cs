using UnityEngine;

namespace Character
{
    public class AnimationParameters : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetBoolParameter(string nameOf, bool value)
        {
            animator.SetBool(nameOf, value);
        }
        public void SetFloatParameter(string nameOf, float value)
        {
            animator.SetFloat(nameOf, value);
        }
        public void SetIntParameter(string nameOf, int value)
        {
            animator.SetInteger(nameOf, value);
        }
    }
}