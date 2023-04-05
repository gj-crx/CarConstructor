using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;
using UnityEngine.EventSystems;

namespace UI.Buttons
{
    public class ButtonAccelerate: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private float fixedAccelerationInput = 1;

        [SerializeField]
        private Animator animator;



        public void OnPointerDown(PointerEventData eventData)
        {
            PlayerRepresentation.LocalPlayer.SelectedCar.PowerInput = fixedAccelerationInput;
            PlayerRepresentation.LocalPlayer.SelectedCar.ContiniousAcceleration = true;

            animator.Play("Pressed");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PlayerRepresentation.LocalPlayer.SelectedCar.ContiniousAcceleration = false;

            animator.Play("Unpressed");
        }
    }
}
