﻿namespace CAS.UI.UIControls.OilControls
{
	partial class OilGraphForm
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
			Telerik.WinControls.UI.CartesianArea cartesianArea2 = new Telerik.WinControls.UI.CartesianArea();
			this.radChartView1 = new Telerik.WinControls.UI.RadChartView();
			this.label2 = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.radRangeSelector1 = new Telerik.WinControls.UI.RadRangeSelector();
			this.telerikMetroTheme1 = new Telerik.WinControls.Themes.TelerikMetroTheme();
			((System.ComponentModel.ISupportInitialize)(this.radChartView1)).BeginInit();
			this.radChartView1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.radRangeSelector1)).BeginInit();
			this.SuspendLayout();
			// 
			// radChartView1
			// 
			this.radChartView1.AreaDesign = cartesianArea2;
			this.radChartView1.Controls.Add(this.label2);
			this.radChartView1.Controls.Add(this.flowLayoutPanel1);
			this.radChartView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.radChartView1.Location = new System.Drawing.Point(0, 0);
			this.radChartView1.Name = "radChartView1";
			this.radChartView1.ShowGrid = false;
			this.radChartView1.ShowPanZoom = true;
			this.radChartView1.Size = new System.Drawing.Size(1111, 476);
			this.radChartView1.TabIndex = 0;
			this.radChartView1.ThemeName = "TelerikMetro";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 11F);
			this.label2.Location = new System.Drawing.Point(3, 456);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "Rolle Average:";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(1111, 26);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(20, 60);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.radChartView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.radRangeSelector1);
			this.splitContainer1.Size = new System.Drawing.Size(1111, 597);
			this.splitContainer1.SplitterDistance = 476;
			this.splitContainer1.TabIndex = 2;
			// 
			// radRangeSelector1
			// 
			this.radRangeSelector1.AssociatedControl = this.radChartView1;
			this.radRangeSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.radRangeSelector1.Location = new System.Drawing.Point(0, 0);
			this.radRangeSelector1.Name = "radRangeSelector1";
			this.radRangeSelector1.Size = new System.Drawing.Size(1111, 117);
			this.radRangeSelector1.TabIndex = 0;
			this.radRangeSelector1.ThemeName = "TelerikMetro";
			// 
			// OilGraphForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1151, 677);
			this.Controls.Add(this.splitContainer1);
			this.Name = "OilGraphForm";
			this.Text = "Oil Chart";
			((System.ComponentModel.ISupportInitialize)(this.radChartView1)).EndInit();
			this.radChartView1.ResumeLayout(false);
			this.radChartView1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.radRangeSelector1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Telerik.WinControls.UI.RadChartView radChartView1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Telerik.WinControls.UI.RadRangeSelector radRangeSelector1;
		private Telerik.WinControls.Themes.TelerikMetroTheme telerikMetroTheme1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label2;
	}
}