using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CAS.Core.Types.ATLBs;
using CAS.UI.UIControls.AircraftTechnicalLogBookControls;
using CAS.UI.UIControls.Auxiliary;

namespace CAS.UI.Interfaces
{


    /*
     * ����� ������� - ������������ ������ ������������ ������� 4 ���������� � �� ���������� ������� �� �������� ����������� � ���� ������
     * ���� �� ����� ������������ 4 ���������� �� ��������� ������ ����, ��� ��������, ��� �� ����� ������ ���� ���������� ������ ���� ����������
     */

    /// <summary>
    /// ������ ������ ���������� ���������� �����
    /// </summary>
    public partial class DiscrepanciesListControl : CAS.UI.Interfaces.EditObjectControl
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

        #region public DiscrepanciesListControl()
        /// <summary>
        /// ������ �����������
        /// </summary>
        public DiscrepanciesListControl()
        {
            InitializeComponent();
            FillControls();
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

            // ��������� ��������� ��������� ��������
            for (int i = 0; i < panelMain.Controls.Count; i++)
            {
                DiscrepancyControl d = panelMain.Controls[i] as DiscrepancyControl;
                if (d != null && !d.IsNull)
                {
                    d.ApplyChanges();

                    /*
                     * �������� ��� ��������
                     * 
                     * 1) AircraftFlight ���������� � �� ������ ��������� � ������������ Discrepancy - ����� ������
                     * 2) AircraftFlight �� ���������� � �� ������� ����� Discrepancy - �������
                     * 3) AircraftFlight �� ���������� (�� ������� ����� �������� ������ � ���� �������) � �������������� Discrepancy ���� �����
                     * 
                     * � ������� ������ ��������� �� ������ ������������ � ����� ������� ����� �������� �� 
                     */

                    if (DiscrepancyExists(d.Discrepancy))
                    {
                        // ������ ������
                        d.Discrepancy.Save();
                    }
                    else if (Flight.ExistAtDataBase)
                    {
                        // ������ ������
                        Flight.AddDiscrepancy(d.Discrepancy);
                    }
                    else
                    {
                        // ������ ������
                        // ������ �� ������, ������������, ��� ���� ����� ����� ������ ������ ����� ���������� AircraftFlight
                    }
                }
            }

            // ������ �� ������ ��������� ��� ������ � ����� �������� ������ ��� ��������� ���������� 


            //
            base.ApplyChanges();
        }
        #endregion

        #region public override void FillControls()
        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        public override void FillControls()
        {
            BuildControls();
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

            // ��������� ��������� ������
            // ��������� ������ �� ���������� ������� ������� ���� ����� ������������� (!d.IsNull)
            for (int i = 0; i < panelMain.Controls.Count; i++)
            {
                DiscrepancyControl d = panelMain.Controls[i] as DiscrepancyControl;
                if (d != null && !d.IsNull) 
                    if (!d.CheckData()) return false;
            }

            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private void BuildControls()
        /// <summary>
        /// ������ ������ ��������
        /// </summary>
        private void BuildControls()
        {
            // ����������� ������ ��������
            panelMain.Controls.Clear();


            int count = 4;
            if (Flight != null && Flight.Discrepancies != null && Flight.Discrepancies.Length > count) count = Flight.Discrepancies.Length;

            for (int i = 0; i < count; i++)
            {
                // ��������� �����������
                if (i > 0)
                {
                    Delimiter delimiter = new Delimiter();
                    delimiter.Style = DelimiterStyle.Solid;
                    delimiter.Orientation = DelimiterOrientation.Horizontal;
                    delimiter.Margin = new Padding(0, 10, 0, 10);
                    delimiter.Width = 1000;
                    this.panelMain.Controls.Add(delimiter);
                }

                // ��������� ������� - �������������
                DiscrepancyControl d = new DiscrepancyControl();
                d.Index = i + 1;
                if (Flight != null && Flight.Discrepancies != null && i < Flight.Discrepancies.Length)
                {
                    d.Discrepancy = Flight.Discrepancies[i];
                }
                else if (Flight != null) // �� ����� ��������� Discrepancy, ���� ������ Flight �� ����� - �������� ����������� ��������
                {
                    Discrepancy discrepancy = new Discrepancy();
                    d.Discrepancy = discrepancy;
                    //discrepancy.LoadChildObjectsFromDatabase();
                    //discrepancy.CorrectiveAction.LoadChildObjectsFromDatabase();
                    //discrepancy.Flight = Flight;
                }

                this.panelMain.Controls.Add(d);
            }
        }
        #endregion

        #region private bool DiscrepancyExists(Discrepancy d)
        /// <summary>
        /// ���������� �� ������ ���������� � ������
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private bool DiscrepancyExists(Discrepancy d)
        {
            //
            if (Flight == null || Flight.Discrepancies == null) return false;

            //
            for (int i = 0; i < Flight.Discrepancies.Length; i++)
                if (Flight.Discrepancies[i] == d)
                    return true;

            //
            return false;
        }
        #endregion

        #region protected override void OnSizeChanged(EventArgs e)
        /// <summary>
        /// ��� ��������� ������� �������� ��������� Flow Layout Panel �.�. ���� ��� ����� ������ �� ����� )
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            panelMain.Dock = DockStyle.Fill;
            base.OnSizeChanged(e);
        }
        #endregion


    }
}


