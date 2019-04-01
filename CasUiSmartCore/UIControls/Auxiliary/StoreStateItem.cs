using System;
using CAS.UI.Management;
using SmartCore.Entities.General.Store;

namespace CAS.UI.UIControls.Auxiliary
{
    /// <summary>
    /// �����, ����������� ����������� ��������� �� � ������� ����������
    /// </summary>
    public class StoreStateItem : AbstractAircraftStateItem
    {

        #region Fields

        private Store currentItem;
        private readonly Icons icons = new Icons();

        #endregion

        #region Constructors

        #region public StoreStateItem()

        ///<summary>
        /// ��������� �����, ����������� ����������� ��������� �� � ������� ����������
        ///</summary>
        public StoreStateItem()
        {
        }

        #endregion

        #region public StoreStateItem(Store currentItem)

        /// <summary>
        /// ��������� ����� ������ ����������� ��
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public StoreStateItem(Store currentItem)
        {
            if (currentItem == null) throw new ArgumentNullException("currentItem");
                CurrentItem = currentItem;
        }

        #endregion

        #endregion

        #region Properties

        #region public Store CurrentItem
        ///<summary>
        /// ������� ��
        ///</summary>
        public Store CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                UpdateInformation();
            }
        }
        #endregion

        #endregion

        #region Methods

        #region private void UpdateInformation()

        ///<summary>
        /// ����������� ������������ ����������
        ///</summary>
        private void UpdateInformation()
        {
            if (currentItem != null)
            {
                /*if (currentItem == DirectiveConditionState.NotSatisfactory)
                {
                    Icon = icons.RedArrow;
                }
                else if (currentItem.ConditionState == DirectiveConditionState.Notify)
                {
                    Icon = icons.OrangeArrow;
                }
                else
                {*/
                    Icon = icons.GreenArrow;
                //}
                TextMain = currentItem.Name;
                TextSecondary = currentItem.Location;
            }
        }

        #endregion

        #endregion


    }
}
