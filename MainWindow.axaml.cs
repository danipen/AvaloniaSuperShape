using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

using System;

namespace AvaloniaSuperShape
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            NumericUpDown nm = this.Find<NumericUpDown>("mMUpDown");
            NumericUpDown na = this.Find<NumericUpDown>("mAUpDown");
            NumericUpDown nb = this.Find<NumericUpDown>("mBUpDown");
            NumericUpDown nn1 = this.Find<NumericUpDown>("mN1UpDown");
            NumericUpDown nn2 = this.Find<NumericUpDown>("mN2UpDown");
            NumericUpDown nn3 = this.Find<NumericUpDown>("mN3UpDown");

            DockPanel canvas = this.FindControl<DockPanel>("mCanvas");
            mSuperShape = new SuperShape();
            mSuperShape.Stroke = Brushes.Black;
            mSuperShape.StrokeThickness = 4;
            mSuperShape.Margin = new Thickness(10);

            Slider aspectRatioSlider = this.Find<Slider>("mAspectRatioSlider");
            aspectRatioSlider[!Slider.ValueProperty] = mSuperShape[!SuperShape.AspectRatioProperty];

            nm.Value = mSuperShape.M;
            na.Value = mSuperShape.A;
            nb.Value = mSuperShape.B;
            nn1.Value = mSuperShape.N1;
            nn2.Value = mSuperShape.N2;
            nn3.Value = mSuperShape.N3;

            nm.ValueChanged += (sender, e) => mSuperShape.M = nm.Value;
            na.ValueChanged += (sender, e) => mSuperShape.A = na.Value;
            nb.ValueChanged += (sender, e) => mSuperShape.B = nb.Value;
            nn1.ValueChanged += (sender, e) => mSuperShape.N1 = nn1.Value;
            nn2.ValueChanged += (sender, e) => mSuperShape.N2 = nn2.Value;
            nn3.ValueChanged += (sender, e) => mSuperShape.N3 = nn3.Value;

            canvas.Children.Add(mSuperShape);

            mAnimationsToggleSwitch = this.Find<ToggleSwitch>("mAnimationsToggleSwitch");
            mAnimationsToggleSwitch.Checked += AnimationsToggleSwitch_Checked;
            mAnimationsToggleSwitch.Unchecked += AnimationsToggleSwitch_Unchecked;
        }

        void AnimationsToggleSwitch_Unchecked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (mSuperShape.Transitions != null)
                mSuperShape.Transitions.Clear();
        }

        void AnimationsToggleSwitch_Checked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            DoubleTransition mTransition = new DoubleTransition();
            mTransition.Property = SuperShape.MProperty;
            mTransition.Duration = TimeSpan.FromMilliseconds(1000);

            DoubleTransition aTransition = new DoubleTransition();
            aTransition.Property = SuperShape.AProperty;
            aTransition.Duration = TimeSpan.FromMilliseconds(400);

            DoubleTransition bTransition = new DoubleTransition();
            bTransition.Property = SuperShape.BProperty;
            bTransition.Duration = TimeSpan.FromMilliseconds(400);

            DoubleTransition n1Transition = new DoubleTransition();
            n1Transition.Property = SuperShape.N1Property;
            n1Transition.Duration = TimeSpan.FromMilliseconds(400);

            DoubleTransition n2Transition = new DoubleTransition();
            n2Transition.Property = SuperShape.N2Property;
            n2Transition.Duration = TimeSpan.FromMilliseconds(400);

            DoubleTransition n3Transition = new DoubleTransition();
            n3Transition.Property = SuperShape.N3Property;
            n3Transition.Duration = TimeSpan.FromMilliseconds(400);

            mSuperShape.Transitions = new Transitions();
            mSuperShape.Transitions.Add(mTransition);
            mSuperShape.Transitions.Add(aTransition);
            mSuperShape.Transitions.Add(bTransition);
            mSuperShape.Transitions.Add(n1Transition);
            mSuperShape.Transitions.Add(n2Transition);
            mSuperShape.Transitions.Add(n3Transition);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        ToggleSwitch mAnimationsToggleSwitch;
        SuperShape mSuperShape;
    }
}