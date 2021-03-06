﻿using MetroFramework.Controls;
using CAS.UI.Helpers;

namespace CAS.UI.UIControls.ScheduleControls.Trip
{
	partial class TrackFlightForm
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
			MetroFramework.Controls.MetroLabel labelNumber;
			MetroFramework.Controls.MetroLabel label1;
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.dataGridViewFlights = new System.Windows.Forms.DataGridView();
			this.ColumnCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.ColumnFlightNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnFlightDate = new CAS.UI.UIControls.Auxiliary.DataGridViewElements.DataGridViewCalendarColumn();
			this.ColumnTakeOff = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ColumnLDG = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.buttonOk = new System.Windows.Forms.Button();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			labelNumber = new MetroFramework.Controls.MetroLabel();
			label1 = new MetroFramework.Controls.MetroLabel();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFlights)).BeginInit();
			this.SuspendLayout();
			// 
			// labelNumber
			// 
			labelNumber.AutoSize = true;
			labelNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			labelNumber.Location = new System.Drawing.Point(14, 63);
			labelNumber.Name = "labelNumber";
			labelNumber.Size = new System.Drawing.Size(41, 19);
			labelNumber.TabIndex = 29;
			labelNumber.Text = "Track:";
			labelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
			label1.Location = new System.Drawing.Point(14, 90);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(39, 19);
			label1.TabIndex = 245;
			label1.Text = "Date:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(71, 63);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(648, 21);
			this.comboBox1.TabIndex = 30;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			this.comboBox1.TextUpdate += new System.EventHandler(this.comboBox1_TextUpdate);
			this.comboBox1.MouseWheel += CmbScrollHelper.ComboBoxScroll_MouseWheel;
			// 
			// dataGridViewFlights
			// 
			this.dataGridViewFlights.AllowUserToAddRows = false;
			this.dataGridViewFlights.AllowUserToDeleteRows = false;
			this.dataGridViewFlights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewFlights.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridViewFlights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewFlights.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.ColumnCheck,
			this.ColumnFlightNo,
			this.ColumnFrom,
			this.ColumnTo,
			this.ColumnFlightDate,
			this.ColumnTakeOff,
			this.ColumnLDG});
			this.dataGridViewFlights.Location = new System.Drawing.Point(7, 120);
			this.dataGridViewFlights.Margin = new System.Windows.Forms.Padding(2);
			this.dataGridViewFlights.Name = "dataGridViewFlights";
			this.dataGridViewFlights.RowHeadersVisible = false;
			this.dataGridViewFlights.RowHeadersWidth = 4;
			this.dataGridViewFlights.RowTemplate.Height = 24;
			this.dataGridViewFlights.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewFlights.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridViewFlights.Size = new System.Drawing.Size(712, 246);
			this.dataGridViewFlights.TabIndex = 31;
			this.dataGridViewFlights.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFlights_CellClick);
			this.dataGridViewFlights.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridViewFlights_ColumnWidthChanged);
			this.dataGridViewFlights.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewFlights_Scroll);
			// 
			// ColumnCheck
			// 
			this.ColumnCheck.HeaderText = "";
			this.ColumnCheck.Name = "ColumnCheck";
			this.ColumnCheck.Width = 30;
			// 
			// ColumnFlightNo
			// 
			this.ColumnFlightNo.HeaderText = "FlightNo";
			this.ColumnFlightNo.Name = "ColumnFlightNo";
			this.ColumnFlightNo.ReadOnly = true;
			this.ColumnFlightNo.Width = 80;
			// 
			// ColumnFrom
			// 
			this.ColumnFrom.HeaderText = "From";
			this.ColumnFrom.Name = "ColumnFrom";
			this.ColumnFrom.ReadOnly = true;
			this.ColumnFrom.Width = 150;
			// 
			// ColumnTo
			// 
			this.ColumnTo.HeaderText = "To";
			this.ColumnTo.Name = "ColumnTo";
			this.ColumnTo.ReadOnly = true;
			this.ColumnTo.Width = 150;
			// 
			// ColumnFlightDate
			// 
			this.ColumnFlightDate.HeaderText = "Flight Date";
			this.ColumnFlightDate.Name = "ColumnFlightDate";
			// 
			// ColumnTakeOff
			// 
			this.ColumnTakeOff.HeaderText = "Take-off";
			this.ColumnTakeOff.Name = "ColumnTakeOff";
			// 
			// ColumnLDG
			// 
			this.ColumnLDG.HeaderText = "LDG";
			this.ColumnLDG.Name = "ColumnLDG";
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonOk.Font = new System.Drawing.Font("Verdana", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.buttonOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(155)))), ((int)(((byte)(246)))));
			this.buttonOk.Location = new System.Drawing.Point(644, 376);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 33);
			this.buttonOk.TabIndex = 244;
			this.buttonOk.Text = "OK";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(71, 90);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(148, 20);
			this.dateTimePicker1.TabIndex = 246;
			this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
			// 
			// TrackFlightForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(729, 420);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(label1);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.dataGridViewFlights);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(labelNumber);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TrackFlightForm";
			this.Resizable = false;
			this.ShowIcon = false;
			this.Text = "Track Flight Form";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFlights)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.DataGridView dataGridViewFlights;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnCheck;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFlightNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFrom;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTo;
		private Auxiliary.DataGridViewElements.DataGridViewCalendarColumn ColumnFlightDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTakeOff;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLDG;
	}
}