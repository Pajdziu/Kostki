﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Kostki.Exceptions;
using Kostki.Class;
using System.Diagnostics;

namespace Kostki.Class
{
    public class Game
    {
        private Id[,,] GameBoard;
        private ControlPanel controlPanel;

        public Game(ControlPanel controlPanel)
        {
            this.controlPanel = controlPanel;
            this.GameBoard = new Id[4, 4, 4];
        }

        public Id GetBoardField(PlaceType place, int x, int y)
        {
            return GameBoard[(int)place, x, y];
        }

        public Id[,,] GetGameBoard() 
        {
            return this.GameBoard;
        }

        private void ClearBoards()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.GameBoard[(int)PlaceType.Grid,i, j] = null;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                this.GameBoard[(int)PlaceType.Rand, i, 0] = null;
            }
        }

        public Boolean IsRandBoardClear() //tymczasowo public
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.GameBoard[(int)PlaceType.Rand,i, 0] != null)
                {
                    return false;
                }
            }
            return true;
        }

        public int HowMuchFreeSpaceOnGameBoard()
        {
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (this.GameBoard[(int)PlaceType.Grid, i, j] == null)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public CardImage[] RandNewCards()
        {
            CardImage[] randImages = new CardImage[4];
            int HowMuch = this.HowMuchFreeSpaceOnGameBoard();
            Random random = new Random();
            int FigureType, CardColor;

            if (!this.IsRandBoardClear())
            {
                throw new AlreadyDoneException();
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    randImages[i] = null;
                }

                for (int i = 0; i < Math.Min(HowMuch,4); i++)
                {
                    FigureType = random.Next(4);
                    CardColor = random.Next(4);
                    CardImage cardImage = new CardImage(FigureType, CardColor);
                    cardImage.SetImage(this.controlPanel.GetImageByColorAndId(FigureType, CardColor));
                    randImages[i] = cardImage;
                    this.GameBoard[(int)PlaceType.Rand, i, 0] = new Id((Figures)FigureType, (CardColors)CardColor);
                }
                return randImages;
            }
        }

        public Boolean IsFieldFree(int x, int y, PlaceType placeType)
        {
            x--;
            y--;

            if (placeType == PlaceType.Grid && !(x > 3 || x < 0 || y > 3 || y < 0))
            {
                if (this.GameBoard[(int)PlaceType.Grid,x, y] != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (placeType == PlaceType.Rand && !(x > 3 || x < 0 || y != 0))
            {
                if (this.GameBoard[(int)PlaceType.Rand,x,y] != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void MoveCards(PlaceType start, int startX, int startY, PlaceType end, int endX, int endY)
        {
            try
            {
                if (this.GameBoard[(int)end, endX, endY] != null)
                {
                    throw new AlreadyTakenException();
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new AlreadyTakenException();
            }
            catch (AlreadyTakenException)
            {
                throw new AlreadyTakenException();
            }

            Id id = this.GameBoard[(int)start, startX, startY];
            if (start == PlaceType.Rand)
            {
                this.GameBoard[(int)start, startX, startY] = null;
            }
            else
            {
                this.GameBoard[(int)start, startX, startY] = null;
            }
            this.GameBoard[(int)end, endX, endY] = id;
            return;
        }

        /// <summary>
        /// Funkcja blokująca podany kafelek. Wykorzystywana po wciśnięciu przyciski next [apply] na appbarze.
        /// </summary>
        /// <param name="x">Współrzędna X</param>
        /// <param name="y">Współrzędna Y</param>

        public void BlockField(int x, int y)
        {
            if (x < 4 && y < 4)
            {
                this.GameBoard[(int) PlaceType.Grid, x, y].Blocked = true;
            }
        }

        /// <summary>
        /// Funkcja zwracająca stan zablokowania danego kafelka. 
        /// </summary>
        /// <param name="x">Współrzędna X</param>
        /// <param name="y">Współrzędna Y</param>
        /// <returns>False, gdy kafelkiem możemy poruszać. True w przeciwnym wypadku</returns>

        public Boolean IsFieldBlocked(int x, int y)
        {
            return this.GameBoard[(int) PlaceType.Grid, x, y].Blocked;
        }

        public void SetImageOnCoords(PlaceType place, int x, int y, Image image)
        {
            this.GameBoard[(int)place, x, y].Image = image;
        }

        public Image GetImageOnCoords(int x, int y)
        {
            return this.GameBoard[(int)PlaceType.Grid, x, y].Image;
        }

        public Image GetImageOnCoords(PlaceType place,int x, int y)
        {
            return this.GameBoard[(int)place, x, y].Image;
        }

        public Image DeleteImageOnCoords(int x, int y)
        {
            Image image = this.GameBoard[(int)PlaceType.Grid, x, y].Image;
            this.GameBoard[(int)PlaceType.Grid, x, y] = null;
            return image;
        }

        public void SetJokerOnCoords(int x, int y)
        {
            try
            {
                GameBoard[(int)PlaceType.Grid, x, y].IsJoker = true;
                GameBoard[(int)PlaceType.Grid, x, y].IsJokerBlocked = true;
            }
            catch (NullReferenceException)
            {
                GameBoard[(int)PlaceType.Grid, x, y] = /*new Id(Figures.Club, CardColors.Green);//*/new Id(Figures.Null, CardColors.Null);
                GameBoard[(int)PlaceType.Grid, x, y].IsJoker = true;
                GameBoard[(int)PlaceType.Grid, x, y].IsJokerBlocked = true;
            }
        }

        public void SetJokerOnCoords(PlaceType place, int x, int y)
        {
            try
            {
                GameBoard[(int)place, x, y].IsJoker = true; 
                GameBoard[(int)place, x, y].IsJokerBlocked = true;
            }
            catch (NullReferenceException)
            {
                GameBoard[(int)place, x, y] = /*new Id(Figures.Club, CardColors.Green);//*/new Id(Figures.Null, CardColors.Null);
                GameBoard[(int)place, x, y].IsJoker = true;
                GameBoard[(int)place, x, y].IsJokerBlocked = true;
            }
        }

        public Boolean GetJokerOnCoords(PlaceType place, int x, int y)
        {
            return GameBoard[(int)place, x, y].IsJoker;
        }

        public void MoveJoker(PlaceType start, int startX, int startY, PlaceType end, int endX, int endY)
        {
            if (GameBoard[(int)end, endX, endY] == null)
            {
                if (GameBoard[(int)start, startX, startY].Figure == Figures.Null)
                {
                    GameBoard[(int)end, endX, endY] = GameBoard[(int)start, startX, startY];
                    GameBoard[(int)start, startX, startY] = null;
                }
                else
                {
                    GameBoard[(int)start, startX, startY].IsJoker = false;
                    GameBoard[(int)start, startX, startY].IsJokerBlocked = false;
                    SetJokerOnCoords(end,endX, endY);
                }
            }
            else
            {
                if (GameBoard[(int)start, startX, startY].Figure == Figures.Null)
                {
                    GameBoard[(int)end, endX, endY].IsJoker = true;
                    GameBoard[(int)start, startX, startY] = null;
                }
                else
                {
                    GameBoard[(int)start, startX, startY].IsJoker = false;
                    GameBoard[(int)start, startX, startY].IsJokerBlocked = false;
                    GameBoard[(int)end, endX, endY].IsJoker = true;
                }
            }
        }

        public void AddJoker(Image image, int x)
        {
            GameBoard[(int)PlaceType.Joker, x, 0] = new Id(Figures.Null, CardColors.Null);
            GameBoard[(int)PlaceType.Joker, x, 0].Image = image;
            GameBoard[(int)PlaceType.Joker, x, 0].IsJoker = true;
        }

        public Boolean NoJokerOnBoard()
        {
            for (int i = 0; i < 2; i++)
            {
                if (GameBoard[(int)PlaceType.Joker, i, 0] != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}