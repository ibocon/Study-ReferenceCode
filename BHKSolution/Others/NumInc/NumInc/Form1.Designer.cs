namespace NumInc
{
    partial class NumInc
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
            this.SelectedText = new System.Windows.Forms.GroupBox();
            this.listBox_Input = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_Basic = new System.Windows.Forms.GroupBox();
            this.radioButtonDivide = new System.Windows.Forms.RadioButton();
            this.radioButtonMultiply = new System.Windows.Forms.RadioButton();
            this.radioButtonMinus = new System.Windows.Forms.RadioButton();
            this.radioButtonPlus = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_underPos = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_upperPos = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_Semicolon = new System.Windows.Forms.CheckBox();
            this.checkBox_Hex = new System.Windows.Forms.CheckBox();
            this.checkBox_Partial = new System.Windows.Forms.CheckBox();
            this.groupBox_Partial = new System.Windows.Forms.GroupBox();
            this.comboBox_Direction2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_SpecificChar = new System.Windows.Forms.TextBox();
            this.radioButton_Specific = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown_CharCount = new System.Windows.Forms.NumericUpDown();
            this.comboBox_Direction = new System.Windows.Forms.ComboBox();
            this.radioButton_General = new System.Windows.Forms.RadioButton();
            this.apply = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox_Inc = new System.Windows.Forms.TextBox();
            this.SelectedText.SuspendLayout();
            this.groupBox_Basic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_underPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_upperPos)).BeginInit();
            this.groupBox_Partial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CharCount)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectedText
            // 
            this.SelectedText.Controls.Add(this.listBox_Input);
            this.SelectedText.Location = new System.Drawing.Point(12, 12);
            this.SelectedText.Name = "SelectedText";
            this.SelectedText.Size = new System.Drawing.Size(244, 120);
            this.SelectedText.TabIndex = 0;
            this.SelectedText.TabStop = false;
            this.SelectedText.Text = "선택된 문자열";
            // 
            // listBox_Input
            // 
            this.listBox_Input.FormattingEnabled = true;
            this.listBox_Input.ItemHeight = 12;
            this.listBox_Input.Location = new System.Drawing.Point(6, 18);
            this.listBox_Input.Name = "listBox_Input";
            this.listBox_Input.Size = new System.Drawing.Size(232, 88);
            this.listBox_Input.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "증감숫자 :";
            // 
            // groupBox_Basic
            // 
            this.groupBox_Basic.Controls.Add(this.textBox_Inc);
            this.groupBox_Basic.Controls.Add(this.radioButtonDivide);
            this.groupBox_Basic.Controls.Add(this.radioButtonMultiply);
            this.groupBox_Basic.Controls.Add(this.radioButtonMinus);
            this.groupBox_Basic.Controls.Add(this.radioButtonPlus);
            this.groupBox_Basic.Controls.Add(this.label4);
            this.groupBox_Basic.Controls.Add(this.numericUpDown_underPos);
            this.groupBox_Basic.Controls.Add(this.label3);
            this.groupBox_Basic.Controls.Add(this.numericUpDown_upperPos);
            this.groupBox_Basic.Controls.Add(this.label2);
            this.groupBox_Basic.Controls.Add(this.label1);
            this.groupBox_Basic.Location = new System.Drawing.Point(12, 138);
            this.groupBox_Basic.Name = "groupBox_Basic";
            this.groupBox_Basic.Size = new System.Drawing.Size(244, 135);
            this.groupBox_Basic.TabIndex = 2;
            this.groupBox_Basic.TabStop = false;
            // 
            // radioButtonDivide
            // 
            this.radioButtonDivide.AutoSize = true;
            this.radioButtonDivide.Location = new System.Drawing.Point(192, 107);
            this.radioButtonDivide.Name = "radioButtonDivide";
            this.radioButtonDivide.Size = new System.Drawing.Size(29, 16);
            this.radioButtonDivide.TabIndex = 12;
            this.radioButtonDivide.Text = "/";
            this.radioButtonDivide.UseVisualStyleBackColor = true;
            // 
            // radioButtonMultiply
            // 
            this.radioButtonMultiply.AutoSize = true;
            this.radioButtonMultiply.Location = new System.Drawing.Point(157, 107);
            this.radioButtonMultiply.Name = "radioButtonMultiply";
            this.radioButtonMultiply.Size = new System.Drawing.Size(29, 16);
            this.radioButtonMultiply.TabIndex = 11;
            this.radioButtonMultiply.Text = "*";
            this.radioButtonMultiply.UseVisualStyleBackColor = true;
            // 
            // radioButtonMinus
            // 
            this.radioButtonMinus.AutoSize = true;
            this.radioButtonMinus.Location = new System.Drawing.Point(122, 107);
            this.radioButtonMinus.Name = "radioButtonMinus";
            this.radioButtonMinus.Size = new System.Drawing.Size(29, 16);
            this.radioButtonMinus.TabIndex = 10;
            this.radioButtonMinus.Text = "-";
            this.radioButtonMinus.UseVisualStyleBackColor = true;
            // 
            // radioButtonPlus
            // 
            this.radioButtonPlus.AutoSize = true;
            this.radioButtonPlus.Checked = true;
            this.radioButtonPlus.Location = new System.Drawing.Point(87, 107);
            this.radioButtonPlus.Name = "radioButtonPlus";
            this.radioButtonPlus.Size = new System.Drawing.Size(29, 16);
            this.radioButtonPlus.TabIndex = 9;
            this.radioButtonPlus.TabStop = true;
            this.radioButtonPlus.Text = "+";
            this.radioButtonPlus.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "계산방식 :";
            // 
            // numericUpDown_underPos
            // 
            this.numericUpDown_underPos.Location = new System.Drawing.Point(74, 69);
            this.numericUpDown_underPos.Name = "numericUpDown_underPos";
            this.numericUpDown_underPos.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_underPos.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "소수점 :";
            // 
            // numericUpDown_upperPos
            // 
            this.numericUpDown_upperPos.Location = new System.Drawing.Point(74, 42);
            this.numericUpDown_upperPos.Name = "numericUpDown_upperPos";
            this.numericUpDown_upperPos.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown_upperPos.TabIndex = 5;
            this.numericUpDown_upperPos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "자리 수 :";
            // 
            // checkBox_Semicolon
            // 
            this.checkBox_Semicolon.AutoSize = true;
            this.checkBox_Semicolon.Location = new System.Drawing.Point(18, 279);
            this.checkBox_Semicolon.Name = "checkBox_Semicolon";
            this.checkBox_Semicolon.Size = new System.Drawing.Size(140, 16);
            this.checkBox_Semicolon.TabIndex = 3;
            this.checkBox_Semicolon.Text = "천단위 구분기호 삽입";
            this.checkBox_Semicolon.UseVisualStyleBackColor = true;
            // 
            // checkBox_Hex
            // 
            this.checkBox_Hex.AutoSize = true;
            this.checkBox_Hex.Location = new System.Drawing.Point(18, 301);
            this.checkBox_Hex.Name = "checkBox_Hex";
            this.checkBox_Hex.Size = new System.Drawing.Size(100, 16);
            this.checkBox_Hex.TabIndex = 4;
            this.checkBox_Hex.Text = "16진수로 적용";
            this.checkBox_Hex.UseVisualStyleBackColor = true;
            // 
            // checkBox_Partial
            // 
            this.checkBox_Partial.AutoSize = true;
            this.checkBox_Partial.Location = new System.Drawing.Point(18, 323);
            this.checkBox_Partial.Name = "checkBox_Partial";
            this.checkBox_Partial.Size = new System.Drawing.Size(160, 16);
            this.checkBox_Partial.TabIndex = 5;
            this.checkBox_Partial.Text = "문자 내용 중 일부만 인식";
            this.checkBox_Partial.UseVisualStyleBackColor = true;
            this.checkBox_Partial.CheckedChanged += new System.EventHandler(this.checkBox_Partial_CheckedChanged);
            // 
            // groupBox_Partial
            // 
            this.groupBox_Partial.Controls.Add(this.comboBox_Direction2);
            this.groupBox_Partial.Controls.Add(this.label6);
            this.groupBox_Partial.Controls.Add(this.textBox_SpecificChar);
            this.groupBox_Partial.Controls.Add(this.radioButton_Specific);
            this.groupBox_Partial.Controls.Add(this.label5);
            this.groupBox_Partial.Controls.Add(this.numericUpDown_CharCount);
            this.groupBox_Partial.Controls.Add(this.comboBox_Direction);
            this.groupBox_Partial.Controls.Add(this.radioButton_General);
            this.groupBox_Partial.Enabled = false;
            this.groupBox_Partial.Location = new System.Drawing.Point(12, 345);
            this.groupBox_Partial.Name = "groupBox_Partial";
            this.groupBox_Partial.Size = new System.Drawing.Size(244, 149);
            this.groupBox_Partial.TabIndex = 6;
            this.groupBox_Partial.TabStop = false;
            // 
            // comboBox_Direction2
            // 
            this.comboBox_Direction2.Enabled = false;
            this.comboBox_Direction2.FormattingEnabled = true;
            this.comboBox_Direction2.Location = new System.Drawing.Point(87, 122);
            this.comboBox_Direction2.Name = "comboBox_Direction2";
            this.comboBox_Direction2.Size = new System.Drawing.Size(148, 20);
            this.comboBox_Direction2.TabIndex = 13;
            this.comboBox_Direction2.Tag = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(15, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "지정 문자";
            // 
            // textBox_SpecificChar
            // 
            this.textBox_SpecificChar.Enabled = false;
            this.textBox_SpecificChar.Location = new System.Drawing.Point(9, 91);
            this.textBox_SpecificChar.Name = "textBox_SpecificChar";
            this.textBox_SpecificChar.Size = new System.Drawing.Size(226, 21);
            this.textBox_SpecificChar.TabIndex = 11;
            // 
            // radioButton_Specific
            // 
            this.radioButton_Specific.AutoSize = true;
            this.radioButton_Specific.Enabled = false;
            this.radioButton_Specific.Location = new System.Drawing.Point(9, 69);
            this.radioButton_Specific.Name = "radioButton_Specific";
            this.radioButton_Specific.Size = new System.Drawing.Size(115, 16);
            this.radioButton_Specific.TabIndex = 10;
            this.radioButton_Specific.TabStop = true;
            this.radioButton_Specific.Text = "지정 문자만 인식";
            this.radioButton_Specific.UseVisualStyleBackColor = true;
            this.radioButton_Specific.CheckedChanged += new System.EventHandler(this.radioButton_Specific_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(166, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "문자만 인식";
            // 
            // numericUpDown_CharCount
            // 
            this.numericUpDown_CharCount.Enabled = false;
            this.numericUpDown_CharCount.Location = new System.Drawing.Point(101, 42);
            this.numericUpDown_CharCount.Name = "numericUpDown_CharCount";
            this.numericUpDown_CharCount.Size = new System.Drawing.Size(59, 21);
            this.numericUpDown_CharCount.TabIndex = 8;
            // 
            // comboBox_Direction
            // 
            this.comboBox_Direction.Enabled = false;
            this.comboBox_Direction.FormattingEnabled = true;
            this.comboBox_Direction.Location = new System.Drawing.Point(9, 42);
            this.comboBox_Direction.Name = "comboBox_Direction";
            this.comboBox_Direction.Size = new System.Drawing.Size(86, 20);
            this.comboBox_Direction.TabIndex = 1;
            // 
            // radioButton_General
            // 
            this.radioButton_General.AutoSize = true;
            this.radioButton_General.Enabled = false;
            this.radioButton_General.Location = new System.Drawing.Point(9, 20);
            this.radioButton_General.Name = "radioButton_General";
            this.radioButton_General.Size = new System.Drawing.Size(151, 16);
            this.radioButton_General.TabIndex = 0;
            this.radioButton_General.TabStop = true;
            this.radioButton_General.Text = "앞, 뒤 일부 문자만 인식";
            this.radioButton_General.UseVisualStyleBackColor = true;
            this.radioButton_General.CheckedChanged += new System.EventHandler(this.radioButton_General_CheckedChanged);
            // 
            // apply
            // 
            this.apply.Location = new System.Drawing.Point(12, 510);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(75, 23);
            this.apply.TabIndex = 7;
            this.apply.Text = "확인";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(97, 510);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "종료";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(178, 510);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "도움말";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox_Inc
            // 
            this.textBox_Inc.Location = new System.Drawing.Point(74, 15);
            this.textBox_Inc.Name = "textBox_Inc";
            this.textBox_Inc.Size = new System.Drawing.Size(120, 21);
            this.textBox_Inc.TabIndex = 13;
            this.textBox_Inc.Text = "0";
            // 
            // NumInc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 549);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.groupBox_Partial);
            this.Controls.Add(this.checkBox_Partial);
            this.Controls.Add(this.checkBox_Hex);
            this.Controls.Add(this.checkBox_Semicolon);
            this.Controls.Add(this.groupBox_Basic);
            this.Controls.Add(this.SelectedText);
            this.Name = "NumInc";
            this.Text = "숫자 증감";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SelectedText.ResumeLayout(false);
            this.groupBox_Basic.ResumeLayout(false);
            this.groupBox_Basic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_underPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_upperPos)).EndInit();
            this.groupBox_Partial.ResumeLayout(false);
            this.groupBox_Partial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_CharCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox SelectedText;
        private System.Windows.Forms.ListBox listBox_Input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_Basic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonDivide;
        private System.Windows.Forms.RadioButton radioButtonMultiply;
        private System.Windows.Forms.RadioButton radioButtonMinus;
        private System.Windows.Forms.RadioButton radioButtonPlus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown_underPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_upperPos;
        private System.Windows.Forms.CheckBox checkBox_Semicolon;
        private System.Windows.Forms.CheckBox checkBox_Hex;
        private System.Windows.Forms.CheckBox checkBox_Partial;
        private System.Windows.Forms.GroupBox groupBox_Partial;
        private System.Windows.Forms.ComboBox comboBox_Direction2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_SpecificChar;
        private System.Windows.Forms.RadioButton radioButton_Specific;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown_CharCount;
        private System.Windows.Forms.ComboBox comboBox_Direction;
        private System.Windows.Forms.RadioButton radioButton_General;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox_Inc;
    }
}

