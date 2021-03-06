﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using Kostki.Exceptions;

namespace Kostki.Class
{
    public enum FourcardType 
    {
        Row,
        Column,
        Cross,
        Rectangle,
        Corners
    }

    public enum CardColors
    {
        Blue,
        Green,
        Red,
        Yellow,
        Joker
    }

    public enum PlaceType
    {
        Grid,
        Rand,
        Joker,
        Null
    }

    public enum Figures
    {
        Club,
        Diamond,
        Heart,
        Spade,
        Joker
    }

    public class ControlPanel
    {
        public readonly int AddPanel = 60;
        public readonly int LeftAndRight = 42;
        public readonly int CardSize = 86;
        public readonly int BorderSize = 3;
        public readonly int SpaceSize = 4;
        public readonly double OpacityCoefficient = 0.5;
        public readonly double ResizeCoefficient = 1.3;
        public readonly Point NewCardGrid;
        public readonly Point Grid4x4;
        public readonly SolidColorBrush BlockedColor = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
        public readonly SolidColorBrush FreeColor = new SolidColorBrush(Color.FromArgb(255, 100, 100, 100));

        private BitmapImage[,] cards;
        private BitmapImage joker;

        public ControlPanel()
        {
            this.cards = new BitmapImage[4, 4];
            this.NewCardGrid = new Point(45, 565);
            this.Grid4x4 = new Point(45, 153);

            this.LoadCards();
        }

        private void LoadCards()
        {
            this.cards[(int)Figures.Club, (int)CardColors.Blue] = new BitmapImage(new Uri("/img/clubs/blue.png", UriKind.Relative));
            this.cards[(int)Figures.Club, (int)CardColors.Green] = new BitmapImage(new Uri("/img/clubs/green.png", UriKind.Relative));
            this.cards[(int)Figures.Club, (int)CardColors.Red] = new BitmapImage(new Uri("/img/clubs/red.png", UriKind.Relative));
            this.cards[(int)Figures.Club, (int)CardColors.Yellow] = new BitmapImage(new Uri("/img/clubs/yellow.png", UriKind.Relative));

            this.cards[(int)Figures.Diamond, (int)CardColors.Blue] = new BitmapImage(new Uri("/img/diamond/blue.png", UriKind.Relative));
            this.cards[(int)Figures.Diamond, (int)CardColors.Green] = new BitmapImage(new Uri("/img/diamond/green.png", UriKind.Relative));
            this.cards[(int)Figures.Diamond, (int)CardColors.Red] = new BitmapImage(new Uri("/img/diamond/red.png", UriKind.Relative));
            this.cards[(int)Figures.Diamond, (int)CardColors.Yellow] = new BitmapImage(new Uri("/img/diamond/yellow.png", UriKind.Relative));

            this.cards[(int)Figures.Heart, (int)CardColors.Blue] = new BitmapImage(new Uri("/img/heart/blue.png", UriKind.Relative));
            this.cards[(int)Figures.Heart, (int)CardColors.Green] = new BitmapImage(new Uri("/img/heart/green.png", UriKind.Relative));
            this.cards[(int)Figures.Heart, (int)CardColors.Red] = new BitmapImage(new Uri("/img/heart/red.png", UriKind.Relative));
            this.cards[(int)Figures.Heart, (int)CardColors.Yellow] = new BitmapImage(new Uri("/img/heart/yellow.png", UriKind.Relative));

            this.cards[(int)Figures.Spade, (int)CardColors.Blue] = new BitmapImage(new Uri("/img/spade/blue.png", UriKind.Relative));
            this.cards[(int)Figures.Spade, (int)CardColors.Green] = new BitmapImage(new Uri("/img/spade/green.png", UriKind.Relative));
            this.cards[(int)Figures.Spade, (int)CardColors.Red] = new BitmapImage(new Uri("/img/spade/red.png", UriKind.Relative));
            this.cards[(int)Figures.Spade, (int)CardColors.Yellow] = new BitmapImage(new Uri("/img/spade/yellow.png", UriKind.Relative));

            this.joker = new BitmapImage(new Uri("/img/jokers/joker_violet.png", UriKind.Relative));
        }

        public int GetFieldSize()
        {
            return this.CardSize + this.BorderSize * 2;
        }

        public Image GetJoker()
        {
            Image image = new Image
                              {
                                  Source = this.joker, 
                                  Height = this.CardSize, 
                                  Width = this.CardSize
                              };

            return image;
        }

        public Image GetImageByColorAndId(Figures figure, CardColors cardColor)
        {
            Image image = new Image
                              {
                                  Source = this.cards[(int) figure, (int) cardColor],
                                  Height = this.CardSize,
                                  Width = this.CardSize
                              };
            return image;
        }

        public Image GetImageByColorAndId(int figure, int cardColor)
        {
            Image image = new Image
                              {
                                  Source = this.cards[figure, cardColor], 
                                  Height = this.CardSize, 
                                  Width = this.CardSize
                              };
            return image;
        }

        public Point GetTopJoker()
        {
            return new Point((double)this.LeftAndRight, (double)this.AddPanel + 4);
        }

        public Point GetTopGrid()
        {
            Point point = this.GetTopJoker();
            point.Y += 112;
            return point;
        }

        public Point GetTopRand()
        {
            Point point = this.GetTopGrid();
            point.Y += 412;
            return point;
        }

        /// <summary>
        /// Funkcja zwracająca prostokąt o kolorze szarym (Colors.Gray)
        /// </summary>
        /// <returns>Szary prostokąt</returns>
        public Rectangle GetRectangle()
        {
            Rectangle rect = new Rectangle
                                 {
                                     Fill = this.FreeColor, 
                                     Height = 96, 
                                     Width = 96
                                 };

            return rect;
        }

        /// <summary>
        /// Funkcja zwracająca prostokąt o podanym kolorze.
        /// </summary>
        /// <param name="color">Kolor prostokąta</param>
        /// <returns>Prostokąt o podanym kolorze</returns>
        public Rectangle GetRectangle(Color color)
        {
            Rectangle rectangle = new Rectangle
                                      {
                                          Fill = new SolidColorBrush(color), 
                                          Height = CardSize, 
                                          Width = CardSize
                                      };

            return rectangle;
        }

        public Rectangle GetMarkRectangle()
        {
            Rectangle rect = new Rectangle
                                 {
                                     Fill = new SolidColorBrush(Colors.White),
                                     Opacity = 0.35,
                                     Height = this.GetFieldSize(),
                                     Width = this.GetFieldSize()
                                 };

            return rect;
        }

        public Point GetCoordsFromActualPoint(Point point, PlaceType place)
        {
            if (place == PlaceType.Grid)
            {
                return GetRowAndColumnFromViewportPoint(point, this.GetTopGrid().Y);
            }
            else if (place == PlaceType.Rand)
            {
                return GetRowAndColumnFromViewportPoint(point, this.GetTopRand().Y);
            }
            else
            {
                return GetRowAndColumnFromViewportPoint(point, this.GetTopJoker().Y);
            }
        }

        public Point GetViewportPointFromActualPoint(Point point)
        {
            PlaceType place = this.RecognizePlace(point);
            Point p = new Point();

            if (place == PlaceType.Grid)
            {
                p = this.GetRowAndColumnFromViewportPoint(point, this.GetTopGrid().Y);
                return this.GetGridCoordsForMarkRectangle((int)p.X, (int)p.Y);
            }
            else if (place == PlaceType.Rand)
            {
                p = this.GetRowAndColumnFromViewportPoint(point, this.GetTopRand().Y);
                return this.GetRandCoordsForMarkRectangle((int)p.X, (int)p.Y);
            }
            else
            {
                p = this.GetRowAndColumnFromViewportPoint(point, this.GetTopJoker().Y);
                return this.GetJokerCoordsForMarkRectangle((int)p.X, (int)p.Y);
            }
        }

        public Point GetJokerViewportPointFromCoords(int x, int y)
        {
            if (x == 0)
            {
                return new Point(this.GetTopJoker().X, this.GetTopJoker().Y);
            }
            else
            {
                return new Point(this.GetTopJoker().X + 100, this.GetTopJoker().Y);
            }
        }

        public Point GetJokerCoordsForMarkRectangle(int x, int y)
        {
            Point resultPoint = this.GetTopJoker();
            resultPoint.X += (x - 1) * 100 + 2;
            resultPoint.Y += (y - 1) * 100 + 2;

            return resultPoint;
        }

        public Point GetGridCoordsForMarkRectangle(int x, int y)
        {
            Point resultPoint = this.GetTopGrid();
            resultPoint.X += (x - 1) * 100 + 3;
            resultPoint.Y += (y - 1) * 100 + 3;

            return resultPoint;
        }

        public Point GetRandCoordsForMarkRectangle(int x, int y)
        {
            Point resultPoint = this.GetTopRand();
            resultPoint.X += (x - 1) * 100 + 2;
            resultPoint.Y += (y - 1) * 100 + 2;

            return resultPoint;
        }

        public PlaceType RecognizePlace(Point current)
        {
            double top = this.GetTopGrid().Y;
            double bottom = top + 396;

            if (this.InsideRange(top, bottom, current.Y) && this.InsideRange(this.LeftAndRight,
                   this.LeftAndRight + 396, current.X))
            {
                return PlaceType.Grid;
            }

            top = this.GetTopRand().Y;
            bottom = top + 96;

            if (this.InsideRange(top, bottom, current.Y) && this.InsideRange(this.LeftAndRight,
                this.LeftAndRight + 396, current.X))
            {
                return PlaceType.Rand;
            }

            top = this.GetTopJoker().Y;
            bottom = top + 96;

            if (this.InsideRange(top, bottom, current.Y) && this.InsideRange(this.LeftAndRight,
                this.LeftAndRight + 188, current.X))
            {
                return PlaceType.Joker;
            }

            throw new OutOfBoardException();
                
        }

        public Point GetRowAndColumnFromViewportPoint(Point current, double top)
        {
            Point point = new Point
                              {
                                  Y = this.CalculateGridRowAndColumn((int) (current.Y - top)),
                                  X = this.CalculateGridRowAndColumn((int) (current.X - this.LeftAndRight))
                              };
            return point;
        }

        public Boolean InsideRange(double first, double second, double value)
        {
            if (value > first && value < second)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CalculateGridRowAndColumn(int y)
        {
            int result = (int) (y / 100);
            return result + 1;
        }
    }
}
