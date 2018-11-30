using System.Windows.Forms;
using CAS.UI.Events;
using CAS.UI.Interfaces;
using HelpEventHandler=CAS.UI.Events.HelpEventHandler;

namespace CAS.UI.Management.Dispatchering
{
    ///<summary>
    /// �����, ����������� ������, ������������� �������
    ///</summary>
    public class HelpRequestingLink : LinkLabel, IHelpRequester
    {

        #region Fields

        private string _topicId;

        #endregion
        
        #region Constructors

        #region public HelpRequestingLink()

        ///<summary>
        /// ��������� ��������� ������, �������������� �������
        ///</summary>
        public HelpRequestingLink()
        {
            LinkClicked += HelpRequestingLinkLinkClicked;
        }

        #endregion

        #region public HelpRequestingLink(string topicID) : this()

        ///<summary>
        /// ��������� ��������� ������, �������������� �������
        ///</summary>
        ///<param name="topicId">������ ������� ��� �����������</param>
        public HelpRequestingLink(string topicId) : this()
        {
            _topicId = topicId;
        }

        #endregion
        
        #endregion

        #region Methods

        #region private void HelpRequestingLinkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)

        private void HelpRequestingLinkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RequestHelp();
        }

        #endregion

        #endregion

        #region IHelpRequester Members

        /// <summary>
        /// Occurs when help was requested by object
        /// </summary>
        public new event HelpEventHandler HelpRequested;

        ///<summary>
        /// �������� ������� ������
        ///</summary>
        public string TopicId
        {
            get { return _topicId; }
            set { _topicId = value; }
        }

        /// <summary>
        /// ���������� ������� HelpRequested
        /// </summary>
        public void RequestHelp()
        {
            RequestHelp(_topicId);
        }

        /// <summary>
        /// ���������� ������� HelpRequested ��� ����������� ������� ��������� �������
        /// </summary>
        /// <param name="topicId">�������� ������� �������</param>
        public void RequestHelp(string topicId)
        {
            if (HelpRequested != null)
                HelpRequested.Invoke(this, new HelpEventHandlerArgs(topicId));
        }

        #endregion

    }
}
