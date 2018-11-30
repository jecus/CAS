using System;

namespace SmartCore.Management
{
    /// <summary>
    /// ���� �������������� ��� ����������� � SQL �������
    /// </summary>
    public enum AuthenticationType : int 
    {
        /// <summary>
        /// �������������� ���������� Windows
        /// </summary>
        Windows = 1,

        /// <summary>
        /// �������������� ���������� SQL Server
        /// </summary>
        SqlServer
    }
}
