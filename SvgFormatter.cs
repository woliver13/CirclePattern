using System.Collections.Generic;
using System.Xml.Linq;

namespace CirclePattern
{
    public class SvgFormatter
    {
        public SvgFormatter()
        {
            ScaleFactor = 25.4;
        }
        public double ScaleFactor {get;set;}
        public void Write(string outputFileName, IEnumerable<Circle> circles)
        {
            XNamespace svgNs = "http://www.w3.org/2000/svg";
            var svgElement = new XElement(svgNs + "svg", new XElement(svgNs + "style", "circle {fill:none;stroke:black;stroke-width:2px;}"));
            foreach (var circle in circles)
            {
                var circleElement = new XElement(svgNs + "circle",
                new XAttribute("cx", circle.Center.X * ScaleFactor),
                new XAttribute("cy", circle.Center.Y * ScaleFactor),
                new XAttribute("r", (circle.Diameter / 2.0) * ScaleFactor));
                svgElement.Add(circleElement);
            }
            svgElement.Save(outputFileName);
        }
    }
}