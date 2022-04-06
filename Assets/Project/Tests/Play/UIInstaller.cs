using JetBrains.Annotations;
using Project.UI;
using Zenject;

namespace Project.Tests.Play
{
    [UsedImplicitly]
    internal sealed class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OpponentInputPresenter>().AsSingle().NonLazy();
        }
    }
}