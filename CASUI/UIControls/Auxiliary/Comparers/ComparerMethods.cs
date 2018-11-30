using System;
using System.Text.RegularExpressions;

namespace CAS.UI.UIControls.Auxiliary.Comparers
{
    /// <summary>
    /// ������ ���������� �������
    /// </summary>
    public static class ComparerMethods
    {
        #region public static bool GetTitleDigits(string text,out string year,out string nextDigits)
        /// <summary>
        /// ���������� ������ ���� � ��������� ���������
        /// </summary>
        /// <param name="text">����� ��������� �����</param>
        /// <param name="year">���</param>
        /// <returns>��������� true ���� �������</returns>
        /// <param name="nextString">��������� �� ����� ������</param>
        public static bool GetTitleDigits(string text,out string year,out string nextString)
        {
            Regex regex = new Regex(@"^([A-Za-z]{0,} |)(?<Result>\d{1,4}?)-(?<SecondaryResult>.+?)( |\z)");
            Match match = regex.Match(text);
            year = match.Success ? match.Groups["Result"].Value : null;
            nextString = match.Success ? match.Groups["SecondaryResult"].Value : null;
            return match.Success; 
        }
        #endregion

        #region private static string GetStringDigitsFromATAChapter(string text)

        ///<summary>
        /// ������� �� �������� ��� ����� �����
        ///</summary>
        ///<param name="text">����� �������� ��� �����</param>
        ///<returns></returns>
        private static string GetStringDigitsFromATAChapter(string text)
        {
            Regex regex = new Regex(@"^[0-9]{1,3}");
            Match match = regex.Match(text);
            return (match.Success) ? match.Value : "";
        }

        #endregion

        #region private static string GetADStatusTitleAfterSpacePart(string text)

        private static string GetADStatusTitleAfterSpacePart(string text)
        {
            Regex regex = new Regex(@" .*\z");
            Match match = regex.Match(text);
            return (match.Success) ? match.Value : "";
        }

        #endregion


        #region public static int ATACompare(string text1, string text2)

        ///<summary>
        /// ��������� ���� ��� ����
        ///</summary>
        ///<param name="text1"></param>
        ///<param name="text2"></param>
        ///<returns></returns>
        public static int ATACompare(string text1, string text2)
        {
            string firstStringNumber = GetStringDigitsFromATAChapter(text1);
            string secondStringNumber = GetStringDigitsFromATAChapter(text2);
            return (firstStringNumber != "" && secondStringNumber != "")
                       ?
                           Convert.ToInt32(secondStringNumber) - Convert.ToInt32(firstStringNumber)
                       : 0;
        }

        #endregion

        #region public static string GetDescriptionDigits(string text)
        /// <summary>
        /// ���������� ���� � ���� Description
        /// </summary>
        /// <param name="text">����� ��������� �����</param>
        /// <returns>�������� �����, NULL - �� ������ </returns>
        public static string GetDescriptionDigits(string text)
        {
            Regex regex = new Regex(@"^(?<Result>\d+?)[a-z]");
            Match match = regex.Match(text);
            return match.Success ? match.Groups["Result"].Value : null;
        }
        #endregion

        #region public static int AdStatusComparer(string text1, string text2)
        /// <summary>
        /// ��������� ���� ����� Title � ��������
        /// </summary>
        /// <param name="text1">������ ����</param>
        /// <param name="text2">������ ����</param>
        /// <returns>��������� ���������</returns>
        public static int AdStatusComparer(string text1, string text2)
        {
            string year1, year2, nextString1, nextString2;
            if (!GetTitleDigits(text1, out year1, out nextString1) || !GetTitleDigits(text2, out year2, out nextString2))
                return string.Compare(text1, text2);
            int firstValue = int.Parse(year1);
            int secondValue = int.Parse(year2);

            if (firstValue - secondValue == 0)
            {
                int compareResult = string.Compare(nextString1, nextString2);
                if (compareResult == 0)
                {
                    return AdCompareAfterSpacePart(text1, text2);
                }
                else return compareResult;
            }
            else return firstValue - secondValue;
            
        }
        #endregion

        #region private static int AdCompareAfterSpacePart(string text1, string text2)
        /// <summary>
        /// C�������� ����������� ��������� AD Status ����� �������
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        private static int AdCompareAfterSpacePart(string text1, string text2)
        {
            return string.Compare(GetADStatusTitleAfterSpacePart(text2), GetADStatusTitleAfterSpacePart(text1));
        }

        #endregion

        #region public static int DescriptionComparer(string text1, string text2)
        /// <summary>
        /// ��������� ���� ����� Description � ���������
        /// </summary>
        /// <param name="text1">������ ����</param>
        /// <param name="text2">������ ����</param>
        /// <returns>��������� ���������</returns>
        public static int DescriptionComparer(string text1, string text2)
        {
            int firstValue, secondValue;
            if (int.TryParse(GetDescriptionDigits(text1), out firstValue) |
                int.TryParse(GetDescriptionDigits(text2), out secondValue))
            {
                return firstValue - secondValue;
            }
            return string.Compare(text1, text2);
        }
        #endregion

    }
}
