using NUnit.Framework;
using Project.UI;
using UnityEngine;

namespace Project.Tests.Edit
{
    internal sealed class GameTests
    {
        [Test]
        public void Returns_Rock_Image_When_Rock_Selected()
        {
            string enteredSymbol = Symbol.Rock;

            Sprite icon = Symbol.GetSymbolImage(enteredSymbol);

            Assert.AreEqual(Symbol.Rock, icon.name);
        }

        [Test]
        public void Returns_Paper_Image_When_Paper_Selected()
        {
            string enteredSymbol = Symbol.Paper;

            Sprite icon = Symbol.GetSymbolImage(enteredSymbol);

            Assert.AreEqual(Symbol.Paper, icon.name);
        }

        [Test]
        public void Returns_Scissors_Image_When_Scissors_Selected()
        {
            string enteredSymbol = Symbol.Scissors;

            Sprite icon = Symbol.GetSymbolImage(enteredSymbol);

            Assert.AreEqual(Symbol.Scissors, icon.name);
        }

        [Test]
        public void Returns_True_When_Rock_Beats_Scissors()
        {
            Assert.IsTrue(Symbol.IsGreater(Symbol.Rock, Symbol.Scissors));
        }

        [Test]
        public void Returns_True_When_Scissors_Beats_Paper()
        {
            Assert.IsTrue(Symbol.IsGreater(Symbol.Scissors, Symbol.Paper));
        }

        [Test]
        public void Returns_True_When_Paper_Beats_Rock()
        {
            Assert.IsTrue(Symbol.IsGreater(Symbol.Paper, Symbol.Rock));
        }

        [Test]
        public void Returns_True_When_Rock_Equals_Rock()
        {
            Assert.IsTrue(Symbol.AreEqual(Symbol.Rock, Symbol.Rock));
        }

        [Test]
        public void Returns_True_When_Paper_Equals_Paper()
        {
            Assert.IsTrue(Symbol.AreEqual(Symbol.Paper, Symbol.Paper));
        }

        [Test]
        public void Returns_True_When_Scissors_Equals_Scissors()
        {
            Assert.IsTrue(Symbol.AreEqual(Symbol.Scissors, Symbol.Scissors));
        }
    }
}
