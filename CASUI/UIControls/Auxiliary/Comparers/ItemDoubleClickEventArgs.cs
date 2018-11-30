using System;

namespace CAS.UI.UIControls.Auxiliary.Comparers
{
    /// <summary>
    /// ��������� ������� ItemDoubleClick
    /// </summary>
    /// <typeparam name="T">��� ��������</typeparam>
    public class ItemDoubleClickEventArgs<T>:EventArgs 
    {
        private readonly T item;

        /// <summary>
        /// ��������� ��������� ������� ItemDoubleClick
        /// </summary>
        /// <param name="_item"></param>
        public ItemDoubleClickEventArgs(T _item)
        {
            item = _item;
        }
        /// <summary>
        /// �������
        /// </summary>
        public T Item
        {
            get { return item; }
        }
    }
}
