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
using System.Net;
using System.IO;
using DrinksAdvisorSOM.Extensions;

namespace DrinksAdvisorSOM.Forms
{
    public partial class MainForm : Form
    {
        private IDrinksSelfOrganizingMapController drinksMapController;
        private bool isFilterEnabled;
        private string inputFilter;
        private string columnName;
        private Dictionary<int, Image> drinksImagesDictionary;
        private readonly Size SIMILAR_DRINK_IMAGE_SIZE = new Size(160, 160);

        public MainForm()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            isFilterEnabled = false;
            columnName = "DrinkName";
            dgv_Drinks.SelectionChanged += new EventHandler(dgv_Drinks_SelectionChanged);
            dgv_Drinks.CellDoubleClick += new DataGridViewCellEventHandler(dgv_Drinks_CellDoubleClick);
            drinksImagesDictionary = new Dictionary<int, Image>();

            webBrowserDrinks.ScriptErrorsSuppressed = true;
            InitializeListViewSimilarImages();
        }

 

        private void learnNeuralNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NeuralNetLearningParametersForm parametersForm = new NeuralNetLearningParametersForm();
                DialogResult parametersFormResult = parametersForm.ShowDialog();
                if( parametersFormResult == System.Windows.Forms.DialogResult.OK)
                {
                    drinksMapController = new DrinksSelfOrganizingMapController();

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    drinksMapController.LearnNeuralNet(parametersForm.EpochsCount, parametersForm.InitialLearningRate, parametersForm.DistanceBetweenNeurons,
                        parametersForm.NeuralMapWidth, parametersForm.NeuralMapHeight, parametersForm.MinNeuronPotential, parametersForm.MaxNeuronRestTime);
                    sw.Stop();
                    Console.WriteLine("Elapsed time: " + sw.ElapsedMilliseconds / 1000.0f);
                    ts_lbl_Status.Text = "Elapsed time: " + sw.ElapsedMilliseconds / 1000.0f;

                    RefreshDrinksTable(drinksMapController.GetDrinksContainer());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeListViewSimilarImages()
        {
            lv_SimilarDrinks.LargeImageList = new ImageList();
            lv_SimilarDrinks.LargeImageList.ImageSize = SIMILAR_DRINK_IMAGE_SIZE;
            lv_SimilarDrinks.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
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
                    drinksMapController.SaveNeuralNet(saveFileDialog.FileName);
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
                    drinksMapController = new DrinksSelfOrganizingMapController();
                    drinksMapController.LoadNeuralNet(filename);
                    RefreshDrinksTable(drinksMapController.GetDrinksContainer());

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
                SimilarDrinksCountInquiryForm parametersForm = new SimilarDrinksCountInquiryForm();
                DialogResult parametersFormDialogResult = parametersForm.ShowDialog();

                if (parametersFormDialogResult == System.Windows.Forms.DialogResult.OK)
                {  
                    int similarDrinksCount = parametersForm.SimilarDrinksCount;

                    IEnumerable<Drink> similarDrinksCollection = drinksMapController.FindSimilarDrinks(GetSelectedDrinkID(), similarDrinksCount);
                    RefreshListViewSimilarDrinks(similarDrinksCollection.ToArray());
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshListViewSimilarDrinks(Drink[] similarDrinksArray)
        {
            lv_SimilarDrinks.BeginUpdate();

            lv_SimilarDrinks.Items.Clear();
            lv_SimilarDrinks.LargeImageList.Images.Clear();

            for (int i = 0; i < similarDrinksArray.Length; i++)
            {
                lv_SimilarDrinks.LargeImageList.Images.Add(GetDrinkImage(similarDrinksArray[i]).ResizeImage(SIMILAR_DRINK_IMAGE_SIZE));
                ListViewItem lvi = new ListViewItem(similarDrinksArray[i].Name);
                lvi.Tag = similarDrinksArray[i].Url;
                lvi.ImageIndex = i;

                lv_SimilarDrinks.Items.Add(lvi);
            }

            lv_SimilarDrinks.EndUpdate();
        }


        private void EnsureDrinksMapIsNotNull()
        {
            if (drinksMapController == null)
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
                    drinksMapController.GetRender().Save(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vectorQuantizationErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureDrinksMapIsNotNull();

                Tuple<double, double> vectorQuantizationError = drinksMapController.GetVectorQuantizationError();

                MessageBox.Show("Error = " + vectorQuantizationError.Item1 + "\n" +
                                "Standard deviation = " + vectorQuantizationError.Item2,
                                "Vector Quantization Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private Image GetDrinkImage(Drink drink)
        {
            if (drinksImagesDictionary.ContainsKey(drink.ID))
            {
                return drinksImagesDictionary[drink.ID];
            }
            else
            {
                Image drinkImage = GetImageFromUrl(drink.ImageUrl);
                drinksImagesDictionary.Add(drink.ID, drinkImage);
                return drinkImage;
            }
        }


        private Image GetImageFromUrl(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);

            using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpWebReponse.GetResponseStream())
                {
                    return Image.FromStream(stream);
                }
            }
        }

        private void lv_SimilarDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lv_SimilarDrinks.SelectedItems.Count > 0)
                {
                    ListViewItem lvi = lv_SimilarDrinks.SelectedItems[0];
                    string drinkUrl = lvi.Tag.ToString();
                    webBrowserDrinks.Navigate(drinkUrl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
