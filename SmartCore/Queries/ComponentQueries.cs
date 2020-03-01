/*
* Class code generate programm
* Date: 12.07.2010
* Database: CasDemo
* Table: Detail
*/

using System;
    // DataRow, DataTable, DataSet
    // SqlParameter
using System.Collections.Generic;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Store;
using SmartCore.Filters;


namespace SmartCore.Queries
{

    // Easa data �� ��������������

    public static class ComponentQueries
    {

        #region public static string GetSelectQuery(DirectiveType directiveType, IEnumerable<IQueryFilter> filters = null, bool loadChild = false, bool getDeleted = false)

        /// <summary>
        /// ���������� ������ ������� �� ��������� ����������� ������
        /// </summary>
        /// <param name="store">�����, �������� ����������� ����������</param>
        /// <param name="filters">������� ��� �����������</param>
        /// <param name="loadChild">��������� �������� ��������</param>
        /// <param name="getDeleted">��������� ���������������� ������</param>
        /// <returns></returns>
        public static string GetSelectQuery(Store store,
                                            ICommonFilter[] filters = null,
                                            bool loadChild = false,
                                            bool getDeleted = false)
        {
            List<ICommonFilter> allFilters = new List<ICommonFilter>();
            if (filters != null && filters.Length > 0)
                allFilters.AddRange(filters);
            allFilters.Add(GetWhereStatement(store));
            string qrs = BaseQueries.GetSelectQueryWithWhere<Entities.General.Accessory.Component>(allFilters.ToArray(), loadChild, getDeleted);
            return qrs;

        }
		#endregion

		#region private static ICommonFilter GetWhereStatement(BaseComponent baseComponent)
		/// <summary>
		/// ���������� ������� ���� ������ ����������� �������� ��������
		/// </summary>
		/// <param name="baseComponent"></param>
		/// <returns></returns>
		private static ICommonFilter GetWhereStatement(BaseComponent baseComponent)
        {
            return
                new CommonFilter<string>(string.Format(@"({0}  in (select top 1 destinationobjectId from dbo.TransferRecords where 
                                 dbo.Components.ItemId=Parentid and isdeleted=0 
								 and parenttype = {2} and destinationobjecttype = {1} 
							     order by transferDate desc  )

						and  {1} in (select top 1 destinationobjecttype 
                                        from dbo.TransferRecords 
                                        where dbo.Components.ItemId=Parentid and isdeleted=0 and 
                                        parenttype = {2}
                                        order by transferDate desc ))",
                    baseComponent.ItemId, baseComponent.SmartCoreObjectType.ItemId, SmartCoreType.Component.ItemId));
        }

		#endregion

		#region private static ICommonFilter GetWhereStatement(List<BaseComponent> baseComponent)
		/// <summary>
		/// ���������� ������� ���� ������ ����������� �������� ��������
		/// </summary>
		/// <param name="baseComponent"></param>
		/// <returns></returns>
		private static ICommonFilter GetWhereStatement(List<BaseComponent> baseComponent)
        {
            string s = "(";
            for (int i = 0; i < baseComponent.Count; i++)
            {
                s += baseComponent[i].ItemId;
                if (i < baseComponent.Count - 1)
                    s += ",";
            }
            s += ")";

            var res = $@"Components.ItemId in (select ParentID from TransferRecords 
            where DestinationObjectID in {s}
            and DestinationObjectType = {SmartCoreType.BaseComponent.ItemId} 
            and ParentType = {SmartCoreType.Component.ItemId}
            and IsDeleted = 0 )";

            return new CommonFilter<string>(res);
                new CommonFilter<string>($@"((select top 1 destinationobjectId from dbo.TransferRecords where 
                                 dbo.Components.ItemId=Parentid and isdeleted=0 
								 and parenttype = 5 and destinationobjecttype = 6 
							     order by transferDate desc ) in {s}

						and  {SmartCoreType.BaseComponent.ItemId} in (select top 1 destinationobjecttype 
                                        from dbo.TransferRecords 
                                        where dbo.Components.ItemId=Parentid and isdeleted=0 and 
                                        parenttype = {SmartCoreType.Component.ItemId}
                                        order by transferDate desc ))");
        }

        #endregion

        #region private static ICommonFilter GetWhereStatement(Store parentStore)
        /// <summary>
        /// ���������� ������� ���� ������ ����������� ������
        /// </summary>
        /// <param name="parentStore"></param>
        /// <returns></returns>
        private static ICommonFilter GetWhereStatement(Store parentStore)
        {
            //��� �� ������� ������ ��� ������ �� ��������� ������ � ����������� 
            //������ ��������� ID ������� ������, � ��� ������������� ������� - ��� ������ (0) 

            //������ ����� ���� �� ��������������� ������ � ������� ID ������ 
            //����� ��������� � ����������

            //������ ����� ��������� �������� �� ������ �� ������� ������������� ������ - �������
            //���� ��� ������� ��������� � ������ ����� �������, �� �� ������ 
            //�� ��������� ������ � ����������� ��� ������ ������

            //������ ����� ���� ������, ������� ���� ���������� �� ������, 
            //�� ����������� �� ���� ������������
            return new CommonFilter<string>(string.Format(@"({0} in (select top 1 destinationobjectId 
                                        from dbo.TransferRecords 
                                        where dbo.Components.ItemId=Parentid and isdeleted=0 and 
                                        parenttype = {1} and destinationobjecttype = {2} 
                                        order by transferDate desc ) 
                                            and  {2} in (select top 1 [destinationobjecttype] 
                                        from dbo.TransferRecords 
                                        where dbo.Components.ItemId=Parentid and isdeleted=0 and 
                                        parenttype = {1}
                                        order by transferDate desc ))",
                    parentStore.ItemId, SmartCoreType.Component.ItemId, parentStore.SmartCoreObjectType.ItemId));
        }

        #endregion

        #region private public static string GetWhereStatement(Operator @operator)
        /// <summary>
        /// ���������� ������� ����, ����������� �� ������ ��������� (��� ��������� ������������)
        /// </summary>
        /// <param name="operator"></param>
        /// <returns></returns>
        private static string GetWhereStatement(Operator @operator)
        {
            //��� �� ������� ������ ��� ������ �� ��������� ������ � ����������� 
            //������ ��������� ID ������� ���������, � ��� ������������� ������� - ��� ������ (0) 

            //������ ����� ���� �� ��������������� ������ � ������� ID ��������� 
            //����� ��������� � ����������

            //������ ����� ��������� �������� �� ������ �� ������� ������������� ������ - ����������
            //���� ��� ������� ��������� � ������ ����� �������, �� �� ������ 
            //�� ��������� ������ � ����������� ��� ������ ������

            //������ ����� ���� ������, ������� ���� ���������� � ���������, 
            //�� ����������� �� ���� ������������
            return
                string.Format(@"
                                ({0} in (select top 1 destinationobjectId 
                                        from dbo.TransferRecords 
                                        where dbo.Components.ItemId=Parentid and isdeleted=0 and 
                                        parenttype = {1} and destinationobjecttype = {2} 
                                        order by transferDate desc ) 
                           and  {2} in (select top 1 [destinationobjecttype] 
                                        from dbo.TransferRecords 
                                        where dbo.Components.ItemId=Parentid and isdeleted=0 and 
                                        parenttype = {1}
                                        order by transferDate desc ))",
                    @operator.ItemId, SmartCoreType.Component.ItemId, @operator.SmartCoreObjectType.ItemId);
        }

		#endregion

		#region public static ICommonFilter GetWhereStatement(int aircraftId)
		/// <summary>
		/// ���������� ������� ���� ������ ����������� ���������� �����
		/// </summary>
		/// <param name="aircraftId"></param>
		/// <returns></returns>
		public static ICommonFilter GetWhereStatement(int aircraftId)
        {
            return new CommonFilter<string>(string.Format(
	            $@"(Select top 1 DestinationObjectId from dbo.TransferRecords Where 
                    ParentType = {SmartCoreType.Component.ItemId} 
					and ParentId = dbo.Components.ItemId --dbo.Components.ComponentId
					and IsDeleted = 0
					order by dbo.TransferRecords.TransferDate Desc) in (Select bd.ItemId from dbo.Components bd where bd.IsBaseComponent = 1 and bd.IsDeleted = 0 and" +
	            $@"( Select top 1 DestinationObjectId from dbo.TransferRecords Where 
					ParentType = {SmartCoreType.BaseComponent.ItemId} 
					and ParentId = bd.ItemId
					and IsDeleted = 0
					order by dbo.TransferRecords.TransferDate Desc) = {aircraftId}" + ")"));
        }
		#endregion

		#region public static string GetSelectQuery(int aircraftId, ICommonFilter[] filters = null, bool loadChild = false, bool getDeleted = false)

		/// <summary>
		/// ���������� ������ ������� �� ��������� ����������� ������
		/// </summary>
		/// <param name="aircraftId">Id ��, �������� ����������� ����������</param>
		/// <param name="filters">������� ��� �����������</param>
		/// <param name="loadChild">��������� �������� ��������</param>
		/// <param name="getDeleted">��������� ���������������� ������</param>
		/// <returns></returns>
		public static string GetSelectQuery(int aircraftId,
                                            ICommonFilter[] filters = null,
                                            bool loadChild = false,
                                            bool getDeleted = false)
        {
            List<ICommonFilter> allFilters = new List<ICommonFilter>();
            if (filters != null && filters.Length > 0)
                allFilters.AddRange(filters);
            allFilters.Add(GetWhereStatement(aircraftId));
            string qrs = BaseQueries.GetSelectQueryWithWhere<Entities.General.Accessory.Component>(allFilters.ToArray(), loadChild, getDeleted);
            return qrs;

        }
		#endregion

		#region public static String GetSelectQuery(BaseComponent component)
		/// <summary>
		/// ���������� ������ SQL ������� �� �������������� ������ �� �� 
		/// </summary>
		public static String GetSelectQuery(BaseComponent component, 
                                            ICommonFilter[] filters = null,
                                            bool loadChild = false,
                                            bool getDeleted = false)
        {
            List<ICommonFilter> allFilters = new List<ICommonFilter>();
            if (filters != null && filters.Length > 0)
                allFilters.AddRange(filters);
            allFilters.Add(GetWhereStatement(component));
            string qrs = BaseQueries.GetSelectQueryWithWhere<Entities.General.Accessory.Component>(allFilters.ToArray(), loadChild, getDeleted);
            return qrs;

        }
		#endregion

		#region public static String GetSelectQuery(List<BaseComponent> components)
		/// <summary>
		/// ���������� ������ SQL ������� �� �������������� ������ �� �� 
		/// </summary>
		public static String GetSelectQuery(List<BaseComponent> components,
                                            ICommonFilter[] filters = null,
                                            bool loadChild = false,
                                            bool getDeleted = false)
        {
            List<ICommonFilter> allFilters = new List<ICommonFilter>();
            if (filters != null && filters.Length > 0)
                allFilters.AddRange(filters);
            allFilters.Add(GetWhereStatement(components));
            string qrs = BaseQueries.GetSelectQueryWithWhere<Entities.General.Accessory.Component>(allFilters.ToArray(), loadChild, getDeleted);
            return qrs;

        }
		#endregion

		public static string GetSelectQueryAll(
			string text,
			bool loadChild = false,
			bool getDeleted = false)
		{
			List<ICommonFilter> allFilters = new List<ICommonFilter>();
			string qrs = BaseQueries.GetSelectQueryWithWhere<Entities.General.Accessory.Component>(allFilters.ToArray(), loadChild, getDeleted);
			return qrs + $" and (Components.PartNumber like '%{text}%' or Components.SerialNumber like '%{text}%' or Components.Description like '%{text}%')";

		}

		#region public static String GetSelectQuery(BaseComponent component, bool llpMark)
		/// <summary>
		/// ���������� ������ SQL ������� �� �������������� ������ �� �� 
		/// </summary>
		public static string GetSelectQuery(BaseComponent component, bool llpMark)
        {
            ICommonFilter baseComponentFilter = GetWhereStatement(component);
            ICommonFilter llpMarkFilter = new CommonFilter<bool>(Entities.General.Accessory.Component.LLPMarkProperty, llpMark);

            return BaseQueries.GetSelectQueryWithWhere<Entities.General.Accessory.Component>(new[]{llpMarkFilter, baseComponentFilter}, true);
        }
		#endregion

		#region public static String GetSelectDetailonStoreQuery(Store store)
		///// <summary>
		///// ���������� ������ SQL ������� �� �������������� ������ �� �� 
		///// </summary>
		//public static String GetSelectDetailonStoreQuery(Store store)
		//{
		//    return BaseQueries.GetSelectQueryWithWhere<Detail>() + " and " + GetWhereStatement(store);

		//}
		#endregion


		#region public static String GetSelectComponentonOperatorQuery(Operator @operator)
		/// <summary>
		/// ���������� ������ SQL ������� �� �������������� ������ �� �� 
		/// </summary>
		public static string GetSelectComponentonOperatorQuery(Operator @operator)
        {
            return BaseQueries.GetSelectQueryWithWhere<Entities.General.Accessory.Component>() + " and " + GetWhereStatement(@operator);

        }
		#endregion

		#region public static string GetSelectQueryPrimaryColumnOnly(Aircraft aircraft)

		/// <summary>
		/// ���������� ������� ���� ������ ����������� �������� ��������
		/// </summary>
		/// <param name="aircraftId"></param>
		/// <returns></returns>
		public static string GetSelectQueryPrimaryColumnOnly(int aircraftId)
        {
            return BaseQueries.GetSelectQueryColumnOnly<BaseComponent>(BaseEntityObject.ItemIdProperty) +
                   $@" and ( Select top 1 DestinationObjectId 
                                           from dbo.TransferRecords 
                                           Where ParentType = {SmartCoreType.BaseComponent.ItemId}
                                           and DestinationObjectType = {SmartCoreType.Aircraft.ItemId} 
				                           and ParentId = dbo.Components.ItemId 
					                       and IsDeleted = 0) = {aircraftId}";
        }

        #endregion
    }
}
  
  
  
  
  
  
