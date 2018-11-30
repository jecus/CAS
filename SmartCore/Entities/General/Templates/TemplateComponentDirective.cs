using System;
using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Attributes;

namespace SmartCore.Entities.General.Templates
{

    /// <summary>
    /// ����� ��������� ��������� �����
    /// </summary>
    [Serializable]
    [Table("ComponentDirectives", "Template", "ItemId")]
    public class TemplateComponentDirective: BaseEntityObject, IComparable<TemplateComponentDirective>
    {

        /*
        *  �������� �� ���� ������
        */
        #region public Int32 TemplatelId { get; set; }
        /// <summary>
        /// ��� �������, �������� ����������� ������ ������
        /// </summary>
        [TableColumnAttribute("TemplateId")]
        public Int32 TemplatelId { get; set; }
		#endregion

		#region public Int32 ComponentId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[TableColumnAttribute("ComponentId")]
        public int ComponentId { get; set; }
		#endregion

		#region public Double ManHours { get; set; }
		/// <summary>
		/// 
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

		#region public ComponentRecordType DirectiveType { get; set; }

		/// <summary>
		/// ��� ���������
		/// </summary>
		[TableColumnAttribute("DirectiveTypeId"), ListViewData("Directive Type")]
        public ComponentRecordType DirectiveType { get; set; }
		#endregion

		#region public ComponentDirectiveThreshold Threshold { get; set; }
		/// <summary>
		/// ������� ���������� ���������
		/// </summary>
		[TableColumnAttribute("Threshold"), ListViewData("Threshold")]
        public ComponentDirectiveThreshold Threshold { get; set; }
		#endregion

		#region public NDTType NDTType { get; set; }
		/// <summary>
		/// ��� ������������� Non-Destructive-Test
		/// </summary>
		[TableColumnAttribute("NDTType"), ListViewData("NDT")]
		public NDTType NDTType { get; set; }
		#endregion

		/*
		*  ������ 
		*/

		#region public TemplateComponentDirective()
		/// <summary>
		/// ������� ��������� ����� ��� �������������� ����������
		/// </summary>
		public TemplateComponentDirective()
        {
            SmartCoreObjectType = SmartCoreType.ComponentDirective;
            ItemId = -1;
        }
        #endregion
      
        #region public override string ToString()
        /// <summary>
        /// ����������� ��� �������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DirectiveType.ToString();
        }
		#endregion

		#region public int CompareTo(TemplateComponentDirective y)

		public int CompareTo(TemplateComponentDirective y)
        {
            return ItemId.CompareTo(y.ItemId);
        }

		#endregion

		/*
         * �������������� ��������
         */

		#region public Component ParentComponent { get; internal set; }
		/// <summary>
		/// �������, ��� �������� ������ ������ ������ - ����� �������� ��� �������� ParentComponent � ParentBaseComponent - ���� �� ��� ����� null
		/// </summary>
		public Accessory.Component ParentComponent { get; internal set; }
		#endregion

		#region public BaseComponent ParentBaseComponent { get; internal set; }
		/// <summary>
		/// ������� �������, ��� �������� ������ ������ ������ - ����� �������� ��� �������� ParentComponent � ParentBaseComponent - ���� �� ��� ����� null
		/// </summary>
		public BaseComponent ParentBaseComponent { get; internal set; }
        #endregion
    }

}
