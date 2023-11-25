using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace Svg.Model
{
	public class ShapeInfo
	{
        public string Name { get; set; }
        public int Width { get; set; }
        public int Heigth { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public string Fill { get; set; }
        public string Stroke { get; set; }
        public int StrokeWidth { get; set; }
        public Shape Shape { get; set; }
    }
}
