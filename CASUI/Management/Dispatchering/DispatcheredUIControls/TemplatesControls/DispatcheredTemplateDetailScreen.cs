using System;
using System.ComponentModel;
using System.Windows.Forms;
using CAS.Core.ProjectTerms;
using CAS.Core.Types.Aircrafts.Parts.Templates;
using CAS.UI.Interfaces;
using CAS.UI.UIControls.TemplatesControls;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.DetailsControl
{
    /// <summary>
    /// ����� ��� ����������� ������
    /// </summary>
    [ToolboxItem(false)]
    public partial class DispatcheredTemplateDetailScreen : TemplateDetailScreen, IDisplayingEntity
    {
        /// <summary>
        /// ������������ ������
        /// </summary>
        private TemplateAbstractDetail detail;

        ///<summary>
        /// ��������� ������� ����������� ���������� ��������
        ///</summary>
        /// <param name="detail">������ ��� �����������</param>
        public DispatcheredTemplateDetailScreen(TemplateAbstractDetail detail) : base(detail)
        {
            if (detail == null) throw new ArgumentNullException("detail");
            this.detail = detail;
        }

        /// <summary>
        /// Represents data being displayed
        /// </summary>
        public object ContainedData
        {
            get { return detail; }
            set
            {
                if (value is TemplateAbstractDetail)
                    detail = (TemplateAbstractDetail)value;
            }
        }

        #region public bool ContainedDataEquals(IDisplayingEntity obj)
        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        bool IDisplayingEntity.ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredTemplateDetailScreen))
                return false;
            if (!(obj.ContainedData is TemplateAbstractDetail))
                return false;
            return detail.ID == ((TemplateAbstractDetail)obj.ContainedData).ID;
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
        #endregion

        #region public void OnDisplayerRemoving(DisplayerCancelEventArgs arguments)
        /// <summary>
        /// ���������� ������� �������� ������������� �������
        /// </summary>
        /// <param name="arguments"></param>
        public void OnDisplayerRemoving(DisplayerCancelEventArgs arguments)
        {
            if (generalInformationControl.GetChangeStatus() || limitationControl.GetChangeStatus() || parametersControl.GetChangeStatus())
            {
                switch (MessageBox.Show("Do you want to save changes?", (string)new TermsProvider()["SystemName"],
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button1))
                {
                    case DialogResult.Yes:
                        ButtonSave_DisplayerRequested(this, new ReferenceEventArgs());
                        arguments.Cancel = false;
                        break;
                    case DialogResult.Cancel:
                        arguments.Cancel = true;
                        break;
                }
            }
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
            
        }

        /// <summary>
        /// call after add to IDisplayerCollectionProxy 
        /// </summary>
        public event EventHandler InitComplition;
        #endregion

    }
}
