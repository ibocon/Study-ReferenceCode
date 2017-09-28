using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumInc
{
    enum DirectionType
    {
        Front,
        Back
    }

    enum OperatorType
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }

    public partial class NumInc : Form
    {
        List<string> inputs;

        decimal inc;
        decimal upperPos;
        decimal underPos;
        OperatorType opType;
        bool semicolon;
        bool hex;
        bool partial;

        public NumInc()
        {
            inputs = new List<string>() {
                "EL:123", "가나123.456다라", "123-ABC-456",
                "123.456+678.890", "AA03-BB1", ".AB12,00", "A100100$",
                "A100Bd100", "A102.03", "BW100.33"
            };
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listBox_Input.Items.AddRange(inputs.ToArray());
            this.comboBox_Direction.Items.Add(new ComboboxItem("앞에서", DirectionType.Front));
            this.comboBox_Direction.Items.Add(new ComboboxItem("뒤에서", DirectionType.Back));
            this.comboBox_Direction2.Items.Add(new ComboboxItem("앞에서", DirectionType.Front));
            this.comboBox_Direction2.Items.Add(new ComboboxItem("뒤에서", DirectionType.Back));
        }

        private void apply_Click(object sender, EventArgs e)
        {
            //Get Input
            inc = Decimal.Parse(this.textBox_Inc.Text);
            upperPos = Decimal.Round(this.numericUpDown_upperPos.Value);
            underPos = Decimal.Round(this.numericUpDown_underPos.Value);
            
            opType = OperatorType.Plus;
            if (this.radioButtonPlus.Checked)
            {
                opType = OperatorType.Plus;
            }
            else if (this.radioButtonMinus.Checked)
            {
                opType = OperatorType.Minus;
            }
            else if (this.radioButtonMultiply.Checked)
            {
                opType = OperatorType.Multiply;
            }
            else if (this.radioButtonDivide.Checked)
            {
                opType = OperatorType.Divide;
            }

            bool semicolon = this.checkBox_Semicolon.Checked;
            bool hex = this.checkBox_Hex.Checked;
            bool partial = this.checkBox_Partial.Checked;

            //Find string & Change string to fit options
            string target = (string)this.listBox_Input.SelectedItem;
            string result = (string)this.listBox_Input.SelectedItem;
            string pattern = @"\d+(?:[,.]\d+)*";
            List<string> numbers = new List<string>();
            MatchCollection matches = Regex.Matches(target, pattern);

            if (partial)
            {
                //partial은 결국 pattern을 변화시켜 매칭을 바꾸는 것 뿐이다.
                if (this.radioButton_Specific.Checked)
                {
                    //일부 문자 중, 지정 문자 앞이나 뒤에서
                    DirectionType direction = (DirectionType)(this.comboBox_Direction2.SelectedItem as ComboboxItem).Value;
                    string specificChar = this.textBox_SpecificChar.Text.Trim();
                    if(direction == DirectionType.Back)
                    {
                        matches = Regex.Matches(target.Substring(target.IndexOf(specificChar)), pattern);
                    }
                    else
                    {
                        matches = Regex.Matches(target.Substring(0, target.LastIndexOf(specificChar)), pattern);
                    }
                }
                else
                {
                    //일부 문자 중, 몇 문자 앞이나 뒤에서
                    DirectionType direction = (DirectionType)(this.comboBox_Direction.SelectedItem as ComboboxItem).Value;
                    decimal charCount = Decimal.Round(this.numericUpDown_CharCount.Value);
                    if (direction == DirectionType.Back)
                    {
                        matches = Regex.Matches(target.Substring(target.Length - Decimal.ToInt16(charCount)), pattern);
                    }
                    else
                    {
                        matches = Regex.Matches(target.Substring(0, Decimal.ToInt16(charCount)), pattern);
                    }
                }
            }
            else
            {
                //문자 전체에 해당하는 번호를 바꾼다.
                pattern = @"\d+(?:[,.]\d+)*";
                matches = Regex.Matches(target, pattern);
            }

            //number Format을 결정하면 된다.
            //고려해야 되는 변수는 upperPos, underPos, semicolon, hex
            string numberFormat = "{0:";

            if (upperPos == 1 || hex)
            {
                if (hex)
                {
                    numberFormat += "X";
                }
                else if (semicolon)
                {
                    numberFormat += "n";
                }
                else
                {
                    numberFormat += "g";
                }
            }
            else
            {
                if (semicolon)
                {
                    numberFormat += "#,";
                }

                numberFormat += new string('0', Decimal.ToInt16(upperPos));

                if(underPos > 0)
                {
                    numberFormat += ".";
                    numberFormat += new string('0', Decimal.ToInt16(underPos));
                }
            }

            numberFormat += "}";

            foreach (Match match in matches)
            {
                decimal changedValue = Convert.ToDecimal(match.Value);

                if (this.opType == OperatorType.Plus)
                {
                    changedValue += inc;
                }
                else if (this.opType == OperatorType.Minus)
                {
                    changedValue -= inc;
                }
                else if (this.opType == OperatorType.Multiply)
                {
                    changedValue *= inc;
                }
                else if (this.opType == OperatorType.Divide)
                {
                    changedValue /= inc;
                }

                if (hex)
                {
                    result = result.Replace(match.Value, String.Format(numberFormat, Decimal.ToInt64(changedValue)));
                }
                else
                {
                    result = result.Replace(match.Value, String.Format(numberFormat, changedValue));
                }
                
            }

            //Show changed String!
            MessageBox.Show("\"" + target + "\" 이 변환되어 \"" + result + "\" 가 되었습니다.");
        }

        private void checkBox_Partial_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox_Partial.Enabled = this.checkBox_Partial.Checked;
            this.radioButton_General.Enabled = this.checkBox_Partial.Checked;
            this.radioButton_Specific.Enabled = this.checkBox_Partial.Checked;
            this.radioButton_General.Checked = this.checkBox_Partial.Checked;
        }

        private void radioButton_General_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBox_Direction.Enabled = this.radioButton_General.Checked;
            this.numericUpDown_CharCount.Enabled = this.radioButton_General.Checked;
            this.label5.Enabled = this.radioButton_General.Checked;
        }

        private void radioButton_Specific_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBox_Direction2.Enabled = this.radioButton_Specific.Checked;
            this.textBox_SpecificChar.Enabled = this.radioButton_Specific.Checked;
            this.label6.Enabled = this.radioButton_Specific.Checked;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public ComboboxItem(string text, object value)
        {
            this.Text = text;
            this.Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
