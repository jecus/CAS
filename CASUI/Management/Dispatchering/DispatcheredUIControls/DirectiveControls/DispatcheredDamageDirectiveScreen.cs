using System;
using System.Windows.Forms;
using CAS.Core.ProjectTerms;
using CAS.Core.Types.Directives;
using CAS.UI.Interfaces;
using CAS.UI.UIControls.DirectivesControls;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.DirectiveControls
{
    public class DispatcheredDamageDirectiveScreen : DamageDirectiveScreen, IDisplayingEntity
    {
        #region Constructor

        /// <summary>
        /// ������� ������� ���������� ��� ����������� ���������
        /// </summary>
        /// <param name="directive">���� ��������</param>
        public DispatcheredDamageDirectiveScreen(BaseDetailDirective directive) : base(directive)
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
            get { return currentDirective; }
            set
            {
                if (value is BaseDetailDirective)
                    currentDirective = (BaseDetailDirective)value;
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
            if (!(obj is DispatcheredDamageDirectiveScreen))
                return false;
            if (!(obj.ContainedData is BaseDetailDirective))
                return false;
            return currentDirective.ID == ((BaseDetailDirective)obj.ContainedData).ID;
        }

        public void OnInitCompletion(object sender)
        {
        }

        #endregion

        #region public void OnDisplayerRemoving(DisplayerCancelEventArgs arguments)

        /// <summary>
        /// ���������� ������� �������� ������������� �������
        /// </summary>
        /// <param name="arguments"></param>
        public void OnDisplayerRemoving(DisplayerCancelEventArgs arguments)
        {
            if (generalDataAndPerformanceControl.GetChangeStatus(true))
            {
                switch (MessageBox.Show("Do you want to save changes?", (string)new TermsProvider()["SystemName"],
                                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button1))
                {
                    case DialogResult.Yes:
                        //arguments.Cancel = !Save(); 
                        SaveData();
                        break;
                    case DialogResult.Cancel:
                        arguments.Cancel = true;
                        break;
                }
            }
        }

        #endregion

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

        public event EventHandler InitComplition;

        #endregion
    }
}
