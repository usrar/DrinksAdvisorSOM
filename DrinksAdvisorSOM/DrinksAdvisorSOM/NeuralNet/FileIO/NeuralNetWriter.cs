using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DrinksAdvisorSOM.NeuralNet.FileIO
{
    class NeuralNetWriter
    {
        public void SaveNodes(Node[] nodes, string filename, int dimensions)
        {
            using (XmlWriter writer = XmlWriter.Create(filename))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("NeuralNet");
                writer.WriteStartElement("Meta");
                writer.WriteElementString("Dimensions", dimensions.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Data");


                StringBuilder sb = new StringBuilder();
                foreach (Node node in nodes)
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
