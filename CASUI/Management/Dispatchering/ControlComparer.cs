using System.Collections.Generic;
using System.Windows.Forms;
using LTR.UI.Interfaces;

namespace LTR.UI.Management.Dispatchering
{
    /// <summary>
    /// �����, ��������������� ��� ���������� ��������� ReferenceButton
    /// </summary>
    internal class ControlComparer : IComparer<Control>
    {
        #region IComparer<ReferenceButton> Members

        /// <summary>
        /// ���������� ��� �������� ���������� �� ������, ������������� ���
        /// </summary>
        public int Compare(Control x, Control y)
        {
            return string.Compare(x.Text, y.Text);
        }

        #endregion
    }
}
