using System;
using System.ComponentModel;
using System.Windows.Forms;
using CAS.Core.Core.Interfaces;
using CAS.Core.ProjectTerms;
using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Aircrafts.Parts;
using CAS.Core.Types.Dictionaries;
using CAS.Core.Types.Directives;
using CAS.UI.Appearance;
using CAS.UI.Interfaces;
using CAS.UI.Management;
using CAS.UI.Management.Dispatchering;
using CAS.UI.Management.Dispatchering.DispatcheredUIControls.DirectiveControls;
using CAS.UI.UIControls.Auxiliary;
using CAS.UI.UIControls.ReferenceControls;
using CASTerms;

namespace CAS.UI.UIControls.DirectivesControls
{
    /// <summary>
    /// �����, ����������� ����������� ���������� <see cref="EngineeringOrderDirective"/>
    /// </summary>
    [ToolboxItem(false)]
    public class CPCPDirectiveAddingScreen : UserControl
    {

        #region Fields
        /// <summary>
        /// ������������ ������, � ������� ����������� ���������
        /// </summary>
        protected IDirectiveContainer parentBaseDetail;
        private BaseDetailDirective addedDirective;

        private readonly HeaderControl headerControl;
        private readonly AircraftHeaderControl aircraftHeader;
        private readonly FooterControl footerControl;
        private readonly Panel mainPanel;

        private readonly ExtendableRichContainer generalDataAndPerformanceContainer;

        protected readonly CPCPDirectiveGeneralInformationControl generalDataAndPerformanceControl;
        private readonly Icons icons = new Icons();

        #endregion

        #region Constructors

        #region private CPCPDirectiveAddingScreen()

        ///<summary>
        /// ��������� ������, ����������� ����������� ���������� ���������
        ///</summary>
        private CPCPDirectiveAddingScreen()
        {
            Dock = DockStyle.Fill;
            BackColor = Css.CommonAppearance.Colors.BackColor;
            footerControl = new FooterControl();
            headerControl = new HeaderControl();
            aircraftHeader = new AircraftHeaderControl();
            mainPanel = new Panel();

            generalDataAndPerformanceControl = new CPCPDirectiveGeneralInformationControl();
            
            generalDataAndPerformanceContainer = new ExtendableRichContainer();

            aircraftHeader.OperatorClickable = true;
            aircraftHeader.AircraftClickable = true;
            //
            // headerControl
            //
            headerControl.Controls.Add(aircraftHeader);
            headerControl.ButtonReload.Icon = icons.SaveAndAdd;
            headerControl.ButtonReload.IconNotEnabled = icons.SaveAndAddGray;
            headerControl.ButtonReload.IconLayout = ImageLayout.Center;
            headerControl.ButtonReload.TextMain = "Save and";
            headerControl.ButtonReload.TextSecondary = "add another";
            headerControl.ButtonReload.Click += buttonSaveAndAdd_Click;

            headerControl.ButtonEdit.Icon = icons.Save;
            headerControl.ButtonEdit.IconNotEnabled = icons.SaveGray;
            headerControl.ButtonEdit.IconLayout = ImageLayout.Center;
            headerControl.ButtonEdit.ReflectionType = ReflectionTypes.DisplayInCurrent;
            headerControl.ButtonEdit.TextMain = "Save";
            headerControl.ButtonEdit.TextSecondary = "and Edit";
            headerControl.ButtonEdit.DisplayerRequested += buttonSaveAndEdit_DisplayerRequested;
            headerControl.TabIndex = 0;
            //
            // mainPanel
            //
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.AutoScroll = true;
            mainPanel.TabIndex = 1;
            //
            // footerControl
            //
            footerControl.TabIndex = 2;
            //
            // generalDataAndPerformanceContainer
            //
            generalDataAndPerformanceContainer.Dock = DockStyle.Top;
            generalDataAndPerformanceContainer.UpperLeftIcon = icons.GrayArrow;
            generalDataAndPerformanceContainer.Caption = "General data and Performance";
            generalDataAndPerformanceContainer.MainControl = generalDataAndPerformanceControl;
            generalDataAndPerformanceContainer.TabIndex = 0;

            mainPanel.Controls.Add(generalDataAndPerformanceContainer);

            Controls.Add(mainPanel);
            Controls.Add(footerControl);
            Controls.Add(headerControl);
        }

        #endregion

        #region public OutOffPhaseReferencAdding(BaseDetail parentBaseDetail) : this()

        ///<summary>
        /// ��������� ������, ����������� ����������� ���������� ���������
        ///</summary>
        /// <param name="parentBaseDetail">������������ ������, � ������� ����������� ���������</param>
        public CPCPDirectiveAddingScreen(BaseDetail parentBaseDetail) : this()
        {
            if (parentBaseDetail == null) throw new ArgumentNullException("parentBaseDetail");
            this.parentBaseDetail = parentBaseDetail;

            aircraftHeader.Aircraft = parentBaseDetail.ParentAircraft;
            ClearFields();
        }

        #endregion

        #region public OutOffPhaseReferencAdding(Aircraft parentAircraft) : this()

        ///<summary>
        /// ��������� ������, ����������� ����������� ���������� ���������
        ///</summary>
        /// <param name="parentAircraft">������������ ������, � ������� ����������� ���������</param>
        public CPCPDirectiveAddingScreen(Aircraft parentAircraft) : this()
        {
            if (parentAircraft == null) throw new ArgumentNullException("parentAircraft");
            parentBaseDetail = parentAircraft;
            aircraftHeader.Aircraft = parentAircraft;
            ClearFields();
        }

        #endregion

        #endregion

        #region Methods

        #region private void buttonSaveAndEdit_DisplayerRequested(object sender, ReferenceEventArgs e)

        private void buttonSaveAndEdit_DisplayerRequested(object sender, ReferenceEventArgs e)
        {
            if (AddNewDirective(true))
            {
                e.RequestedEntity = new DispatcheredCPCPDirectiveScreen(addedDirective);
                if (addedDirective.Parent.Parent is Aircraft)
                    e.DisplayerText = ((Aircraft)addedDirective.Parent.Parent).RegistrationNumber + ". " + addedDirective.DirectiveType.CommonName + ". " + addedDirective.Title;
            }
            else
                e.Cancel = true;
        }

        #endregion

        #region private void buttonSaveAndAdd_Click(object sender, EventArgs e)

        private void buttonSaveAndAdd_Click(object sender, EventArgs e)
        {
            if (AddNewDirective(false))
            {
                MessageBox.Show("Directive added successfully", new TermsProvider()["SystemName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                generalDataAndPerformanceControl.TextBoxCPCPNumber.Focus();
            }
        }

        #endregion

        #region protected bool AddNewDirective(bool changePageName)

        protected bool AddNewDirective(bool changePageName)
        {
            if (generalDataAndPerformanceControl.CPCPNumber == "")
            {
                MessageBox.Show("You should enter CPCP Number", new GlobalTermsProvider()["SystemName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (generalDataAndPerformanceControl.RepeatInterval == null)
            {
                MessageBox.Show("You should enter Repeat Interval", new GlobalTermsProvider()["SystemName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (!generalDataAndPerformanceControl.SaveData(addedDirective, changePageName))
                return false;
            addedDirective.FirstPerformSinceNew = new Lifelength(generalDataAndPerformanceControl.RepeatInterval);
            parentBaseDetail.Add(addedDirective);
            return true;
        }

        #endregion

        #region private void ClearFields()

        private void ClearFields()
        {
            addedDirective = new BaseDetailDirectiveProxy(parentBaseDetail);
            addedDirective.SetDirectiveType(DirectiveTypeCollection.Instance.OutOffPhaseDirectiveType);
            generalDataAndPerformanceControl.ClearFields();
        }

        #endregion

        #endregion
                

    }
}