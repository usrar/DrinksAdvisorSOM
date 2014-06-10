namespace DrinksAdvisorSOM.Forms.Auxiliaries
{
    partial class SimilarDrinksCountInquiryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlp_Parameters = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_SimilarDrinksCount = new System.Windows.Forms.Label();
            this.tb_SimilarDrinksCount = new System.Windows.Forms.TextBox();
            this.tlp_FromActions = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.tlp_Parameters.SuspendLayout();
            this.tlp_FromActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Parameters
            // 
            this.tlp_Parameters.ColumnCount = 2;
            this.tlp_Parameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.34615F));
            this.tlp_Parameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.65385F));
            this.tlp_Parameters.Controls.Add(this.lbl_SimilarDrinksCount, 0, 0);
            this.tlp_Parameters.Controls.Add(this.tb_SimilarDrinksCount, 1, 0);
            this.tlp_Parameters.Location = new System.Drawing.Point(12, 12);
            this.tlp_Parameters.Name = "tlp_Parameters";
            this.tlp_Parameters.RowCount = 1;
            this.tlp_Parameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Parameters.Size = new System.Drawing.Size(328, 58);
            this.tlp_Parameters.TabIndex = 0;
            // 
            // lbl_SimilarDrinksCount
            // 
            this.lbl_SimilarDrinksCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_SimilarDrinksCount.AutoSize = true;
            this.lbl_SimilarDrinksCount.Location = new System.Drawing.Point(31, 22);
            this.lbl_SimilarDrinksCount.Name = "lbl_SimilarDrinksCount";
            this.lbl_SimilarDrinksCount.Size = new System.Drawing.Size(101, 13);
            this.lbl_SimilarDrinksCount.TabIndex = 0;
            this.lbl_SimilarDrinksCount.Text = "Similar drinks count:";
            // 
            // tb_SimilarDrinksCount
            // 
            this.tb_SimilarDrinksCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_SimilarDrinksCount.Location = new System.Drawing.Point(138, 19);
            this.tb_SimilarDrinksCount.Name = "tb_SimilarDrinksCount";
            this.tb_SimilarDrinksCount.Size = new System.Drawing.Size(187, 20);
            this.tb_SimilarDrinksCount.TabIndex = 1;
            // 
            // tlp_FromActions
            // 
            this.tlp_FromActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp_FromActions.ColumnCount = 2;
            this.tlp_FromActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_FromActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_FromActions.Controls.Add(this.btn_Cancel, 1, 0);
            this.tlp_FromActions.Controls.Add(this.btn_OK, 0, 0);
            this.tlp_FromActions.Location = new System.Drawing.Point(165, 92);
            this.tlp_FromActions.Name = "tlp_FromActions";
            this.tlp_FromActions.RowCount = 1;
            this.tlp_FromActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_FromActions.Size = new System.Drawing.Size(175, 31);
            this.tlp_FromActions.TabIndex = 2;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(90, 3);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(82, 25);
            this.btn_Cancel.TabIndex = 110;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.Location = new System.Drawing.Point(3, 3);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(81, 25);
            this.btn_OK.TabIndex = 100;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // SimilarDrinksCountInquiryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 135);
            this.Controls.Add(this.tlp_FromActions);
            this.Controls.Add(this.tlp_Parameters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SimilarDrinksCountInquiryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please enter similar drinks count";
            this.tlp_Parameters.ResumeLayout(false);
            this.tlp_Parameters.PerformLayout();
            this.tlp_FromActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Parameters;
        private System.Windows.Forms.TableLayoutPanel tlp_FromActions;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label lbl_SimilarDrinksCount;
        private System.Windows.Forms.TextBox tb_SimilarDrinksCount;
    }
}