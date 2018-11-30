using System;
using CAS.Core.Types.Aircrafts;

namespace CAS.UI.UIControls.Auxiliary
{
    /// <summary>
    /// ������ ����������� ���������� ��
    /// </summary>
    public class TemplateAircraftStateItem : AbstractAircraftStateItem
    {

        #region Fields

        private TemplateAircraft currentItem;

        #endregion

        #region Constructors

        #region public AircraftStateItem()

        /// <summary>
        /// ��������� ����� ������ ����������� ���������� ��
        /// </summary>
        public TemplateAircraftStateItem()
        {

        }

        #endregion

        #region public TemplateAircraftStateItem(TemplateAircraft currentItem)

        /// <summary>
        /// ��������� ����� ������ ����������� ���������� ��
        /// </summary>
        /// <param name="currentItem">������������ ������</param>
        public TemplateAircraftStateItem(TemplateAircraft currentItem)
        {
            if (currentItem == null) throw new ArgumentNullException("currentItem");
                CurrentItem = currentItem;
        }

        #endregion

        #endregion

        #region Properties

        #region public TemplateAircraft CurrentItem
        ///<summary>
        /// ������� ��������� ��
        ///</summary>
        public TemplateAircraft CurrentItem
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
                TextMain = currentItem.Model;
        }

        #endregion

        #endregion
    }
}
