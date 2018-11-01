namespace WindowsFormsApp1
{
    partial class FormFlatFileParser
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
            this.btnLoadFIle = new System.Windows.Forms.Button();
            this.lblDLLFolder = new System.Windows.Forms.Label();
            this.tbDLLFolder = new System.Windows.Forms.TextBox();
            this.btnDLLFolder = new System.Windows.Forms.Button();
            this.btnFile = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.tbDelimiter = new System.Windows.Forms.TextBox();
            this.lblDelimiter = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.btnValidate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadFIle
            // 
            this.btnLoadFIle.Location = new System.Drawing.Point(12, 122);
            this.btnLoadFIle.Name = "btnLoadFIle";
            this.btnLoadFIle.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFIle.TabIndex = 0;
            this.btnLoadFIle.Text = "Load File";
            this.btnLoadFIle.UseVisualStyleBackColor = true;
            this.btnLoadFIle.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // lblDLLFolder
            // 
            this.lblDLLFolder.AutoSize = true;
            this.lblDLLFolder.Location = new System.Drawing.Point(5, 9);
            this.lblDLLFolder.Name = "lblDLLFolder";
            this.lblDLLFolder.Size = new System.Drawing.Size(82, 17);
            this.lblDLLFolder.TabIndex = 1;
            this.lblDLLFolder.Text = "DLL Folder:";
            // 
            // tbDLLFolder
            // 
            this.tbDLLFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDLLFolder.Location = new System.Drawing.Point(93, 6);
            this.tbDLLFolder.Name = "tbDLLFolder";
            this.tbDLLFolder.Size = new System.Drawing.Size(716, 22);
            this.tbDLLFolder.TabIndex = 2;
            this.tbDLLFolder.Text = "C:\\Users\\Scott\\source\\Output";
            // 
            // btnDLLFolder
            // 
            this.btnDLLFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDLLFolder.Location = new System.Drawing.Point(822, 6);
            this.btnDLLFolder.Name = "btnDLLFolder";
            this.btnDLLFolder.Size = new System.Drawing.Size(35, 23);
            this.btnDLLFolder.TabIndex = 3;
            this.btnDLLFolder.Text = "...";
            this.btnDLLFolder.UseVisualStyleBackColor = true;
            this.btnDLLFolder.Click += new System.EventHandler(this.btnDLLFolder_Click);
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.Location = new System.Drawing.Point(822, 34);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(35, 23);
            this.btnFile.TabIndex = 6;
            this.btnFile.Text = "...";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(49, 37);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(34, 17);
            this.lblFile.TabIndex = 4;
            this.lblFile.Text = "File:";
            // 
            // tbFile
            // 
            this.tbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFile.Location = new System.Drawing.Point(93, 34);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(716, 22);
            this.tbFile.TabIndex = 5;
            // 
            // tbDelimiter
            // 
            this.tbDelimiter.Location = new System.Drawing.Point(93, 62);
            this.tbDelimiter.Name = "tbDelimiter";
            this.tbDelimiter.Size = new System.Drawing.Size(35, 22);
            this.tbDelimiter.TabIndex = 8;
            this.tbDelimiter.Text = "|";
            // 
            // lblDelimiter
            // 
            this.lblDelimiter.AutoSize = true;
            this.lblDelimiter.Location = new System.Drawing.Point(16, 65);
            this.lblDelimiter.Name = "lblDelimiter";
            this.lblDelimiter.Size = new System.Drawing.Size(67, 17);
            this.lblDelimiter.TabIndex = 7;
            this.lblDelimiter.Text = "Delimiter:";
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.BackColor = System.Drawing.Color.Black;
            this.tbOutput.ForeColor = System.Drawing.Color.White;
            this.tbOutput.Location = new System.Drawing.Point(8, 435);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(849, 95);
            this.tbOutput.TabIndex = 9;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(8, 151);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowTemplate.Height = 24;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvResults.Size = new System.Drawing.Size(849, 278);
            this.dgvResults.TabIndex = 10;
            this.dgvResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellClick);
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(105, 122);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 23);
            this.btnValidate.TabIndex = 11;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // FormFlatFileParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 542);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.tbDelimiter);
            this.Controls.Add(this.lblDelimiter);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnDLLFolder);
            this.Controls.Add(this.tbDLLFolder);
            this.Controls.Add(this.lblDLLFolder);
            this.Controls.Add(this.btnLoadFIle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFlatFileParser";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reflection-based parser";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadFIle;
        private System.Windows.Forms.Label lblDLLFolder;
        private System.Windows.Forms.TextBox tbDLLFolder;
        private System.Windows.Forms.Button btnDLLFolder;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.TextBox tbDelimiter;
        private System.Windows.Forms.Label lblDelimiter;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Button btnValidate;
    }
}

