using System.Windows.Forms;


using Auxiliary;
using CAS.UI.UIControls.Auxiliary;
using SmartCore.Entities.General.Atlbs;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ��������� ������ �������� � �����
    /// </summary>
    public partial class TirePressureControl : Interfaces.EditObjectControl
    {

        #region public AircraftFlight Flight
        /// <summary>
        /// �����, � ������� ������ �������
        /// </summary>
        public AircraftFlight Flight
        {
            get { return AttachedObject as AircraftFlight; }
        }
        #endregion

        /*
         * �����������
         */

        #region public TirePressureControl()
        /// <summary>
        /// ���������� ������ �������� � �����
        /// </summary>
        public TirePressureControl()
        {
            InitializeComponent();
        }
        #endregion

        /*
         * ������������� ������
         */

        #region public override void ApplyChanges()
        /// <summary>
        /// ��������� � ������� ��������� ��������� �� ��������. 
        /// ���� �� ��� ������ ������������� ������� ����� (�������� ��� ����� �����), �������� ������� �� ����������, ������������ false
        /// ����� base.ApplyChanges() ����������
        /// </summary>
        /// <returns></returns>
        public override void ApplyChanges()
        {
            // ������ ������ �����
            if (Flight != null && Flight.LandingGearConditions != null && Flight.LandingGearConditions.Count >= 1)
                ApplyBundle(Flight.LandingGearConditions[0], textN11, textN12);

            // ������ ������ �����
            if (Flight != null && Flight.LandingGearConditions != null && Flight.LandingGearConditions.Count >= 2)
                ApplyBundle(Flight.LandingGearConditions[1], textN21, textN22);
            // ������ ������ �����
            if (Flight != null && Flight.LandingGearConditions != null && Flight.LandingGearConditions.Count >= 3)
                ApplyBundle(Flight.LandingGearConditions[2], textN31, textN32);

            
            base.ApplyChanges();
        }
        #endregion

        #region public override void FillControls()
        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        public override void FillControls()
        {

            BeginUpdate();

            // ������ ������ �����
            if (Flight != null && Flight.LandingGearConditions != null && Flight.LandingGearConditions.Count >= 1)
            {
                FillBundle(Flight.LandingGearConditions[0], labelTitle1, textN11, textN12);
            }
            else
            {
                FillBundle(null, labelTitle1, textN11, textN12);
            }

            // ������ ������ ����� 
            if (Flight != null && Flight.LandingGearConditions != null && Flight.LandingGearConditions.Count >= 2)
            {
                FillBundle(Flight.LandingGearConditions[1], labelTitle2, textN21, textN22);
            }
            else
            {
                FillBundle(null, labelTitle2, textN21, textN22);
            }

            // ������ ������ �����
            if (Flight != null && Flight.LandingGearConditions != null && Flight.LandingGearConditions.Count >= 3)
            {
                FillBundle(Flight.LandingGearConditions[2], labelTitle3, textN31, textN32);
            }
            else
            {
                FillBundle(null, labelTitle3, textN31, textN32);
            }

            EndUpdate();
        }
        #endregion

        #region public override bool CheckData()
        /// <summary>
        /// ��������� ��������� ������.
        /// ���� �����-���� ���� �� �������� �� �������, ������� ����� ������ MessageBox, ������� ������ � ����������� ���� � ���������� false � �������� ���������� ������
        /// </summary>
        /// <returns></returns>
        public override bool CheckData()
        {

            // ��������� ��������� ������ ��� ��� ���� �����
            if (Flight != null && Flight.LandingGearConditions != null && Flight.LandingGearConditions.Count >= 1)
                if (!ValidateBundle(null, labelTitle1, textN11, textN12)) return false;

            if (Flight != null && Flight.LandingGearConditions != null && Flight.LandingGearConditions.Count >= 2)
                if (!ValidateBundle(null, labelTitle2, textN21, textN22)) return false;

            if (Flight != null && Flight.LandingGearConditions != null && Flight.LandingGearConditions.Count >= 3)
                if (!ValidateBundle(null, labelTitle3, textN31, textN32)) return false;

            
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private void FillBundle(LandingGearCondition condition, Label labelTitle, TextBox textN1, TextBox textN2)

        /// <summary>
        /// ��������� �������� ��� �������� �����
        /// </summary>
        /// <param name="labelTitle"></param>
        /// <param name="textN1"></param>
        /// <param name="textN2"></param>
        /// <param name="condition"></param>
        private void FillBundle(LandingGearCondition condition, Label labelTitle, TextBox textN1, TextBox textN2)
        {
            if (condition != null && condition.LandingGearId > 0)
            {
                labelTitle.Text = condition.LandingGear != null ? condition.LandingGear.LandingGear.ToString() : "";
                textN1.Text = condition.TirePressure1.ToString();
                textN2.Text = condition.TirePressure2.ToString();
            }
            else
            {
                labelTitle.Text = textN1.Text = textN2.Text = "";
            }
        }
        #endregion

        #region private bool ValidateBundle()
        /// <summary>
        /// ��������� ������������ ��������� ������
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="labelTitle"></param>
        /// <param name="textN1"></param>
        /// <param name="textN2"></param>
        /// <returns></returns>
        private bool ValidateBundle(LandingGearCondition condition, Label labelTitle, TextBox textN1, TextBox textN2)
        {
            double d;
            if (!UsefulMethods.StringToDouble(textN1.Text, out d))
            {

                SimpleBalloon.Show(textN1, ToolTipIcon.Warning, "Incorrect numeric format", "Enter valid number"); 

                return false;
            }
            if (!UsefulMethods.StringToDouble(textN2.Text, out d))
            {

                SimpleBalloon.Show(textN2, ToolTipIcon.Warning, "Incorrect numeric format", "Enter valid number"); 

                return false;
            }

            //
            return true;
        }
        #endregion

        #region private void ApplyBundle(LandingGearCondition condition, TextBox textN1, TextBox textN2)
        /// <summary>
        /// ��������� ��������� ������ � ������� �����
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="textN1"></param>
        /// <param name="textN2"></param>
        private void ApplyBundle(LandingGearCondition condition, TextBox textN1, TextBox textN2)
        {
            if (condition.LandingGear != null)
            {
                condition.TirePressure1 = UsefulMethods.StringToDouble(textN1.Text);
                condition.TirePressure2 = UsefulMethods.StringToDouble(textN2.Text);
            }
        }
        #endregion

    }
}

