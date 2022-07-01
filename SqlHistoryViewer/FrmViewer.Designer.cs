namespace SqlHistoryViewer
{
    partial class FrmViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgFileSql = new System.Windows.Forms.DataGridView();
            this.cboVersion = new System.Windows.Forms.ComboBox();
            this.txtCriteria = new System.Windows.Forms.TextBox();
            this.txtXmlLocation = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cDeployVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOpenContent = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgFileSql)).BeginInit();
            this.SuspendLayout();
            // 
            // dgFileSql
            // 
            this.dgFileSql.AllowUserToAddRows = false;
            this.dgFileSql.AllowUserToDeleteRows = false;
            this.dgFileSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFileSql.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFileSql.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cDeployVersion,
            this.cFileName,
            this.cCreated,
            this.cOpenContent});
            this.dgFileSql.Location = new System.Drawing.Point(12, 184);
            this.dgFileSql.Name = "dgFileSql";
            this.dgFileSql.ReadOnly = true;
            this.dgFileSql.Size = new System.Drawing.Size(883, 402);
            this.dgFileSql.TabIndex = 0;
            this.dgFileSql.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFileSql_CellContentClick);
            // 
            // cboVersion
            // 
            this.cboVersion.FormattingEnabled = true;
            this.cboVersion.Location = new System.Drawing.Point(140, 89);
            this.cboVersion.Name = "cboVersion";
            this.cboVersion.Size = new System.Drawing.Size(241, 21);
            this.cboVersion.TabIndex = 1;
            // 
            // txtCriteria
            // 
            this.txtCriteria.Location = new System.Drawing.Point(140, 117);
            this.txtCriteria.Name = "txtCriteria";
            this.txtCriteria.Size = new System.Drawing.Size(241, 20);
            this.txtCriteria.TabIndex = 2;
            // 
            // txtXmlLocation
            // 
            this.txtXmlLocation.Location = new System.Drawing.Point(140, 12);
            this.txtXmlLocation.Multiline = true;
            this.txtXmlLocation.Name = "txtXmlLocation";
            this.txtXmlLocation.Size = new System.Drawing.Size(364, 71);
            this.txtXmlLocation.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(140, 144);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Xml Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Version";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Criteria";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(510, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cDeployVersion
            // 
            this.cDeployVersion.DataPropertyName = "DeployVersion";
            this.cDeployVersion.HeaderText = "Version";
            this.cDeployVersion.Name = "cDeployVersion";
            this.cDeployVersion.ReadOnly = true;
            // 
            // cFileName
            // 
            this.cFileName.DataPropertyName = "FileName";
            this.cFileName.HeaderText = "File";
            this.cFileName.Name = "cFileName";
            this.cFileName.ReadOnly = true;
            this.cFileName.Width = 500;
            // 
            // cCreated
            // 
            this.cCreated.DataPropertyName = "DateCreated";
            this.cCreated.HeaderText = "Created";
            this.cCreated.Name = "cCreated";
            this.cCreated.ReadOnly = true;
            // 
            // cOpenContent
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = "Open Content";
            this.cOpenContent.DefaultCellStyle = dataGridViewCellStyle5;
            this.cOpenContent.HeaderText = "";
            this.cOpenContent.Name = "cOpenContent";
            this.cOpenContent.ReadOnly = true;
            this.cOpenContent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cOpenContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // FrmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 598);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtXmlLocation);
            this.Controls.Add(this.txtCriteria);
            this.Controls.Add(this.cboVersion);
            this.Controls.Add(this.dgFileSql);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL History Viewer";
            this.Load += new System.EventHandler(this.FrmViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFileSql)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgFileSql;
        private System.Windows.Forms.ComboBox cboVersion;
        private System.Windows.Forms.TextBox txtCriteria;
        private System.Windows.Forms.TextBox txtXmlLocation;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDeployVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCreated;
        private System.Windows.Forms.DataGridViewButtonColumn cOpenContent;
    }
}

