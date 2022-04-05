using JetBrains.Annotations;
using System;
using TMPro;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class PlayerInputPresenter
    {
        private readonly TMP_InputField _playerInput;

        public event Action<string> OnSubmit;

        public PlayerInputPresenter(TMP_InputField field)
        {
            _playerInput = field;
            _playerInput.onSubmit.AddListener(OnSubmitListener);
        }

        public void Reset()
        {
            _playerInput.text = "";
            _playerInput.enabled = true;
        }

        public void DisableInput()
        {
            _playerInput.enabled = false;
        }

        private void OnSubmitListener(string text)
        {           
            if (OnSubmit != null)
            {
                OnSubmit(text);
            }
        }
    }
}