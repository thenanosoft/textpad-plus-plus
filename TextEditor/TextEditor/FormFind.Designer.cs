namespace TextEditor
{
    partial class FormFind
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFind));
            this.txtSrch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBxMatchCase = new System.Windows.Forms.CheckBox();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbtnDown = new System.Windows.Forms.RadioButton();
            this.rdbtnUp = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSrch
            // 
            this.txtSrch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrch.Location = new System.Drawing.Point(21, 40);
            this.txtSrch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSrch.Name = "txtSrch";
            this.txtSrch.Size = new System.Drawing.Size(300, 23);
            this.txtSrch.TabIndex = 0;
            this.txtSrch.TextChanged += new System.EventHandler(this.txtSrch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find What";
            // 
            // chkBxMatchCase
            // 
            this.chkBxMatchCase.AutoSize = true;
            this.chkBxMatchCase.Location = new System.Drawing.Point(21, 100);
            this.chkBxMatchCase.Margin = new System.Windows.Forms.Padding(2);
            this.chkBxMatchCase.Name = "chkBxMatchCase";
            this.chkBxMatchCase.Size = new System.Drawing.Size(83, 17);
            this.chkBxMatchCase.TabIndex = 2;
            this.chkBxMatchCase.Text = "Match Case";
            this.chkBxMatchCase.UseVisualStyleBackColor = true;
            this.chkBxMatchCase.CheckedChanged += new System.EventHandler(this.chkBxMatchCase_CheckedChanged);
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(335, 38);
            this.btnFindNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(84, 25);
            this.btnFindNext.TabIndex = 3;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(335, 76);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbtnDown);
            this.panel1.Controls.Add(this.rdbtnUp);
            this.panel1.Location = new System.Drawing.Point(137, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 41);
            this.panel1.TabIndex = 5;
            // 
            // rdbtnDown
            // 
            this.rdbtnDown.AutoSize = true;
            this.rdbtnDown.Checked = true;
            this.rdbtnDown.Location = new System.Drawing.Point(90, 16);
            this.rdbtnDown.Name = "rdbtnDown";
            this.rdbtnDown.Size = new System.Drawing.Size(53, 17);
            this.rdbtnDown.TabIndex = 1;
            this.rdbtnDown.TabStop = true;
            this.rdbtnDown.Text = "Down";
            this.rdbtnDown.UseVisualStyleBackColor = true;
            this.rdbtnDown.CheckedChanged += new System.EventHandler(this.rdbtnDown_CheckedChanged);
            // 
            // rdbtnUp
            // 
            this.rdbtnUp.AutoSize = true;
            this.rdbtnUp.Location = new System.Drawing.Point(29, 16);
            this.rdbtnUp.Name = "rdbtnUp";
            this.rdbtnUp.Size = new System.Drawing.Size(39, 17);
            this.rdbtnUp.TabIndex = 0;
            this.rdbtnUp.Text = "Up";
            this.rdbtnUp.UseVisualStyleBackColor = true;
            this.rdbtnUp.CheckedChanged += new System.EventHandler(this.rdbtnUp_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Direction";
            // 
            // FormFind
            // 
            this.AcceptButton = this.btnFindNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 129);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.chkBxMatchCase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSrch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFind";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFind_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSrch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBxMatchCase;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbtnUp;
        private System.Windows.Forms.RadioButton rdbtnDown;
        private System.Windows.Forms.Label label2;
    }
}