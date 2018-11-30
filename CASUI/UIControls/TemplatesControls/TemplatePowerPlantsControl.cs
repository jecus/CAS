using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Aircrafts.Parts.Templates;

namespace CAS.UI.UIControls.AircraftsControls.AircraftGeneralDataControls
{
    /// <summary>
    /// ������� ���������� ��� ����������� ���������� � ���������� ���������� ��
    /// </summary>
    public class TemplatePowerPlantsControl : PictureBox
    {

        #region Fields

        private const int MARGIN = 30;
        private TemplateAircraft currentAircraft;
        private readonly List<TemplateEngineControl> powerPlants = new List<TemplateEngineControl>();
        

        #endregion

        #region Constructor

        /// <summary>
        /// ������� ������� ���������� ��� ����������� ���������� � ���������� ���������� ��
        /// </summary>
        /// <param name="currentAircraft"></param>
        public TemplatePowerPlantsControl(TemplateAircraft currentAircraft)
        {
            if (currentAircraft.Engines.Length > 0) 
                Size = new Size((TemplateEngineControl.StandartSize.Width)*4 + MARGIN*3, TemplateEngineControl.StandartSize.Height);
            else
                Size = new Size(0,0);
            this.currentAircraft = currentAircraft;
           
            UpdateControl();
        }

        #endregion
        
        #region Properties

        #region public TemplateAircraft Aircraft

        /// <summary>
        /// ���������� ��� ������������� ������� ��������� ��
        /// </summary>
        public TemplateAircraft Aircraft
        {
            get
            {
                return currentAircraft;
            }
            set
            {
                currentAircraft = value;
                UpdateControl();
            }
        }

        #endregion

        #endregion
        
        #region Methods

        #region public bool GetChangeStatus()

        /// <summary>
        /// ���������� ��������, ������������ ���� �� ��������� � ������ �������� ����������
        /// </summary>
        /// <returns></returns>
        public bool GetChangeStatus()
        {
            for (int i = 0; i < powerPlants.Count; i++)
            {
                if (powerPlants[i].GetChangeStatus())
                    return true;
            }
            return false;
        }

        #endregion

        #region public bool SaveData()

        /// <summary>
        /// ���������� ������ ���������� �������� ���������� ��
        /// </summary>
        public void SaveData()
        {
            for (int i =0; i < powerPlants.Count; i++)
                powerPlants[i].SaveData();
        }

        #endregion

        #region public bool CheckAmount()

        ///<summary>
        /// ��������� �������� Amount � ������� ���������� ���������
        ///</summary>
        ///<returns>���������� true ���� �������� ����� ������������� � ��� int, ����� ���������� false</returns>
        public bool CheckAmount()
        {
            bool accept = true;
            for (int i = 0; i < powerPlants.Count; i++)
            {
                accept = powerPlants[i].CheckAmount();
                if (!accept)
                    return false;
            }
            return accept;
        }

        #endregion

        #region public void UpdateControl()

        /// <summary>
        /// ��������� ���������� � ���������� �������� ���������� ��
        /// </summary>
        public void UpdateControl()
        {
            List<TemplateEngine> engines = new List<TemplateEngine>(currentAircraft.Engines);
            Controls.Clear();
            powerPlants.Clear();
            for (int i = 0; i < engines.Count; i++)
            {
                if (i == 0)
                {
                    powerPlants.Add(new TemplateEngineControl(engines[i], true));
                    powerPlants[i].Location = new Point(0, 0);
                }
                else
                {
                    powerPlants.Add(new TemplateEngineControl(engines[i], false));
                    powerPlants[i].Location = new Point(powerPlants[i - 1].Right + MARGIN, 0);
                }
            }
            Controls.AddRange(powerPlants.ToArray());
        }

        #endregion

        #endregion

    }
}
