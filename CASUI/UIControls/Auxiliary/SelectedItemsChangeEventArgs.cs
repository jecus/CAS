using System;

namespace CAS.UI.UIControls.Auxiliary
{
    /// <summary>
    /// ��������� ������� SelectedItemChange
    /// </summary>
    public class SelectedItemsChangeEventArgs : EventArgs
    {
        private readonly int itemsCount=0;

        /// <summary>
        /// ��������� ��������� ������� SelectedItemChange
        /// </summary>
        /// <param name="itemsCount">���������� ���������� ���������</param>
        public SelectedItemsChangeEventArgs(int itemsCount)
        {
            this.itemsCount = itemsCount;
        }

        /// <summary>
        /// ���������� ���������� ���������
        /// </summary>
        public int ItemsCount
        {
            get { return itemsCount; }
        }
    }
}
