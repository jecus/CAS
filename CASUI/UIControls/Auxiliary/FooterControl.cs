using System;
using System.Drawing;
using System.Windows.Forms;
using CAS.Core.Core.Management;
using CAS.UI.Management;
using CAS.UI.Management.Dispatchering;

namespace CAS.UI.UIControls.Auxiliary
{
    /// <summary>
    /// ����� ��� ������ ������
    /// </summary>
    public partial class FooterControl : UserControl, IApplicationControlRequester
    {
        #region Fields
        /// <summary>
        /// �������� ��� �������� ������������
        /// </summary>
        private readonly string userName = "Not connected";

        private readonly Icons icons = new Icons();

        #endregion

        #region public FooterControl()
        /// <summary>
        /// ������� ����� ������ ������ ������ 
        /// </summary>
        public FooterControl()
        {
            InitializeComponent();
            pictureSeparatorLine.BackgroundImage = icons.SeparatorLine;
            pictureSeparatorLine.Image = icons.SeparatorLine;
            pictureSeparatorEnd.BackgroundImage = icons.SeparatorLine;

            Dock = DockStyle.Bottom;
            if (Users.CurrentUser != null)
            {
                userName = Users.CurrentUser.FullName;
            }
            else
            {
                userName = "Not connected";
            }
            labelUsername.Text = userName;
        }
        #endregion

        #region public event EventHandler<ApplicationControlRequestArgs> ControlRequest
        /// <summary>
        /// ��������� �������� ����������
        /// </summary>
        public event EventHandler<ApplicationControlRequestArgs> ControlRequest;
        #endregion

        #region private void avButtonExit_Click(object sender, EventArgs e)
        private void avButtonExit_Click(object sender, EventArgs e)
        {
            if (ControlRequest != null)
                ControlRequest(this, new ApplicationControlRequestArgs(ControlType.Exit));
        }
        #endregion

        #region private void avButtonLogout_Click(object sender, EventArgs e)
        private void avButtonLogout_Click(object sender, EventArgs e)
        {
            if (ControlRequest != null)
                ControlRequest(this, new ApplicationControlRequestArgs(ControlType.LogOut));
        }
        #endregion

        #region private void avButtonLogout_LocationChanged(object sender, EventArgs e)
        private void avButtonLogout_LocationChanged(object sender, EventArgs e)
        {
            int freeWidth = avButtonLogout.Left - labelUsername.Left;
            labelUsername.Text = TruncateUserName(freeWidth); 
        }
        #endregion

        #region string TruncateUserName(int freeWidth)
        string TruncateUserName(int freeWidth)
        {
            using (Graphics gfx = CreateGraphics())
            {
                if (gfx.MeasureString(userName, labelUsername.Font, 1280).Width > freeWidth)
                {
                    for (int length = userName.Length; length > 0; length--)
                    {
                        string currentVariant = userName.Substring(0, length) + "�";
                        SizeF stringSize = gfx.MeasureString(currentVariant, labelUsername.Font, 1280);

                        if (stringSize.Width <= freeWidth) return currentVariant;
                    }
                }
                else
                {
                    return userName;
                }
            }
            return "";
        }
        #endregion

        #region private void splitContainer1_SizeChanged(object sender, EventArgs e)
        /// <summary>
        /// Occurs when size of splitContainer1 been changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            panelLogoutButtons.Location = new Point(labelUsername.Right + 10, panelLogoutButtons.Top);
            splitContainer1.SplitterDistance = panelLogoutButtons.Right - 3;
        }

        #endregion

    }
}
