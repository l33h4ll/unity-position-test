using JetBrains.Annotations;
using TMPro;

namespace Project.UI
{
    [UsedImplicitly]
    public sealed class ResultTextPresenter
    {
        private readonly TMP_Text _resultText;

        public ResultTextPresenter(TMP_Text text)
        {
            _resultText = text;
        }

        public void ShowResult(string result)
        {
            _resultText.text = result;
            _resultText.gameObject.SetActive(true);
        }

        public void HideResult()
        {
            _resultText.gameObject.SetActive(false);
        }
    }
}