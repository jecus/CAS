using System;

namespace CAS.UI.UIControls.Auxiliary
{
    /// <summary>
    /// ��������� ������� SelectedItemChange
    /// </summary>
    public class SelectedItemsChangeEventArgs : EventArgs
    {
        private readonly int _itemsCount;

        /// <summary>
        /// ��������� ��������� ������� SelectedItemChange
        /// </summary>
        /// <param name="itemsCount">���������� ���������� ���������</param>
        public SelectedItemsChangeEventArgs(int itemsCount)
        {
            _itemsCount = itemsCount;
        }

        /// <summary>
        /// ���������� ���������� ���������
        /// </summary>
        public int ItemsCount
        {
            get { return _itemsCount; }
        }
    }
}
