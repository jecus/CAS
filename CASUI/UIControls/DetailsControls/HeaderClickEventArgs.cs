using System;

namespace CAS.UI.UIControls.DetailsControls
{
    /// <summary>
    /// ��������� ������� HeaderClick
    /// </summary>
    public class HeaderClickEventArgs:EventArgs
    {
        #region Fields
        private readonly string columnName;
        #endregion

        #region Constructors

        /// <summary>
        /// ��������� ��������� ������� HeaderClick
        /// </summary>
        /// <param name="columnName">��� ���������</param>
        public HeaderClickEventArgs(string columnName)
        {
            this.columnName = columnName;
        }

        #endregion

        #region Properties

        #region public string ColumnName

        /// <summary>
        /// ��� ���������
        /// </summary>
        public string ColumnName
        {
            get { return columnName; }
        }

        #endregion

        #endregion

    }
}
