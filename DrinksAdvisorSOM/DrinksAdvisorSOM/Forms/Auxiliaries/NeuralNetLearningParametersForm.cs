using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrinksAdvisorSOM.Forms.Auxiliaries
{
    public partial class NeuralNetLearningParametersForm : Form
    {
        private const int DEFAULT_EPOCHS_COUNT = 14200,
                          DEFAULT_NEURAL_MAP_WIDTH = 142,
                          DEFAULT_NEURAL_MAP_HEIGHT = 100;

        private const double DEFAULT_INITIAL_LEARNING_RATE = 0.10;
        private const float DEFAULT_DISTANCE_BETWEEN_NEURONS = 1;


        public double InitialLearningRate{ get; private set; }
        public float DistanceBetweenNeurons { get; private set; }
        public int EpochsCount { get; private set; }
        public int NeuralMapWidth { get; private set; }
        public int NeuralMapHeight { get; private set; }


        public NeuralNetLearningParametersForm()
        {
            InitializeComponent();
        }

        private void NeuralNetLearningParametersForm_Load(object sender, EventArgs e)
        {
            tb_EpochsCount.Text = DEFAULT_EPOCHS_COUNT.ToString();
            tb_InitialLearningRate.Text = DEFAULT_INITIAL_LEARNING_RATE.ToString();
            tb_DistanceBetweenNeurons.Text = DEFAULT_DISTANCE_BETWEEN_NEURONS.ToString();
            tb_NeuralMapWidth.Text = DEFAULT_NEURAL_MAP_WIDTH.ToString();
            tb_NeuralMapHeight.Text = DEFAULT_NEURAL_MAP_HEIGHT.ToString();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int int_val;
            double d_val;
            float f_val;

            EpochsCount = int.TryParse(tb_EpochsCount.Text.Trim(), out int_val) ? int_val : DEFAULT_EPOCHS_COUNT;
            InitialLearningRate = double.TryParse(tb_InitialLearningRate.Text.Trim(), out d_val) ? d_val : DEFAULT_INITIAL_LEARNING_RATE;
            DistanceBetweenNeurons = float.TryParse(tb_DistanceBetweenNeurons.Text.Trim(), out f_val) ? f_val : DEFAULT_DISTANCE_BETWEEN_NEURONS;
            NeuralMapWidth = int.TryParse(tb_NeuralMapWidth.Text.Trim(), out int_val) ? int_val : DEFAULT_NEURAL_MAP_WIDTH;
            NeuralMapHeight = int.TryParse(tb_NeuralMapHeight.Text.Trim(), out int_val) ? int_val : DEFAULT_NEURAL_MAP_HEIGHT;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
    }
}
