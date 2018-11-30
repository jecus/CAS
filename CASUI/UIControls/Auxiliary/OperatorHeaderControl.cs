using System;
using System.Drawing;
using CAS.Core.Types;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering.DispatcheredUIControls.AircraftsControls;

namespace CAS.UI.UIControls.Auxiliary
{
    ///<summary>
    /// ������� ���������� - ��������� ������ �������������
    ///</summary>
    public class OperatorHeaderControl : AbstractOperatorHeaderControl
    {

        #region Fields

        private Operator currentOperator;

        #endregion
        
        #region Constructors

        #region public OperatorHeaderControl()

        ///<summary>
        /// ��������� ����� ���������� ������� ���������� - ��������� ������������
        ///</summary>
        public OperatorHeaderControl()
        {
            
        }

        #endregion

        #region public OperatorHeaderControl(Operator currentOperator) : this(currentOperator,false)

        ///<summary>
        /// ��������� ����� ������ ����������� ���������
        ///</summary>
        public OperatorHeaderControl(Operator currentOperator) : this(currentOperator,false)
        {
        }

        #endregion

        #region public OperatorHeaderControl(Operator currentOperator, bool operatorClickable)

        ///<summary>
        /// ��������� ����� ������ ����������� ���������
        ///</summary>
        public OperatorHeaderControl(Operator currentOperator, bool operatorClickable)
        {
            if (currentOperator == null)
                throw new ArgumentNullException("currentOperator", "Cannot display Null operator");

            Operator = currentOperator;
            OperatorClickable = operatorClickable;
        }

        #endregion

        #region public OperatorHeaderControl(string caption, Image logotype)

        ///<summary>
        /// ��������� ����� ������ ��� ���������� �������� ��������� ��������� ��
        ///</summary>
        public OperatorHeaderControl(string caption, Image logotype)
        {
            UpdateOperatorInfo(caption, logotype);
            OperatorClickable = false;
        }

        #endregion

        #endregion

        #region Properties

        #region public Operator Operator

        /// <summary>
        /// ���������� ��� ������������� ������� �����������
        /// </summary>
        public Operator Operator
        {
            get
            {
                return currentOperator;
            }
            set
            {
                currentOperator = value;
                if (currentOperator != null)
                    UpdateOperatorInfo(currentOperator.Name, currentOperator.LogoType);
            }
        }

        #endregion

        #endregion

        #region Methods

        #region protected override void logotypeButton_DisplayerRequested(object sender, ReferenceEventArgs e)

        protected override void logotypeButton_DisplayerRequested(object sender, ReferenceEventArgs e)
        {
            if (OperatorClickable && Operator != null)
            {
                e.DisplayerText = Operator.Name;
                e.RequestedEntity = new DispatcheredAircraftCollectionScreen(Operator);
            }
            else
                e.Cancel = true;
        }

        #endregion

        #endregion

    }
}
