using System.Collections.Generic;

namespace CirclePattern
{
    public class PatternGenerator
    {
        private readonly SvgFormatter _formatter = new SvgFormatter();
        public void Generate(string outputFileName, int horizontalOffsetRule, int verticalOffsetRule, double maxDiameter, double stepSize, double minDiameter)
        {
            var centercircles = GenerateCenterCircles(maxDiameter, stepSize, minDiameter);
            var topcircles = GenerateTopCircles(maxDiameter, stepSize, minDiameter);
            var bottomcircles = GenerateBottomCircles(maxDiameter, stepSize, minDiameter);
            topcircles = Transform(topcircles, 0, 3);
            bottomcircles = Transform(bottomcircles, 0, -3);
            var circles = new List<Circle>();
            circles.AddRange(topcircles);
            circles.AddRange(centercircles);
            circles.AddRange(bottomcircles);
            _formatter.Write(outputFileName, circles);
        }

        private static IEnumerable<Circle> GenerateCenterCircles(double maxDiameter, double stepSize, double minDiameter)
        {
            double diameter = maxDiameter;
            double radius = maxDiameter / 2.0;
            var circles = new List<Circle>();
            Point oldCenter = new Point(radius, radius);
            circles.Add(new Circle { Center = oldCenter, Diameter = diameter });
            while (diameter >= minDiameter)
            {
                var newCenter = new Point(oldCenter.X + diameter, maxDiameter / 2.0);
                diameter -= stepSize;
                radius = diameter / 2.0;
                circles.Add(new Circle { Center = newCenter, Diameter = diameter });
                oldCenter = newCenter;
            }
            return circles;
        }

        private static IEnumerable<Circle> GenerateBottomCircles(double maxDiameter, double stepSize, double minDiameter)
        {
            double diameter = maxDiameter;
            double radius = diameter / 2.0;
            var circles = new List<Circle>();
            Point oldCenter = new Point(radius, radius);
            circles.Add(new Circle { Center = oldCenter, Diameter = diameter });
            while (diameter >= minDiameter)
            {
                var newX = oldCenter.X + diameter;
                var newDiameter = diameter - stepSize;
                var newY = newDiameter / 2.0;

                var newCenter = new Point(newX, newY);
                circles.Add(new Circle { Center = newCenter, Diameter = newDiameter });
                diameter = newDiameter;
                oldCenter = newCenter;
            }
            return circles;
        }

        private static IEnumerable<Circle> GenerateTopCircles(double maxDiameter, double stepSize, double minDiameter)
        {
            double diameter = maxDiameter;
            double radius = maxDiameter / 2.0;
            List<Circle> circles = new List<Circle>();
            Point oldCenter = new Point(radius, radius);
            circles.Add(new Circle { Center = oldCenter, Diameter = diameter });
            while (diameter >= minDiameter)
            {
                var newX = oldCenter.X + diameter;
                var newDiameter = diameter - stepSize;
                var newY = maxDiameter - newDiameter / 2.0;

                var newCenter = new Point(newX, newY);
                circles.Add(new Circle { Center = newCenter, Diameter = newDiameter });
                diameter = newDiameter;
                oldCenter = newCenter;
            }
            return circles;
        }

        private IEnumerable<Circle> Transform(IEnumerable<Circle> circles, double xoffset, double yoffset)
        {
            List<Circle> result = new List<Circle>();
            foreach (var circle in circles)
            {
                result.Add(new Circle { Center = new Point(circle.Center.X + xoffset, circle.Center.Y + yoffset), Diameter = circle.Diameter });
            }
            return result;
        }
    }
}