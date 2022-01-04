using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfAnimation
{
    /// <summary>
    /// Interaction logic for Preloader2.xaml
    /// </summary>
    public partial class SquearsContainer : UserControl
    {
        public SquearsContainer()
        {
            InitializeComponent();
            InitAnimation();
        }

        private void InitAnimation()
        {
            var storyboard = new Storyboard
            {
                Duration = TimeSpan.FromSeconds(4),
                //RepeatBehavior = RepeatBehavior.Forever,
            };

            DoubleAnimation selfRotation = new DoubleAnimation();
            selfRotation.By = 90;
            selfRotation.Duration = TimeSpan.FromSeconds(3);
            selfRotation.AccelerationRatio = 0.5;
            selfRotation.DecelerationRatio = 0.5;
            var rotateTransform = new RotateTransform(0, 10, 10);
            canv.RenderTransform = rotateTransform;

            Storyboard.SetTarget(selfRotation, canv);
            Storyboard.SetTargetProperty(selfRotation, new PropertyPath("(Rectangle.RenderTransform).(RotateTransform.Angle)"));

            storyboard.Children.Add(selfRotation);
            storyboard.Completed += (o, e) => { canv.BeginStoryboard(storyboard); };
            canv.BeginStoryboard(storyboard);
        }
    }
}
