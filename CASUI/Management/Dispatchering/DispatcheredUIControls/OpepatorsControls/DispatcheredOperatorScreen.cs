using System;
using System.Windows.Forms;
using CAS.Core.ProjectTerms;
using CAS.Core.Types;
using CAS.UI.Interfaces;
using CAS.UI.UIControls.OpepatorsControls;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.OpepatorsControls
{
    /// <summary>
    /// ������� ����������, ������������ ���������� ��������� ������������
    /// </summary>
    public class DispatcheredOperatorScreen : OperatorScreen, IDisplayingEntity
    {

        #region Constructor

        #region public DispatcheredOperatorScreen()

        /// <summary>
        /// ������� ��������� �������� ����������, ������������� ���������� ��������� ������������
        /// </summary>
        public DispatcheredOperatorScreen()
        {
            Dock = DockStyle.Fill;
        }

        #endregion

        #region public DispatcheredOperatorScreen(Operator currentOperator) : base(currentOperator)

        /// <summary>
        /// ������� ��������� �������� ����������, ������������� ���������� ��������� ������������
        /// </summary>
        /// <param name="currentOperator">�������� �����������</param>
        public DispatcheredOperatorScreen(Operator currentOperator) : base(currentOperator)
        {
            Dock = DockStyle.Fill;
        }

        #endregion
        
        #endregion


        #region IDisplayingEntity Members

        /// <summary>
        /// Represents data being displayed
        /// </summary>
        public object ContainedData
        {
            get { return currentOperator; }
            set { currentOperator = (Operator)value; }
        }

        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        public bool ContainedDataEquals(IDisplayingEntity obj)
        {
            if (!(obj is DispatcheredOperatorScreen))
                return false;
            if (!(obj.ContainedData is Operator))
                return false;

            return (currentOperator.ID == ((Operator)obj.ContainedData).ID);
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
            if (operatorControl.GetChangeStatus())
            {
                switch (MessageBox.Show("Do you want to save changes?", (string)new TermsProvider()["SystemName"], MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1))
                {
                    case DialogResult.Yes:
                        arguments.Cancel = !Save();
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
