using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace NCircuitDesigner.Models
{
    public class NControl : INotifyPropertyChanged
    {
        private int _width;
        private int _height;
        private int _x;
        private int _y;

        private const int _baseScale = 75;
        private const int _scaleStep = 5;

        public NControl(Point loc, Point scale)
        {
            Width = (int)scale.X;
            Height = (int)scale.Y;
            X = (int)loc.X;
            Y = (int)loc.Y;
        }

        public int Width
        {
            get { return _width; }
            set {
                Debug.WriteLine($"W = {_width} | {value}");
                _width = value; 
                OnPropertyChanged(nameof(Width));
            }
        }

        public int Height
        {
            get { return (int)_height; }
            set {
                Debug.WriteLine($"H = {_height} | {value}");
                _height = value; 
                OnPropertyChanged(nameof(Height));
            }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; OnPropertyChanged(nameof(X)); }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; OnPropertyChanged(nameof(Y)); }
        }

        public void SetScale(int amount)
        {
            var dim = _baseScale + (amount * _scaleStep);
            Width = dim;
            Height = dim;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
        
    }
}
