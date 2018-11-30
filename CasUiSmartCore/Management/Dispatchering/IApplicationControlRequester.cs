using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CAS.UI.Management.Dispatchering
{
    /// <summary>
    /// ����������� ��������� ��������, ������� ����� ����������� ���������� �����������
    /// </summary>
    public interface IApplicationControlRequester
    {
        /// <summary>
        /// ��������� �������� ����������
        /// </summary>
        event EventHandler<ApplicationControlRequestArgs> ControlRequest;
    }

    ///<summary>
    /// ��������� ���������� �����������
    ///</summary>
    public class ApplicationControlRequestArgs:CancelEventArgs
    {
        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        /// <param name="controlType">��� ��������</param>
        public ApplicationControlRequestArgs(ControlType controlType)
        {
            this.controlType = controlType;
        }

        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        /// <param name="cancel">�������� �� ��������</param>
        /// <param name="controlType">��� ��������</param>
        public ApplicationControlRequestArgs(bool cancel, ControlType controlType) : base(cancel)
        {
            this.controlType = controlType;
        }

        private ControlType controlType;
        
        ///<summary>
        /// ��� ���������� �����������
        ///</summary>
        public ControlType ControlType
        {
            get { return controlType; }
            set { controlType = value; }
        }
    }

    /// <summary>
    /// ���� ���������� �����������
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// ������������� ����� �� ������� � ����������� ������ � ���
        /// </summary>
        LogOut,
        /// <summary>
        /// ����� �� �������
        /// </summary>
        Exit,
        /// <summary>
        /// �������������� ����������
        /// </summary>
        Trivial
    }
}
