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
    public partial class SimilarDrinksCountInquiryForm : Form
    {
        private const int DEFAULT_SIMILAR_DRINKS_COUNT = 5;
        public int SimilarDrinksCount { get; private set; } 

        public SimilarDrinksCountInquiryForm()
        {
            InitializeComponent();
            tb_SimilarDrinksCount.Text = DEFAULT_SIMILAR_DRINKS_COUNT.ToString();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            int intVal;

            SimilarDrinksCount = int.TryParse(tb_SimilarDrinksCount.Text.Trim(), out intVal) ? intVal : DEFAULT_SIMILAR_DRINKS_COUNT;

            if (SimilarDrinksCount < 1)
                SimilarDrinksCount = 1;


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
