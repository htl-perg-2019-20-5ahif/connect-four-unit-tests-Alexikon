using ConnectFour.Logic;
using System;
using Xunit;

namespace ConnectFour.Tests
{
    public class BoardTests
    {
        [Fact]
        public void AddWithInvalidColumnIndex()
        {
            var b = new Board();

            var oldPlayer = b.Turns;
            Assert.Throws<ArgumentOutOfRangeException>(() => b.AddStone(7));
            Assert.Equal(oldPlayer, b.Turns);
        }

        [Fact]
        public void PlayerChangesWhenAddingStone()
        {
            var b = new Board();

            var oldPlayer = b.Turns;
            b.AddStone(0);

            // Verify that player has changed
            Assert.NotEqual(oldPlayer, b.Turns);
        }

        [Fact]
        public void AddingTooManyStonesToARow()
        {
            var b = new Board();

            for(byte i = 0; i < 6; i++)
            {
                b.AddStone(0);
            }

            var oldPlayer = b.Turns;
            Assert.Throws<InvalidOperationException>(() => b.AddStone(0));
            Assert.Equal(oldPlayer, b.Turns);
        }

        [Fact]
        public void HorizontalWinInTheBeginning()
        {
            var b = new Board();
            byte win = 0;

            for (byte i = 0; i < 4; i++)
            {
                win = b.AddStone(i);

                if (win == 1)
                {
                    break;
                }

                b.AddStone(i);
            }

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void HorizontalWinInTheEnd()
        {
            var b = new Board();
            byte win = 0;

            for (byte i = 3; i < 7; i++)
            {
                win = b.AddStone(i);

                if (win == 1)
                {
                    break;
                }

                b.AddStone(i);
            }

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void HorizontalWinInTheMiddle()
        {
            var b = new Board();
            byte win = 0;

            for (byte i = 1; i < 5; i++)
            {
                win = b.AddStone(i);

                if (win == 1)
                {
                    break;
                }

                b.AddStone(i);
            }

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void HorizontalWinInTheMiddleWithHeight()
        {
            var b = new Board();

            b.AddStone(1);
            b.AddStone(1);

            b.AddStone(2);
            b.AddStone(2);

            b.AddStone(3);
            b.AddStone(3);

            b.AddStone(6);
            b.AddStone(4);

            b.AddStone(6);
            byte win = b.AddStone(4);

            Assert.True(b.ended);
            Assert.Equal(2, win);
        }

        [Fact]
        public void HorizontalWinInTheMiddleByAddingStoneInTheMiddle()
        {
            var b = new Board();

            b.AddStone(1);
            b.AddStone(1);

            b.AddStone(2);
            b.AddStone(2);

            b.AddStone(4);
            b.AddStone(4);

            byte win = b.AddStone(3);

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void VerticalWin()
        {
            var b = new Board();
            byte win = 0;

            for (byte i = 0; i < 4; i++)
            {
                win = b.AddStone(0);

                if(win == 1)
                {
                    break;
                }

                b.AddStone(1);
            }

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void VerticalWinWithHeight()
        {
            var b = new Board();
            byte win = 0;

            b.AddStone(1);
            b.AddStone(0);

            for (byte i = 0; i < 4; i++)
            {
                win = b.AddStone(0);

                if (win == 1)
                {
                    break;
                }

                b.AddStone(1);
            }

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void DiagonalWinUpperRightToLeftDown()
        {
            var b = new Board();

            b.AddStone(1);

            b.AddStone(2);
            b.AddStone(2);

            b.AddStone(3);
            b.AddStone(3);
            b.AddStone(6);
            b.AddStone(3);

            b.AddStone(4);
            b.AddStone(4);
            b.AddStone(4);
            byte win = b.AddStone(4);

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void DiagonalWinUpperRightToLeftDownWithHeight()
        {
            var b = new Board();

            b.AddStone(1);
            b.AddStone(2);
            b.AddStone(3);
            b.AddStone(4);

            b.AddStone(1);

            b.AddStone(2);
            b.AddStone(2);

            b.AddStone(3);
            b.AddStone(3);
            b.AddStone(6);
            b.AddStone(3);

            b.AddStone(4);
            b.AddStone(4);
            b.AddStone(4);
            byte win = b.AddStone(4);

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void DiagonalWinUpperLeftToRightDown()
        {
            var b = new Board();

            b.AddStone(4);

            b.AddStone(3);
            b.AddStone(3);

            b.AddStone(2);
            b.AddStone(2);
            b.AddStone(6);
            b.AddStone(2);

            b.AddStone(1);
            b.AddStone(1);
            b.AddStone(1);
            byte win = b.AddStone(1);

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void DiagonalWinUpperLeftToRightDownWithHeight()
        {
            var b = new Board();

            b.AddStone(1);
            b.AddStone(2);
            b.AddStone(3);
            b.AddStone(4);

            b.AddStone(4);

            b.AddStone(3);
            b.AddStone(3);

            b.AddStone(2);
            b.AddStone(2);
            b.AddStone(6);
            b.AddStone(2);

            b.AddStone(1);
            b.AddStone(1);
            b.AddStone(1);
            byte win = b.AddStone(1);

            Assert.True(b.ended);
            Assert.Equal(1, win);
        }

        [Fact]
        public void EndOfGame()
        {
            var b = new Board();

            b.AddStone(0);
            b.AddStone(1);
            b.AddStone(2);
            b.AddStone(3);
            b.AddStone(4);
            b.AddStone(5);
            b.AddStone(6);

            b.AddStone(6);
            b.AddStone(5);
            b.AddStone(4);
            b.AddStone(3);
            b.AddStone(2);
            b.AddStone(1);
            b.AddStone(0);


            b.AddStone(5);
            b.AddStone(4);
            b.AddStone(3);
            b.AddStone(2);
            b.AddStone(1);
            b.AddStone(0);
            b.AddStone(6);

            b.AddStone(0);
            b.AddStone(1);
            b.AddStone(2);
            b.AddStone(3);
            b.AddStone(4);
            b.AddStone(5);
            b.AddStone(6);


            b.AddStone(0);
            b.AddStone(1);
            b.AddStone(2);
            b.AddStone(3);
            b.AddStone(4);
            b.AddStone(5);
            b.AddStone(6);

            b.AddStone(5);
            b.AddStone(4);
            b.AddStone(3);
            b.AddStone(2);
            b.AddStone(1);
            b.AddStone(0);
            b.AddStone(6);

            Assert.Throws<InvalidOperationException>(() => b.AddStone(0));
        }
    }
}
