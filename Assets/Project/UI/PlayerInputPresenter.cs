using JetBrains.Annotations;
using TMPro;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class PlayerInputPresenter
    {
        private readonly TMP_InputField _playerInput;
        private readonly GameManager _gameManager;

        public PlayerInputPresenter(TMP_InputField field, GameManager gameManager)
        {
            _playerInput = field;
            _gameManager = gameManager;

            _playerInput.onSubmit.AddListener(SubmitEntry);
        }

        /// <summary>
        /// Event handler for when player enters their symbol selection
        /// </summary>
        /// <param name="entry">Entered symbol</param>
        private void SubmitEntry(string entry)
        {            
            _gameManager.OnPlayerEntryEntered(entry);
        }                
    }
}