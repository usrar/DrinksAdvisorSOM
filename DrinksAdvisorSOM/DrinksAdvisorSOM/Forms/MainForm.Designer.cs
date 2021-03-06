﻿namespace DrinksAdvisorSOM.Forms
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.learnNeuralNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveNeuralNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNeuralNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getRenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vectorQuantizationErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ts_lbl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgv_Drinks = new System.Windows.Forms.DataGridView();
            this.DrinkID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrinkName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrinkUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_FilterInput = new System.Windows.Forms.TextBox();
            this.lbl_FilterInput = new System.Windows.Forms.Label();
            this.webBrowserDrinks = new System.Windows.Forms.WebBrowser();
            this.btn_FindSimilar = new System.Windows.Forms.Button();
            this.gb_SimilarDrinks = new System.Windows.Forms.GroupBox();
            this.lv_SimilarDrinks = new System.Windows.Forms.ListView();
            this.gb_WebBrowser = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Drinks)).BeginInit();
            this.gb_SimilarDrinks.SuspendLayout();
            this.gb_WebBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imagingToolStripMenuItem,
            this.statisticsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.learnNeuralNetToolStripMenuItem,
            this.saveNeuralNetToolStripMenuItem,
            this.openNeuralNetToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // learnNeuralNetToolStripMenuItem
            // 
            this.learnNeuralNetToolStripMenuItem.Name = "learnNeuralNetToolStripMenuItem";
            this.learnNeuralNetToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.learnNeuralNetToolStripMenuItem.Text = "Learn neural net";
            this.learnNeuralNetToolStripMenuItem.Click += new System.EventHandler(this.learnNeuralNetToolStripMenuItem_Click);
            // 
            // saveNeuralNetToolStripMenuItem
            // 
            this.saveNeuralNetToolStripMenuItem.Name = "saveNeuralNetToolStripMenuItem";
            this.saveNeuralNetToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.saveNeuralNetToolStripMenuItem.Text = "Save neural net";
            this.saveNeuralNetToolStripMenuItem.Click += new System.EventHandler(this.saveNeuralNetToolStripMenuItem_Click);
            // 
            // openNeuralNetToolStripMenuItem
            // 
            this.openNeuralNetToolStripMenuItem.Name = "openNeuralNetToolStripMenuItem";
            this.openNeuralNetToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.openNeuralNetToolStripMenuItem.Text = "Open neural net";
            this.openNeuralNetToolStripMenuItem.Click += new System.EventHandler(this.openNeuralNetToolStripMenuItem_Click);
            // 
            // imagingToolStripMenuItem
            // 
            this.imagingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getRenderToolStripMenuItem});
            this.imagingToolStripMenuItem.Name = "imagingToolStripMenuItem";
            this.imagingToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.imagingToolStripMenuItem.Text = "Imaging";
            // 
            // getRenderToolStripMenuItem
            // 
            this.getRenderToolStripMenuItem.Name = "getRenderToolStripMenuItem";
            this.getRenderToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.getRenderToolStripMenuItem.Text = "Get render";
            this.getRenderToolStripMenuItem.Click += new System.EventHandler(this.getRenderToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vectorQuantizationErrorToolStripMenuItem});
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            // 
            // vectorQuantizationErrorToolStripMenuItem
            // 
            this.vectorQuantizationErrorToolStripMenuItem.Name = "vectorQuantizationErrorToolStripMenuItem";
            this.vectorQuantizationErrorToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.vectorQuantizationErrorToolStripMenuItem.Text = "Vector quantization error";
            this.vectorQuantizationErrorToolStripMenuItem.Click += new System.EventHandler(this.vectorQuantizationErrorToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_lbl_Status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 640);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ts_lbl_Status
            // 
            this.ts_lbl_Status.Name = "ts_lbl_Status";
            this.ts_lbl_Status.Size = new System.Drawing.Size(118, 17);
            this.ts_lbl_Status.Text = "toolStripStatusLabel1";
            // 
            // dgv_Drinks
            // 
            this.dgv_Drinks.AllowUserToAddRows = false;
            this.dgv_Drinks.AllowUserToDeleteRows = false;
            this.dgv_Drinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgv_Drinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Drinks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrinkID,
            this.DrinkName,
            this.DrinkUrl});
            this.dgv_Drinks.Location = new System.Drawing.Point(12, 53);
            this.dgv_Drinks.MultiSelect = false;
            this.dgv_Drinks.Name = "dgv_Drinks";
            this.dgv_Drinks.ReadOnly = true;
            this.dgv_Drinks.RowHeadersVisible = false;
            this.dgv_Drinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Drinks.Size = new System.Drawing.Size(266, 546);
            this.dgv_Drinks.TabIndex = 2;
            // 
            // DrinkID
            // 
            this.DrinkID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrinkID.FillWeight = 30F;
            this.DrinkID.HeaderText = "ID";
            this.DrinkID.Name = "DrinkID";
            this.DrinkID.ReadOnly = true;
            // 
            // DrinkName
            // 
            this.DrinkName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrinkName.HeaderText = "Name";
            this.DrinkName.Name = "DrinkName";
            this.DrinkName.ReadOnly = true;
            // 
            // DrinkUrl
            // 
            this.DrinkUrl.HeaderText = "DrinkUrl";
            this.DrinkUrl.Name = "DrinkUrl";
            this.DrinkUrl.ReadOnly = true;
            // 
            // tb_FilterInput
            // 
            this.tb_FilterInput.Location = new System.Drawing.Point(113, 27);
            this.tb_FilterInput.Name = "tb_FilterInput";
            this.tb_FilterInput.Size = new System.Drawing.Size(165, 20);
            this.tb_FilterInput.TabIndex = 3;
            this.tb_FilterInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_FilterInput_KeyDown);
            // 
            // lbl_FilterInput
            // 
            this.lbl_FilterInput.AutoSize = true;
            this.lbl_FilterInput.Location = new System.Drawing.Point(12, 30);
            this.lbl_FilterInput.Name = "lbl_FilterInput";
            this.lbl_FilterInput.Size = new System.Drawing.Size(32, 13);
            this.lbl_FilterInput.TabIndex = 4;
            this.lbl_FilterInput.Text = "Filter:";
            // 
            // webBrowserDrinks
            // 
            this.webBrowserDrinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserDrinks.Location = new System.Drawing.Point(3, 16);
            this.webBrowserDrinks.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserDrinks.Name = "webBrowserDrinks";
            this.webBrowserDrinks.Size = new System.Drawing.Size(665, 361);
            this.webBrowserDrinks.TabIndex = 5;
            this.webBrowserDrinks.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserDrinks_DocumentCompleted);
            this.webBrowserDrinks.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowserDrinks_Navigating);
            // 
            // btn_FindSimilar
            // 
            this.btn_FindSimilar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_FindSimilar.Location = new System.Drawing.Point(195, 605);
            this.btn_FindSimilar.Name = "btn_FindSimilar";
            this.btn_FindSimilar.Size = new System.Drawing.Size(83, 23);
            this.btn_FindSimilar.TabIndex = 6;
            this.btn_FindSimilar.Text = "Find similar";
            this.btn_FindSimilar.UseVisualStyleBackColor = true;
            this.btn_FindSimilar.Click += new System.EventHandler(this.btn_FindSimilar_Click);
            // 
            // gb_SimilarDrinks
            // 
            this.gb_SimilarDrinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_SimilarDrinks.Controls.Add(this.lv_SimilarDrinks);
            this.gb_SimilarDrinks.Location = new System.Drawing.Point(301, 431);
            this.gb_SimilarDrinks.Name = "gb_SimilarDrinks";
            this.gb_SimilarDrinks.Size = new System.Drawing.Size(671, 197);
            this.gb_SimilarDrinks.TabIndex = 9;
            this.gb_SimilarDrinks.TabStop = false;
            this.gb_SimilarDrinks.Text = "Similar Drinks";
            // 
            // lv_SimilarDrinks
            // 
            this.lv_SimilarDrinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_SimilarDrinks.Location = new System.Drawing.Point(3, 16);
            this.lv_SimilarDrinks.MultiSelect = false;
            this.lv_SimilarDrinks.Name = "lv_SimilarDrinks";
            this.lv_SimilarDrinks.Size = new System.Drawing.Size(665, 178);
            this.lv_SimilarDrinks.TabIndex = 0;
            this.lv_SimilarDrinks.UseCompatibleStateImageBehavior = false;
            this.lv_SimilarDrinks.SelectedIndexChanged += new System.EventHandler(this.lv_SimilarDrinks_SelectedIndexChanged);
            // 
            // gb_WebBrowser
            // 
            this.gb_WebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_WebBrowser.Controls.Add(this.webBrowserDrinks);
            this.gb_WebBrowser.Location = new System.Drawing.Point(301, 30);
            this.gb_WebBrowser.Name = "gb_WebBrowser";
            this.gb_WebBrowser.Size = new System.Drawing.Size(671, 380);
            this.gb_WebBrowser.TabIndex = 10;
            this.gb_WebBrowser.TabStop = false;
            this.gb_WebBrowser.Text = "Web browser";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.gb_WebBrowser);
            this.Controls.Add(this.gb_SimilarDrinks);
            this.Controls.Add(this.btn_FindSimilar);
            this.Controls.Add(this.lbl_FilterInput);
            this.Controls.Add(this.tb_FilterInput);
            this.Controls.Add(this.dgv_Drinks);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Drinks)).EndInit();
            this.gb_SimilarDrinks.ResumeLayout(false);
            this.gb_WebBrowser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem learnNeuralNetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveNeuralNetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNeuralNetToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ts_lbl_Status;
        private System.Windows.Forms.DataGridView dgv_Drinks;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrinkID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrinkName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrinkUrl;
        private System.Windows.Forms.TextBox tb_FilterInput;
        private System.Windows.Forms.Label lbl_FilterInput;
        private System.Windows.Forms.WebBrowser webBrowserDrinks;
        private System.Windows.Forms.Button btn_FindSimilar;
        private System.Windows.Forms.ToolStripMenuItem imagingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getRenderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vectorQuantizationErrorToolStripMenuItem;
        private System.Windows.Forms.GroupBox gb_SimilarDrinks;
        private System.Windows.Forms.ListView lv_SimilarDrinks;
        private System.Windows.Forms.GroupBox gb_WebBrowser;
    }
}

