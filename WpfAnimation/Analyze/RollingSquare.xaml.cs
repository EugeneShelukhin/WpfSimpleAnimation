using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfAnimation
{
    /// <summary>
    /// Interaction logic for PreloaderControl.xaml
    /// </summary>
    public partial class RollingSquare : UserControl
    {
        public static readonly DependencyProperty ColorProperty;


        public RollingSquare()
        {
            InitializeComponent();
            InitAnimation();
        }

        static RollingSquare()
        {
            ColorProperty = DependencyProperty.Register(
                    "Color",
                    typeof(Brush),
                    typeof(TextBlock),
                    new FrameworkPropertyMetadata(
                        null,
                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                        FrameworkPropertyMetadataOptions.AffectsRender));
        }

        private void InitAnimation()
        {
            var storyboard = new Storyboard
            {
                Duration = TimeSpan.FromSeconds(4),
                RepeatBehavior = RepeatBehavior.Forever
            };

            DoubleAnimation selfRotation = new DoubleAnimation();
            selfRotation.From = 45;
            selfRotation.By = 720;
            selfRotation.Duration = TimeSpan.FromSeconds(3);
            selfRotation.AccelerationRatio = 0.5;
            selfRotation.DecelerationRatio = 0.5;
            var rotateTransform = new RotateTransform(0, 10, 10);
            rect.RenderTransform = rotateTransform;

            Storyboard.SetTarget(selfRotation, rect);
            Storyboard.SetTargetProperty(selfRotation, new PropertyPath("(Rectangle.RenderTransform).(RotateTransform.Angle)"));

            DoubleAnimation xShift = new DoubleAnimation();
            xShift.From = Canvas.GetLeft(rect);
            xShift.By = 50;
            xShift.Duration = TimeSpan.FromSeconds(1.5);
            xShift.AutoReverse = true;
            xShift.AccelerationRatio = 0.5;
            xShift.DecelerationRatio = 0.5;
            Storyboard.SetTarget(xShift, rect);
            Storyboard.SetTargetProperty(xShift, new PropertyPath(Canvas.LeftProperty));

            storyboard.Children.Add(xShift);
            storyboard.Children.Add(selfRotation);
            rect.BeginStoryboard(storyboard);
        }

        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set
            {
                rect.Fill = value;
                SetValue(ColorProperty, value);
            }
        }
    }
}
