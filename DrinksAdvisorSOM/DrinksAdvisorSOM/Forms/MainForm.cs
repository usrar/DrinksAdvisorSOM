using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DrinksAdvisorSOM.Forms.Auxiliaries;
using DrinksAdvisorSOM.NeuralNet;
using DrinksAdvisorSOM.Models;

namespace DrinksAdvisorSOM.Forms
{
    public partial class MainForm : Form
    {
        private INeuralNet drinksMap;
        private bool isFilterEnabled;
        private string inputFilter;
        private string columnName;

        public MainForm()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            isFilterEnabled = false;
            columnName = "DrinkName";
            dgv_Drinks.SelectionChanged += new EventHandler(dgv_Drinks_SelectionChanged);
            dgv_Drinks.CellDoubleClick += new DataGridViewCellEventHandler(dgv_Drinks_CellDoubleClick);

            webBrowserDrinks.ScriptErrorsSuppressed = true;
        }

 

        private void learnNeuralNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NeuralNetLearningParametersForm parametersForm = new NeuralNetLearningParametersForm();
                DialogResult parametersFormResult = parametersForm.ShowDialog();
                if( parametersFormResult == System.Windows.Forms.DialogResult.OK)
                {
                    drinksMap = new DrinksSelfOrganizingMap();

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    drinksMap.LearnNeuralNet(parametersForm.EpochsCount, parametersForm.InitialLearningRate, parametersForm.DistanceBetweenNeurons, parametersForm.NeuralMapWidth, parametersForm.NeuralMapHeight);
                    sw.Stop();
                    Console.WriteLine("Elapsed time: " + sw.ElapsedMilliseconds / 1000.0f);
                    ts_lbl_Status.Text = "Elapsed time: " + sw.ElapsedMilliseconds / 1000.0f;

                    drinksMap.GetRender().Save(@"D:\neuralnet.png", System.Drawing.Imaging.ImageFormat.Png);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveNeuralNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".xml";
            saveFileDialog.Filter = "Neural Net Files (*.xml)|*.xml";
            DialogResult saveFileDialogResult = saveFileDialog.ShowDialog();
            if (saveFileDialogResult == DialogResult.OK)
            {
                drinksMap.SaveNeuralNet(saveFileDialog.FileName);
            }

        }

        private void openNeuralNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult openFileDialogResult = openFileDialog.ShowDialog();
            openFileDialog.Filter = "Neural Net Files (*.xml)|*.xml";
            if (openFileDialogResult == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                try
                {
                    drinksMap = new DrinksSelfOrganizingMap();
                    drinksMap.LoadNeuralNet(filename);
                    RefreshDrinksTable(drinksMap.GetDrinksContainer());

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void RefreshDrinksTable(DrinksContainer drinksContainer)
        {
            dgv_Drinks.ClearSelection();

            foreach (Drink drink in drinksContainer.DrinksDictionary.Values.OrderBy(d => d.Name))
            {
                dgv_Drinks.Rows.Add(
                    drink.ID,
                    drink.Name,
                    drink.Url
                    );

                if (isFilterEnabled) SetFilter();
            }

            
        }

        private void tb_FilterInput_KeyDown(object sender, KeyEventArgs ea)
        {
            if (ea.KeyCode == Keys.Enter)
            {
                inputFilter = tb_FilterInput.Text.Trim();

                isFilterEnabled = true;
                SetFilter();
            }
        }

        private void SetFilter()
        {
            foreach (DataGridViewRow row in dgv_Drinks.Rows)
            {
                if (row.Cells[columnName].Value.ToString().ToUpper().Contains(inputFilter.ToUpper()))
                    row.Visible = true;
                else
                    row.Visible = false;
            }
        }

        private void dgv_Drinks_SelectionChanged(object sender, EventArgs ea)
        {

        }

        private void dgv_Drinks_CellDoubleClick(object sender, DataGridViewCellEventArgs ea)
        {
            string drinkUrl = dgv_Drinks.Rows[ea.RowIndex].Cells["DrinkUrl"].Value.ToString();
            webBrowserDrinks.Navigate(drinkUrl);
            //System.Diagnostics.Process.Start(drinkUrl);
        }

        private void webBrowserDrinks_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            ts_lbl_Status.Text = "Navigating to: " + e.Url.ToString();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ts_lbl_Status.Text = e.Url.ToString() + " loaded";
        }

        


    }
}
