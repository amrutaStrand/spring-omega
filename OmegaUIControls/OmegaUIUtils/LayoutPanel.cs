using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OmegaUIControls.OmegaUIUtils
{
    /// <summary>
    /// A grid panel inside a border with given number of rows and columns. By default, star sizing is used to
    /// size the rows and columns with equal height / width.
    /// </summary>
    class LayoutPanel : Border
    {
        private Grid grid;
        private int curCol = 0;
        public LayoutPanel(int rows, int cols) : base()
        {
            init();
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

        private void init()
        {
            grid = new Grid();
            this.Child = grid;
            this.Padding = new System.Windows.Thickness(3);
            this.Width = 400;
            this.Height = 30;
        }

        public void Add(UIElement comp, double weight)
        {
            Add(comp, weight, 0);
        }

        public void Add(UIElement comp, double weight, int height)
        {
            //comp.Measure(new Size(0, 20));
            grid.Children.Add(comp);
            if (weight!=1)
                grid.ColumnDefinitions[curCol].Width = new GridLength(weight, GridUnitType.Star);
            Grid.SetColumn(comp, curCol);
            curCol++;
        }

        private void addRows(int r)
        {
            for(int i=0; i < r; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(row);
            }
        }

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
