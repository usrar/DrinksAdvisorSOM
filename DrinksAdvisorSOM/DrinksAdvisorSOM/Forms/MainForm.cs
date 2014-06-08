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
        private IDrinksSelfOrganizingMapController drinksMap;
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
                    drinksMap = new DrinksSelfOrganizingMapController();

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    drinksMap.LearnNeuralNet(parametersForm.EpochsCount, parametersForm.InitialLearningRate, parametersForm.DistanceBetweenNeurons, parametersForm.NeuralMapWidth, parametersForm.NeuralMapHeight);
                    sw.Stop();
                    Console.WriteLine("Elapsed time: " + sw.ElapsedMilliseconds / 1000.0f);
                    ts_lbl_Status.Text = "Elapsed time: " + sw.ElapsedMilliseconds / 1000.0f;

                    RefreshDrinksTable(drinksMap.GetDrinksContainer());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveNeuralNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureDrinksMapIsNotNull();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".xml";
                saveFileDialog.Filter = "Neural Net Files (*.xml)|*.xml";
                DialogResult saveFileDialogResult = saveFileDialog.ShowDialog();
                if (saveFileDialogResult == DialogResult.OK)
                {
                    drinksMap.SaveNeuralNet(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    drinksMap = new DrinksSelfOrganizingMapController();
                    drinksMap.LoadNeuralNet(filename);
                    RefreshDrinksTable(drinksMap.GetDrinksContainer());

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void webBrowserDrinks_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ts_lbl_Status.Text = e.Url.ToString() + " loaded";
        }

        private void btn_FindSimilar_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureDrinksMapIsNotNull();
                IEnumerable<Drink> similarDrinks = drinksMap.FindSimilarDrinks(GetSelectedDrinkID(), 5);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void EnsureDrinksMapIsNotNull()
        {
            if (drinksMap == null)
                throw new Exception("Try to load neural net first.");
        }

        private int GetSelectedDrinkID()
        {
            EnsureDrinksMapIsNotNull();
            return (int) (dgv_Drinks.SelectedRows[0].Cells["DrinkID"].Value);
        }

        private void EnsureDrinkIsSelected()
        {
            if (dgv_Drinks.SelectedRows.Count == 0)
                throw new Exception("Try to indicate a drink first.");
        }

        private void getRenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureDrinksMapIsNotNull();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".png";
                saveFileDialog.Filter = "Images (*.png)|*.png";
                DialogResult saveFileDialogResult = saveFileDialog.ShowDialog();
                if (saveFileDialogResult == DialogResult.OK)
                {
                    drinksMap.GetRender().Save(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
