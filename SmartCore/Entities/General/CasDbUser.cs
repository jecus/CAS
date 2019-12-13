using System;
using SmartCore.Entities.General.Attributes;

namespace SmartCore.Entities.General
{

	/// <summary>
	/// ����� ��������� ��������� �����
	/// </summary>
	[Table("Users", "dbo", "ItemId")]
	[Serializable]
	public class CasDbUser : BaseEntityObject
	{

		/*
		*  ��������
		*/
		

		#region public String FullName { get; set; }
		/// <summary>
		/// ������ ��� ������������
		/// </summary>
		[TableColumnAttribute("FullName")]
		public String FullName { get; set; }
		#endregion

		#region public DateTime ExpiryDate { get; set; }
		/// <summary>
		/// ����, �� ������� ������ �������� ��������������
		/// </summary>
		[TableColumnAttribute("ExpiryDate")]
		public DateTime ExpiryDate { get; set; }
		#endregion

		#region public Byte[] Photo { get; set; }
		/// <summary>
		/// ���������� ������������
		/// </summary>
		[TableColumnAttribute("Photo")]
		public Byte[] Photo { get; set; }
		#endregion

		#region public Int32 AuthenticationTypeId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("AuthenticationTypeId")]
		public Int32 AuthenticationTypeId { get; set; }
		#endregion
		
		/*
		*  ������ 
		*/
		
		#region public CasDbUser()
		/// <summary>
		/// ������� ��������� ����� ��� �������������� ����������
		/// </summary>
		public CasDbUser()
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
