using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CAS.Core.Types.Aircrafts.Parts.Templates;
using CAS.UI.UIControls.AircraftsControls.AircraftGeneralDataControls;

namespace CAS.UI.UIControls.TemplatesControls
{
    /// <summary>
    /// ������� ���������� ��� ����������� ���������� � ����� ��
    /// </summary>
    public class TemplateLandingGearsControl : PictureBox
    {

        #region Fields

        private const int MARGIN = 30;
        private const int TOP_MARGIN = 20;
        private const int BOTTOM_MARGIN = 20;
        private TemplateDetail[] details;
        private readonly List<TemplateLandingGearControl> landingGears = new List<TemplateLandingGearControl>();

        #endregion
        
        #region Constructor

        /// <summary>
        /// ������� ������� ���������� ��� ����������� ���������� � ����� ���������� ��
        /// </summary>
        public TemplateLandingGearsControl(TemplateDetail[] details)
        {
            if (details.Length > 0) 
                Size = new Size((TemplateLandingGearControl.StandartSize.Width)*3 + MARGIN*2, TemplateLandingGearControl.StandartSize.Height + TOP_MARGIN + BOTTOM_MARGIN);
            else
                Size = new Size(0,0);
            this.details = details;
            UpdateControl();
        }

        #endregion

        #region Properties

        #region public TemplateDetail[] Details

        /// <summary>
        /// ���������� ��� ������������� ������� �����
        /// </summary>
        public TemplateDetail[] Details
        {
            get
            {
                return details;
            }
            set
            {
                details = value;
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
            for (int i = 0; i < landingGears.Count; i++)
            {
                if (landingGears[i].GetChangeStatus())
                    return true;
            }
            return false;
        }

        #endregion

        #region public void SaveData()

        /// <summary>
        /// ���������� ������ ���������� �������� ���������� ��
        /// </summary>
        public void SaveData()
        {
            for (int i = 0; i < landingGears.Count; i++)
                landingGears[i].SaveData();
        }

        #endregion

        #region public bool CheckAmount()

        ///<summary>
        /// ��������� �������� Amount � ������� ���������� ����� ��
        ///</summary>
        ///<returns>���������� true ���� �������� ����� ������������� � ��� int, ����� ���������� false</returns>
        public bool CheckAmount()
        {
            bool accept = true;
            for (int i = 0; i < landingGears.Count; i++)
            {
                accept = landingGears[i].CheckAmount();
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
            List<TemplateDetail> sortedLandingGears = new List<TemplateDetail>(details);
            
            Controls.Clear();
            landingGears.Clear();
            for (int i = 0; i < sortedLandingGears.Count; i++)
            {
                if (i == 0)
                {
                    landingGears.Add(new TemplateLandingGearControl(sortedLandingGears[i], true));
                    landingGears[i].Location = new Point(0, TOP_MARGIN);
                }
                else
                {
                    landingGears.Add(new TemplateLandingGearControl(sortedLandingGears[i], false));
                    landingGears[i].Location = new Point(landingGears[i - 1].Right + MARGIN, TOP_MARGIN);
                }
            }
            Controls.AddRange(landingGears.ToArray());
        }

        #endregion

        #endregion

    }
}