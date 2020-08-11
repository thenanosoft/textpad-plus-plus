using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextEditor.Functionality;

namespace TextEditor
{
    public partial class Main : Form
    {
        Main nestedform;
        String filename = "Untitled";
        EditOperation editOperation;
        FormFind formFind;
        FormReplace formReplace;
        Timer timer;

        public EditOperation EditOperation { get => editOperation; set => editOperation = value; }

        public Main()
        {
            InitializeComponent();
            editOperation = new EditOperation();
            formFind = new FormFind(this);
            formFind.Editor = TextEditor;
            TextEditor.DetectUrls = true;
            timer = new Timer();
            timer.Tick += myTimer_Tick;
            timer.Interval = 500;

            TextEditor.HideSelection = false;
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            editOperation.Add_UndoRedo(TextEditor.Text);
        }

        /*
         * *******************
         * Menu Bar Functions
         * *******************
         */

        // ---------------------------------------------------

        /*
         * ------------------
         * File Menu Options
         * ------------------
        */

        // "New" Option
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(closeConfirmation())
            {
                TextEditor.Clear();
            }
            else
            {
                return;
            }
        }


        // "New Window" Option
        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nestedform = new Main();
            nestedform.Show();
        }

        // 
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(closeConfirmation())
            {
                OpenFile();
            }
            else
            {
                return;
            }
        }

        // "Save" Option
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "Untitled")
            {
                saveAsToolStripMenuItem_Click(this, e);
                return;
            }
            string strExt;
            strExt = Path.GetExtension(filename);
            strExt = strExt.ToUpper();
            if (strExt == ".TXT")
            {
                TextEditor.SaveFile(filename);
            }
            else
            {
                StreamWriter txtWriter;
                txtWriter = new StreamWriter(filename);
                txtWriter.Write(TextEditor.Text);
                txtWriter.Close();
                txtWriter = null;
                TextEditor.SelectionStart = 0;
                TextEditor.SelectionLength = 0;
                TextEditor.Modified = false;
            }
            this.Text = filename + " - Textpad++";

        }

        // "Save As" Option
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
            {
                return;
            }
            string strExt;
            strExt = Path.GetExtension(saveFileDialog.FileName);
            strExt = strExt.ToUpper();
            if (strExt == ".TXT")
            {
                TextEditor.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
            }
            else
            {
                StreamWriter txtWriter;
                txtWriter = new StreamWriter(saveFileDialog.FileName);
                txtWriter.Write(TextEditor.Text);
                txtWriter.Close();
                txtWriter = null;
                TextEditor.SelectionStart = 0;
                TextEditor.SelectionLength = 0;
            }
            filename = saveFileDialog.FileName;
            TextEditor.Modified = false;
            this.Text = filename + " - Textpad++";
        }
        // "Page Setup" Option

        // "Print" Option
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        // "Exit" Option
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        // ---------------------------------------------------

        /*
         * ------------------
         * Edit Menu Options
         * ------------------
        */

        // "Undo" Option
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (TextEditor.CanUndo)
            //{
            //    TextEditor.Undo();
            //    TextEditor.ClearUndo();
            //}

            TextEditor.Text = editOperation.UndoClicked();

        }

        // "Redo" Option
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.Text = editOperation.RedoClicked();
        }

        // "Cut" Option

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (TextEditor.SelectionLength > 0)
                TextEditor.Cut();
        }

        // "Copy" Option

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextEditor.SelectionLength > 0)
                TextEditor.Copy();
        }

        // "Paste" Option
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                TextEditor.Paste();
            }
        }

        // "Delete" Option
        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if(TextEditor.TextLength > 0)
            {
                TextEditor.Text = TextEditor.Text.Remove(TextEditor.SelectionStart, TextEditor.SelectionLength);
            }
        }
        // "Search with Being" Option
        private void searchWithBingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string srchText = TextEditor.SelectedText;
            string srchString = "https://www.bing.com/search?q=" + srchText + "&form=NPCTXT";
            System.Diagnostics.Process.Start(srchString);
        }

        // "Find" Option
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formFind == null)
            {
                formFind = new FormFind(this);
                formFind.Editor = TextEditor;
            }
            formFind.Show();
        }

        // "Find Next" Option
        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formFind.UpdateSearchQuery();
            if (formFind.NextSearch.SearchString.Length == 0)
            {
                formFind.Show();
            }
            else
            {
                FindNextResult result = editOperation.findNext(formFind.NextSearch);
                if (result.SearchStatus)
                {
                    TextEditor.Select(result.SelectionStart, formFind.NextSearch.SearchString.Length);
                }
            }
        }

        // "Find Previous" Option

        // "Replace" Option
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(formReplace == null)
            {
                formReplace = new FormReplace();
                formReplace.Editor = TextEditor;
                formReplace.editOperation = editOperation;
            }
            formReplace.Show();
        }
        // "Go to" Option

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Line Number", "Go To Line", TextEditor.Lines.Length.ToString());
            try
            {
                int line = Convert.ToInt32(input);
                if (line > TextEditor.Lines.Length)
                {
                    MessageBox.Show("Total lines is beyond the total number of lines", "Textpad++ - Goto Line", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goToToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    string[] lines = TextEditor.Lines;
                    int len = 0;
                    for (int i = 0; i < line - 1; i++)
                    {
                        len = len + lines[i].Length + 1;
                    }
                    TextEditor.Focus();
                    TextEditor.Select(len, 0);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter a valid Integer", "Wrong Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // "Select All" Option
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextEditor.TextLength > 0)
            {
                TextEditor.SelectAll();
            }
        }

        // "De Select All" Option
        private void deSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextEditor.TextLength > 0)
            {
                TextEditor.DeselectAll();
            }
        }

        // "Time & Date" Option
        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            TextEditor.AppendText(dateTime.ToString());

            // TextEditor.Text = TextEditor.Text.Insert(TextEditor.SelectionStart, editOperation.DateTime_Now());
        }

        // ---------------------------------------------------

        /*
         * ------------------
         * Format Menu Options
         * ------------------
        */

        // "Word Wrap" Option
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.CheckState == CheckState.Checked)
            {
                TextEditor.WordWrap = true;
            }
            else
            {
                TextEditor.WordWrap = false;
            }
        }

        // "Font..." Option
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!(TextEditor.SelectionFont == null))
            {
                fontDialog.Font = TextEditor.SelectionFont;
            }
            else
            {
                fontDialog.Font = null;
            }
            fontDialog.ShowApply = true;
            if(fontDialog.ShowDialog() == DialogResult.OK)
            {
                TextEditor.SelectionFont = fontDialog.Font;
            }
        }

        // "Font Color" Option
        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.Color = TextEditor.ForeColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                TextEditor.SelectionColor = colorDialog.Color;
            }
        }

        // "Background Color" Option
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.Color = TextEditor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                TextEditor.BackColor = colorDialog.Color;
            }
        }


        // "Text Size" Option

        // "Text Style >> Bold" Option
        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextEditor.SelectionFont != null)
            {
                Font font = TextEditor.SelectionFont;
                FontStyle fontStyle;
                if (TextEditor.SelectionFont.Bold == true)
                {
                    fontStyle = FontStyle.Regular;
                }
                else
                {
                    fontStyle = FontStyle.Bold;
                }
                TextEditor.SelectionFont = new Font(font.FontFamily, font.Size, fontStyle);
            }
        }

        // "Text Style >> Regular" Option
        private void regularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextEditor.SelectionFont != null)
            {
                Font font = TextEditor.SelectionFont;
                FontStyle fontStyle;
                if (TextEditor.SelectionFont.Bold == true)
                {
                    fontStyle = FontStyle.Regular;
                    TextEditor.SelectionFont = new Font(font.FontFamily, font.Size, fontStyle);
                }
            }
        }

        // "Text Style >> Italic" Option
        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextEditor.SelectionFont != null)
            {
                Font font = TextEditor.SelectionFont;
                FontStyle fontStyle;
                if (TextEditor.SelectionFont.Italic == true)
                {
                    fontStyle = FontStyle.Regular;
                }
                else
                {
                    fontStyle = FontStyle.Italic;
                }
                TextEditor.SelectionFont = new Font(font.FontFamily, font.Size, fontStyle);
            }
        }

        // "Text Style >> Underline" Option
        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextEditor.SelectionFont != null)
            {
                Font font = TextEditor.SelectionFont;
                FontStyle fontStyle;
                if (TextEditor.SelectionFont.Underline == true)
                {
                    fontStyle = FontStyle.Regular;
                }
                else
                {
                    fontStyle = FontStyle.Underline;
                }
                TextEditor.SelectionFont = new Font(font.FontFamily, font.Size, fontStyle);
            }
        }

        // "Text Style >> Strikeout" Option
        private void strikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TextEditor.SelectionFont != null)
            {
                Font font = TextEditor.SelectionFont;
                FontStyle fontStyle;
                if (TextEditor.SelectionFont.Strikeout == true)
                {
                    fontStyle = FontStyle.Regular;
                }
                else
                {
                    fontStyle = FontStyle.Strikeout;
                }
                TextEditor.SelectionFont = new Font(font.FontFamily, font.Size, fontStyle);
            }
        }


        // "Align >> Left" Option
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.SelectionAlignment = HorizontalAlignment.Left;
        }

        // "Align >> Right" Option
        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.SelectionAlignment = HorizontalAlignment.Right;
        }

        // "Align >> Center" Option
        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.SelectionAlignment = HorizontalAlignment.Center;
        }

        // "Indent >> None" Option

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.SelectionIndent = 0;
        }

        // "Indent >> Right" Option
        private void ptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.SelectionIndent += 50;
        }

        // "Indent >> Left" Option
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TextEditor.SelectionIndent -= 50;
        }

        // ---------------------------------------------------

        /*
         * ------------------
         * View Menu Options
         * ------------------
        */

        // "Zoom  >> Zoom In" Option
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.AutoWordSelection = true;
            if (TextEditor.ZoomFactor == 10.0f)
            {

            }
            else
            {
                TextEditor.ZoomFactor += 1.0f;
            }

        }

        // "Zoom >> Zoom Out" Option
        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.AutoWordSelection = true;
            try
            {
                TextEditor.ZoomFactor -= 1.0f;
            }
            catch (Exception error)
            {

            }
        }

        // "Zoom >> Zoom Default" Option
        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor.AutoWordSelection = true;
            TextEditor.ZoomFactor = 1.0f;
        }

        // "Status Bar" Option
        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.CheckState == CheckState.Checked)
            {
                statusBar.Show();
            }
            else
            {
                statusBar.Hide();
            }
        }


        // ---------------------------------------------------

        /*
         * ------------------
         * Help Menu Options
         * ------------------
        */

        // "View Help" Option

        // "Send Feedback" Option

        // "About Textpad++" Option

        private void aboutTextEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        // ---------------------------------------------------

        /*
         * ------------------
         * Tool Strip Icons
         * [sorting by icons indexing]
         * ------------------
        */

        private void toolStripbtnNew_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender,e);
        }

        private void toolStripbtnOpen_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnSave_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnSaveAs_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnPrint_Click(object sender, EventArgs e)
        {
            printToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnUndo_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnCut_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem1_Click(sender, e);
        }

        private void toolStripbtnCopy_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnPaste_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnDelete_Click(object sender, EventArgs e)
        {
            deleteToolStripMenuItem_Click_1(sender, e);
        }

        private void toolStripbtnBold_Click(object sender, EventArgs e)
        {
            boldToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnItalic_Click(object sender, EventArgs e)
        {
            italicToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnUnderline_Click(object sender, EventArgs e)
        {
            underlineToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnStrikeout_Click(object sender, EventArgs e)
        {
            strikeoutToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnZoomIn_Click(object sender, EventArgs e)
        {
            zoomInToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnZoomOut_Click(object sender, EventArgs e)
        {
            zoomOutToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnLeftAlign_Click(object sender, EventArgs e)
        {
            leftToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnCenterAlign_Click(object sender, EventArgs e)
        {
            centerToolStripMenuItem_Click(sender, e);
        }

        private void toolStripbtnRightAlign_Click(object sender, EventArgs e)
        {
            rightToolStripMenuItem_Click(sender, e);
        }



        // -----------------------------------------
        // -----------------------------------------



        /*
         * **********************
         * Text Editor Functions
         * **********************
        */


        public void richTextEditorOptions()
        {
            TextEditor.WordWrap = false;
            TextEditor.ScrollBars = RichTextBoxScrollBars.ForcedBoth; 
            if(TextEditor.Modified)
            {
                this.Text = "* " + filename + " - Textpad++";
            }


            // status bar labels

            var lineCount = TextEditor.Lines.Count();
            toolStripStatusLineLabel.Text ="Lines: " + lineCount.ToString();

            toolStripStatusWordLabel.Text = "Word: " + wordCounter();

            

            var charCount = TextEditor.TextLength; 
            toolStripStatusCharLabel.Text = "Length: " + charCount.ToString();
        }

        

        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            richTextEditorOptions();
            if(editOperation.TxtAreaTextChangeRequired)
            {
                timer.Start();
            }
            else
            {
                editOperation.TxtAreaTextChangeRequired = false;
            }
        }
        private void TextEditor_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private  bool closeConfirmation()
        {
            var exitmessage = "The current document has not been saved, would you like to continue without saving ? ";
            if (TextEditor.Modified)
            {
                var answer = MessageBox.Show(exitmessage, filename, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.No)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

        }


        // Open file Function
        private void OpenFile()
        {
            openFileDialog.Filter = "Text Files|*.txt|HTML Files|*.htm; *.html|All Files|*.*";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName == "")
            {
                return;
            }

            string strExt;
            strExt = Path.GetExtension(openFileDialog.FileName);
            strExt = strExt.ToUpper();
            if (strExt == ".TXT")
            {
                TextEditor.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
            }
            else
            {
                try
                {
                    StreamReader txtReader;
                    txtReader = new StreamReader(openFileDialog.FileName);
                    TextEditor.Text = txtReader.ReadToEnd();
                    txtReader.Close();
                    txtReader = null;
                    TextEditor.SelectionStart = 0;
                    TextEditor.SelectionLength = 0;
                }
                catch (Exception error)
                {
                    return;
                }
            }

            
            filename = openFileDialog.FileName;
            TextEditor.Modified = false;
            this.Text = filename.ToString() + " - Textpad++";
        }

        

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog.Document = printDocument;
            pageSetupDialog.ShowDialog();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.github.com/thenanosoft");
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/thenanosoft");    
        }




        /*
         * Count Total Number of Words without white spaces & white lines
         */
        public int wordCounter()
        {
            int wordCount = 0, index = 0;
            

            // skip whitespace until first word
            while (index < TextEditor.Text.Length && char.IsWhiteSpace(TextEditor.Text[index]))
                index++;

            while (index < TextEditor.Text.Length)
            {
                // check if current char is part of a word
                while (index < TextEditor.Text.Length && !char.IsWhiteSpace(TextEditor.Text[index]))
                {
                    index++;
                }
                    

                wordCount++;

                // skip whitespace until next word
                while (index < TextEditor.Text.Length && char.IsWhiteSpace(TextEditor.Text[index]))
                    index++;
            }
            return wordCount;
        }

        

        private void toolStripbtnExit_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem_Click(sender, e);
        }

        private void forceResizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (forceResizeToolStripMenuItem.CheckState == CheckState.Checked)
            {
                this.MinimumSize = new Size(0, 0);
            }
            else
            {
                this.MinimumSize = new Size(400, 400);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem1.Enabled = TextEditor.SelectedText.Length > 0 ? true : false;
            copyToolStripMenuItem.Enabled = TextEditor.SelectedText.Length > 0 ? true : false;
            pasteToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent(DataFormats.Text);
            searchWithBingToolStripMenuItem.Enabled = TextEditor.SelectedText.Length > 0 ? true : false;

            findToolStripMenuItem.Enabled = TextEditor.Text.Length > 0 ? true : false;
            findNextToolStripMenuItem.Enabled = TextEditor.Text.Length > 0 ? true : false;
            findPreviousToolStripMenuItem.Enabled = TextEditor.Text.Length > 0 ? true : false;

            undoToolStripMenuItem.Enabled = editOperation.CanUndo() ? true : false;
            redoToolStripMenuItem.Enabled = editOperation.CanRedo() ? true : false;
            findNextToolStripMenuItem.Enabled = findNextToolStripMenuItem.Enabled;
        }

        
    }
}
