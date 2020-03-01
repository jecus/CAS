using System;
using SmartCore.Entities.Collections;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Attributes;
using SmartCore.Entities.General.Interfaces;

namespace SmartCore.Entities.General.Templates
{

	/// <summary>
	/// ���������� �������� (���������)
	/// </summary>
	[Table("NonRoutineJob", "Template", "ItemId")]
	[Serializable]
	public class TemplateNonRoutineJob : BaseEntityObject, IKitRequired
	{

		/*
		*  ��������
		*/
		#region public Int32 TemplateId { get; set; }
		/// <summary>
		/// ��� �������, �������� ����������� ������ �������
		/// </summary>
		[TableColumnAttribute("TemplateId")]
		public Int32 TemplateId { get; set; }
		#endregion

		#region public AtaChapter AtaChapter { get; set; }

		private AtaChapter _ataChapter;
		/// <summary>
		/// ����� ���������� �����, ��� ��������� �������� ������
		/// </summary>
		[TableColumnAttribute("AtaChapterId"), ListViewData("ATA Chapter")]
		public AtaChapter AtaChapter
		{
			get { return _ataChapter; }
			set
			{
				_ataChapter = value;
			}
		}
		#endregion

		#region public String Title { get; set; }
		/// <summary>
		/// �������� ������
		/// </summary>
		[TableColumnAttribute("Title"), ListViewData("Title")]
		public String Title { get; set; }
		#endregion

		#region public String Description { get; set; }
		/// <summary>
		/// �������� ������
		/// </summary>
		[TableColumnAttribute("Description"), ListViewData("Description")]
		public String Description { get; set; }
		#endregion

		#region public Double ManHours { get; set; }
		/// <summary>
		/// ���������� �������� �����, ����������� ��� ���������� ������
		/// </summary>
		[TableColumnAttribute("ManHours"), ListViewData("Man Hours")]
		public Double ManHours { get; set; }
		#endregion

		#region public Double Cost { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("Cost"), ListViewData("Cost")]
		public Double Cost { get; set; }
		#endregion

		#region public String KitRequired { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("KitRequired"), ListViewData("Kit Req.")]
		public String KitRequired { get; set; }
		#endregion

		//TODO: ���������� �� ������������� ������ fileCore
		#region public AttachedFile AttachedFile { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[ListViewData("Non-Routine Job File")]
		public AttachedFile AttachedFile { get; set; }
		#endregion

		#region public Int32 FileId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("FileId")]
		public Int32 FileId { get; set; }
		#endregion

		#region Implement of IKitRequired

		#region public string KitParentString { get; }
		/// <summary>
		/// ���������� ������ ��� �������� �������� ����
		/// </summary>
		public string KitParentString
		{
			get
			{
				return $"Templ.N-Rout. job:{AtaChapter} {Title}";
			}
		}
		#endregion

		public CommonCollection<AccessoryRequired> Kits { get; set; }
		#endregion

		/*
		*  ������ 
		*/

		#region public TemplateNonRoutineJob()
		/// <summary>
		/// ������� ���������� ������ ��� �������������� ����������
		/// </summary>
		public TemplateNonRoutineJob()
		{
			SmartCoreObjectType = SmartCoreType.NonRoutineJob;

			Kits = new CommonCollection<AccessoryRequired>();
		}
		#endregion
	  
		#region public override string ToString()
		/// <summary>
		/// ����������� ��� �������
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return (AtaChapter != null ? "ata: "+ AtaChapter.ShortName + " ": "") +  Title;
		}
		#endregion   

	}

}
