using CAS.Core.Types.Aircrafts.Parts;
using CAS.UI.UIControls.AircraftsControls;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.AircraftsControls
{
    /// <summary>
    /// ����� ����������� ����������� <see cref="BaseDetailControl"/>, ����������� Dispatcher ��
    /// </summary>
    public class DispatcheredAircraftBaseDetailInfoControl : BaseDetailControl
    {

        #region Constructors

        #region public DispatcheredAircraftBaseDetailInfoControl(BaseDetail item)
        /// <summary>
        /// ��������� ������� - ����������� <see cref="BaseDetailControl"/>, ����������� Dispatcher ��
        /// </summary>
        /// <param name="item">������������ �������</param>
        public DispatcheredAircraftBaseDetailInfoControl(BaseDetail item) : base(item)
        {
            InitializeControl();
        }
        #endregion

        #region public DispatcheredAircraftBaseDetailInfoControl(APU apu)
        /// <summary>
        /// ��������� ������� - ����������� <see cref="BaseDetailControl"/>, ����������� Dispatcher ��
        /// </summary>
        /// <param name="apu">������������ �������</param>
        public DispatcheredAircraftBaseDetailInfoControl(APU apu) : base(apu)
        {
            InitializeControl();
        }
        #endregion

        #region public DispatcheredAircraftBaseDetailInfoControl(Engine engine)
        /// <summary>
        /// ��������� ������� - ����������� <see cref="BaseDetailControl"/>, ����������� Dispatcher ��
        /// </summary>
        /// <param name="engine">������������ �������</param>
        public DispatcheredAircraftBaseDetailInfoControl(Engine engine) : base(engine)
        {
            InitializeControl();
        }
        #endregion

        #region public DispatcheredAircraftBaseDetailInfoControl(Engine engine, int engineNumber)
        /// <summary>
        /// ��������� ������� - ����������� <see cref="BaseDetailControl"/>, ����������� Dispatcher ��
        /// </summary>
        /// <param name="engine">������������ �������</param>
        /// <param name="engineNumber"></param>
        public DispatcheredAircraftBaseDetailInfoControl(Engine engine, int engineNumber) : base(engine)
        {
            InitializeControl();
        }
        #endregion

        #region public DispatcheredAircraftBaseDetailInfoControl(AircraftFrame aircraftFrame)
        /// <summary>
        /// ��������� ������� - ����������� <see cref="BaseDetailControl"/>, ����������� Dispatcher ��
        /// </summary>
        /// <param name="aircraftFrame">������������ �������</param>
        public DispatcheredAircraftBaseDetailInfoControl(AircraftFrame aircraftFrame) : base(aircraftFrame)
        {
            InitializeControl();
        }
        #endregion

        #endregion

        private void InitializeControl()
        {
            //baseDetailButton.Click += aircraftButton_Click;
            //aircraftButton.DisplayerRequested += aircraftButton_DisplayerRequested;
        }

/*        private void aircraftButton_Click(object sender, EventArgs e)
        {
            string newTabText = aircraftButton.Text + ": " + currentBaseDetail.SerialNumber;
            ReferenceEventArgs eventArgs = new ReferenceEventArgs(
                new DispathceredBaseDetailContainedDetailView(currentBaseDetail), ReflectionTypes.DisplayInNew, newTabText);
            OnDisplayerReauested(eventArgs);
        }*/
    }
}
