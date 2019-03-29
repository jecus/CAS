using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Attributes;
using SmartCore.Purchase;

namespace SmartCore.Entities.General.Interfaces
{
    /// <summary>
    /// ���������, ����������� ��������� ��� ��������� ����������� � ����� �� �����������
    /// </summary>
    public interface IComponentFilterParams : IBaseEntityObject
    {
        #region String PartNumber { get; }

        /// <summary>
        /// �������� ����� ��������
        /// </summary>
        [Filter("Part. Number:", Order = 1)]
        string PartNumber { get; }
        #endregion

        #region String SerialNumber { get; }

        /// <summary>
        /// �������� ����� ���������
        /// </summary>
        [Filter("Serial Number:", Order = 2)]
        string SerialNumber { get; }
        #endregion

        #region string Description { get; }

        /// <summary>
        /// �������� ��������
        /// </summary>
        [Filter("Description:", Order = 3)]
        string Description { get; }
        #endregion

        #region string Manufacturer { get; }

        /// <summary>
        /// �������������
        /// </summary>
        [Filter("Manufacturer:", Order = 4)]
        string Manufacturer { get;  }
        #endregion

        #region string Remarks { get; }
        /// <summary>
        /// 
        /// </summary>
        [Filter("Remarks:", Order = 5)]
        string Remarks { get; }
        #endregion

        #region string HiddenRemarks { get; }
        /// <summary>
        /// 
        /// </summary>
        [Filter("Hidden Remarks:", Order = 6)]
        string HiddenRemarks { get; }
		#endregion

		#region bool IsPOOL { get; }

		[Filter("IsPool :", Order = 10)]
		bool IsPOOL { get; }

		#endregion

		#region bool IsDangerous { get; }

		[Filter("IsDangerous :", Order = 12)]
		bool IsDangerous { get; }

		#endregion

		#region GoodsClass GoodsClass { get; }

		[Filter("Class:", Order = 7)]
	    GoodsClass GoodsClass { get; }

	    #endregion

		#region AtaChapter ATAChapter { get;}
		/// <summary>
		/// ������� ���� �� ������������� ������ �� ���������� �����������
		/// </summary>
		[Filter("Ata Chapter:", Order = 8)]
        AtaChapter ATAChapter { get; }
		#endregion

        #region MaintenanceControlProcess MaintenanceControlProcess { get; }
        /// <summary>
        /// ������������� ������ � ����� ������������ ������������
        /// </summary>
        [Filter("Maint. Proc.:", Order = 9)]
        MaintenanceControlProcess MaintenanceControlProcess { get;}
        #endregion

        #region DirectiveStatus Status { get; }

        /// <summary>
        /// ���������� ������� ������ ������: �������, ����������� ��� �������
        /// </summary>
        [Filter("Status:", Order = 11)]
        DirectiveStatus Status { get; }
        #endregion

        #region ConditionState Condition { get; }

        /// <summary>
        /// ���������� ��������� ���������� ���������� ������ (���� ��� ���������) ��� ConditionState.NotEstimated
        /// </summary>
        [Filter("Condition:", Order = 13)]
        ConditionState Condition { get; }
		#endregion

		#region NDTType NDTType { get; }
		/// <summary>
		/// ���������� ��� ������������� Non-Destructive-Test
		/// </summary>
		[Filter("NDT:", Order = 14)]
	    NDTType NDTType { get; }

		#endregion

		#region ComponentRecordType DirectiveType  { get; }

		/// <summary>
		/// ���������� ��� ����������� �����
		/// </summary>
		[Filter("Work Type:")]
        ComponentRecordType DirectiveType { get; }
		#endregion

		#region BaseComponent ParentBaseComponent { get; }

		/// <summary>
		/// �������� ������� �������, �� ������� ���������� �������
		/// </summary>
		[Filter("Base Detail:")]
        BaseComponent ParentBaseComponent { get; }
        #endregion

        #region Lifelength FirstPerformanceSinceNew { get; }

        /// <summary>
        /// ���������� ����� ������� ���������� ������
        /// </summary>
        [Filter("1st. Perf.:")]
        Lifelength FirstPerformanceSinceNew { get; }
        #endregion

        #region Lifelength Interval { get; }

        /// <summary>
        /// ���������� �������� ���������� ���������� ������ ��� Lifelength.Null
        /// </summary>
        [Filter("Rpt. Int.:")]
        Lifelength Interval { get; }
		#endregion

		#region SupplierCollection Suppliers  { get; set; }

		/// <summary>
		/// ���������� ������ ������
		/// </summary>
		[Filter("From Supplier:")]
		Supplier FromSupplier { get; }
		#endregion
	}
}
