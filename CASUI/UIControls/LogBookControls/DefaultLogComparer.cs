using System.Collections.Generic;

namespace LTR.UI.UIControls.LogBookControls
{
    /// <summary>
    /// ����������� ��� ���������� ������ �������
    /// </summary>
    public class DefaultLogComparer : IComparer<AircraftsLogBookItem>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(AircraftsLogBookItem x, AircraftsLogBookItem y)
        {
            return
                string.Compare(x.RegistrationNumber, y.RegistrationNumber);
        }
    }
}
