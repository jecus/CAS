using System;
using System.Windows.Forms;
using CAS.Core.Types.WorkPackages;
using CAS.UI.Interfaces;
using CAS.UI.UIControls.WorkPackages;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.WorkPackages
{
    ///<summary>
    /// ������� ���������� ��� ����������� ������ ����� �������� ������
    ///</summary>
    public class  DispatcheredWorkPackageScreen : WorkPackageScreen, IDisplayingEntity
    {

        #region Constructor

        ///<summary>
        /// ������ ������� ���������� ��� ����������� ������ ������� �������
        ///</summary>
        ///<param name="workPackage">������� �����</param>
        public DispatcheredWorkPackageScreen(WorkPackage workPackage) : base(workPackage)
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
                return currentWorkPackage;
            }
            set
            {
                if (value is WorkPackage)
                    currentWorkPackage = value as WorkPackage;
            }
        }

        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        public bool ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredWorkPackageScreen)) return false;
            if (!(obj.ContainedData is WorkPackage)) return false;

            return (currentWorkPackage.ID == ((WorkPackage)obj.ContainedData).ID);
        }

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
