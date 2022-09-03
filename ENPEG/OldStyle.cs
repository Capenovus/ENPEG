using System;
using System.Windows.Forms;

namespace ENPEG
{
    public partial class OldStyle : Form
    {
        public OldStyle()
        {
            InitializeComponent();
        }

        private string path = string.Empty;
        private string iconpath = string.Empty;

        private void button1_Click(object sender, EventArgs e)
        {
            FolderPicker fp = new FolderPicker();
            fp.InputPath = "";
            if (fp.ShowDialog(IntPtr.Zero) == true)
            {
                if (fp.ResultPath != null)
                {
                    path = fp.ResultPath;
                    textBox1.Text = fp.ResultPath;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Filter = "Icons (*.ico)|*.ico";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    iconpath = fbd.FileName;
                    textBox2.Text = fbd.FileName;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (path == string.Empty || iconpath == string.Empty || textBox3.Text == string.Empty)
            {
                MessageBox.Show("Please set all Values first", "ERROR", MessageBoxButtons.OK);
                return;
            }
            RegeditGen.Create(textBox3.Text, path, iconpath);
        }
    }
}
