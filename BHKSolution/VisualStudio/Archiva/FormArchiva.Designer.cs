namespace Archiva
{
    partial class FormArchiva
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.test = new System.Windows.Forms.Button();
            this.label_title = new System.Windows.Forms.Label();
            this.label_WSL = new System.Windows.Forms.Label();
            this.numericUpDown_wsl_x = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_wsl_y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_wsl_z = new System.Windows.Forms.NumericUpDown();
            this.label_wsl_x = new System.Windows.Forms.Label();
            this.label_wsl_y = new System.Windows.Forms.Label();
            this.label_wsl_z = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown_maxspace = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown_bednum = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_restnum = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.radioButton_simple = new System.Windows.Forms.RadioButton();
            this.groupBox_comb = new System.Windows.Forms.GroupBox();
            this.radioButton_complicate = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Design = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Roof = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_wsl_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_wsl_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_wsl_z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maxspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_bednum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_restnum)).BeginInit();
            this.groupBox_comb.SuspendLayout();
            this.SuspendLayout();
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(525, 377);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(93, 45);
            this.test.TabIndex = 1;
            this.test.Text = "TEST!";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.createModel);
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Gulim", 30F);
            this.label_title.Location = new System.Drawing.Point(12, 9);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(155, 40);
            this.label_title.TabIndex = 2;
            this.label_title.Text = "Archiva";
            // 
            // label_WSL
            // 
            this.label_WSL.AutoSize = true;
            this.label_WSL.Font = new System.Drawing.Font("Gulim", 12F);
            this.label_WSL.Location = new System.Drawing.Point(12, 65);
            this.label_WSL.Name = "label_WSL";
            this.label_WSL.Size = new System.Drawing.Size(188, 16);
            this.label_WSL.TabIndex = 3;
            this.label_WSL.Text = "World Standard Location";
            // 
            // numericUpDown_wsl_x
            // 
            this.numericUpDown_wsl_x.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_wsl_x.Location = new System.Drawing.Point(217, 67);
            this.numericUpDown_wsl_x.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.numericUpDown_wsl_x.Minimum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            -2147483648});
            this.numericUpDown_wsl_x.Name = "numericUpDown_wsl_x";
            this.numericUpDown_wsl_x.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_wsl_x.TabIndex = 4;
            this.numericUpDown_wsl_x.ThousandsSeparator = true;
            this.numericUpDown_wsl_x.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // numericUpDown_wsl_y
            // 
            this.numericUpDown_wsl_y.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_wsl_y.Location = new System.Drawing.Point(374, 67);
            this.numericUpDown_wsl_y.Maximum = new decimal(new int[] {
            -1593835521,
            466537709,
            54210,
            0});
            this.numericUpDown_wsl_y.Minimum = new decimal(new int[] {
            268435455,
            1042612833,
            542101086,
            -2147483648});
            this.numericUpDown_wsl_y.Name = "numericUpDown_wsl_y";
            this.numericUpDown_wsl_y.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_wsl_y.TabIndex = 5;
            this.numericUpDown_wsl_y.ThousandsSeparator = true;
            this.numericUpDown_wsl_y.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // numericUpDown_wsl_z
            // 
            this.numericUpDown_wsl_z.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_wsl_z.Location = new System.Drawing.Point(523, 67);
            this.numericUpDown_wsl_z.Maximum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            0});
            this.numericUpDown_wsl_z.Minimum = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            -2147483648});
            this.numericUpDown_wsl_z.Name = "numericUpDown_wsl_z";
            this.numericUpDown_wsl_z.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_wsl_z.TabIndex = 6;
            this.numericUpDown_wsl_z.ThousandsSeparator = true;
            this.numericUpDown_wsl_z.Value = new decimal(new int[] {
            1350,
            0,
            0,
            0});
            // 
            // label_wsl_x
            // 
            this.label_wsl_x.AutoSize = true;
            this.label_wsl_x.Location = new System.Drawing.Point(217, 49);
            this.label_wsl_x.Name = "label_wsl_x";
            this.label_wsl_x.Size = new System.Drawing.Size(41, 12);
            this.label_wsl_x.TabIndex = 7;
            this.label_wsl_x.Text = "X axis";
            // 
            // label_wsl_y
            // 
            this.label_wsl_y.AutoSize = true;
            this.label_wsl_y.Location = new System.Drawing.Point(372, 48);
            this.label_wsl_y.Name = "label_wsl_y";
            this.label_wsl_y.Size = new System.Drawing.Size(41, 12);
            this.label_wsl_y.TabIndex = 8;
            this.label_wsl_y.Text = "Y axis";
            // 
            // label_wsl_z
            // 
            this.label_wsl_z.AutoSize = true;
            this.label_wsl_z.Location = new System.Drawing.Point(523, 49);
            this.label_wsl_z.Name = "label_wsl_z";
            this.label_wsl_z.Size = new System.Drawing.Size(41, 12);
            this.label_wsl_z.TabIndex = 9;
            this.label_wsl_z.Text = "Z axis";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Gulim", 12F);
            this.label9.Location = new System.Drawing.Point(22, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 16);
            this.label9.TabIndex = 10;
            this.label9.Text = "Max Space (cm^2)";
            // 
            // numericUpDown_maxspace
            // 
            this.numericUpDown_maxspace.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_maxspace.Location = new System.Drawing.Point(217, 112);
            this.numericUpDown_maxspace.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.numericUpDown_maxspace.Minimum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            -2147483648});
            this.numericUpDown_maxspace.Name = "numericUpDown_maxspace";
            this.numericUpDown_maxspace.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_maxspace.TabIndex = 11;
            this.numericUpDown_maxspace.ThousandsSeparator = true;
            this.numericUpDown_maxspace.Value = new decimal(new int[] {
            900000,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Gulim", 12F);
            this.label10.Location = new System.Drawing.Point(44, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "How many Bed?";
            // 
            // numericUpDown_bednum
            // 
            this.numericUpDown_bednum.Location = new System.Drawing.Point(217, 163);
            this.numericUpDown_bednum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_bednum.Name = "numericUpDown_bednum";
            this.numericUpDown_bednum.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_bednum.TabIndex = 13;
            this.numericUpDown_bednum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_restnum
            // 
            this.numericUpDown_restnum.Location = new System.Drawing.Point(217, 212);
            this.numericUpDown_restnum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_restnum.Name = "numericUpDown_restnum";
            this.numericUpDown_restnum.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_restnum.TabIndex = 15;
            this.numericUpDown_restnum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Gulim", 12F);
            this.label11.Location = new System.Drawing.Point(44, 212);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 16);
            this.label11.TabIndex = 14;
            this.label11.Text = "How many Rest?";
            // 
            // radioButton_simple
            // 
            this.radioButton_simple.AutoSize = true;
            this.radioButton_simple.Checked = true;
            this.radioButton_simple.Location = new System.Drawing.Point(27, 29);
            this.radioButton_simple.Name = "radioButton_simple";
            this.radioButton_simple.Size = new System.Drawing.Size(62, 16);
            this.radioButton_simple.TabIndex = 16;
            this.radioButton_simple.TabStop = true;
            this.radioButton_simple.Text = "Simple";
            this.radioButton_simple.UseVisualStyleBackColor = true;
            // 
            // groupBox_comb
            // 
            this.groupBox_comb.Controls.Add(this.radioButton_complicate);
            this.groupBox_comb.Controls.Add(this.radioButton_simple);
            this.groupBox_comb.Location = new System.Drawing.Point(388, 133);
            this.groupBox_comb.Name = "groupBox_comb";
            this.groupBox_comb.Size = new System.Drawing.Size(200, 100);
            this.groupBox_comb.TabIndex = 17;
            this.groupBox_comb.TabStop = false;
            this.groupBox_comb.Text = "Combination";
            // 
            // radioButton_complicate
            // 
            this.radioButton_complicate.AutoSize = true;
            this.radioButton_complicate.Location = new System.Drawing.Point(27, 62);
            this.radioButton_complicate.Name = "radioButton_complicate";
            this.radioButton_complicate.Size = new System.Drawing.Size(87, 16);
            this.radioButton_complicate.TabIndex = 17;
            this.radioButton_complicate.TabStop = true;
            this.radioButton_complicate.Text = "Complicate";
            this.radioButton_complicate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gulim", 12F);
            this.label2.Location = new System.Drawing.Point(44, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Design?";
            // 
            // comboBox_Design
            // 
            this.comboBox_Design.FormattingEnabled = true;
            this.comboBox_Design.Location = new System.Drawing.Point(217, 265);
            this.comboBox_Design.Name = "comboBox_Design";
            this.comboBox_Design.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Design.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gulim", 12F);
            this.label1.Location = new System.Drawing.Point(44, 319);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "RoofType?";
            // 
            // comboBox_Roof
            // 
            this.comboBox_Roof.FormattingEnabled = true;
            this.comboBox_Roof.Location = new System.Drawing.Point(217, 319);
            this.comboBox_Roof.Name = "comboBox_Roof";
            this.comboBox_Roof.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Roof.TabIndex = 22;
            // 
            // FormArchiva
            // 
            this.ClientSize = new System.Drawing.Size(663, 434);
            this.Controls.Add(this.comboBox_Roof);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_Design);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox_comb);
            this.Controls.Add(this.numericUpDown_restnum);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numericUpDown_bednum);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericUpDown_maxspace);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label_wsl_z);
            this.Controls.Add(this.label_wsl_y);
            this.Controls.Add(this.label_wsl_x);
            this.Controls.Add(this.numericUpDown_wsl_z);
            this.Controls.Add(this.numericUpDown_wsl_y);
            this.Controls.Add(this.numericUpDown_wsl_x);
            this.Controls.Add(this.label_WSL);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.test);
            this.Name = "FormArchiva";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_wsl_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_wsl_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_wsl_z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_maxspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_bednum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_restnum)).EndInit();
            this.groupBox_comb.ResumeLayout(false);
            this.groupBox_comb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_WSL;
        private System.Windows.Forms.NumericUpDown numericUpDown_wsl_x;
        private System.Windows.Forms.NumericUpDown numericUpDown_wsl_y;
        private System.Windows.Forms.NumericUpDown numericUpDown_wsl_z;
        private System.Windows.Forms.Label label_wsl_x;
        private System.Windows.Forms.Label label_wsl_y;
        private System.Windows.Forms.Label label_wsl_z;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown_maxspace;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDown_bednum;
        private System.Windows.Forms.NumericUpDown numericUpDown_restnum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton radioButton_simple;
        private System.Windows.Forms.GroupBox groupBox_comb;
        private System.Windows.Forms.RadioButton radioButton_complicate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Design;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Roof;
    }
}

