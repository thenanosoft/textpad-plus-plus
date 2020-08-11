using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditor.Functionality;

namespace TextEditor
{
    public partial class FormReplace : Form
    {
        FindNextSearch nextSearch = new FindNextSearch();

        public RichTextBox Editor;

        public EditOperation editOperation;
        public FormReplace()
        {
            InitializeComponent();
        }

        public FindNextSearch NextSearch { get => nextSearch; set => nextSearch = value; }

        private void FormReplace_Load(object sender, EventArgs e)
        {
            DisableButtons();
            rdbtnDown.Checked = true;
        }

        private void DisableButtons()
        {
            if(txtSrch.Text.Length == 0)
            {
                btnFindNext.Enabled = btnReplace.Enabled = btnReplaceAll.Enabled = false;
            }
            else
            {
                btnFindNext.Enabled = btnReplace.Enabled = btnReplaceAll.Enabled = true;
            }
        }

        private void txtSrch_TextChanged(object sender, EventArgs e)
        {
            DisableButtons();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            UpdateSearchQuery();
            FindNextResult result = editOperation.findNext(this.nextSearch);
            if (result.SearchStatus)
            {
                this.Editor.Select(result.SelectionStart, txtSrch.Text.Length);
            }
        }

        public void UpdateSearchQuery()
        {
            nextSearch.SearchString = txtSrch.Text;
            nextSearch.Direction = rdbtnUp.Checked ? "UP" : "DOWN";
            nextSearch.MatchCase = chkBxMatchCase.Checked;
            nextSearch.Content = Editor.Text;
            nextSearch.Position = Editor.SelectionStart;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if(Editor.SelectionLength == 0)
            {
                btnFindNext.PerformClick();
            }
            else
            {
                Editor.SelectedText = txtReplace.Text;
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            Editor.Text = Editor.Text.Replace(txtSrch.Text, txtReplace.Text);
        }
    }
}
