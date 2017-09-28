using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archiva
{
    public partial class FormArchiva : Form
    {
        private string path;
        private Interpreter interpreter;

        public FormArchiva()
        {
            InitializeComponent();
            path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\Archiva";
            Directory.CreateDirectory(path);

            //TODO: 디자인의 종류를 Excel에서 추가할 수 있도록 한다.
            this.comboBox_Design.Items.Add("Basic");
            this.comboBox_Design.Items.Add("Wood");
            this.comboBox_Design.SelectedItem = this.comboBox_Design.Items[1];

            //TODO: 현재 지원되는 지붕은 Flat 지붕이긴 한데, Unreal의 지붕이 생성되는 규칙 자체를 바꿀 필요가 있다.
            this.comboBox_Roof.Items.Add("Flat");
            this.comboBox_Roof.Items.Add("Hip");
            this.comboBox_Roof.SelectedItem = this.comboBox_Roof.Items[0];

        }

        private void createModel(object sender, EventArgs e)
        {
            if (!Directory.EnumerateFiles(path).Any())
            {
                DialogResult dialogResult = MessageBox.Show("Previous model already exist on " + path +"\nDo you want to overwrite on it?", "Create Models", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(path);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Failed to create models because directory is not clean.");
                }
            }

            double x = System.Convert.ToDouble(this.numericUpDown_wsl_x.Value);
            double y = System.Convert.ToDouble(this.numericUpDown_wsl_y.Value);
            double z = System.Convert.ToDouble(this.numericUpDown_wsl_z.Value);
            Data.Cord worldloc = new Data.Cord(x, y, z);
            double max = System.Convert.ToDouble(this.numericUpDown_maxspace.Value);
            int bed = System.Convert.ToInt32(this.numericUpDown_bednum.Value);
            int rest = System.Convert.ToInt32(this.numericUpDown_restnum.Value);

            bool simple = true;

            if (this.radioButton_complicate.Checked)
            {
                simple = false;
            }
            else if (this.radioButton_simple.Checked)
            {
                simple = true;
            }

            string design = (string)this.comboBox_Design.SelectedItem;
            string roof = (string)this.comboBox_Roof.SelectedItem;

            Variables var = new Variables(max, worldloc, bed, rest, simple, design, roof);
            interpreter = new Interpreter(var);
            List<List<Blueprint>> all = interpreter.GenerateBlueprints();

            //Debug Code
            int count = all.Count;
            /*
            if (count > 50)
            {
                count = 50;
            }
            */

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < all[i].Count; j++)
                {
                    all[i][j].CreateXML(path + "\\test" + (i + 1) + "-" + (j + 1) + ".xml");
                }
            }

            DialogResult closeit = MessageBox.Show("The calculations are complete. Do you want to close it?", "Archiva", MessageBoxButtons.YesNo);


            if (closeit == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
