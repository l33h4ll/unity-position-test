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
    }
}