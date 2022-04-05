using JetBrains.Annotations;
using UnityEngine.UI;

namespace Project.UI
{
    public abstract class SymbolPresenter
    {
        protected readonly Image _image;
        protected SymbolPresenter(Image image)
        {
            _image = image;
        }

        public void SetImage(UnityEngine.Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }

    [UsedImplicitly]
    public sealed class PlayerSymbolPresenter : SymbolPresenter
    {
        public PlayerSymbolPresenter(Image image) : base(image)
        {
            
        }
    }

    [UsedImplicitly]
    public sealed class OpponentSymbolPresenter : SymbolPresenter
    {
        public OpponentSymbolPresenter(Image image) : base(image)
        {

        }
    }
}