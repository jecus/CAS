using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SmartCore.Management;

namespace SmartCore.Entities.General
{

    /// <summary>
    /// ����� ��������� ��������� �����
    /// </summary>
    public class UsersPermission
    {

		/*
		*  ��������
		*/
		


		#region public Int32 PermissionId { get; set; }
		/// <summary>
		/// ������������� ������
		/// </summary>
		public Int32 PermissionId { get; set; }
		#endregion

		#region public Int32 UserId { get; set; }
		/// <summary>
		/// Id ������������
		/// </summary>
		public Int32 UserId { get; set; }
		#endregion

		#region public Int32 OperatorId { get; set; }
		/// <summary>
		/// Id ������������, �� �������� � ������������ ���� �����
		/// </summary>
		public Int32 OperatorId { get; set; }
		#endregion
		
		/*
		*  ������ 
		*/
		
		#region public UsersPermission()
        /// <summary>
        /// ������� ��������� ����� ��� �������������� ����������
        /// </summary>
        public UsersPermission()
        {
        }
        #endregion


      
        #region public override string ToString()
        /// <summary>
        /// ����������� ��� �������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
        }
        #endregion   

    }

}
