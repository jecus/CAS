namespace CAS.UI.UIControls.QualityAssuranceControls
{
    partial class ProcedureDocRefListControl
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
            this.label11 = new System.Windows.Forms.Label();
            this.flowLayoutPanelMain = new System.Windows.Forms.FlowLayoutPanel();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.linkLabelAddNew = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanelMain.SuspendLayout();
            this.panelAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "Document Referencres";
            // 
            // flowLayoutPanelMain
            // 
            this.flowLayoutPanelMain.AutoScroll = true;
            this.flowLayoutPanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.flowLayoutPanelMain.Controls.Add(this.panelAdd);
            this.flowLayoutPanelMain.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanelMain.MinimumSize = new System.Drawing.Size(480, 25);
            this.flowLayoutPanelMain.Name = "flowLayoutPanelMain";
            this.flowLayoutPanelMain.Size = new System.Drawing.Size(721, 210);
            this.flowLayoutPanelMain.TabIndex = 41;
            // 
            // panelAdd
            // 
            this.panelAdd.Controls.Add(this.linkLabelAddNew);
            this.panelAdd.Location = new System.Drawing.Point(0, 0);
            this.panelAdd.Margin = new System.Windows.Forms.Padding(0);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(700, 30);
            this.panelAdd.TabIndex = 3;
            // 
            // linkLabelAddNew
            // 
            this.linkLabelAddNew.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabelAddNew.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(155)))), ((int)(((byte)(246)))));
            this.linkLabelAddNew.Location = new System.Drawing.Point(315, 4);
            this.linkLabelAddNew.Name = "linkLabelAddNew";
            this.linkLabelAddNew.Size = new System.Drawing.Size(70, 23);
            this.linkLabelAddNew.TabIndex = 2;
            this.linkLabelAddNew.TabStop = true;
            this.linkLabelAddNew.Text = "Add new";
            this.linkLabelAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelAddNew.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelAddNewLinkClicked);
            // 
            // ProcedureDocRefListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.flowLayoutPanelMain);
            this.Controls.Add(this.label11);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ProcedureDocRefListControl";
            this.Size = new System.Drawing.Size(727, 230);
            this.flowLayoutPanelMain.ResumeLayout(false);
            this.panelAdd.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMain;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.LinkLabel linkLabelAddNew;
    }
}
