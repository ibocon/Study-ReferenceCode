namespace iboconPCFExporter
{
    partial class AppUI
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
            this.ExportPCF = new System.Windows.Forms.Button();
            this.GenPCFParam = new System.Windows.Forms.Button();
            this.SelectExcelFile = new System.Windows.Forms.Button();
            this.openExcelDialog = new System.Windows.Forms.OpenFileDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.ExcelName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ExportPCF
            // 
            this.ExportPCF.Font = new System.Drawing.Font("Gulim", 16F);
            this.ExportPCF.Location = new System.Drawing.Point(159, 301);
            this.ExportPCF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExportPCF.Name = "ExportPCF";
            this.ExportPCF.Size = new System.Drawing.Size(188, 41);
            this.ExportPCF.TabIndex = 0;
            this.ExportPCF.Text = "Export PCF";
            this.ExportPCF.UseVisualStyleBackColor = true;
            this.ExportPCF.Click += new System.EventHandler(this.ExportPCF_Click);
            // 
            // GenPCFParam
            // 
            this.GenPCFParam.Font = new System.Drawing.Font("Gulim", 16F);
            this.GenPCFParam.Location = new System.Drawing.Point(16, 193);
            this.GenPCFParam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GenPCFParam.Name = "GenPCFParam";
            this.GenPCFParam.Size = new System.Drawing.Size(331, 32);
            this.GenPCFParam.TabIndex = 1;
            this.GenPCFParam.Text = "Generate PCF Parameter Binding";
            this.GenPCFParam.UseVisualStyleBackColor = true;
            this.GenPCFParam.Click += new System.EventHandler(this.GenPCFParamBinding_Click);
            // 
            // SelectExcelFile
            // 
            this.SelectExcelFile.Font = new System.Drawing.Font("Gulim", 16F);
            this.SelectExcelFile.Location = new System.Drawing.Point(16, 229);
            this.SelectExcelFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectExcelFile.Name = "SelectExcelFile";
            this.SelectExcelFile.Size = new System.Drawing.Size(331, 32);
            this.SelectExcelFile.TabIndex = 2;
            this.SelectExcelFile.Text = "Select Excel File";
            this.SelectExcelFile.UseVisualStyleBackColor = true;
            this.SelectExcelFile.Click += new System.EventHandler(this.SelectExcelFile_Click);
            // 
            // openExcelDialog
            // 
            this.openExcelDialog.FileName = "openExcelDialog";
            // 
            // ExcelName
            // 
            this.ExcelName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ExcelName.Font = new System.Drawing.Font("Gulim", 16F);
            this.ExcelName.Location = new System.Drawing.Point(18, 265);
            this.ExcelName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExcelName.Name = "ExcelName";
            this.ExcelName.ReadOnly = true;
            this.ExcelName.Size = new System.Drawing.Size(331, 32);
            this.ExcelName.TabIndex = 3;
            this.ExcelName.Text = "Select Excel File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gulim", 16F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "Project Name:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Gulim", 16F);
            this.textBox1.Location = new System.Drawing.Point(159, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(227, 32);
            this.textBox1.TabIndex = 6;
            // 
            // AppUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 370);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExcelName);
            this.Controls.Add(this.SelectExcelFile);
            this.Controls.Add(this.GenPCFParam);
            this.Controls.Add(this.ExportPCF);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AppUI";
            this.Text = "AppUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExportPCF;
        private System.Windows.Forms.Button GenPCFParam;
        private System.Windows.Forms.Button SelectExcelFile;
        private System.Windows.Forms.OpenFileDialog openExcelDialog;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.TextBox ExcelName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}