using System;

namespace CAS.UI.Events
{
    ///<summary> 
    ///</summary>
    ///<param name="sender"></param>
    ///<param name="args"></param>
    public delegate void HelpEventHandler(object sender, HelpEventHandlerArgs args);

    /// <summary>
    /// �����, ���������� �������� �������
    /// </summary>
    public class HelpEventHandlerArgs : EventArgs
    {
        private readonly string topicId;

        /// <summary>
        /// �����, ���������� �������� �������
        /// </summary>
        /// <param name="topicId">������ � ���������� ���������</param>
        public HelpEventHandlerArgs(string topicId)
        {
            this.topicId = topicId;
        }

        /// <summary>
        /// ������ � ���������� ���������
        /// </summary>
        public string TopicId
        {
            get { return topicId; }
        }
    }
}