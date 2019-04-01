using System;
using AvControls.AvalonButtonM;
using SmartCore.Entities.General;

namespace CAS.UI.UIControls.OpepatorsControls
{
    /// <summary>
    /// ������� ���������� ����������� ���������� � ������������
    /// </summary>
    public class OperatorListItem : AvalonButtonM
    {
        #region Fields
        private Operator currentOperator;
        #endregion

        #region Constructors
        /// <summary>
        /// ��������� ������� ���������� ����������� ���������� � ������������
        /// </summary>
        public OperatorListItem()
        {
        }

        /// <summary>
        /// ��������� ������� ���������� ����������� ���������� � ������������
        /// </summary>
        /// <param name="currentOperator">������������ �����������</param>
        public OperatorListItem(Operator currentOperator)
        {
            if (null == currentOperator) throw new ArgumentNullException("currentOperator");

            this.currentOperator = currentOperator;
            UpdateFromOperator(currentOperator);
        }
        #endregion

        #region Properties

        #region public Operator CurrentOperator
        /// <summary>
        /// Gets or sets operator associated with control
        /// </summary>
        public Operator CurrentOperator
        {
            get { return currentOperator; }
            set { currentOperator = value; }
        }
        #endregion

        #endregion

        #region Methods

        #region private void UpdateFromOperator(Operator @operator)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operator"></param>
        private void UpdateFromOperator(Operator @operator)
        {
            base.Text = @operator.Name;
            base.SecondText = @operator.Address;
            
        }
        #endregion

        #endregion

    }
}
