using System;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace AvaloniaSuperShape
{
    // http://paulbourke.net/geometry/supershape/
    public class SuperShape : Shape
    {
        static SuperShape()
        {
            AffectsGeometry<SuperShape>(
                BoundsProperty,
                StrokeThicknessProperty,
                MProperty,
                AProperty,
                BProperty,
                N1Property,
                N2Property,
                N3Property,
                AspectRatioProperty);
        }

        public SuperShape()
        {
            ClipToBounds = true;
        }

        public static readonly StyledProperty<double> MProperty =
            AvaloniaProperty.Register<SuperShape, double>("M", 0);

        public double M
        {
            get => GetValue(MProperty);
            set => SetValue(MProperty, value);
        }

        public static readonly StyledProperty<double> AProperty =
            AvaloniaProperty.Register<SuperShape, double>("A", 1);

        public double A
        {
            get => GetValue(AProperty);
            set => SetValue(AProperty, value);
        }

        public static readonly StyledProperty<double> BProperty =
            AvaloniaProperty.Register<SuperShape, double>("B", 1);

        public double B
        {
            get => GetValue(BProperty);
            set => SetValue(BProperty, value);
        }

        public static readonly StyledProperty<double> N1Property =
            AvaloniaProperty.Register<SuperShape, double>("N1", 0.5);

        public double N1
        {
            get => GetValue(N1Property);
            set => SetValue(N1Property, value);
        }

        public static readonly StyledProperty<double> N2Property =
            AvaloniaProperty.Register<SuperShape, double>("N2", 0.5);
        
        public double N2
        {
            get => GetValue(N2Property);
            set => SetValue(N2Property, value);
        }

        public static readonly StyledProperty<double> N3Property =
            AvaloniaProperty.Register<SuperShape, double>("N3", 0.5);

        public double N3
        {
            get => GetValue(N3Property);
            set => SetValue(N3Property, value);
        }

        public static readonly StyledProperty<double> AspectRatioProperty =
            AvaloniaProperty.Register<SuperShape, double>("AspectRatio", 1);

        public double AspectRatio
        {
            get => GetValue(AspectRatioProperty);
            set => SetValue(AspectRatioProperty, value);
        }

        protected override Geometry? CreateDefiningGeometry()
        {
            StreamGeometry geometry = new StreamGeometry();

            var totalPoints = 500;
            var increment = 2 * Math.PI / totalPoints;

            using (StreamGeometryContext context = geometry.Open())
            {
                var radiusX = (Bounds.Width * AspectRatio - StrokeThickness) / 2;
                var radiusY = (Bounds.Height * AspectRatio - StrokeThickness) / 2;

                var center = new Point(Bounds.Width / 2, Bounds.Height / 2);

                for (double angle = 0; angle <= 2 * Math.PI; angle += increment)
                {
                    var r = GetSupershapePoint(angle);

                    var x = center.X + radiusX * r * Math.Cos(angle);
                    var y = center.Y + radiusY * r * Math.Sin(angle);

                    if (angle == 0)
                        context.BeginFigure(new Point(x, y), true);

                    context.LineTo(new Point(x, y));
                }

                context.EndFigure(true);
            }

            return geometry;
        }

        double GetSupershapePoint(double theta)
        {
            var part1 = (1 / A) * Math.Cos(theta * M / 4);
            part1 = Math.Abs(part1);
            part1 = Math.Pow(part1, N2);

            var part2 = (1 / B) * Math.Sin(theta * M / 4);
            part2 = Math.Abs(part2);
            part2 = Math.Pow(part2, N3);

            var part3 = Math.Pow(part1 + part2, 1 / N1);

            if (part3 == 0) {
                return 0;
            }

            return (1 / part3);
        }
    }
}