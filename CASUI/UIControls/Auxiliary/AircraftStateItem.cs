using System;
using CAS.Core.Types.Aircrafts;
using CAS.UI.Management;
using CAS.Core.Types.Dictionaries;

namespace CAS.UI.UIControls.Auxiliary
{
    /// <summary>
    /// �����, ����������� ����������� ��������� �� � ������� ����������
    /// </summary>
    public class AircraftStateItem : AbstractAircraftStateItem
    {

        #region Fields

        private Aircraft currentItem;
        private readonly Icons icons = new Icons();

        #endregion

        #region Constructors

        #region public AircraftStateItem()

        ///<summary>
        /// ��������� �����, ����������� ����������� ��������� �� � ������� ����������
        ///</summary>
        public AircraftStateItem()
        {
        }

        #endregion

        #region public AircraftStateItem(Aircraft currentItem)

        /// <summary>
        /// ��������� ����� ������ ����������� ��
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public AircraftStateItem(Aircraft currentItem)
        {
            if (currentItem == null) throw new ArgumentNullException("currentItem");
                CurrentItem = currentItem;
        }

        #endregion

        #endregion

        #region Properties

        #region public Aircraft CurrentItem
        ///<summary>
        /// ������� ��
        ///</summary>
        public Aircraft CurrentItem
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
                if (currentItem.ConditionState == DirectiveConditionState.NotSatisfactory)
                {
                    Icon = icons.RedArrow;
                }
                else if (currentItem.ConditionState == DirectiveConditionState.Notify)
                {
                    Icon = icons.OrangeArrow;
                }
                else
                {
                    Icon = icons.GreenArrow;
                }
                TextMain = currentItem.RegistrationNumber;
                TextSecondary = currentItem.Model;
            }
        }

        #endregion

        #endregion


    }
}
