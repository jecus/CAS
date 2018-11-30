using System.Collections.Generic;
using System.Linq;
using CASTerms;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Atlbs;
using SmartCore.Entities.General.Personnel;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{

    /// <summary>
    /// ������� ���������� ��������� � ������ ������
    /// </summary>
    public partial class FlightTakeOffWeightControl : Interfaces.EditObjectControl
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

        #region public List<Specialist> Crew
        ///<summary>
        /// ���������� ��� ������ ������ ������
        ///</summary>
        public List<Specialist> Crew
        {
            set
            {
                if(value != null)
                {
                    double crew = value
                        .Where(specialist => specialist.AGWCategory != null)
                        .Aggregate<Specialist, double>(0, (current, specialist) => current + specialist.AGWCategory.WeightSummer);
                    numericUpDownCrew.Value = (decimal)crew;   
                }
                else
                {
                    numericUpDownCrew.Value = 0;
                }
            }
        }
        #endregion

        #region public double OnBoardFuel
        ///<summary>
        /// ������ ���-�� ������� �� �����
        ///</summary>
        public double OnBoardFuel
        {
            set { numericUpDownFuel.Value = (decimal) value; }
        }
        #endregion

        #region public double RemainAfterFuel
        ///<summary>
        /// ������ ���-�� ������� ����� ������
        ///</summary>
        public double RemainAfterFuel
        {
            set { numericUpDownFuelRemainAfter.Value = (decimal)value; }
        }
        #endregion

        #region public double PassengersWeight
        ///<summary>
        /// ��� ���������� �� �����
        ///</summary>
        public double PassengersWeight
        {
            set { numericUpDownPassengers.Value = (decimal)value; }
        }
        #endregion

        #region public double CargoWeight
        ///<summary>
        /// ��� ����� �� �����
        ///</summary>
        public double CargoWeight
        {
            set { numericUpDownCargo.Value = (decimal)value; }
        }
        #endregion

        /*
         * �����������
         */

        #region public FlightTakeOffWeightControl()
        /// <summary>
        /// ������� ���������� ��������� � ������ ������
        /// </summary>
        public FlightTakeOffWeightControl()
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
            if (Flight != null)
            {
                Flight.TakeOffWeight = (double)numericUpDownTakeOffWeight.Value;
            }
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

            BeginUpdate();

            if (Flight != null)
            {
				var aircraft = GlobalObjects.AircraftsCore.GetAircraftById(Flight.AircraftId);

				//����������� ������ ���
				numericUpDownOpEmptyWeight.Value = (decimal)aircraft.OperationalEmptyWeight;
                //������
                double crew = Flight.FlightCrewRecords
                    .Where(flightCrewRecord => flightCrewRecord.Specialist != null && flightCrewRecord.Specialist.AGWCategory != null)
                    .Aggregate<FlightCrewRecord, double>(0, (current, flightCrewRecord) => current + flightCrewRecord.Specialist.AGWCategory.WeightSummer);

                numericUpDownCrew.Value = (decimal)crew;
                //���������
                double passengers = Flight.FlightPassengerRecords
                    .Where(fpr => fpr.PassengerCategory != null)
                    .Aggregate<FlightPassengerRecord, double>(0, (current, fpr) => current + (fpr.CountEconomy + fpr.CountBusiness + fpr.CountFirst)*fpr.PassengerCategory.WeightSummer);
                numericUpDownPassengers.Value = (decimal)passengers;
                //����
                double cargo = Flight.FlightCargoRecords.Sum(fcr => Measure.Convert(fcr.Weigth, fcr.Measure, Measure.Kilograms));
                numericUpDownCargo.Value = (decimal) cargo;
                //������������ ��������
                numericUpDownPayload.Value = (decimal)(passengers + cargo);
                //������������ ������������ ��������
                numericUpDownMaxPayload.Value = (decimal)(aircraft.MaxPayloadWeight);
                //��� ��� �������
                numericUpDownZeroFuel.Value = (decimal)(aircraft.OperationalEmptyWeight + crew + passengers + cargo);
                //������������ ��� ��� �������
                numericUpDownMaxZeroFuel.Value = (decimal)aircraft.MaxZeroFuelWeight;
                #region �������
                double fuelOnBoard = 0;
                double fuelRemainAfter = 0;
                for (int i = 0; i < Flight.FuelTankCollection.Count; i++ )
                {
                    fuelOnBoard += Flight.FuelTankCollection[i].OnBoard;
                    fuelRemainAfter += Flight.FuelTankCollection[i].RemainingAfter;
                }
                numericUpDownFuel.Value = (decimal)fuelOnBoard;
                #endregion
                //������������ �������� ���
                numericUpDownMaxTaxiWeight.Value = (decimal)(aircraft.MaxTaxiWeight);
                //�������� ���
                numericUpDownTakeOffWeight.Value =
                    (decimal)(aircraft.OperationalEmptyWeight + crew + passengers + cargo + fuelOnBoard);
                //������������ �������� ���
                numericUpDownMaxTakeOffWeight.Value = (decimal)aircraft.MaxTakeOffCrossWeight;
                //���������� ���
                numericUpDownLandingWeight.Value =
                    (decimal)(aircraft.OperationalEmptyWeight + crew + passengers + cargo + fuelRemainAfter);
                //������������ ���������� ���
                numericUpDownMaxLandingWeight.Value = (decimal)aircraft.MaxLandingWeight;
            
            }
            else
            {
                numericUpDownOpEmptyWeight.Value = 0;
                numericUpDownCrew.Value = 0;
                numericUpDownCargo.Value = 0;
                numericUpDownMaxPayload.Value = 0;
                numericUpDownZeroFuel.Value = numericUpDownMaxZeroFuel.Value = 0;
                numericUpDownFuel.Value = 0;
                numericUpDownMaxTaxiWeight.Value = 0;
                numericUpDownTakeOffWeight.Value = numericUpDownMaxTakeOffWeight.Value = 0;
                numericUpDownLandingWeight.Value = numericUpDownMaxLandingWeight.Value = 0;
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
            //
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private void NumericUpDownValueChanged(object sender, System.EventArgs e)
        private void NumericUpDownValueChanged(object sender, System.EventArgs e)
        {
			if (Flight != null)
            {
				var aircraft = GlobalObjects.AircraftsCore.GetAircraftById(Flight.AircraftId);
				//������������ ��������
				numericUpDownPayload.Value =
                    numericUpDownPassengers.Value +
                    numericUpDownCargo.Value;
                //��� ��� �������
                numericUpDownZeroFuel.Value =
                    (decimal)aircraft.OperationalEmptyWeight + 
                    numericUpDownCrew.Value + 
                    numericUpDownPassengers.Value +
                    numericUpDownCargo.Value;
                //�������� ���
                numericUpDownTakeOffWeight.Value =
                    (decimal)aircraft.OperationalEmptyWeight +
                    numericUpDownCrew.Value +
                    numericUpDownPassengers.Value +
                    numericUpDownCargo.Value +
                    numericUpDownFuel.Value;
                //���������� ���
                numericUpDownLandingWeight.Value =
                    (decimal)aircraft.OperationalEmptyWeight +
                    numericUpDownCrew.Value +
                    numericUpDownPassengers.Value +
                    numericUpDownCargo.Value +
                    numericUpDownFuelRemainAfter.Value;
            }
            else
            {
                //��� ��� �������
                numericUpDownZeroFuel.Value =
                    numericUpDownCrew.Value +
                    numericUpDownPassengers.Value +
                    numericUpDownCargo.Value;
                //�������� ���
                numericUpDownTakeOffWeight.Value =
                    numericUpDownCrew.Value +
                    numericUpDownPassengers.Value +
                    numericUpDownCargo.Value +
                    numericUpDownFuel.Value;
            }    
        }
        #endregion

    }
}

