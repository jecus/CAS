using System;
using System.ComponentModel;
using System.Windows.Forms;
using CAS.Core.Types;
using CAS.UI.Interfaces;
using CAS.UI.UIControls.AircraftsControls;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.AircraftsControls
{
    ///<summary>
    /// ���������� ����� ����������� ������ �� ���������
    ///</summary>
    [ToolboxItem(false)]
    public class DispatcheredAircraftCollectionScreen:AircraftCollectionScreen, IDisplayingEntity
    {
        ///<summary>
        /// ��������� ����� ������ ����������� ������ �� ���������
        ///</summary>
        ///<param name="currentOperator">������������ ��������</param>
        public DispatcheredAircraftCollectionScreen(Operator currentOperator) : base(currentOperator)
        {
            Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Represents data being displayed
        /// </summary>
        public object ContainedData
        {
            get
            {
                return currentOperator;
            }
            set
            {
                if (value is Operator)
                    currentOperator = value as Operator;
            }
        }

        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        public bool ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredAircraftCollectionScreen))
                return false;
            if (obj.ContainedData is Operator)
                return (currentOperator.ID == ((Operator)obj.ContainedData).ID);
            else
                return false;
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

        #region public void OnDisplayerRemoving(DisplayerCancelEventArgs arguments)
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
            base.SetEnabledToControls(isEnbaled);
        }

        /// <summary>
        /// call after add to IDisplayerCollectionProxy 
        /// </summary>
        public event EventHandler InitComplition;
        #endregion

        protected override void SetEnabledToControls(bool isEnabled)
        {
            base.SetEnabledToControls(isEnabled);
        }
    }
}