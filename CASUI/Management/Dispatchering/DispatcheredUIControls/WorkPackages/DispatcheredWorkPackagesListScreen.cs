using System;
using System.ComponentModel;
using System.Windows.Forms;
using CAS.Core.Types.Aircrafts;
using CAS.UI.Interfaces;
using CAS.UI.UIControls.WorkPackages;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.WorkPackages
{
    ///<summary>
    /// ������� ���������� ��� ����������� ������ ������� �������
    ///</summary>
    [ToolboxItem(false)]
    public class DispatcheredWorkPackagesListScreen : WorkPackagesListScreen, IDisplayingEntity
    {

        #region Constructor

        ///<summary>
        /// ������ ������� ���������� ��� ����������� ������ ������� �������
        ///</summary>
        ///<param name="currentAircraft">��, �������� ����������� ������� ������</param>
        public DispatcheredWorkPackagesListScreen(Aircraft currentAircraft) : base(currentAircraft)
        {
            Dock = DockStyle.Fill;
        }

        #endregion


        #region IDisplayingEntity Members

        /// <summary>
        /// Represents data being displayed
        /// </summary>
        public object ContainedData
        {
            get
            {
                return currentAircraft;
            }
            set
            {
                if (value is Aircraft)
                    currentAircraft = value as Aircraft;
            }
        }

        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        public bool ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredWorkPackagesListScreen)) return false;
            if (!(obj.ContainedData is Aircraft)) return false;

            return (currentAircraft.ID == ((Aircraft)obj.ContainedData).ID);
        }

/*        public void Show()
        {
            throw new NotImplementedException();
        }*/

        /// <summary>
        /// Method call after add to IDisplayerCollectionProxy
        /// </summary>
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

        /// <summary>
        /// ����� �������� ��������� ��������� [:|||:]
        /// </summary>
        /// <param name="isEnbaled">���������</param>
        public void SetEnabled(bool isEnbaled)
        {
            SetPageEnable(isEnbaled);
        }

        /// <summary>
        /// call after add to IDisplayerCollectionProxy 
        /// </summary>
        public event EventHandler InitComplition;

        #endregion
    }
}
