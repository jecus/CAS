using System;
using System.Collections.Generic;
using System.Text;
using Controls.AvButton;
using LTR.Events;

namespace LTR.UI.Interfaces
{
    ///<summary>
    /// �����, ����������� ������, ������������� �������
    ///</summary>
    public class HelpRequestingButton : AvButton, IHelpRequester
    {
        private string topicID;


        ///<summary>
        /// ��������� ��������� ������, �������������� �������
        ///</summary>
        public HelpRequestingButton()
        {
            this.Click += new EventHandler(HelpRequestingButton_Click);
        }

        void HelpRequestingButton_Click(object sender, EventArgs e)
        {
            RequestHelp();
        }

        ///<summary>
        /// ��������� ��������� ������, �������������� �������
        ///</summary>
        ///<param name="topicID">������ ������� ��� �����������</param>
        public HelpRequestingButton(string topicID):this()
        {
            this.topicID = topicID;
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
            get
            {
                return topicID;
            }
            set
            {
                topicID = value;
            }
        }

        /// <summary>
        /// ���������� ������� HelpRequested
        /// </summary>
        public void RequestHelp()
        {
            RequestHelp(topicID);
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
