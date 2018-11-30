using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Aircrafts.Parts;
using CAS.UI.UIControls.Auxiliary.Comparers;

namespace CAS.UI.UIControls.AircraftsControls.AircraftGeneralDataControls
{
    /// <summary>
    /// ������� ���������� ��� ����������� ���������� � ���������� ��
    /// </summary>
    public partial class PowerPlantsControl : PictureBox
    {

        #region Fields

        private const int MARGIN = 30;
        private Aircraft currentAircraft;
        private DateTime dateAsOf;
        private readonly List<EngineControl> powerPlants = new List<EngineControl>();
        

        #endregion

        #region Constructor

        /// <summary>
        /// ������� ������� ���������� ��� ����������� ���������� � ���������� ��
        /// </summary>
        /// <param name="currentAircraft"></param>
        /// <param name="dateAsOf"></param>
        public PowerPlantsControl(Aircraft currentAircraft, DateTime dateAsOf)
        {
            if (currentAircraft.Engines.Length > 0) 
                Size = new Size((EngineControl.StandartSize.Width)*4 + MARGIN*3,EngineControl.StandartSize.Height);
            else
                Size = new Size(0,0);
            this.currentAircraft = currentAircraft;
            this.dateAsOf = dateAsOf;
           
            UpdateControl();
        }

        #endregion

        #region Properties

        #region public Aircraft Aircraft

        /// <summary>
        /// ���������� ��� ������������� ������� ��
        /// </summary>
        public Aircraft Aircraft
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

        #region public DateTime DateAsOf

        /// <summary>
        /// ���������� ��� ������������� ���� DateAsOf
        /// </summary>
        public DateTime DateAsOf
        {
            get
            {
                return dateAsOf;
            }
            set
            {
                dateAsOf = value;
                UpdateDateAsOf();
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

        #region public void SaveData()

        /// <summary>
        /// ���������� ������ ���������� �������� ��
        /// </summary>
        public void SaveData()
        {
            for (int i =0; i < powerPlants.Count; i++)
            {
                powerPlants[i].SaveData();
            }
        }

        #endregion

        #region public void UpdateControl()

        /// <summary>
        /// ��������� ���������� � ���������� �������� ��
        /// </summary>
        public void UpdateControl()
        {
            List<Engine> engines = new List<Engine>(currentAircraft.Engines);
            engines.Sort(new EnginePositionSerialNumberComparer());
            Controls.Clear();
            powerPlants.Clear();
            for (int i = 0; i < engines.Count; i++)
            {
                if (i == 0)
                {
                    powerPlants.Add(new EngineControl(engines[i], DateTime.Today, true, true)); //todo
                    powerPlants[i].Location = new Point(0, 0);
                }
                else
                {
                    powerPlants.Add(new EngineControl(engines[i], DateTime.Today, false, true)); //todo
                    powerPlants[i].Location = new Point(powerPlants[i - 1].Right + MARGIN, 0);
                }
            }
            Controls.AddRange(powerPlants.ToArray());
        }

        #endregion

        #region private void UpdateDateAsOf()

        private void UpdateDateAsOf()
        {
            for (int i = 0; i < powerPlants.Count; i++)
            {
                powerPlants[i].DateAsOf = dateAsOf;
            }
        }

        #endregion

        #endregion


    }
}
