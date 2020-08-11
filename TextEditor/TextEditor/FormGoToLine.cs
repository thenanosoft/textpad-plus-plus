using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class FormGoToLine : Form
    {
        Main mainForm = new Main();
        public FormGoToLine()
        {
            InitializeComponent();
            MessageBox.Show(mainForm.TextEditor.Lines.Length.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int line = Convert.ToInt32(txtLineNumber.Text);
                if (line > mainForm.TextEditor.Lines.Length)
                {
                    MessageBox.Show("Total lines is beyond the total number of lines", "Textpad++ - Goto Line", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string[] lines = mainForm.TextEditor.Lines;
                    int len = 0;
                    for (int i = 0; i < line - 1; i++)
                    {
                        len = len + lines[i].Length + 1;
                    }
                    mainForm.TextEditor.Focus();
                    mainForm.TextEditor.Select(len, 0);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void txtLineNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
