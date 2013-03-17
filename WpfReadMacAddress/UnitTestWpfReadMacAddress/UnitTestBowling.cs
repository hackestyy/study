using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZTE.Game;

namespace UnitTestProject1
{
    [TestClass]
    public class BowlingUnitTest
    {
        private Game game;
        public BowlingUnitTest()
        {
            game = new Game();
        }
        [TestMethod]
        public void TestScoreForOther()
        {
            Assert.AreEqual(0, game.ScoreForFrame(0));
            Assert.AreEqual(0, game.Score);

            game.Add(4);
            game.Add(5);
            Assert.AreEqual(9, game.ScoreForFrame(1));
            Assert.AreEqual(9, game.Score);
        }
        [TestMethod]
        public void TestScoreBuZhong()
        {

            game.Add(4);
            game.Add(6);
            game.Add(3);
            Assert.AreEqual(13, game.ScoreForFrame(1));

            game.Add(4);
            Assert.AreEqual(7, game.ScoreForFrame(2));
            Assert.AreEqual(20, game.Score);
        }

        [TestMethod]
        public void TestScoreQuanZhong()
        {
            game.Add(10);
            game.Add(6);
            game.Add(3);
            Assert.AreEqual(19, game.ScoreForFrame(1));
            Assert.AreEqual(9, game.ScoreForFrame(2));
            Assert.AreEqual(28, game.Score);
        }

        [TestMethod]
        public void TestScoreAllQuanZhong()
        {
            for (int i = 0; i < 12; i++)
                game.Add(10);

            Assert.AreEqual(30, game.ScoreForFrame(1));
            Assert.AreEqual(30, game.ScoreForFrame(2));
            Assert.AreEqual(30, game.ScoreForFrame(10));
            Assert.AreEqual(300, game.Score);
        }

        [TestMethod]
        public void TestScoreRandom()
        {
            game.Add(1);
            game.Add(4);
            game.Add(4);
            game.Add(5);
            game.Add(6);
            game.Add(4);
            game.Add(5);
            game.Add(5);
            game.Add(10);
            game.Add(0);
            game.Add(1);
            game.Add(7);
            game.Add(3);
            game.Add(6);
            game.Add(4);
            game.Add(10);
            game.Add(2);
            game.Add(8);
            game.Add(6);

            Assert.AreEqual(133, game.Score);
        }
    }
}
