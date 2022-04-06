using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Project.UI;
using UnityEngine.TestTools;
using Zenject;

namespace Project.Tests.Play
{
    internal sealed class GameTests : ZenjectIntegrationTestFixture
    {
        [Inject] private OpponentInputPresenter opponentInputPresenter;
        
        [SetUp]
        public void CommonInstall()
        {
            PreInstall();
            UIInstaller.Install(Container);
            PostInstall();
            Container.Inject(this);
        }
         
        [UnityTest]
        public IEnumerator Get_Returns_Random_Number_Between_0_And_2() => UniTask.ToCoroutine(async () =>
        {
            int selection = await opponentInputPresenter.GetRandomSelection(0, 2);

            Assert.That(selection, Is.InRange(0, 2));
        });
    }
}