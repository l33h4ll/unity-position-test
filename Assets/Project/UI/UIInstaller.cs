using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI
{
    internal sealed class UIInstaller : MonoInstaller<UIInstaller>
    {
        [SerializeField] private TMP_InputField playerInput;
        
        [SerializeField] private Image playerSymbol;
        [SerializeField] private Image opponentSymbol;
        
        [SerializeField] private TMP_Text countdownText;
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private Timer timer;

        public override void InstallBindings()
        {
            
            Container.BindInterfacesAndSelfTo<PlayerInputPresenter>().AsSingle().WithArguments(playerInput).NonLazy();            
            Container.BindInterfacesAndSelfTo<PlayerSymbolPresenter>().AsSingle().WithArguments(playerSymbol).NonLazy();
            Container.BindInterfacesAndSelfTo<OpponentInputPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<OpponentSymbolPresenter>().AsSingle().WithArguments(opponentSymbol).NonLazy();
            
            Container.BindInterfacesAndSelfTo<CountdownPresenter>().AsSingle().WithArguments(countdownText, timer).NonLazy();
            Container.BindInterfacesAndSelfTo<ResultTextPresenter>().AsSingle().WithArguments(resultText).NonLazy();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();            
        }
    }
}