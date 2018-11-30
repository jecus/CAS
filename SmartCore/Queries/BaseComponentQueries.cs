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

    public static class BaseComponentQueries
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

            return
                new CommonFilter<string>(string.Format(@"((select top 1 destinationobjectId from dbo.TransferRecords where 
                                 dbo.Components.ItemId=Parentid and isdeleted=0 
								 and parenttype = 5 and destinationobjecttype = 6 
							     order by transferDate desc ) in {0}

						and  {1} in (select top 1 destinationobjecttype 
                                        from dbo.TransferRecords 
                                        where dbo.Components.ItemId=Parentid and isdeleted=0 and 
                                        parenttype = {2}
                                        order by transferDate desc ))",
                    s, SmartCoreType.BaseComponent.ItemId, SmartCoreType.Component.ItemId));
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

        #region private static string GetWhereStatement(Operator @operator)
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

		#region private static ICommonFilter GetWhereStatement(int aircraftId)
		/// <summary>
		/// ���������� ������� ���� ������� ������� ����������� ���������� �����
		/// </summary>
		/// <param name="aircraftId"></param>
		/// <returns></returns>
		public static ICommonFilter GetWhereStatement(int aircraftId)
        {
            return new CommonFilter<string>(String.Format(@"( Select top 1 DestinationObjectId 
                                        from dbo.TransferRecords 
                                        Where ParentType = {0} and 
                                              ParentId = [dbo].Components.ItemId and 
                                              IsDeleted = 0
					                    order by dbo.TransferRecords.TransferDate Desc) = {1}",
                                        SmartCoreType.BaseComponent.ItemId, aircraftId));
        }
        #endregion

        #region public static string GetSelectQuery(Aircraft aircraft, IEnumerable<IQueryFilter> filters = null, bool loadChild = false, bool getDeleted = false)

        /// <summary>
        /// ���������� ������ ������� �� ��������� ����������� ������
        /// </summary>
        /// <param name="aircraft">��, �������� ����������� ����������</param>
        /// <param name="filters">������� ��� �����������</param>
        /// <param name="loadChild">��������� �������� ��������</param>
        /// <param name="getDeleted">��������� ���������������� ������</param>
        /// <returns></returns>
        public static string GetSelectQuery(Aircraft aircraft,
                                            ICommonFilter[] filters = null,
                                            bool loadChild = false,
                                            bool getDeleted = false)
        {
            List<ICommonFilter> allFilters = new List<ICommonFilter>();
            if (filters != null && filters.Length > 0)
                allFilters.AddRange(filters);
            allFilters.Add(GetWhereStatement(aircraft.ItemId));
            string qrs = BaseQueries.GetSelectQueryWithWhere<BaseComponent>(allFilters.ToArray(), loadChild, getDeleted);
            return qrs;

        }
        #endregion

        #region public static List<DbQuery> GetSelectQueries(Aircraft aircraft, IEnumerable<IQueryFilter> filters = null, bool loadChild = false, bool getDeleted = false)

        /// <summary>
        /// ���������� ������ ������� �� ��������� ���� ����������� ���������� �����
        /// </summary>
        /// <param name="aircraft">��, �������� �������� ���������� ��������</param>
        /// <param name="filters">������� ��� Maintenance Directives</param>
        /// <param name="loadChild">��������� �������� ��������</param>
        /// <param name="getDeleted">��������� ���������������� ������</param>
        /// <returns></returns>
        public static List<DbQuery> GetSelectQueries(Aircraft aircraft,
                                                     ICommonFilter[] filters = null,
                                                     bool loadChild = false,
                                                     bool getDeleted = false)
        {
            List<ICommonFilter> allFilters = new List<ICommonFilter>();
            if (filters != null && filters.Length > 0)
                allFilters.AddRange(filters);
            allFilters.Add(GetWhereStatement(aircraft.ItemId));
            List<DbQuery> qrs = BaseQueries.GetSelectQueryWithWhereAll<BaseComponent>(allFilters.ToArray(), loadChild, getDeleted);
            return qrs;

        }
		#endregion

		#region public static String GetSelectQuery(BaseComponent component)
		/// <summary>
		/// ���������� ������ SQL ������� �� �������������� ������ �� �� 
		/// </summary>
		public static string GetSelectQuery(BaseComponent component, 
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

		#region public static String GetSelectQuery(BaseComponent component, bool llpMark)
		/// <summary>
		/// ���������� ������ SQL ������� �� �������������� ������ �� �� 
		/// </summary>
		public static String GetSelectQuery(BaseComponent component, bool llpMark)
        {
            return BaseQueries.GetSelectQueryWithWhere<Entities.General.Accessory.Component>() + " and LLPMark = " + (llpMark ? 1 : 0) + " and " + GetWhereStatement(component);
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
		public static String GetSelectComponentonOperatorQuery(Operator @operator)
        {
            return BaseQueries.GetSelectQueryWithWhere<Entities.General.Accessory.Component>() + " and " + GetWhereStatement(@operator);

        }
        #endregion

        #region public static string GetSelectQueryPrimaryColumnOnly(Aircraft aircraft)

        /// <summary>
        /// ���������� ������� ���� ������ ����������� �������� ��������
        /// </summary>
        /// <param name="aircraft"></param>
        /// <returns></returns>
        public static string GetSelectQueryPrimaryColumnOnly(Aircraft aircraft)
        {
            if (aircraft == null)
                throw new ArgumentNullException("aircraft", "must be not null");
            return BaseQueries.GetSelectQueryColumnOnly<BaseComponent>(BaseEntityObject.ItemIdProperty) +
                   string.Format(
					   @" and ( Select top 1 DestinationObjectId 
                                           from dbo.TransferRecords 
                                           Where ParentType = {0}
                                           and DestinationObjectType = {1} 
				                           and ParentId = dbo.Components.ItemId 
					                       and IsDeleted = 0) = {2}",
                   SmartCoreType.BaseComponent.ItemId, aircraft.SmartCoreObjectType.ItemId, aircraft.ItemId);
        }

        #endregion
    }
}
  
  
  
  
  
  
