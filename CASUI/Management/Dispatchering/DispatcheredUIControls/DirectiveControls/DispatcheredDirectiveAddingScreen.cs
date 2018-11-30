using System;
using System.ComponentModel;
using System.Windows.Forms;
using CAS.Core;
using CAS.Core.Core.Interfaces;
using CAS.Core.ProjectTerms;
using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Aircrafts.Parts;
using CAS.Core.Types.Dictionaries;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering.DispatcheredUIControls.DetailsControl;
using CAS.UI.UIControls.DirectivesControls;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.DirectiveControls
{
    /// <summary>
    /// �����, ����������� ����������� ���������� ���������
    /// </summary>
    [ToolboxItem(false)]
    public class DispatcheredDirectiveAddingScreen : DirectiveAddingScreen, IDisplayingEntity
    {

        #region Constructor

        ///<summary>
        /// ��������� ������, ����������� ����������� ���������� ���������
        ///</summary>
        /// <param name="directiveContainer">������������ ������, � ������� ����������� ���������</param>
        /// <param name="directiveType">��� ���������</param>
        public DispatcheredDirectiveAddingScreen(IDirectiveContainer directiveContainer, DirectiveType directiveType) : base(directiveContainer, directiveType)
        {
        }

        #endregion

        #region Properties

        #region public object ContainedData
        /// <summary>
        /// Represents data being displayed
        /// </summary>
        public object ContainedData
        {
            get { return currentDirectiveContainer; }
            set
            {
                if (value is BaseDetail)
                    currentDirectiveContainer = (BaseDetail)value;
            }
        }
        #endregion

        #endregion

        #region Methods

        #region public bool ContainedDataEquals(IDisplayingEntity obj)
        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        public bool ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredDetailAddingScreen))
                return false;
            if (!(obj.ContainedData is BaseDetail))
                return false;
            if (currentDirectiveContainer == null) return false;
                
            return ((CoreType)currentDirectiveContainer).ID == ((BaseDetail)obj.ContainedData).ID;
        }

        #endregion

        #region public void OnInitCompletion(object sender)

        /// <summary>
        /// Method call after add to IDisplayerCollectionProxy
        /// </summary>
        /// <returns></returns>
        public void OnInitCompletion(object sender)
        {
            if (InitComplition != null)
                InitComplition(sender, new EventArgs());

        }

        #endregion

        #region public void OnDisplayerRemoving(DisplayerCancelEventArgs arguments)
        /// <summary>
        /// ���������� ������� �������� ������������� �������
        /// </summary>
        /// <param name="arguments"></param>
        public void OnDisplayerRemoving(DisplayerCancelEventArgs arguments)
        {
            if ((directiveApplicabilityControl != null && directiveApplicabilityControl.GetChangeStatus()) || generalDataAndPerformanceControl.GetChangeStatus(false) || attributesAndParametersControl.GetChangeStatus(false))
            {
                switch (MessageBox.Show("Do you want to save changes?", (string)new TermsProvider()["SystemName"],
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button1))
                {
                    case DialogResult.Yes:
                        if (!AddNewDirective(false))
                            arguments.Cancel = true;
                        break;
                    case DialogResult.Cancel:
                        arguments.Cancel = true;
                        break;
                }
            }
        }

        #endregion

        #region public void OnDisplayerDeselecting(DisplayerCancelEventArgs arguments)

        /// <summary>
        /// ��������, ������������ ��� ����������� �������, ���������� ������ ��������
        /// </summary>
        /// <param name="arguments"></param>
        public void OnDisplayerDeselecting(DisplayerCancelEventArgs arguments)
        {

        }

        #endregion
        
        #region public void SetEnabled(bool isEnbaled)

        /// <summary>
        /// ����� �������� ��������� ��������� [:|||:]
        /// </summary>
        /// <param name="isEnbaled">���������</param>
        public void SetEnabled(bool isEnbaled)
        {

        }

        #endregion
        
        #endregion

        #region Events

        /// <summary>
        /// call after add to IDisplayerCollectionProxy 
        /// </summary>
        public event EventHandler InitComplition;

        #endregion

    }
}