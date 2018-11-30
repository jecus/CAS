using System;
using System.Windows.Forms;
using CAS.UI.Management;
using CAS.UI.Properties;
using CASTerms;

namespace CAS.UI
{
    /// <summary>
    /// ����� � ������ ��������
    /// </summary>
    public partial class Screen : Form
    {
        private readonly Icons icons = new Icons();

        /// <summary>
        /// ������� ����� � ������ ��������
        /// </summary>
        public Screen()
        {
            InitializeComponent();
            pictureBox1.Image = icons.AirplaneStartLogo;
            Icon = Resources.LTR;
            Text = (string)new GlobalTermsProvider()["SystemName"];
        }

        private bool closedForNextStep = false;

        #region public bool ClosedForNextStep

        /// <summary>
        /// ���������� �������� ������������ ����� �� ���������� � ���������� ����� ����� �������� ������ �����
        /// </summary>
        public bool ClosedForNextStep
        {
            get { return closedForNextStep; }
        }

        #endregion


        private void Screen_SizeChanged(object sender, EventArgs e)
        {
            panelImageContainer.Left = (ClientRectangle.Width - panelImageContainer.Width) / 2;
            panelImageContainer.Top = (ClientRectangle.Height - panelImageContainer.Height) / 2;
        }

        private void linkLabelContinueLoading_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            closedForNextStep = true;
            Close();
        }


    }
}