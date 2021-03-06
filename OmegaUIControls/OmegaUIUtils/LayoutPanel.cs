﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Agilent.MHDA.Omega
{
    /// <summary>
    /// LayoutPanel is a <see cref="Grid"/> inside a <see cref="GroupBox"/> with given number of rows and columns. 
    /// By default, star sizing is used to size the rows and columns with equal height / width.
    /// </summary>
    class LayoutPanel : GroupBox
    {
        private Grid grid;

        //Counters to maintain the next available row and column in the grid. 
        private int curCol = 0;
        private int curRow = 0;

        //Constructors
        public LayoutPanel(int rows, int cols) : base()
        {
            if (rows == 1)
                init();
            else
                init(rows, cols);

            addRows(rows);
            addCols(cols);
        }

        public LayoutPanel(UIElement comp1, UIElement comp2) : base()
        {
            init();
            addCols(2);
            Add(comp1, 1);
            Add(comp2, 1);
        }

        public LayoutPanel(UIElement comp1, UIElement comp2, UIElement comp3) : base()
        {
            init();
            addCols(3);
            Add(comp1, 1);
            Add(comp2, 1);
            Add(comp3, 1);
        }

        public LayoutPanel(UIElement comp1, UIElement comp2, UIElement comp3, UIElement comp4) : base()
        {
            init();
            addCols(4);
            Add(comp1, 1);
            Add(comp2, 1);
            Add(comp3, 1);
            Add(comp4, 1);
        }

        /// <summary>
        /// Inititalizes the <see cref="Grid"/> and adds it to the <see cref="GroupBox"/>.
        /// </summary>
        private void init()
        {
            grid = new Grid();
            grid.VerticalAlignment = VerticalAlignment.Center;
            this.Content = grid;
            this.BorderThickness = new Thickness(0);
            this.Margin = new Thickness(10);
            this.Width = 400;
            this.Height = 30;
        }

        private void init(int r, int c)
        {
            grid = new Grid();
            this.Content = grid;
            this.BorderThickness = new Thickness(0);
            this.Margin = new Thickness(10);
        }

        /// <summary>
        /// Adds border to the <see cref="GroupBox"/> with the specified <paramref name="header"/>.
        /// </summary>
        /// <param name="header"></param>
        public void AddBorder(string header)
        {
            this.BorderThickness = new Thickness(1);
            this.Padding = new Thickness(3);

            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 16;
            textBlock.Text = header;
            this.Header = textBlock;
        }

        /// <summary>
        /// Add the control at the specified row and column
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="weight"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void Add(UIElement comp, double weight, int row, int col)
        {
            grid.Children.Add(comp);
            if (weight != 1)
                grid.ColumnDefinitions[col].Width = new GridLength(weight, GridUnitType.Star);
            Grid.SetColumn(comp, col);
            curCol++;
            Grid.SetRow(comp, row);
            curRow++;
        }

        /// <summary>
        /// Add the <paramref name="comp"/> to the next available column in the grid.
        /// </summary>
        /// <param name="comp">Control to be added.</param>
        /// <param name="weight">Relative width (star sizing) of the component.</param>
        public void Add(UIElement comp, double weight)
        {
            Add(comp, weight, 0);
        }

        /// <summary>
        /// Add the <paramref name="comp"/> to the next available column in the grid.
        /// </summary>
        /// <param name="comp">Control to be added.</param>
        /// <param name="weight">Relative width (star sizing) of the component.</param>
        /// <param name="height"></param>
        public void Add(UIElement comp, double weight, int height)
        {
            //comp.Measure(new Size(0, 20));
            grid.Children.Add(comp);
            if (weight!=1)
                grid.ColumnDefinitions[curCol].Width = new GridLength(weight, GridUnitType.Star);
            Grid.SetColumn(comp, curCol);
            curCol++;
        }

        /// <summary>
        /// Changes the height and width of the <see cref="GroupBox"/>. 
        /// </summary>
        /// <param name="h"></param>
        /// <param name="w"></param>
        public void ChangeDimension(double h, double w)
        {
            this.Height = h;
            this.Width = w;
        }

        /// <summary>
        /// Adds specified number of rows to the grid.
        /// </summary>
        /// <param name="r"></param>
        private void addRows(int r)
        {
            for(int i=0; i < r; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                //row.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(row);
            }
        }

        /// <summary>
        /// Adds specified number of columns to the grid.
        /// </summary>
        /// <param name="c"></param>
        private void addCols(int c)
        {
            for (int i = 0; i < c; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col);
            }
        }
    }
}
