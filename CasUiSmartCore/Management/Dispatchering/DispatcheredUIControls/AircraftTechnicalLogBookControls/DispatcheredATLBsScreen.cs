using System;
using System.ComponentModel;
using System.Windows.Forms;
using CAS.Core.Types.Aircrafts;
using CAS.UI.Interfaces;
using CAS.UI.UIControls.AircraftTechnicalLogBookControls;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.AircraftTechnicalLogBookControls
{
    /// <summary>
    /// ������� ���������� ��� ����������� ������ ��������� ������
    /// </summary>
    [ToolboxItem(false)]
    public class DispatcheredATLBsScreen : ATLBsScreen, IDisplayingEntity
    {

        #region Fields

        private Aircraft currentAircraft;

        #endregion


        #region Constructor

        /// <summary>
        /// ������� ������� ���������� ��� ����������� ������ ��������� ������
        /// </summary>
        public DispatcheredATLBsScreen(Aircraft aircraft) : base(aircraft)
        {
            currentAircraft = aircraft;
            Dock = DockStyle.Fill;
        }

        #endregion


        #region IDisplayingEntity Members
        /// <summary>
        /// Represents data being displayed
        /// </summary>
        public object ContainedData
        {
            get { return currentAircraft; }
            set
            {
                currentAircraft = (Store)value;
            }
        }

        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        public bool ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredATLBsScreen))
                return false;
            if (!(obj.ContainedData is Aircraft))
                return false;

            return (currentAircraft.ID == ((Aircraft)obj.ContainedData).ID);
        }

        /// <summary>
        /// Method call after add to IDisplayerCollectionProxy
        /// </summary>

        /// <returns></returns>
        public void OnInitCompletion(object sender)
        {
            if (InitComplition != null)
                InitComplition(sender, new EventArgs());

        }

        /// <summary>
        /// ���������� ������� �������� ������������� �������
        /// </summary>
        /// <param name="arguments"></param>
        public void OnDisplayerRemoving(DisplayerCancelEventArgs arguments)
        {
        }

        /// <summary>
        /// ��������, ������������ ��� ����������� �������, ���������� ������ ��������
        /// </summary>
        /// <param name="arguments"></param>
        public void OnDisplayerDeselecting(DisplayerCancelEventArgs arguments)
        {

        }

        public void SetEnabled(bool isEnbaled)
        {
          //  SetEnabledToControls(isEnbaled);
        }

        /// <summary>
        /// call after add to IDisplayerCollectionProxy 
        /// </summary>
        public event EventHandler InitComplition;
        #endregion
    }
}
