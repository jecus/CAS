using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LTR.Events;
using LTR.UI.Interfaces;
using HelpEventHandler=LTR.Events.HelpEventHandler;

namespace LTR.UI
{
    public partial class SampleHelpRequester : UserControl, IHelpRequester
    {
        ///<summary>
        ///</summary>
        public SampleHelpRequester()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RequestHelp();
        }

        #region IHelpRequester Members
        /// <summary>
        /// Occurs when help was requested by object
        /// </summary>
        public new event HelpEventHandler HelpRequested;

        ///<summary>
        /// �������� ������� ������
        ///</summary>
        public string TopicID
        {
            get { return "Start page"; }
            set {  }
        }

        /// <summary>
        /// ���������� ������� HelpRequested
        /// </summary>
        public void RequestHelp()
        {
            RequestHelp(TopicID);
        }

        /// <summary>
        /// ���������� ������� HelpRequested ��� ����������� ������� ��������� �������
        /// </summary>
        /// <param name="topicID">�������� ������� �������</param>
        public void RequestHelp(string topicID)
        {
            if (HelpRequested != null)
                HelpRequested.Invoke(this, new HelpEventHandlerArgs(topicID));
        }
        #endregion
    }
}