using System;
using System.Windows;
using System.Windows.Controls;              // UserControl.
using System.Windows.Controls.Primitives;   // RepeatButton.
using System.Windows.Media;
using System.Windows.Shapes;                // Rectangle.


namespace BBIM
{
    public class NumericUpDown : UserControl
    {
        public event EventHandler ValueChanged;

        private Canvas mainCanvas;
        private Rectangle mainBorder;
        private TextBox textBox;        
        //private RepeatButton upButton;
        //private RepeatButton downButton;
        private Button upButton;
        private Button downButton;
        private decimal _Increment;
        private decimal _Maximum;
        private decimal _Minimum;
        private decimal _Value;
        

        /// <summary>
        /// Initializes a new instance of the NumericUpDown Control.
        /// </summary>
        public NumericUpDown()
        {
            mainCanvas = new Canvas();
            mainBorder = new Rectangle();
            textBox = new TextBox();
            //upButton = new RepeatButton();
            //downButton = new RepeatButton();
            upButton = new Button();
            downButton = new Button();

            #region |--------------------- UI -------------------------|
            // 
            // textBox
            //
            textBox.BorderThickness = new Thickness(0);
            textBox.Margin = new Thickness(0);
            //textBox.Background = Brushes.White;
            textBox.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            textBox.FontSize = 12.0;
            textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            textBox.TextWrapping = TextWrapping.NoWrap;
            textBox.AllowDrop = false;
            textBox.MinLines = 1;
            textBox.MaxLines = 1;
            textBox.AcceptsReturn = false;
            textBox.TextAlignment = TextAlignment.Right;
            textBox.TextChanged += new TextChangedEventHandler(textBlock_TextChanged);
            //
            // upButton
            //
            upButton.Padding = new Thickness(0);
            upButton.Content = "";
            upButton.Focusable = false;
            upButton.Click += new RoutedEventHandler(upButton_Click);
            //
            // downButton
            //
            downButton.Padding = new Thickness(0);
            downButton.Content = "";
            downButton.Focusable = false;
            downButton.Click += new RoutedEventHandler(downButton_Click);
            //
            // mainBorder
            //
            mainBorder.StrokeThickness = 1;
            mainBorder.Stroke = Brushes.LightGray;
            mainBorder.Margin = new Thickness(0);
            mainBorder.VerticalAlignment = VerticalAlignment.Center;
            mainBorder.HorizontalAlignment = HorizontalAlignment.Stretch;
            //
            // mainCanvas
            //
            mainCanvas.Children.Add(mainBorder);
            mainCanvas.Children.Add(textBox);
            mainCanvas.Children.Add(upButton);
            mainCanvas.Children.Add(downButton);

            // this UserControl
            this.Width = 80;
            this.Height = 20;
            this.Content = mainCanvas;
            this.SizeChanged += new SizeChangedEventHandler(NumericUpDown_SizeChanged);

            #endregion
            
            // Inits
            _Increment = 1;
            _Maximum = 100;
            _Minimum = 0;
            _Value = 0;
            textBox.Text = _Value.ToString();
        }

        // Resize the control.
        private void NumericUpDown_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeMe();
        }


        // Resize the contents of the control (Width and height property).
        private void ResizeMe()
        {
            int iW = (int)this.Width;
            int iH = (int)this.Height;
            // Width of up/down buttons.
            int iBw = 15;

            mainCanvas.Width = iW;
            mainCanvas.Height = iH;

            mainBorder.Width = iW;
            mainBorder.Height = iH;
            Canvas.SetLeft(mainBorder, 0);
            Canvas.SetTop(mainBorder, 0);

            textBox.Width = iW - (iBw);
            textBox.Height = iH - 2;
            Canvas.SetLeft(textBox, 1);
            Canvas.SetTop(textBox, 1);

            // Vertical center of text.
            int pad = (iH - 18) / 2;
            textBox.Padding = new Thickness(0, 0, 0, 0);
            textBox.FontSize = 10;

            upButton.Width = iBw;
            upButton.Height = iH / 2 - 1;
            Canvas.SetLeft(upButton, iW - (iBw + 1));
            Canvas.SetTop(upButton, 1);

            downButton.Width = iBw;
            downButton.Height = iH / 2 - 1;
            Canvas.SetLeft(downButton, iW - (iBw + 1));
            Canvas.SetTop(downButton, iH - downButton.Height - 1);
        }


        // Event - Value changed.
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }


        // Property - Increment.
        // ------------------------------------------------------
        /// <summary>
        /// Gets or sets the value to increment or decrement the spin box
        /// when the up or down buttons are clicked.
        /// </summary>
        // ------------------------------------------------------
        public decimal Increment
        {
            get { return _Increment; }
            set { _Increment = value; }
        }


        // Property - Maximum.
        // ------------------------------------------------------
        /// <summary>
        /// Gets or sets the maximum value for the spin box.
        /// </summary>
        // ------------------------------------------------------
        public decimal Maximum
        {
            get { return _Maximum; }
            set { _Maximum = value; }
        }


        // Property - Minimum.  
        // ------------------------------------------------------
        /// <summary>
        /// Gets or sets the minimum allowed value for the spin box.
        /// </summary>
        // ------------------------------------------------------      
        public decimal Minimum
        {
            get { return _Minimum; }
            set { _Minimum = value; }
        }


        // OK 041011
        // Property - Value.
        // ------------------------------------------------------
        /// <summary>
        /// Gets or sets the value assigned to the spin box.
        /// </summary>
        // ------------------------------------------------------
        public decimal Value
        {
            get { return _Value; }
            set 
            { 
                decimal d = value;
                if (d > _Maximum) d = _Maximum;
                if (d < _Minimum) d = _Minimum;
                _Value = d;
                textBox.Text = d.ToString();
                OnValueChanged(EventArgs.Empty);
            }
        }


        // OK 041011
        // The user clicks the up-button.
        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            Value += _Increment;
        }


        // OK 041011
        // The user clicks the down-button.
        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            Value -= _Increment;
        }


        // OK 041011
        // The user changes the text directly in the control, 
        // or it is changed via property Value.
        private void textBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Value = Convert.ToDecimal(textBox.Text.Trim());
            }
            catch
            {
                // Prevent text, only allow numbers.
                Value = 0;
            }
        }


    }   // end class
}       // end namespace
