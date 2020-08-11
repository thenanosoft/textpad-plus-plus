using System;
using System.Windows.Forms;
using TextEditor.Functionality;

namespace TextEditor
{
    public partial class FormFind : Form
    {
        Main mainForm;
        EditOperation editOperation;
        FindNextSearch nextSearch = new FindNextSearch();

        public RichTextBox Editor { get; internal set; }
        public FindNextSearch NextSearch { get => nextSearch; set => nextSearch = value; }

        public FormFind(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            rdbtnDown.Checked = true;
            btnFindNext.Enabled = false;
            editOperation = mainForm.EditOperation;
            nextSearch.Success = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSrch_TextChanged(object sender, EventArgs e)
        {
            btnFindNext.Enabled = (txtSrch.Text.Length > 0) ? true : false;
            UpdateSearchQuery();
        }

        public void UpdateSearchQuery()
        {
            nextSearch.SearchString = txtSrch.Text;
            nextSearch.Direction = rdbtnUp.Checked ? "UP" : "DOWN";
            nextSearch.MatchCase = chkBxMatchCase.Checked;
            nextSearch.Content = Editor.Text;
            nextSearch.Position = Editor.SelectionStart;
        }

        private void chkBxMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSearchQuery();
        }

        private void rdbtnUp_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSearchQuery();
        }

        private void FormFind_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            UpdateSearchQuery();
            FindNextResult result = editOperation.findNext(nextSearch);
            if(result.SearchStatus)
            {
                Editor.Select(result.SelectionStart, txtSrch.Text.Length);
            }
            else
            {
                MessageBox.Show("Result Completed...");
            }
        }

        private void rdbtnDown_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSearchQuery();
        }
    }
}
