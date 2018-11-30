using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CAS.UI.Management;

namespace CAS.UI.UIControls.AircraftsControls
{
    /// <summary>
    /// ���������� ������ IAircraft
    /// </summary>
    [ToolboxItem(false)]
    public abstract partial class AbstractAircraftCollectionControl : UserControl
    {

        #region Fields

        private readonly Icons icons = new Icons();
        private const int WIDTH = 400;

        #endregion
        
        #region Constructor

        /// <summary>
        /// ������� ������� ���������� ��� ���������� ������ ��, ����������� � ��������� ������������
        /// </summary>
        public AbstractAircraftCollectionControl()
        {
            InitializeComponent();
            Width = WIDTH;
        }

        #endregion

        #region Properties

        #region public int TopMargin

        /// <summary>
        /// ���������� ��� ������������� ������� ������
        /// </summary>
        public int TopMargin
        {
            get
            {
                return aircraftsListStatistics.Top;
            }
            set
            {
                aircraftsListStatistics.Top = value;
                flowLayoutPanelAircrafts.Top = value + 76;//todo ������ �������
            }
        }

        #endregion

        #region public int DefaultWidth

        /// <summary>
        /// ���������� ������ �� ���������
        /// </summary>
        public int DefaultWidth
        {
            get
            {
                return WIDTH;
            }
        }

        #endregion


        #endregion

        #region Methods

        #region public abstract void FillUIElementsFromCollection();
        /// <summary>
        /// ��������� ����������� ������� �� ������ ���������
        /// </summary>
        public abstract void FillUIElementsFromCollection();

        #endregion

        #region public void ReloadItems()

        /// <summary>
        /// ����������� ��������
        /// </summary>
        public void ReloadItems()
        {
            FillUIElementsFromCollection();
        }

        #endregion

        #endregion
    }
}