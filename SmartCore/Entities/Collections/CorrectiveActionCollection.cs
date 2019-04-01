using System.Collections.Generic;
using SmartCore.Entities.General.Atlbs;

namespace SmartCore.Entities.Collections
{

    /// <summary>
    /// �������� ������ �������������� �������� ��� ������������ ���������� �����. 
    /// �� ����� ���� ���������� ����� ����� ������ ���� �������������� �������� (����� ���� � ������)
    /// </summary>
    public class CorrectiveActionCollection
    {
        #region private List<CorrectiveAction> _Components = new List<ComponentOilCondition>();
        /// <summary>
        /// ���������� ��������� ���������
        /// </summary>
        private List<CorrectiveAction>_Actions = new List<CorrectiveAction>();
        #endregion

        //#region private daLoggable dataAccessProvider = null;
        ///// <summary>
        ///// ������, ���������� �� ����� ��������� � ����� ������
        ///// </summary>
        //private daLoggable dataAccessProvider = null;
        //#endregion

        //#region protected override CAS.daCore.daLoggable CreateLoggableDataAccessProvider()
        ///// <summary>
        ///// ������� ������, ���������� �� ����� ��������� � ����� ������ 
        ///// </summary>
        ///// <returns></returns>
        //protected override CAS.daCore.daLoggable CreateLoggableDataAccessProvider()
        //{
        //    if (dataAccessProvider == null) dataAccessProvider = new daCorrectiveAction();
        //    return dataAccessProvider;
        //}
        //#endregion

        //#region public override bool HasPermission(UserRole userRole, DataEvent operation)
        ///// <summary>
        ///// ����� �� ������������ �������� ��������� ����� �� ���������� �������� ��������
        ///// </summary>
        ///// <param name="userRole">��������� ������������</param>
        ///// <param name="operation">��� ����������� ��������</param>
        ///// <returns>true, ���� ������� ����� �� ���������� ������ �������� � false ���� ���</returns>
        ///// <remarks>����� ������ ���� ����������� ������������� � �����������</remarks>
        //public override bool HasPermission(UserRole userRole, DataEvent operation)
        //{
        //    return DirectiveCollection.HasAccess(userRole, operation);
        //}
        //#endregion

        /// <summary>
        /// �������� ������ �������������� �������� ��� ���������� ���������� �����.
        /// �� ����� ���� ���������� ����� ����� ������ ���� �������������� �������� (����� ���� � ������)
        /// </summary>
        /// <param name="parent"></param>
        public CorrectiveActionCollection(Discrepancy parent)// : base(parent)
        {
        }

        public CorrectiveActionCollection()
        {
        }

        #region public CorrectiveAction this[int index]
        /// <summary>
        /// ������� ����� �������� ��� ����������� ���������� �������
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CorrectiveAction this[int index]
        {
            get { return _Actions[index]; }
            set { _Actions[index] = value; }
        }
        #endregion

        #region public int Count
        /// <summary>
        /// ���������� ���������
        /// </summary>
        public int Count
        {
            get { return _Actions.Count; }
        }
        #endregion

        #region public void Add(ComponentOilCondition oilCondition)
        /// <summary>
        /// ��������� ���������� �� �������� � ���������
        /// </summary>
        public void Add(CorrectiveAction Action )
        {
            _Actions.Add(Action);
            //    oilCondition.DataChange += ComponentOilConditionCollection_DataChange;
            //    OnCollectionChange();
        }
		#endregion

	    public void AddRange(IEnumerable<CorrectiveAction> action)
	    {
			_Actions.AddRange(action);
	    }

	    #region public void RemoveAt(int index)
			/// <summary>
			/// ������� ���������� �� �������� ��� �������� �������
			/// </summary>
			/// <param name="index"></param>
		public void RemoveAt(int index)
        {
            _Actions.RemoveAt(index);
            //    OnCollectionChange();
        }
        #endregion

        #region public void Remove(CorrectiveAction RemovedAction)
        /// <summary>
        /// ������� ���������� �� �������� ��������
        /// </summary>
        /// <param name="index"></param>
        public void Remove(CorrectiveAction RemovedAction)
        {
            int i;
            //CorrectiveAction action = null;
            for (i = 0; i<_Actions.Count; i++)
            {
                if(_Actions[i] == RemovedAction)
                {
                    _Actions.RemoveAt(i);
                    break;
                }
            }
            //_Actions.RemoveAt(i);
            //    OnCollectionChange();
        }
        #endregion

        #region public bool Contains(CorrectiveAction ContainedAction)
        /// <summary>
        /// ������� ���������� �� �������� ��������
        /// </summary>
        /// <param name="index"></param>
        public bool Contains(CorrectiveAction ContainedAction)
        {
            int i;
            for (i = 0; i < _Actions.Count; i++)
            {
                if (_Actions[i] == ContainedAction)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
}
