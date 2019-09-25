﻿using System.Windows.Forms;
using MetroFramework.Controls;
using CASTerms;
using EntityCore.DTO.General;
using CAS.UI.Helpers;

namespace CAS.UI.UIControls.PurchaseControls.Purchase
{
	partial class PurchaseOrderForm
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
			var userType = GlobalObjects.CasEnvironment.IdentityUser.UserType;
			this.ButtonDelete = new AvControls.AvButtonT.AvButtonT();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxCurrency = new System.Windows.Forms.ComboBox();
			this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.button1 = new System.Windows.Forms.Button();
			this.numericUpDownQuantity = new System.Windows.Forms.NumericUpDown();
			this.labelQuantity = new MetroFramework.Controls.MetroLabel();
			this.comboBoxMeasure = new System.Windows.Forms.ComboBox();
			this.labelMeasure = new MetroFramework.Controls.MetroLabel();
			this.labelTotal = new MetroFramework.Controls.MetroLabel();
			this.textBoxTotal = new MetroFramework.Controls.MetroTextBox();
			this.labelReason = new MetroFramework.Controls.MetroLabel();
			this.comboBoxCondition = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.metroTextBoxNumber = new MetroFramework.Controls.MetroTextBox();
			this.metroLabelNumber = new MetroFramework.Controls.MetroLabel();
			this.textBoxClosingBy = new MetroFramework.Controls.MetroTextBox();
			this.textBoxPublishedBy = new MetroFramework.Controls.MetroTextBox();
			this.comboBoxStatus = new System.Windows.Forms.ComboBox();
			this.textBoxRemarks = new MetroFramework.Controls.MetroTextBox();
			this.labelRemarks = new MetroFramework.Controls.MetroLabel();
			this.labelClosedBy = new MetroFramework.Controls.MetroLabel();
			this.labelClosingDate = new MetroFramework.Controls.MetroLabel();
			this.dateTimePickerClosingDate = new System.Windows.Forms.DateTimePicker();
			this.labelPublishDate = new MetroFramework.Controls.MetroLabel();
			this.dateTimePickerPublishDate = new System.Windows.Forms.DateTimePicker();
			this.label5 = new MetroFramework.Controls.MetroLabel();
			this.dateTimePickerOpeningDate = new System.Windows.Forms.DateTimePicker();
			this.labelOpeningDate = new MetroFramework.Controls.MetroLabel();
			this.labelStatus = new MetroFramework.Controls.MetroLabel();
			this.textBoxAuthor = new MetroFramework.Controls.MetroTextBox();
			this.labelAuthor = new MetroFramework.Controls.MetroLabel();
			this.textBoxTitle = new MetroFramework.Controls.MetroTextBox();
			this.labelQOTitle = new MetroFramework.Controls.MetroLabel();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.purchaseRecordListView1 = new CAS.UI.UIControls.PurchaseControls.Purchase.PurchaseRecordListView(true);
			this.documentControl1 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.documentControl2 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.documentControl3 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.documentControl4 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.documentControl5 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.documentControl6 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.documentControl7 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.documentControl8 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.documentControl9 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.documentControl10 = new CAS.UI.UIControls.DocumentationControls.DocumentControl();
			this.comboBoxDesignation = new System.Windows.Forms.ComboBox();
			this.metroLabelDesignation = new MetroFramework.Controls.MetroLabel();
			this.comboBoxPayTerm = new System.Windows.Forms.ComboBox();
			this.metroLabelPayTerm = new MetroFramework.Controls.MetroLabel();
			this.comboBoxIncoTerm = new System.Windows.Forms.ComboBox();
			this.metroLabelIncoTerm = new MetroFramework.Controls.MetroLabel();
			this.comboBoxShipComp = new System.Windows.Forms.ComboBox();
			this.metroLabelShippingCompany = new MetroFramework.Controls.MetroLabel();
			this.textBoxShipTo = new MetroFramework.Controls.MetroTextBox();
			this.metroLabelShipTo = new MetroFramework.Controls.MetroLabel();
			this.textBoxCargoVolume = new MetroTextBox();
			this.metroLabelCargoVolume = new MetroFramework.Controls.MetroLabel();
			this.textBoxBruttoWeight = new MetroTextBox();
			this.metroLabelBruttoWeight = new MetroFramework.Controls.MetroLabel();
			this.textBoxNettoWeight = new MetroTextBox();
			this.metroLabelNettoWeight = new MetroFramework.Controls.MetroLabel();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonDelete
			// 
			this.ButtonDelete.ActiveBackColor = System.Drawing.Color.Transparent;
			this.ButtonDelete.ActiveBackgroundImage = null;
			this.ButtonDelete.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ButtonDelete.FontMain = new System.Drawing.Font("Verdana", 8F);
			this.ButtonDelete.FontSecondary = new System.Drawing.Font("Verdana", 8F);
			this.ButtonDelete.ForeColorMain = System.Drawing.SystemColors.ControlText;
			this.ButtonDelete.ForeColorSecondary = System.Drawing.SystemColors.ControlText;
			this.ButtonDelete.Icon = global::CAS.UI.Properties.Resources.DeleteIconSmall;
			this.ButtonDelete.IconLayout = System.Windows.Forms.ImageLayout.Center;
			this.ButtonDelete.IconNotEnabled = null;
			this.ButtonDelete.Location = new System.Drawing.Point(588, 423);
			this.ButtonDelete.Margin = new System.Windows.Forms.Padding(4);
			this.ButtonDelete.Name = "ButtonDelete";
			this.ButtonDelete.NormalBackgroundImage = null;
			this.ButtonDelete.PaddingMain = new System.Windows.Forms.Padding(0);
			this.ButtonDelete.PaddingSecondary = new System.Windows.Forms.Padding(0);
			this.ButtonDelete.ShowToolTip = false;
			this.ButtonDelete.Size = new System.Drawing.Size(122, 22);
			this.ButtonDelete.TabIndex = 289;
			this.ButtonDelete.TextAlignMain = System.Drawing.ContentAlignment.MiddleLeft;
			this.ButtonDelete.TextAlignSecondary = System.Drawing.ContentAlignment.MiddleLeft;
			this.ButtonDelete.TextMain = "Delete Selected";
			this.ButtonDelete.TextSecondary = "";
			this.ButtonDelete.ToolTipText = "";
			this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBoxCurrency);
			this.groupBox1.Controls.Add(this.metroLabel2);
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Controls.Add(this.metroLabel1);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.numericUpDownQuantity);
			this.groupBox1.Controls.Add(this.labelQuantity);
			this.groupBox1.Controls.Add(this.comboBoxMeasure);
			this.groupBox1.Controls.Add(this.labelMeasure);
			this.groupBox1.Controls.Add(this.labelTotal);
			this.groupBox1.Controls.Add(this.textBoxTotal);
			this.groupBox1.Controls.Add(this.labelReason);
			this.groupBox1.Controls.Add(this.comboBoxCondition);
			this.groupBox1.Location = new System.Drawing.Point(717, 37);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(290, 229);
			this.groupBox1.TabIndex = 290;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Selected Product";
			// 
			// comboBoxCurrency
			// 
			this.comboBoxCurrency.Enabled = false;
			this.comboBoxCurrency.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.comboBoxCurrency.FormattingEnabled = true;
			this.comboBoxCurrency.ItemHeight = 17;
			this.comboBoxCurrency.Location = new System.Drawing.Point(118, 80);
			this.comboBoxCurrency.Name = "comboBoxCurrency";
			this.comboBoxCurrency.Size = new System.Drawing.Size(165, 25);
			this.comboBoxCurrency.TabIndex = 252;
			this.comboBoxCurrency.MouseWheel += CmbScrollHelper.ComboBoxScroll_MouseWheel;
			// 
			// metroLabel2
			// 
			this.metroLabel2.AutoSize = true;
			this.metroLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabel2.Location = new System.Drawing.Point(20, 82);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new System.Drawing.Size(64, 19);
			this.metroLabel2.TabIndex = 253;
			this.metroLabel2.Text = "Currency:";
			this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.DecimalPlaces = 2;
			this.numericUpDown1.Enabled = false;
			this.numericUpDown1.Location = new System.Drawing.Point(118, 136);
			this.numericUpDown1.Maximum = new decimal(new int[] {
			1000000,
			0,
			0,
			0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(165, 20);
			this.numericUpDown1.TabIndex = 250;
			// 
			// metroLabel1
			// 
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabel1.Location = new System.Drawing.Point(21, 136);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new System.Drawing.Size(38, 19);
			this.metroLabel1.TabIndex = 251;
			this.metroLabel1.Text = "Cost:";
			this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(155)))), ((int)(((byte)(246)))));
			this.button1.Location = new System.Drawing.Point(207, 190);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 33);
			this.button1.TabIndex = 249;
			this.button1.Text = "Apply";
			this.button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// numericUpDownQuantity
			// 
			this.numericUpDownQuantity.DecimalPlaces = 2;
			this.numericUpDownQuantity.Location = new System.Drawing.Point(118, 110);
			this.numericUpDownQuantity.Maximum = new decimal(new int[] {
			1000000,
			0,
			0,
			0});
			this.numericUpDownQuantity.Name = "numericUpDownQuantity";
			this.numericUpDownQuantity.Size = new System.Drawing.Size(165, 20);
			this.numericUpDownQuantity.TabIndex = 142;
			this.numericUpDownQuantity.ValueChanged += new System.EventHandler(this.NumericUpDownQuantity_ValueChanged);
			// 
			// labelQuantity
			// 
			this.labelQuantity.AutoSize = true;
			this.labelQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelQuantity.Location = new System.Drawing.Point(21, 110);
			this.labelQuantity.Name = "labelQuantity";
			this.labelQuantity.Size = new System.Drawing.Size(61, 19);
			this.labelQuantity.TabIndex = 158;
			this.labelQuantity.Text = "Quantity:";
			this.labelQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxMeasure
			// 
			this.comboBoxMeasure.Enabled = false;
			this.comboBoxMeasure.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.comboBoxMeasure.FormattingEnabled = true;
			this.comboBoxMeasure.ItemHeight = 17;
			this.comboBoxMeasure.Location = new System.Drawing.Point(118, 49);
			this.comboBoxMeasure.Name = "comboBoxMeasure";
			this.comboBoxMeasure.Size = new System.Drawing.Size(165, 25);
			this.comboBoxMeasure.TabIndex = 141;
			this.comboBoxMeasure.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMeasure_SelectedIndexChanged);
			this.comboBoxMeasure.MouseWheel += CmbScrollHelper.ComboBoxScroll_MouseWheel;
			// 
			// labelMeasure
			// 
			this.labelMeasure.AutoSize = true;
			this.labelMeasure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelMeasure.Location = new System.Drawing.Point(20, 51);
			this.labelMeasure.Name = "labelMeasure";
			this.labelMeasure.Size = new System.Drawing.Size(62, 19);
			this.labelMeasure.TabIndex = 165;
			this.labelMeasure.Text = "Measure:";
			this.labelMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelTotal
			// 
			this.labelTotal.AutoSize = true;
			this.labelTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelTotal.Location = new System.Drawing.Point(21, 163);
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.Size = new System.Drawing.Size(39, 19);
			this.labelTotal.TabIndex = 168;
			this.labelTotal.Text = "Total:";
			this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxTotal
			// 
			this.textBoxTotal.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.textBoxTotal.CustomButton.Image = null;
			this.textBoxTotal.CustomButton.Location = new System.Drawing.Point(146, 2);
			this.textBoxTotal.CustomButton.Name = "";
			this.textBoxTotal.CustomButton.Size = new System.Drawing.Size(17, 17);
			this.textBoxTotal.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxTotal.CustomButton.TabIndex = 1;
			this.textBoxTotal.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxTotal.CustomButton.UseSelectable = true;
			this.textBoxTotal.CustomButton.Visible = false;
			this.textBoxTotal.ForeColor = System.Drawing.Color.Black;
			this.textBoxTotal.Lines = new string[0];
			this.textBoxTotal.Location = new System.Drawing.Point(118, 162);
			this.textBoxTotal.MaxLength = 128;
			this.textBoxTotal.Name = "textBoxTotal";
			this.textBoxTotal.PasswordChar = '\0';
			this.textBoxTotal.ReadOnly = true;
			this.textBoxTotal.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxTotal.SelectedText = "";
			this.textBoxTotal.SelectionLength = 0;
			this.textBoxTotal.SelectionStart = 0;
			this.textBoxTotal.ShortcutsEnabled = true;
			this.textBoxTotal.Size = new System.Drawing.Size(166, 22);
			this.textBoxTotal.TabIndex = 143;
			this.textBoxTotal.UseSelectable = true;
			this.textBoxTotal.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxTotal.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// labelReason
			// 
			this.labelReason.AutoSize = true;
			this.labelReason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelReason.Location = new System.Drawing.Point(20, 21);
			this.labelReason.Name = "labelReason";
			this.labelReason.Size = new System.Drawing.Size(69, 19);
			this.labelReason.TabIndex = 170;
			this.labelReason.Text = "Condition:";
			// 
			// comboBoxCondition
			// 
			this.comboBoxCondition.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.comboBoxCondition.FormattingEnabled = true;
			this.comboBoxCondition.ItemHeight = 17;
			this.comboBoxCondition.Location = new System.Drawing.Point(118, 18);
			this.comboBoxCondition.Name = "comboBoxCondition";
			this.comboBoxCondition.Size = new System.Drawing.Size(165, 25);
			this.comboBoxCondition.TabIndex = 169;
			this.comboBoxCondition.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCondition_SelectedIndexChanged);
			this.comboBoxCondition.MouseWheel += CmbScrollHelper.ComboBoxScroll_MouseWheel;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.metroTextBoxNumber);
			this.groupBox2.Controls.Add(this.metroLabelNumber);
			this.groupBox2.Controls.Add(this.textBoxClosingBy);
			this.groupBox2.Controls.Add(this.textBoxPublishedBy);
			this.groupBox2.Controls.Add(this.comboBoxStatus);
			this.groupBox2.Controls.Add(this.textBoxRemarks);
			this.groupBox2.Controls.Add(this.labelRemarks);
			this.groupBox2.Controls.Add(this.labelClosedBy);
			this.groupBox2.Controls.Add(this.labelClosingDate);
			this.groupBox2.Controls.Add(this.dateTimePickerClosingDate);
			this.groupBox2.Controls.Add(this.labelPublishDate);
			this.groupBox2.Controls.Add(this.dateTimePickerPublishDate);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.dateTimePickerOpeningDate);
			this.groupBox2.Controls.Add(this.labelOpeningDate);
			this.groupBox2.Controls.Add(this.labelStatus);
			this.groupBox2.Controls.Add(this.textBoxAuthor);
			this.groupBox2.Controls.Add(this.labelAuthor);
			this.groupBox2.Controls.Add(this.textBoxTitle);
			this.groupBox2.Controls.Add(this.labelQOTitle);
			this.groupBox2.Location = new System.Drawing.Point(717, 272);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(290, 304);
			this.groupBox2.TabIndex = 291;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Purchase";
			// 
			// metroTextBoxNumber
			// 
			// 
			// 
			// 
			this.metroTextBoxNumber.CustomButton.Image = null;
			this.metroTextBoxNumber.CustomButton.Location = new System.Drawing.Point(146, 2);
			this.metroTextBoxNumber.CustomButton.Name = "";
			this.metroTextBoxNumber.CustomButton.Size = new System.Drawing.Size(17, 17);
			this.metroTextBoxNumber.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.metroTextBoxNumber.CustomButton.TabIndex = 1;
			this.metroTextBoxNumber.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.metroTextBoxNumber.CustomButton.UseSelectable = true;
			this.metroTextBoxNumber.CustomButton.Visible = false;
			this.metroTextBoxNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroTextBoxNumber.Lines = new string[0];
			this.metroTextBoxNumber.Location = new System.Drawing.Point(118, 19);
			this.metroTextBoxNumber.MaxLength = 32767;
			this.metroTextBoxNumber.Name = "metroTextBoxNumber";
			this.metroTextBoxNumber.PasswordChar = '\0';
			this.metroTextBoxNumber.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.metroTextBoxNumber.SelectedText = "";
			this.metroTextBoxNumber.SelectionLength = 0;
			this.metroTextBoxNumber.SelectionStart = 0;
			this.metroTextBoxNumber.ShortcutsEnabled = true;
			this.metroTextBoxNumber.Size = new System.Drawing.Size(166, 22);
			this.metroTextBoxNumber.TabIndex = 268;
			this.metroTextBoxNumber.UseSelectable = true;
			this.metroTextBoxNumber.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.metroTextBoxNumber.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// metroLabelNumber
			// 
			this.metroLabelNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabelNumber.Location = new System.Drawing.Point(20, 16);
			this.metroLabelNumber.Name = "metroLabelNumber";
			this.metroLabelNumber.Size = new System.Drawing.Size(87, 27);
			this.metroLabelNumber.TabIndex = 269;
			this.metroLabelNumber.Text = "№:";
			this.metroLabelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxClosingBy
			// 
			// 
			// 
			// 
			this.textBoxClosingBy.CustomButton.Image = null;
			this.textBoxClosingBy.CustomButton.Location = new System.Drawing.Point(146, 2);
			this.textBoxClosingBy.CustomButton.Name = "";
			this.textBoxClosingBy.CustomButton.Size = new System.Drawing.Size(17, 17);
			this.textBoxClosingBy.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxClosingBy.CustomButton.TabIndex = 1;
			this.textBoxClosingBy.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxClosingBy.CustomButton.UseSelectable = true;
			this.textBoxClosingBy.CustomButton.Visible = false;
			this.textBoxClosingBy.Enabled = false;
			this.textBoxClosingBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.textBoxClosingBy.Lines = new string[0];
			this.textBoxClosingBy.Location = new System.Drawing.Point(118, 242);
			this.textBoxClosingBy.MaxLength = 32767;
			this.textBoxClosingBy.Name = "textBoxClosingBy";
			this.textBoxClosingBy.PasswordChar = '\0';
			this.textBoxClosingBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxClosingBy.SelectedText = "";
			this.textBoxClosingBy.SelectionLength = 0;
			this.textBoxClosingBy.SelectionStart = 0;
			this.textBoxClosingBy.ShortcutsEnabled = true;
			this.textBoxClosingBy.Size = new System.Drawing.Size(166, 22);
			this.textBoxClosingBy.TabIndex = 267;
			this.textBoxClosingBy.UseSelectable = true;
			this.textBoxClosingBy.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxClosingBy.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// textBoxPublishedBy
			// 
			// 
			// 
			// 
			this.textBoxPublishedBy.CustomButton.Image = null;
			this.textBoxPublishedBy.CustomButton.Location = new System.Drawing.Point(146, 2);
			this.textBoxPublishedBy.CustomButton.Name = "";
			this.textBoxPublishedBy.CustomButton.Size = new System.Drawing.Size(17, 17);
			this.textBoxPublishedBy.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxPublishedBy.CustomButton.TabIndex = 1;
			this.textBoxPublishedBy.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxPublishedBy.CustomButton.UseSelectable = true;
			this.textBoxPublishedBy.CustomButton.Visible = false;
			this.textBoxPublishedBy.Enabled = false;
			this.textBoxPublishedBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.textBoxPublishedBy.Lines = new string[0];
			this.textBoxPublishedBy.Location = new System.Drawing.Point(118, 187);
			this.textBoxPublishedBy.MaxLength = 32767;
			this.textBoxPublishedBy.Name = "textBoxPublishedBy";
			this.textBoxPublishedBy.PasswordChar = '\0';
			this.textBoxPublishedBy.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxPublishedBy.SelectedText = "";
			this.textBoxPublishedBy.SelectionLength = 0;
			this.textBoxPublishedBy.SelectionStart = 0;
			this.textBoxPublishedBy.ShortcutsEnabled = true;
			this.textBoxPublishedBy.Size = new System.Drawing.Size(166, 22);
			this.textBoxPublishedBy.TabIndex = 266;
			this.textBoxPublishedBy.UseSelectable = true;
			this.textBoxPublishedBy.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxPublishedBy.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// comboBoxStatus
			// 
			this.comboBoxStatus.Enabled = false;
			this.comboBoxStatus.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.comboBoxStatus.FormattingEnabled = true;
			this.comboBoxStatus.ItemHeight = 17;
			this.comboBoxStatus.Location = new System.Drawing.Point(118, 102);
			this.comboBoxStatus.Name = "comboBoxStatus";
			this.comboBoxStatus.Size = new System.Drawing.Size(165, 25);
			this.comboBoxStatus.TabIndex = 265;
			this.comboBoxStatus.MouseWheel += CmbScrollHelper.ComboBoxScroll_MouseWheel;
			// 
			// textBoxRemarks
			// 
			// 
			// 
			// 
			this.textBoxRemarks.CustomButton.Image = null;
			this.textBoxRemarks.CustomButton.Location = new System.Drawing.Point(146, 2);
			this.textBoxRemarks.CustomButton.Name = "";
			this.textBoxRemarks.CustomButton.Size = new System.Drawing.Size(17, 17);
			this.textBoxRemarks.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxRemarks.CustomButton.TabIndex = 1;
			this.textBoxRemarks.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxRemarks.CustomButton.UseSelectable = true;
			this.textBoxRemarks.CustomButton.Visible = false;
			this.textBoxRemarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.textBoxRemarks.Lines = new string[0];
			this.textBoxRemarks.Location = new System.Drawing.Point(118, 273);
			this.textBoxRemarks.MaxLength = 32767;
			this.textBoxRemarks.Name = "textBoxRemarks";
			this.textBoxRemarks.PasswordChar = '\0';
			this.textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxRemarks.SelectedText = "";
			this.textBoxRemarks.SelectionLength = 0;
			this.textBoxRemarks.SelectionStart = 0;
			this.textBoxRemarks.ShortcutsEnabled = true;
			this.textBoxRemarks.Size = new System.Drawing.Size(166, 22);
			this.textBoxRemarks.TabIndex = 264;
			this.textBoxRemarks.UseSelectable = true;
			this.textBoxRemarks.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxRemarks.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// labelRemarks
			// 
			this.labelRemarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelRemarks.Location = new System.Drawing.Point(21, 273);
			this.labelRemarks.Name = "labelRemarks";
			this.labelRemarks.Size = new System.Drawing.Size(69, 23);
			this.labelRemarks.TabIndex = 263;
			this.labelRemarks.Text = "Remarks:";
			this.labelRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelClosedBy
			// 
			this.labelClosedBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelClosedBy.Location = new System.Drawing.Point(20, 240);
			this.labelClosedBy.Name = "labelClosedBy";
			this.labelClosedBy.Size = new System.Drawing.Size(94, 23);
			this.labelClosedBy.TabIndex = 261;
			this.labelClosedBy.Text = "Closing By:";
			this.labelClosedBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelClosingDate
			// 
			this.labelClosingDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelClosingDate.Location = new System.Drawing.Point(20, 213);
			this.labelClosingDate.Name = "labelClosingDate";
			this.labelClosingDate.Size = new System.Drawing.Size(94, 23);
			this.labelClosingDate.TabIndex = 260;
			this.labelClosingDate.Text = "Closing date:";
			this.labelClosingDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dateTimePickerClosingDate
			// 
			this.dateTimePickerClosingDate.Enabled = false;
			this.dateTimePickerClosingDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateTimePickerClosingDate.Location = new System.Drawing.Point(118, 214);
			this.dateTimePickerClosingDate.Name = "dateTimePickerClosingDate";
			this.dateTimePickerClosingDate.Size = new System.Drawing.Size(165, 22);
			this.dateTimePickerClosingDate.TabIndex = 259;
			// 
			// labelPublishDate
			// 
			this.labelPublishDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelPublishDate.Location = new System.Drawing.Point(20, 158);
			this.labelPublishDate.Name = "labelPublishDate";
			this.labelPublishDate.Size = new System.Drawing.Size(94, 23);
			this.labelPublishDate.TabIndex = 258;
			this.labelPublishDate.Text = "Publish. date:";
			this.labelPublishDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dateTimePickerPublishDate
			// 
			this.dateTimePickerPublishDate.Enabled = false;
			this.dateTimePickerPublishDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateTimePickerPublishDate.Location = new System.Drawing.Point(118, 159);
			this.dateTimePickerPublishDate.Name = "dateTimePickerPublishDate";
			this.dateTimePickerPublishDate.Size = new System.Drawing.Size(165, 22);
			this.dateTimePickerPublishDate.TabIndex = 257;
			// 
			// label5
			// 
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.label5.Location = new System.Drawing.Point(20, 185);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 23);
			this.label5.TabIndex = 255;
			this.label5.Text = "Publishing By:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dateTimePickerOpeningDate
			// 
			this.dateTimePickerOpeningDate.Enabled = false;
			this.dateTimePickerOpeningDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateTimePickerOpeningDate.Location = new System.Drawing.Point(118, 131);
			this.dateTimePickerOpeningDate.Name = "dateTimePickerOpeningDate";
			this.dateTimePickerOpeningDate.Size = new System.Drawing.Size(165, 22);
			this.dateTimePickerOpeningDate.TabIndex = 162;
			// 
			// labelOpeningDate
			// 
			this.labelOpeningDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelOpeningDate.Location = new System.Drawing.Point(20, 131);
			this.labelOpeningDate.Name = "labelOpeningDate";
			this.labelOpeningDate.Size = new System.Drawing.Size(87, 23);
			this.labelOpeningDate.TabIndex = 163;
			this.labelOpeningDate.Text = "Open. date:";
			this.labelOpeningDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelStatus
			// 
			this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelStatus.Location = new System.Drawing.Point(20, 102);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(87, 23);
			this.labelStatus.TabIndex = 160;
			this.labelStatus.Text = "Status:";
			this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxAuthor
			// 
			// 
			// 
			// 
			this.textBoxAuthor.CustomButton.Image = null;
			this.textBoxAuthor.CustomButton.Location = new System.Drawing.Point(146, 2);
			this.textBoxAuthor.CustomButton.Name = "";
			this.textBoxAuthor.CustomButton.Size = new System.Drawing.Size(17, 17);
			this.textBoxAuthor.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxAuthor.CustomButton.TabIndex = 1;
			this.textBoxAuthor.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxAuthor.CustomButton.UseSelectable = true;
			this.textBoxAuthor.CustomButton.Visible = false;
			this.textBoxAuthor.Enabled = false;
			this.textBoxAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.textBoxAuthor.Lines = new string[0];
			this.textBoxAuthor.Location = new System.Drawing.Point(118, 75);
			this.textBoxAuthor.MaxLength = 32767;
			this.textBoxAuthor.Name = "textBoxAuthor";
			this.textBoxAuthor.PasswordChar = '\0';
			this.textBoxAuthor.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxAuthor.SelectedText = "";
			this.textBoxAuthor.SelectionLength = 0;
			this.textBoxAuthor.SelectionStart = 0;
			this.textBoxAuthor.ShortcutsEnabled = true;
			this.textBoxAuthor.Size = new System.Drawing.Size(166, 22);
			this.textBoxAuthor.TabIndex = 159;
			this.textBoxAuthor.UseSelectable = true;
			this.textBoxAuthor.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxAuthor.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// labelAuthor
			// 
			this.labelAuthor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelAuthor.Location = new System.Drawing.Point(20, 74);
			this.labelAuthor.Name = "labelAuthor";
			this.labelAuthor.Size = new System.Drawing.Size(87, 23);
			this.labelAuthor.TabIndex = 158;
			this.labelAuthor.Text = "Author:";
			this.labelAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxTitle
			// 
			// 
			// 
			// 
			this.textBoxTitle.CustomButton.Image = null;
			this.textBoxTitle.CustomButton.Location = new System.Drawing.Point(146, 2);
			this.textBoxTitle.CustomButton.Name = "";
			this.textBoxTitle.CustomButton.Size = new System.Drawing.Size(17, 17);
			this.textBoxTitle.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxTitle.CustomButton.TabIndex = 1;
			this.textBoxTitle.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxTitle.CustomButton.UseSelectable = true;
			this.textBoxTitle.CustomButton.Visible = false;
			this.textBoxTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.textBoxTitle.Lines = new string[0];
			this.textBoxTitle.Location = new System.Drawing.Point(118, 47);
			this.textBoxTitle.MaxLength = 32767;
			this.textBoxTitle.Name = "textBoxTitle";
			this.textBoxTitle.PasswordChar = '\0';
			this.textBoxTitle.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxTitle.SelectedText = "";
			this.textBoxTitle.SelectionLength = 0;
			this.textBoxTitle.SelectionStart = 0;
			this.textBoxTitle.ShortcutsEnabled = true;
			this.textBoxTitle.Size = new System.Drawing.Size(166, 22);
			this.textBoxTitle.TabIndex = 2;
			this.textBoxTitle.UseSelectable = true;
			this.textBoxTitle.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxTitle.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// labelQOTitle
			// 
			this.labelQOTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.labelQOTitle.Location = new System.Drawing.Point(20, 44);
			this.labelQOTitle.Name = "labelQOTitle";
			this.labelQOTitle.Size = new System.Drawing.Size(87, 27);
			this.labelQOTitle.TabIndex = 3;
			this.labelQOTitle.Text = "Title:";
			this.labelQOTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOk.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.buttonOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(155)))), ((int)(((byte)(246)))));
			this.buttonOk.Location = new System.Drawing.Point(1127, 582);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 33);
			this.buttonOk.TabIndex = 293;
			this.buttonOk.Text = "OK";
			this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonCancel.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(155)))), ((int)(((byte)(246)))));
			this.buttonCancel.Location = new System.Drawing.Point(1208, 582);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 33);
			this.buttonCancel.TabIndex = 292;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// purchaseRecordListView1
			// 
			this.purchaseRecordListView1.Displayer = null;
			this.purchaseRecordListView1.DisplayerText = null;
			this.purchaseRecordListView1.Entity = null;
			this.purchaseRecordListView1.IgnoreEnterPress = false;
			this.purchaseRecordListView1.Location = new System.Drawing.Point(23, 64);
			this.purchaseRecordListView1.MenuOpeningAction = null;
			this.purchaseRecordListView1.Name = "purchaseRecordListView1";
			this.purchaseRecordListView1.OldColumnIndex = 2;
			this.purchaseRecordListView1.ReflectionType = CAS.UI.Management.Dispatchering.ReflectionTypes.DisplayInCurrent;
			this.purchaseRecordListView1.Size = new System.Drawing.Size(687, 350);
			this.purchaseRecordListView1.SortMultiplier = 1;
			this.purchaseRecordListView1.TabIndex = 296;
			this.purchaseRecordListView1.SelectedItemsChanged += new System.EventHandler<CAS.UI.UIControls.Auxiliary.SelectedItemsChangeEventArgs>(this.PurchaseRecordListView1_SelectedItemsChanged);
			// 
			// documentControl1
			// 
			this.documentControl1.CurrentDocument = null;
			this.documentControl1.Location = new System.Drawing.Point(1013, 39);
			this.documentControl1.Name = "documentControl1";
			this.documentControl1.Size = new System.Drawing.Size(270, 41);
			this.documentControl1.TabIndex = 297;
			// 
			// documentControl2
			// 
			this.documentControl2.CurrentDocument = null;
			this.documentControl2.Location = new System.Drawing.Point(1013, 86);
			this.documentControl2.Name = "documentControl2";
			this.documentControl2.Size = new System.Drawing.Size(270, 41);
			this.documentControl2.TabIndex = 298;
			// 
			// documentControl3
			// 
			this.documentControl3.CurrentDocument = null;
			this.documentControl3.Location = new System.Drawing.Point(1013, 133);
			this.documentControl3.Name = "documentControl3";
			this.documentControl3.Size = new System.Drawing.Size(270, 41);
			this.documentControl3.TabIndex = 299;
			// 
			// documentControl4
			// 
			this.documentControl4.CurrentDocument = null;
			this.documentControl4.Location = new System.Drawing.Point(1013, 180);
			this.documentControl4.Name = "documentControl4";
			this.documentControl4.Size = new System.Drawing.Size(270, 41);
			this.documentControl4.TabIndex = 300;
			// 
			// documentControl5
			// 
			this.documentControl5.CurrentDocument = null;
			this.documentControl5.Location = new System.Drawing.Point(1013, 227);
			this.documentControl5.Name = "documentControl5";
			this.documentControl5.Size = new System.Drawing.Size(270, 41);
			this.documentControl5.TabIndex = 301;
			// 
			// documentControl6
			// 
			this.documentControl6.CurrentDocument = null;
			this.documentControl6.Location = new System.Drawing.Point(1013, 274);
			this.documentControl6.Name = "documentControl6";
			this.documentControl6.Size = new System.Drawing.Size(270, 41);
			this.documentControl6.TabIndex = 302;
			// 
			// documentControl7
			// 
			this.documentControl7.CurrentDocument = null;
			this.documentControl7.Location = new System.Drawing.Point(1013, 321);
			this.documentControl7.Name = "documentControl7";
			this.documentControl7.Size = new System.Drawing.Size(270, 41);
			this.documentControl7.TabIndex = 303;
			// 
			// documentControl8
			// 
			this.documentControl8.CurrentDocument = null;
			this.documentControl8.Location = new System.Drawing.Point(1013, 368);
			this.documentControl8.Name = "documentControl8";
			this.documentControl8.Size = new System.Drawing.Size(270, 41);
			this.documentControl8.TabIndex = 304;
			// 
			// documentControl9
			// 
			this.documentControl9.CurrentDocument = null;
			this.documentControl9.Location = new System.Drawing.Point(1013, 415);
			this.documentControl9.Name = "documentControl9";
			this.documentControl9.Size = new System.Drawing.Size(270, 41);
			this.documentControl9.TabIndex = 305;
			// 
			// documentControl10
			// 
			this.documentControl10.CurrentDocument = null;
			this.documentControl10.Location = new System.Drawing.Point(1013, 462);
			this.documentControl10.Name = "documentControl10";
			this.documentControl10.Size = new System.Drawing.Size(270, 41);
			this.documentControl10.TabIndex = 306;
			// 
			// comboBoxDesignation
			// 
			this.comboBoxDesignation.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.comboBoxDesignation.FormattingEnabled = true;
			this.comboBoxDesignation.ItemHeight = 17;
			this.comboBoxDesignation.Location = new System.Drawing.Point(161, 452);
			this.comboBoxDesignation.Name = "comboBoxDesignation";
			this.comboBoxDesignation.Size = new System.Drawing.Size(245, 25);
			this.comboBoxDesignation.TabIndex = 308;
			// 
			// metroLabelDesignation
			// 
			this.metroLabelDesignation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabelDesignation.Location = new System.Drawing.Point(23, 452);
			this.metroLabelDesignation.Name = "metroLabelDesignation";
			this.metroLabelDesignation.Size = new System.Drawing.Size(132, 23);
			this.metroLabelDesignation.TabIndex = 307;
			this.metroLabelDesignation.Text = "Designation:";
			this.metroLabelDesignation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxPayTerm
			// 
			this.comboBoxPayTerm.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.comboBoxPayTerm.FormattingEnabled = true;
			this.comboBoxPayTerm.ItemHeight = 17;
			this.comboBoxPayTerm.Location = new System.Drawing.Point(161, 483);
			this.comboBoxPayTerm.Name = "comboBoxPayTerm";
			this.comboBoxPayTerm.Size = new System.Drawing.Size(245, 25);
			this.comboBoxPayTerm.TabIndex = 310;
			// 
			// metroLabelPayTerm
			// 
			this.metroLabelPayTerm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabelPayTerm.Location = new System.Drawing.Point(23, 483);
			this.metroLabelPayTerm.Name = "metroLabelPayTerm";
			this.metroLabelPayTerm.Size = new System.Drawing.Size(132, 23);
			this.metroLabelPayTerm.TabIndex = 309;
			this.metroLabelPayTerm.Text = "Pay Term:";
			this.metroLabelPayTerm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxIncoTerm
			// 
			this.comboBoxIncoTerm.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.comboBoxIncoTerm.FormattingEnabled = true;
			this.comboBoxIncoTerm.ItemHeight = 17;
			this.comboBoxIncoTerm.Location = new System.Drawing.Point(161, 514);
			this.comboBoxIncoTerm.Name = "comboBoxIncoTerm";
			this.comboBoxIncoTerm.Size = new System.Drawing.Size(245, 25);
			this.comboBoxIncoTerm.TabIndex = 312;
			// 
			// metroLabelIncoTerm
			// 
			this.metroLabelIncoTerm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabelIncoTerm.Location = new System.Drawing.Point(23, 514);
			this.metroLabelIncoTerm.Name = "metroLabelIncoTerm";
			this.metroLabelIncoTerm.Size = new System.Drawing.Size(132, 23);
			this.metroLabelIncoTerm.TabIndex = 311;
			this.metroLabelIncoTerm.Text = "Inco Term:";
			this.metroLabelIncoTerm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxShipComp
			// 
			this.comboBoxShipComp.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
			this.comboBoxShipComp.FormattingEnabled = true;
			this.comboBoxShipComp.ItemHeight = 17;
			this.comboBoxShipComp.Location = new System.Drawing.Point(161, 545);
			this.comboBoxShipComp.Name = "comboBoxShipComp";
			this.comboBoxShipComp.Size = new System.Drawing.Size(245, 25);
			this.comboBoxShipComp.TabIndex = 314;
			// 
			// metroLabelShippingCompany
			// 
			this.metroLabelShippingCompany.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabelShippingCompany.Location = new System.Drawing.Point(23, 545);
			this.metroLabelShippingCompany.Name = "metroLabelShippingCompany";
			this.metroLabelShippingCompany.Size = new System.Drawing.Size(132, 23);
			this.metroLabelShippingCompany.TabIndex = 313;
			this.metroLabelShippingCompany.Text = "Shipping company:";
			this.metroLabelShippingCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxShipTo
			// 
			// 
			// 
			// 
			this.textBoxShipTo.CustomButton.Image = null;
			this.textBoxShipTo.CustomButton.Location = new System.Drawing.Point(146, 2);
			this.textBoxShipTo.CustomButton.Name = "";
			this.textBoxShipTo.CustomButton.Size = new System.Drawing.Size(17, 17);
			this.textBoxShipTo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxShipTo.CustomButton.TabIndex = 1;
			this.textBoxShipTo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxShipTo.CustomButton.UseSelectable = true;
			this.textBoxShipTo.CustomButton.Visible = false;
			this.textBoxShipTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.textBoxShipTo.Lines = new string[0];
			this.textBoxShipTo.Location = new System.Drawing.Point(544, 452);
			this.textBoxShipTo.MaxLength = 32767;
			this.textBoxShipTo.Name = "textBoxShipTo";
			this.textBoxShipTo.PasswordChar = '\0';
			this.textBoxShipTo.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxShipTo.SelectedText = "";
			this.textBoxShipTo.SelectionLength = 0;
			this.textBoxShipTo.SelectionStart = 0;
			this.textBoxShipTo.ShortcutsEnabled = true;
			this.textBoxShipTo.Size = new System.Drawing.Size(166, 22);
			this.textBoxShipTo.TabIndex = 315;
			this.textBoxShipTo.UseSelectable = true;
			this.textBoxShipTo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxShipTo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// metroLabelShipTo
			// 
			this.metroLabelShipTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabelShipTo.Location = new System.Drawing.Point(413, 448);
			this.metroLabelShipTo.Name = "metroLabelShipTo";
			this.metroLabelShipTo.Size = new System.Drawing.Size(120, 27);
			this.metroLabelShipTo.TabIndex = 316;
			this.metroLabelShipTo.Text = "Ship To:";
			this.metroLabelShipTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxCargoVolume
			// 
			// 
			// 
			// 
			this.textBoxCargoVolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.textBoxCargoVolume.Location = new System.Drawing.Point(544, 483);
			this.textBoxCargoVolume.Name = "textBoxCargoVolume";
			this.textBoxCargoVolume.Size = new System.Drawing.Size(166, 22);
			this.textBoxCargoVolume.TabIndex = 317;
			// 
			// metroLabelCargoVolume
			// 
			this.metroLabelCargoVolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabelCargoVolume.Location = new System.Drawing.Point(413, 479);
			this.metroLabelCargoVolume.Name = "metroLabelCargoVolume";
			this.metroLabelCargoVolume.Size = new System.Drawing.Size(120, 27);
			this.metroLabelCargoVolume.TabIndex = 318;
			this.metroLabelCargoVolume.Text = "Cargo volume:";
			this.metroLabelCargoVolume.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxBruttoWeight
			// 
			// 
			// 
			// 
			this.textBoxBruttoWeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.textBoxBruttoWeight.Location = new System.Drawing.Point(544, 514);
			this.textBoxBruttoWeight.Name = "textBoxBruttoWeight";
			this.textBoxBruttoWeight.Size = new System.Drawing.Size(166, 22);
			this.textBoxBruttoWeight.TabIndex = 319;
			// 
			// metroLabelBruttoWeight
			// 
			this.metroLabelBruttoWeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabelBruttoWeight.Location = new System.Drawing.Point(413, 510);
			this.metroLabelBruttoWeight.Name = "metroLabelBruttoWeight";
			this.metroLabelBruttoWeight.Size = new System.Drawing.Size(120, 27);
			this.metroLabelBruttoWeight.TabIndex = 320;
			this.metroLabelBruttoWeight.Text = "Brutto weight:";
			this.metroLabelBruttoWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxNettoWeight
			// 
			// 
			// 
			// 
			this.textBoxNettoWeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.textBoxNettoWeight.Location = new System.Drawing.Point(544, 545);
			this.textBoxNettoWeight.Name = "textBoxNettoWeight";
			this.textBoxNettoWeight.Size = new System.Drawing.Size(166, 22);
			this.textBoxNettoWeight.TabIndex = 321;
			// 
			// metroLabelNettoWeight
			// 
			this.metroLabelNettoWeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			this.metroLabelNettoWeight.Location = new System.Drawing.Point(413, 541);
			this.metroLabelNettoWeight.Name = "metroLabelNettoWeight";
			this.metroLabelNettoWeight.Size = new System.Drawing.Size(120, 27);
			this.metroLabelNettoWeight.TabIndex = 322;
			this.metroLabelNettoWeight.Text = "Netto weight:";
			this.metroLabelNettoWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PurchaseOrderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1288, 623);
			this.Controls.Add(this.textBoxNettoWeight);
			this.Controls.Add(this.metroLabelNettoWeight);
			this.Controls.Add(this.textBoxBruttoWeight);
			this.Controls.Add(this.metroLabelBruttoWeight);
			this.Controls.Add(this.textBoxCargoVolume);
			this.Controls.Add(this.metroLabelCargoVolume);
			this.Controls.Add(this.textBoxShipTo);
			this.Controls.Add(this.metroLabelShipTo);
			this.Controls.Add(this.comboBoxShipComp);
			this.Controls.Add(this.metroLabelShippingCompany);
			this.Controls.Add(this.comboBoxIncoTerm);
			this.Controls.Add(this.metroLabelIncoTerm);
			this.Controls.Add(this.comboBoxPayTerm);
			this.Controls.Add(this.metroLabelPayTerm);
			this.Controls.Add(this.comboBoxDesignation);
			this.Controls.Add(this.metroLabelDesignation);
			this.Controls.Add(this.documentControl10);
			this.Controls.Add(this.documentControl9);
			this.Controls.Add(this.documentControl8);
			this.Controls.Add(this.documentControl7);
			this.Controls.Add(this.documentControl6);
			this.Controls.Add(this.documentControl5);
			this.Controls.Add(this.documentControl4);
			this.Controls.Add(this.documentControl3);
			this.Controls.Add(this.documentControl2);
			this.Controls.Add(this.documentControl1);
			this.Controls.Add(this.purchaseRecordListView1);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ButtonDelete);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PurchaseOrderForm";
			this.Resizable = false;
			this.Text = "Purchase Order Form";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantity)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private AvControls.AvButtonT.AvButtonT ButtonDelete;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.NumericUpDown numericUpDownQuantity;
		private MetroLabel labelQuantity;
		private System.Windows.Forms.ComboBox comboBoxMeasure;
		private MetroLabel labelMeasure;
		private MetroLabel labelTotal;
		private MetroTextBox textBoxTotal;
		private MetroLabel labelReason;
		private System.Windows.Forms.ComboBox comboBoxCondition;
		private System.Windows.Forms.GroupBox groupBox2;
		private MetroTextBox textBoxTitle;
		private MetroLabel labelQOTitle;
		private MetroTextBox textBoxAuthor;
		private MetroLabel labelAuthor;
		private MetroLabel labelStatus;
		private System.Windows.Forms.DateTimePicker dateTimePickerOpeningDate;
		private MetroLabel labelOpeningDate;
		private MetroLabel label5;
		private MetroLabel labelPublishDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerPublishDate;
		private MetroLabel labelClosingDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerClosingDate;
		private MetroLabel labelClosedBy;
		private MetroTextBox textBoxRemarks;
		private MetroLabel labelRemarks;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ComboBox comboBoxStatus;
		private MetroTextBox textBoxClosingBy;
		private MetroTextBox textBoxPublishedBy;
		private MetroTextBox metroTextBoxNumber;
		private MetroLabel metroLabelNumber;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private MetroLabel metroLabel1;
		private Purchase.PurchaseRecordListView purchaseRecordListView1;
		private System.Windows.Forms.ComboBox comboBoxCurrency;
		private MetroLabel metroLabel2;
		private DocumentationControls.DocumentControl documentControl1;
		private DocumentationControls.DocumentControl documentControl2;
		private DocumentationControls.DocumentControl documentControl3;
		private DocumentationControls.DocumentControl documentControl4;
		private DocumentationControls.DocumentControl documentControl5;
		private DocumentationControls.DocumentControl documentControl6;
		private DocumentationControls.DocumentControl documentControl7;
		private DocumentationControls.DocumentControl documentControl8;
		private DocumentationControls.DocumentControl documentControl9;
		private DocumentationControls.DocumentControl documentControl10;
		private System.Windows.Forms.ComboBox comboBoxDesignation;
		private MetroLabel metroLabelDesignation;
		private System.Windows.Forms.ComboBox comboBoxPayTerm;
		private MetroLabel metroLabelPayTerm;
		private System.Windows.Forms.ComboBox comboBoxIncoTerm;
		private MetroLabel metroLabelIncoTerm;
		private System.Windows.Forms.ComboBox comboBoxShipComp;
		private MetroLabel metroLabelShippingCompany;
		private MetroTextBox textBoxShipTo;
		private MetroLabel metroLabelShipTo;
		private MetroTextBox textBoxCargoVolume;
		private MetroLabel metroLabelCargoVolume;
		private MetroTextBox textBoxBruttoWeight;
		private MetroLabel metroLabelBruttoWeight;
		private MetroTextBox textBoxNettoWeight;
		private MetroLabel metroLabelNettoWeight;
	}
}