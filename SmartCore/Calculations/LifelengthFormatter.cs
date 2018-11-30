using System;


namespace SmartCore.Calculations
{
    ///<summary>
    ///</summary>
    public class LifelengthFormatter
    {
        private TimeSpanFormatter hoursFormatter;
        private TimeSpanFormatter calendarFormatter;
        private int cyclesFieldLength;
        private readonly string notApplicableString = "";

        #region Constructors
        ///<summary>
        ///</summary>
        ///<param name="hoursFormatter"></param>
        ///<param name="cyclesFieldLength"></param>
        ///<param name="calendarFormatter"></param>
        public LifelengthFormatter(TimeSpanFormatter hoursFormatter, int cyclesFieldLength, TimeSpanFormatter calendarFormatter)
        {
            this.cyclesFieldLength = cyclesFieldLength;
            this.hoursFormatter = hoursFormatter;
            this.calendarFormatter = calendarFormatter;
        }

        /// <summary>
        /// ��������� ����������� ��������� �� ���������
        /// </summary>
        public LifelengthFormatter():this(new TimeSpanFormatter(0, 6, 0, false, true, false), 6, new TimeSpanFormatter(6, 0, 0, true, false, false))
        {
            
        }
        #endregion

        #region Methods

        #region public string GetCalendarMask()
        ///<summary>
        /// ����� ����������� ���������
        ///</summary>
        public string GetCalendarMask()
        {
            
            return calendarFormatter.GetMask();
        }
        #endregion

        #region public string GetHoursMask()
        ///<summary>
        /// ����� ��������� �� �����
        ///</summary>
        public string GetHoursMask()
        {
            return hoursFormatter.GetMask();
            
        }
        #endregion

        #region public string GetCyclesMask()
        ///<summary>
        /// ����� ��������� �� ������
        ///</summary>
        public string GetCyclesMask()
        {
            string mask = "";
            for (int i = 0; i < cyclesFieldLength; i++)
                mask = "9" + mask;
            return mask;
        }
        #endregion

        #region public string GetCalendarData(TimeSpan item)
        ///<summary>
        /// ����������� �������� ���������
        ///</summary>
        public string GetCalendarData(TimeSpan item)
        {
            return calendarFormatter.GetData(item);
        }
        #endregion

        #region public string GetCalendarData(Lifelength source, string calendarRemark)

        /// <summary>
        /// ����������� �������� ���������
        /// </summary>
        /// <param name="source">�������� ���������</param>
        /// <param name="calendarRemark">���������, ������ ����� ��������</param>
        /// <returns></returns>
        public string GetCalendarData(Lifelength source, string calendarRemark)
        {
            if (calendarRemark == "")
                return notApplicableString;
            if (source == null)
                return notApplicableString;
            if (source.Days!=null)
                return GetCalendarData(new Lifelength(source.Days,0,0)) + calendarRemark;
            return notApplicableString;
        }

        #endregion

        #region public string GetCalendarData(Lifelength source)

        /// <summary>
        /// ����������� �������� ���������
        /// </summary>
        /// <param name="source">�������� ���������</param>
        /// <returns></returns>
        public string GetCalendarData(Lifelength source)
        {
            int days=0;
            if (source.Days!=null)
                days = (int)Math.Round((double)source.Days);            

            string calendar="";
            if (days != 0)
            {
                if (((int)(days / 365) * 365 <= days) && ((int)(days / 365) * 366 >= days))
                    calendar = (days/365).ToString() + " year";

                else if (days%360 == 0)
                    calendar = (days/360).ToString() + " year";

                else if (days%30 == 0)

                    if ((days/30)%6 == 0)
                        calendar = (days/30/6).ToString() + " year";
                    else
                        calendar = (days/30).ToString() + " month";

                else
                    calendar = days.ToString() + " day";
            }

            return calendar;
        }

        #endregion


        #region public string GetHoursData(Lifelength source, string hoursRemark)

        ///<summary>
        /// ����������� �������� ��������� �� �����
        ///</summary>
        ///<param name="source">�������� ��������</param>
        ///<param name="hoursRemark">���������, ����� ��������</param>
        ///<returns></returns>
        public string GetHoursData(Lifelength source, string hoursRemark)
        {

            if (source == null)
                return notApplicableString;
            if (source.TotalMinutes!=null)
                return GetHoursData(new TimeSpan(0,0,(int)source.TotalMinutes,0)) + hoursRemark;
            return notApplicableString;
        }

        #endregion

        #region public string GetCyclesData(Lifelength source, string cyclesRemark)

        ///<summary>
        /// ����������� �������� ��������� �� ������
        ///</summary>
        ///<param name="source">�������� ���������</param>
        ///<param name="cyclesRemark"></param>
        ///<returns></returns>
        public string GetCyclesData(Lifelength source, string cyclesRemark)
        {

            if (source == null)
                return notApplicableString;
            if (source.Cycles!=null)
                return source.Cycles.ToString() + cyclesRemark;
            return notApplicableString;
        }

        #endregion

        #region public string GetHoursData(TimeSpan item)
        ///<summary>
        /// ������ ��������� �� �����
        ///</summary>
        public string GetHoursData(TimeSpan item)
        {
            return hoursFormatter.GetData(item);
        }
        #endregion

        #region public string GetData(Lifelength source, string hoursRemark, string cyclesRemark, string calendarRemark)
        ///<summary>
        /// ���������� ����������, ������������ ���������
        ///</summary>
        ///<param name="source">�������� ���������</param>
        ///<param name="hoursRemark">�����, ������ ����� �������� ��������� �� �����</param>
        ///<param name="cyclesRemark">�����, ������ ����� �������� ��������� �� ������</param>
        ///<param name="calendarRemark">�����, ������ ����� �������� ��������� �� ���������</param>
        ///<returns></returns>
        public string GetData(Lifelength source, string hoursRemark, string cyclesRemark, string calendarRemark)
        {
            return
                GetHoursData(source, hoursRemark) + GetCyclesData(source, cyclesRemark) + GetCalendarData(source, calendarRemark);
        }
        #endregion

        #region public string GetData(Lifelength source, string hoursRemark, string cyclesRemark)
        ///<summary>
        /// ���������� ����������, ������������ ���������
        ///</summary>
        ///<param name="source">�������� ���������</param>
        ///<param name="hoursRemark">�����, ������ ����� �������� ��������� �� �����</param>
        ///<param name="cyclesRemark">�����, ������ ����� �������� ��������� �� ������</param>
        ///<returns></returns>
        public string GetData(Lifelength source, string hoursRemark, string cyclesRemark)
        {
            return
                GetHoursData(source, hoursRemark) + GetCyclesData(source, cyclesRemark) + GetCalendarData(source);
        }
        #endregion

        #region public string GetData(Lifelength source, string hoursRemark, string cyclesRemark, string calendarRemark, bool showHours, bool showCycles, bool showCalendar)
        ///<summary>
        /// ���������� ����������, ������������ ���������
        ///</summary>
        ///<param name="source">�������� ���������</param>
        ///<param name="hoursRemark">�����, ������ ����� �������� ��������� �� �����</param>
        ///<param name="cyclesRemark">�����, ������ ����� �������� ��������� �� ������</param>
        ///<param name="calendarRemark">�����, ������ ����� �������� ��������� �� ���������</param>
        /// <param name="showHours"></param>
        /// <param name="showCycles"></param>
        /// <param name="showCalendar"></param>
        ///<returns></returns>
        public string GetData(Lifelength source, string hoursRemark, string cyclesRemark, string calendarRemark, bool showHours, bool showCycles, bool showCalendar)
        {
            string res = "";
            if (showHours)
                res += GetHoursData(source, hoursRemark);
            if (showCycles)
                res += GetCyclesData(source, cyclesRemark);
            if (showCalendar)
                res += GetCalendarData(source, calendarRemark);
            return res;
        }
        #endregion

        #region public string GetDataWithCalendar(Lifelength lifelength, string hoursRemark, string cyclesRemark, string calendarRemark)

        ///<summary>
        /// ���������� ����������, ������������ ���������. ����������� ��������� ������������ ��� ����
        ///</summary>
        ///<param name="lifelength">�������� ���������</param>
        ///<param name="hoursRemark">�����, ������ ����� �������� ��������� �� �����</param>
        ///<param name="cyclesRemark">�����, ������ ����� �������� ��������� �� ������</param>
        ///<param name="calendarRemark">�����, ������ ����� �������� ��������� �� ���������</param>
        ///<returns></returns>
        public string GetDataWithCalendar(Lifelength lifelength, string hoursRemark, string cyclesRemark, string calendarRemark)
        {
            return GetHoursData(lifelength, hoursRemark) + GetCyclesData(lifelength, cyclesRemark) + GetCalendar(lifelength, calendarRemark);
        }

        #endregion

        #region public string GetCalendar(Lifelength lifelength, string remark)

        /// <summary>
        /// ����������� ����������� ��������� ��� ����
        /// </summary>
        /// <param name="lifelength">�������� ���������</param>
        /// <param name="remark">��������� ����� ������</param>
        /// <returns></returns>
        public string GetCalendar(Lifelength lifelength, string remark)
        {
            return new DateTime(new TimeSpan(0,0,(int)lifelength.Days).Ticks).ToShortDateString() + remark;
        }

        #endregion

        #region public TimeSpanFormatter HoursFormatter
        ///<summary>
        /// ����������� ������� ����������
        ///</summary>
        public TimeSpanFormatter HoursFormatter
        {
            get { return hoursFormatter; }
            set { hoursFormatter = value; }
        }
        #endregion

        #region public TimeSpanFormatter CalendarFormatter
        ///<summary>
        /// ����������� ����������� ����������
        ///</summary>
        public TimeSpanFormatter CalendarFormatter
        {
            get { return calendarFormatter; }
            set { calendarFormatter = value; }
        }
        #endregion

        #region public int CyclesFieldLength
        ///<summary>
        /// ����������� ���������� �� ������
        ///</summary>
        public int CyclesFieldLength
        {
            get { return cyclesFieldLength; }
            set { cyclesFieldLength = value; }
        }
        #endregion

        #region public TimeSpan CalendarValueFromString(string source)
        /// <summary>
        /// ���������� �������� ����������� ��������� �� �������� ������
        /// </summary>
        /// <param name="source">�������� ������</param>
        /// <returns>���������� ��������</returns>
        public TimeSpan CalendarValueFromString(string source)
        {
            return calendarFormatter.ValueFromString(source);
        }
        #endregion

        #region public TimeSpan HoursValueFromString(string source)
        /// <summary>
        /// ���������� �������� ������� ��������� �� �������� ������
        /// </summary>
        /// <param name="source">�������� ������</param>
        /// <returns>���������� ��������</returns>
        public TimeSpan HoursValueFromString(string source)
        {
            return hoursFormatter.ValueFromString(source);
        }
        #endregion

        #region public int CyclesValueFromString(string source)
        ///<summary>
        /// ��������� ��������� �� ������ �� ������
        ///</summary>
        ///<param name="source">�������� ������</param>
        ///<returns>���������� ��������</returns>
        public int CyclesValueFromString(string source)
        {
            int value;
            int.TryParse(source, out value);
            return value;
        }
        #endregion

        #region public string GetHoursCyclesData(Lifelength source, string hoursRemark, string cyclesRemark)
        ///<summary>
        /// ���������� ����������, ������������ ��������� � ����� � ������
        ///</summary>
        ///<param name="source">�������� ���������</param>
        ///<param name="hoursRemark">�����, ������ ����� �������� ��������� �� �����</param>
        ///<param name="cyclesRemark">�����, ������ ����� �������� ��������� �� ������</param>
        ///<returns></returns>
        public string GetHoursCyclesData(Lifelength source, string hoursRemark, string cyclesRemark)
        {
            return
                GetHoursData(source, hoursRemark) + GetCyclesData(source, cyclesRemark);
        }
        #endregion
        #endregion

    }
}
