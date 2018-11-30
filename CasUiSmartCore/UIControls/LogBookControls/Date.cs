using System;
using System.Windows.Forms;

namespace CAS.UI.UIControls.LogBookControls
{
    /// <summary>
    /// �����, ��������������� ��� �������� ���� ����, ��� �������
    /// </summary>
    public class Date
    {

        #region Fields

        private DateTime date;

        #endregion
        
        #region Constructor

        /// <summary>
        /// ������� ����� ������ ���� Date ��� �������� ����
        /// </summary>
        public Date(DateTime value)
        {
            date = new DateTime(value.Year, value.Month, value.Day,0,0,0);
        }

        #endregion

        #region Properties

        #region public int Year

        /// <summary>
        /// ���������� ���������� ���
        /// </summary>
        public int Year
        {
            get
            {
                return date.Year;
            }
        }

        #endregion

        #region public int Month

        /// <summary>
        /// ���������� ���������� �������
        /// </summary>
        public int Month
        {
            get
            {
                return date.Month;
            }
        }

        #endregion

        #region public int Day

        /// <summary>
        /// ���������� ���������� ����
        /// </summary>
        public int Day
        {
            get
            {
                return date.Day;
            }
        }

        #endregion

        #region public DateTime Value

        /// <summary>
        /// ���������� ��� ������������� �������� (����)
        /// </summary>
        public DateTime Value
        {
            get
            {
                return date;
            }
            set
            {
                date = new DateTime(value.Year, value.Month, value.Day,0,0,0);
            }
        }

        #endregion

        #endregion

        #region Methods

        #region public void AddYears(int yearCount)

        /// <summary>
        /// ��������� ����
        /// </summary>
        /// <param name="yearCount">���������� ���</param>
        public void AddYears(int yearCount)
        {
            date.AddYears(yearCount);
        }

        #endregion

        #region public void AddMonths(int monthsCount)

        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="monthsCount">���������� �������</param>
        public void AddMonths(int monthsCount)
        {
            date.AddMonths(monthsCount);
        }

        #endregion

        #region public void AddMonths(int monthsCount)

        /// <summary>
        /// ��������� ���
        /// </summary>
        /// <param name="daysCount">���������� ����</param>
        public void AddDays(double daysCount)
        {
            date.AddDays(daysCount);
        }

        #endregion

        #region public DateTime ToDateTime()

        /// <summary>
        /// ��������������� �������� � DateTime
        /// </summary>
        public DateTime ToDateTime()
        {
            return date;
        }

        #endregion

        #endregion

    }
}
