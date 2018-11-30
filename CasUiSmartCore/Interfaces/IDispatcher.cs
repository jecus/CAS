using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CAS.UI.Interfaces;

namespace CAS.UI.Interfaces
{
    internal abstract class IDispatcher
    {
        #region Fields
        /// <summary>
        /// Proxy for collection of Displayers
        /// </summary>
        protected IDisplayerCollectionProxy defaultProxy;
        #endregion

        #region private void SetDefaultProxy(IDisplayerCollectionProxy proxy)
        internal virtual void SetDefaultProxy(IDisplayerCollectionProxy proxy)
        {
            if (proxy == null) throw new ArgumentNullException("proxy");
            defaultProxy = proxy;
        }
        #endregion

        /// <summary>
        /// ���������� ������������� ������������ � ��������� �������� ��������
        /// </summary>
        /// <param name="control">�������, ������� �������� ����� �����������</param>
        internal abstract void ProcessControl(Control control);

        /// <summary>
        /// ���������� ������� ������������ ��������� ������� ��������
        /// </summary>
        /// <param name="control">�������, �� ������� �������� ���������� ����������</param>
        internal abstract void UnProcessControl(Control control);
    }
}