using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DrinksAdvisorSOM.NeuralNet.Structure;

namespace DrinksAdvisorSOM.NeuralNet.FileIO
{
    class NeuralNetWriter
    {
        public void SaveNeuralNet(DrinksSelfOrganizingMap drinksSelfOrganizingMap, string filename)
        {
            using (XmlWriter writer = XmlWriter.Create(filename))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("NeuralNet");
                writer.WriteStartElement("Meta");
                writer.WriteElementString("Width", drinksSelfOrganizingMap.NeuralMapWidth.ToString());
                writer.WriteElementString("Height", drinksSelfOrganizingMap.NeuralMapHeight.ToString());
                writer.WriteElementString("DistanceBetweenNeurons", drinksSelfOrganizingMap.DistanceBetweenNeurons.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Data");


                StringBuilder sb = new StringBuilder();
                foreach (Node node in drinksSelfOrganizingMap.NeuralNet)
                {
                    writer.WriteStartElement("Node");
                    writer.WriteElementString("DrinkID", node.DrinkID.ToString());

                   
                    writer.WriteElementString("X", node.X.ToString());
                    writer.WriteElementString("Y", node.Y.ToString());

                    sb.Clear();
                    for (int i = 0; i < node.Weights.Length - 1; i++)
                    {
                        sb.Append(node.Weights[i]);
                        sb.Append(";");
                    }
                    sb.Append(node.Weights[node.Weights.Length - 1]);
                    writer.WriteElementString("Weights", sb.ToString());


                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

        }
    }
}
