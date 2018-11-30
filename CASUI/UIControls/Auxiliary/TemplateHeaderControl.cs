using System.Drawing;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering.DispatcheredUIControls.TemplatesControls;

namespace CAS.UI.UIControls.Auxiliary
{
    ///<summary>
    /// ���������� ������� ���������� - ��������� �������
    ///</summary>
    public class TemplateHeaderControl : AbstractOperatorHeaderControl
    {

        #region Constructors

        #region public TemplateHeaderControl(string caption, Image logotype) : this(caption, logotype, false)

        ///<summary>
        /// ��������� ����� ������ ��� ���������� �������� ��������� ��������� ��
        ///</summary>
        public TemplateHeaderControl(string caption, Image logotype) : this(caption, logotype, false)
        {
        }

        #endregion

        #region public TemplateHeaderControl(string caption, Image logotype, bool templatesClicable)

        ///<summary>
        /// ��������� ����� ������ ��� ���������� �������� ��������� ��������� ��
        ///</summary>
        public TemplateHeaderControl(string caption, Image logotype, bool templatesClicable)
        {
            OperatorClickable = templatesClicable;
            UpdateOperatorInfo(caption, logotype);
        }

        #endregion

        #endregion

        #region Methods

        #region protected override void logotypeButton_DisplayerRequested(object sender, ReferenceEventArgs e)

        protected override void logotypeButton_DisplayerRequested(object sender, ReferenceEventArgs e)
        {
            if (OperatorClickable)
            {
                e.DisplayerText = "Templates";
                e.RequestedEntity = new DispatcheredTemplateAircraftCollectionScreen();
            }
            else
                e.Cancel = true;
        }

        #endregion

        #endregion

    }
}
