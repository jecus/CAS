using System.Diagnostics;
using System.Windows.Forms;
using CAS.UI.Events;
using CAS.UI.Interfaces;
using CASTerms;

namespace CAS.UI.Management.Dispatchering
{
    /// <summary>
    /// ����� ��� �������� �� ��������� ����������� ������, � ������������� ��
    /// </summary>
    internal class HelpDispatcher:IDispatcher
    {
        /// <summary>
        /// ��������� ����� ��������� ������
        /// </summary>
        /// <param name="proxy">������, �� ������� ���������� ��������</param>
        internal HelpDispatcher(IDisplayerCollectionProxy proxy)
        {
            defaultProxy = proxy;
        }

        //private readonly string helpSource = @"..\..\..\..\LTRHelp\CASHelp.chm";
        private readonly string helpSource = "CASHelp.pdf";

        #region internal override void ProcessControl(Control control)
        /// <summary>
        /// ���������, ���������� �� ������� �� ���������.
        /// </summary>
        /// <param name="control">������ ��� ��������</param>
        internal override void ProcessControl(Control control)
        {
            if (control is IHelpRequester)
            {
                IHelpRequester requester = control as IHelpRequester;
                requester.HelpRequested -= RequesterHelpRequested;
                requester.HelpRequested += RequesterHelpRequested;
            }
        }
        #endregion

        #region internal override void UnProcessControl(Control control)
        /// <summary>
        /// ���������, ���������� �� ���������� �� ������� ��������.
        /// </summary>
        /// <param name="control">������ ��� ��������</param>
        internal override void UnProcessControl(Control control)
        {
            if (control is IHelpRequester)
            {
                IHelpRequester requester = control as IHelpRequester;
                requester.HelpRequested -= RequesterHelpRequested;
            }
        }
        #endregion

        private void RequesterHelpRequested(object sender, HelpEventHandlerArgs args)
        {
            ShowHelp(args);
        }

        private void ShowHelp(HelpEventHandlerArgs args)
        {
            try
            {
                Process.Start("Acrobat.exe", "/a nameddest=" + args.TopicId + " \"" + helpSource + "\"");
            }
            catch
            {
                try
                {
                    Process.Start(helpSource);
                }
                catch
                {
                    MessageBox.Show("An error occured while opening help file", new GlobalTermsProvider()["SystemName"].ToString(),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);    
                }
            }
        }
    }
}