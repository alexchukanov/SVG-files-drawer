using Svg.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Rectangle = Windows.UI.Xaml.Shapes.Rectangle;

namespace Svg
{
	public class MainViewModel : INotifyPropertyChanged
	{
        string svg = @"<svg xmlns=""http://www.w3.org/2000/svg"" width=""500"" height=""500"">
                          <circle cx=""250"" cy=""250"" r=""210"" fill=""#ffe0ffff"" stroke=""#ff3c2f2f"" stroke-width = ""8"" />
                          </svg >";
       
        string path = @"shapes.xml";

        public MainViewModel()
		{
            ParseSvg(svg);
            ShapeList = CreateShapeList();
        }

        public void ParseSvg(string svg)
        {
            XmlDocument xmlDoc = new XmlDocument();           
            xmlDoc.Load(path);

            var atrRoot = xmlDoc.DocumentElement.Attributes;

            WinWidth = int.Parse(atrRoot["width"].Value);
            WinHeigth = int.Parse(atrRoot["height"].Value);

            XmlNode currNode = xmlDoc.DocumentElement.FirstChild;

            ShapeInfo shapeInfo1 = new ShapeInfo();

            shapeInfo1.Name = currNode.Name;
            var attrCircle = currNode.Attributes;

            shapeInfo1.Left = int.Parse(attrCircle["cx"].Value);
            shapeInfo1.Top = int.Parse(attrCircle["cy"].Value);

            shapeInfo1.Fill = attrCircle["fill"].Value;
            shapeInfo1.Stroke = attrCircle["stroke"].Value;
            shapeInfo1.StrokeWidth = int.Parse(attrCircle["stroke-width"].Value);

            shapeInfo1.Width = int.Parse(attrCircle["r"].Value) * 2;
            shapeInfo1.Heigth = int.Parse(attrCircle["r"].Value) * 2;

            ShapeInfoList.Add(shapeInfo1);

            currNode = xmlDoc.DocumentElement.LastChild;

            ShapeInfo shapeInfo2 = new ShapeInfo();

            shapeInfo2.Name = currNode.Name;
            var attrRect = currNode.Attributes;

            shapeInfo2.Left = int.Parse(attrRect["x"].Value);
            shapeInfo2.Top = int.Parse(attrRect["y"].Value);

            shapeInfo2.Fill = attrRect["fill"].Value;
            shapeInfo2.Stroke = attrRect["stroke"].Value;
            shapeInfo2.StrokeWidth = int.Parse(attrRect["stroke-width"].Value);

            shapeInfo2.Width = int.Parse(attrRect["width"].Value) * 2;
            shapeInfo2.Heigth = int.Parse(attrRect["height"].Value) * 2;

            ShapeInfoList.Add(shapeInfo2);
        }

        public List<Shape> CreateShapeList()
        {
			foreach (var shapeInfo in ShapeInfoList)
			{
                Shape shape = null;

                switch (shapeInfo.Name)
                {
                    case "circle":
                        shape = CreateEllipse(shapeInfo);
                        break;

                    case "rect":
                        shape = CreateRect(shapeInfo);
                        break;
                    default:
                        return null;
                }

                ShapeList.Add(shape);
            }

            return ShapeList;
        }

        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return myBrush;
        }

        public Shape CreateEllipse(ShapeInfo shapeInfo)
        {
            var ellipse = new Ellipse();
            ellipse.Width = shapeInfo.Width;
            ellipse.Height = shapeInfo.Heigth;
            ellipse.Stroke = GetSolidColorBrush(shapeInfo.Stroke);
            ellipse.Fill = GetSolidColorBrush(shapeInfo.Fill);
            ellipse.StrokeThickness = shapeInfo.StrokeWidth;
           
            return ellipse;
        }

        public Shape CreateRect(ShapeInfo shapeInfo)
        {
            var rect = new Rectangle();
            rect.Width = shapeInfo.Width;
            rect.Height = shapeInfo.Heigth;
            rect.Stroke = GetSolidColorBrush(shapeInfo.Stroke);
            rect.Fill = GetSolidColorBrush(shapeInfo.Fill);
            rect.StrokeThickness = shapeInfo.StrokeWidth;

            return rect;
        }

        public string ShapeName { get; set; }
        public int WinWidth { get; set; }
        public int WinHeigth { get; set; }
        public int Width { get; set; }
        public int Heigth { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public string Fill { get; set; }
        public string Stroke { get; set; }
        public int StrokeWidth { get; set; }
        public List<ShapeInfo> ShapeInfoList { get; set; } = new List<ShapeInfo>();
        public List<Shape> ShapeList { get; set; } = new List<Shape>();


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
