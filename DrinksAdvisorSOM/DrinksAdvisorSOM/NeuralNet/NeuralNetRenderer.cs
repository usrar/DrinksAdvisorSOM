using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrinksAdvisorSOM.NeuralNet
{
    class NeuralNetRenderer
    {
        private const float SCALE_FACTOR = 27;
        private Dictionary<int, Brush> drinksBrushesDictionary;
        private Random randomizer;

        public NeuralNetRenderer ()
	    {
            drinksBrushesDictionary = new Dictionary<int,Brush>();
            randomizer = new Random();
    	}


        public Image GetRender(Node[] neuralNet, int neuralMapWidth, int neuralMapHeight)
        {
            Bitmap render = new Bitmap((int)(neuralMapWidth * SCALE_FACTOR), (int)(neuralMapHeight * SCALE_FACTOR));

            using (Graphics g = Graphics.FromImage(render))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


                foreach (Node node in neuralNet)
                {
                    g.DrawString(node.DrinkID.ToString(), SystemFonts.MessageBoxFont, GetDrinkBrushById(node.DrinkID) , (float)(node.X * SCALE_FACTOR), (float)(node.Y * SCALE_FACTOR));
                }
            }

            return render;
        }


        private Brush GetDrinkBrushById(int id)
        {
            if (drinksBrushesDictionary.ContainsKey(id))
            {
                return drinksBrushesDictionary[id];
            }
            else
            {
                Brush brush = new SolidBrush(RandomizeColor());
                drinksBrushesDictionary.Add(id, brush);
                return brush;
            }
        }

        private Color RandomizeColor()
        {
            return Color.FromArgb(randomizer.Next(255), randomizer.Next(255), randomizer.Next(255));
        }

    }
}
