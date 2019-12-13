using System;
using System.Collections.Generic;
using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Attributes;

namespace SmartCore.Entities.General.Templates
{

	/// <summary>
	/// ����� ��������� ���������
	/// </summary>
	[Table("Directives", "Template", "ItemId")]
	[Condition("IsPrimeDirective", "0")]
	[Serializable]
	public class TemplateDirective: BaseEntityObject, IComparable<TemplateDirective>
	{

		/*
		*  ��������
		*/
		#region public Int32 TemplateId { get; set; }
		/// <summary>
		/// ��� �������, �������� ����������� �������
		/// </summary>
		[TableColumnAttribute("TemplateId")]
		public Int32 TemplateId { get; set; }
		#endregion

		#region public Int32 PrimeDirectiveId { get; set; }
		/// <summary>
		/// ������������� �������� ���������, ������� ����������� ������ ������
		/// </summary>
		[TableColumnAttribute("PrimeDirectiveId")]
		public Int32 PrimeDirectiveId { get; set; }
		#endregion

		#region public DirectiveType DirectiveType { get; set; }
		/// <summary>
		/// ��� ���������
		/// </summary>
		[TableColumnAttribute("DirectiveTypeId"), ListViewData("Directive Type")]
		public DirectiveWorkType DirectiveWorkType { get; set; }
		#endregion

		#region public String Title { get; set; }
		/// <summary>
		/// �������� ���������
		/// </summary>
		[TableColumnAttribute("Title"), ListViewData("Title")]
		public String Title { get; set; }
		#endregion

		#region public Double ManHours { get; set; }
		/// <summary>
		/// �������� �����������
		/// </summary>
		[TableColumnAttribute("ManHours"), ListViewData("Man Hours"), MinMaxValue(0,100000)]
		public Double ManHours { get; set; }
		#endregion

		#region public String Remarks { get; set; }
		/// <summary>
		/// ������� �� ���������
		/// </summary>
		[TableColumnAttribute("Remarks"), ListViewData("Remarks")]
		public String Remarks { get; set; }
		#endregion

		#region public String Applicability { get; set; }
		/// <summary>
		/// ������������ ���������
		/// </summary>
		[TableColumnAttribute("Applicability"), ListViewData("Applicability")]
		public String Applicability { get; set; }
		#endregion

		#region public Int32 ComponentId { get; set; }
		/// <summary>
		/// Id �������� �� ������� ��������� ����������
		/// </summary>
		[TableColumnAttribute("ComponentId")]
		public int ComponentId { get; set; }
		#endregion

		#region public AtaChapter ATAChapter { get; set; }
		/// <summary>
		/// ATA �����, � ������� ��������� ���������
		/// </summary>
		[TableColumnAttribute("ATAChapterId"), ListViewData("ATA Chapter")]
		public AtaChapter ATAChapter { get; set; }
		#endregion

		#region public Int32 DirectiveType { get; set; }
		/// <summary>
		/// ��� ���������
		/// </summary>
		[TableColumnAttribute("PrimaryDirectiveTypeId")]
		public DirectiveType DirectiveType { get; set; }
		#endregion

		#region public ADType ADType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("AdType"), ListViewData("AD Type")]
		public ADType ADType { get; set; }
		#endregion

		#region public String ServiceBulletinNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("ServiceBulletinNo"), ListViewData("Service Bulletin �")]
		public String ServiceBulletinNo { get; set; }
		#endregion

		//TODO: ���������� �� ������������� ������ fileCore
		#region  public AttachedFile ServiceBulletinFile { get; set; }
		/// <summary>
		/// ����� � ������ �������� ���������� ���������
		/// </summary>
		[TableColumnAttribute("ServiceBulletinFileId"), ListViewData("Service Bulletin File")]
		public AttachedFile ServiceBulletinFile { get; set; }
		#endregion

		#region  public AttachedFile ADNoFile { get; set; }
		/// <summary>
		/// ����� � ������ �������� ��������� ������ ��������
		/// </summary>
		[TableColumnAttribute("ADNoFileId"), ListViewData("AD � File")]
		public AttachedFile ADNoFile { get; set; }
		#endregion

		#region public String Description { get; set; }
		/// <summary>
		/// �������� ���������
		/// </summary>
		[TableColumnAttribute("Description"), ListViewData("Description")]
		public String Description { get; set; }
		#endregion

		#region public String EngineeringOrders { get; set; }
		/// <summary>
		/// �������� Engineering orders
		/// </summary>
		[TableColumnAttribute("EngineeringOrders"), ListViewData("Engineering Orders")]
		public String EngineeringOrders { get; set; }
		#endregion

		#region public String JobCardNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("JobCardNo"), ListViewData("Job Card No")]
		public String JobCardNo { get; set; }
		#endregion

		#region  public AttachedFile EngineeringOrderFile { get; set; }
		/// <summary>
		/// ����� � ������ �������� ����������� ������
		/// </summary>
		[TableColumnAttribute("EngineeringOrderFileId"), ListViewData("Eng. Order File")]
		public AttachedFile EngineeringOrderFile { get; set; }
		#endregion

		#region public Double Cost { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("Cost"), ListViewData("Cost"), MinMaxValue(0,1000000000)]
		public Double Cost { get; set; }
		#endregion

		#region public Highlight Highlight { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("Highlight")]
		public Highlight Highlight { get; set; }
		#endregion

		#region public String KitRequired { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("KitRequired"), ListViewData("Kit Req.")]
		public String KitRequired { get; set; }
		#endregion

		#region public String HiddenRemarks { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("HiddenRemarks"), ListViewData("HiddenRemarks")]
		public String HiddenRemarks { get; set; }
		#endregion

		#region public String InspectionDocumentsNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("InspectionDocumentsNo")]
		public String InspectionDocumentsNo { get; set; }
		#endregion

		#region  public AttachedFile InspectionDocumentsFile { get; set; }
		/// <summary>
		/// ����� � ������ �������� ���������� ���������
		/// </summary>
		[TableColumnAttribute("InspectionDocumentsFileId")]
		public AttachedFile InspectionDocumentsFile { get; set; }
		#endregion

		#region public Int32 IsPrimeDirective { get; set; }
		/// <summary>
		/// �������� �� ������ ��������� �������� ��� ����������
		/// </summary>
		[TableColumnAttribute("IsPrimeDirective") ]
		public bool IsPrimeDirective { get; set; }
		#endregion

		/*
		 * �������������� ��������
		 */

		#region public String Paragraph { get; set; }
		/// <summary>
		/// �������� �����������
		/// </summary>
		[TableColumnAttribute("Paragraph"), ListViewData("�")]
		public String Paragraph { get; set; }
		#endregion

		#region public NDTType NDTType { get; set; }
		/// <summary>
		/// ��� ������������� Non-Destructive-Test
		/// </summary>
		[TableColumnAttribute("NDTType"), ListViewData("NDT")]
		public NDTType NDTType { get; set; }
		#endregion

		#region public DirectiveThreshold Threshold { get; set; }
		/// <summary>
		/// ������� ���������� ���������
		/// </summary>
		[TableColumnAttribute("Threshold"), ListViewData("Threshold")]
		public DirectiveThreshold Threshold { get; set; }
		#endregion

		#region Implement of IKitRequired
		public List<AccessoryRequired> Kits { get; set; }
		#endregion

		/*
		*  ������ 
		*/


		#region public TemplateDirective()
		/// <summary>
		/// ������� ��������� ����� ��� �������������� ����������
		/// </summary>
		public TemplateDirective()
		{
			//������ ��� ID � -1
			ItemId = -1;
		   
			// Ad ���������
			DirectiveWorkType = DirectiveWorkType.Inspection; 

			// ������ ��� String
			Title = Remarks = Applicability = Description = EngineeringOrders = JobCardNo =
					KitRequired = HiddenRemarks = "";
		}
		#endregion

		#region public override string ToString()
		/// <summary>
		/// ����������� ��� �������
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Title + " " + Description;
		}
		#endregion   

		#region public int CompareTo(PrimaryDirective y)

		public int CompareTo(TemplateDirective y)
		{
			return ItemId.CompareTo(y.ItemId);
		}

		#endregion

		#region public override int CompareTo(object y)
		public override int CompareTo(object y)
		{
			if(y is TemplateDirective) return ItemId.CompareTo(((TemplateDirective) y).ItemId);
			return 0;
		}
		#endregion

	}
}
