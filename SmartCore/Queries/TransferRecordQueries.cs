/*
* Class code generate programm
* Date: 12.07.2010
* Database: CasDemo
* Table: TransferRecord
*/

using System;
using System.Collections.Generic;
using System.Data; // DataRow, DataTable, DataSet
    // SqlParameter
using SmartCore.Entities.General;


namespace SmartCore.Queries
{
    /// <summary>
    /// ������� ��� ��������� ������ � ������������
    /// </summary>
	public static class TransferRecordQueries
    {

		#region public static String GetSelectQuery()
		/// <summary>
		/// ���������� ������ SQL ������� �� �������������� ������ �� �� 
		/// </summary>
		private static String GetSelectQuery() 
		{
            return BaseQueries.GetSelectQuery<TransferRecord>();
		}
		#endregion

        #region public static String GetSelectQuery(String Statement)
        /// <summary>
        /// ���������� ������ �� ��������� ������ � ����������� ������� �������� ��������
        /// </summary>
        public static String GetSelectQuery(String statement)
        {
            return GetSelectQuery() + string.Format(@"where IsDeleted = 0 and {0}", statement);
        }
        #endregion

        #region private static void Fill(DataRow row, CorrectiveAction item)
        /// <summary>
        /// ��������� ���� 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="item"></param>
        private static void Fill(DataRow row, TransferRecord item)
        {
            BaseQueries.Fill(row, item);
        }
        #endregion

        #region public static List<TransferRecord> GetTransferRecordList(DataTable table)
        /// <summary>
        /// �������� ������ ������� � �����������
        /// </summary>
        /// <param name="table"></param>
		/// <returns></returns>
        public static List<TransferRecord> GetTransferRecordList(DataTable table)
        {
            List<TransferRecord> items = new List<TransferRecord>();
            
            for (int i=0; i < table.Rows.Count;i++)
            {
				TransferRecord item = new TransferRecord();
                Fill(table.Rows[i], item);
                items.Add(item);
            }
            //
            return items;
        }
        #endregion
	} 
}
  
  
  
  
  
  
