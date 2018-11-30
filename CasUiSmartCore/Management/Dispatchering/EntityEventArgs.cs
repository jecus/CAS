using System.ComponentModel;
using CAS.UI.Interfaces;

namespace CAS.UI.Management.Dispatchering
{
    /// <summary>
    /// ��������� ��� ������� ������������, ������� ����� ���� ��������
    /// </summary>
    public class EntityCancelEventArgs : CancelEventArgs
    {
        #region Fields
        
        /// <summary>
        /// ����������, � ������� ��������� �������
        /// </summary>
        private IDisplayingEntity _entity;

        #endregion
        
        #region Constructors

        /// <summary>
        /// ������� ����� ������ ���������
        /// </summary>
        /// <param name="entity">����������, � ������� ��������� �������</param>>
        public EntityCancelEventArgs(IDisplayingEntity entity)
        {
            _entity = entity;
        }

        #endregion

        #region Properties
        /// <summary>
        /// ����������, � ������� ��������� �������
        /// </summary>
        public IDisplayingEntity Entity
        {
            get { return _entity; }
        }

        #endregion
    }
}