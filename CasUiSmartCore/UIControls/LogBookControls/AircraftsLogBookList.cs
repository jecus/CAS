using System;
using System.Collections.Generic;
using System.ComponentModel;
using Controls.ExtendableList;
using LTR.Core;

namespace LTR.UI.UIControls.LogBookControls
{
    /// <summary>
    /// ������� ���������� ��� ����������� ������ AircraftsLog
    /// </summary>
    [ToolboxItem(false)]
    public partial class AircraftsLogBookList : ExtendableList
    {
        #region Constructors

        #region public AircraftsLogBookList(AircraftCollection aircraftCollection)
        /// <summary>
        /// ����������� ��������� ������ ����� 
        /// </summary>
        /// <param name="aircraftCollection"></param>
        public AircraftsLogBookList(AircraftCollection aircraftCollection)
            : this()
        {
            InitializeComponent();
            DisplayAircraftCollection(aircraftCollection);
        }
        #endregion

        #region public AircraftsLogBookList()
        /// <summary>
        /// ������ ����������
        /// </summary>
        public AircraftsLogBookList()
        {
            InitializeComponent();
            listAircraftsLogBookItem = new List<AircraftsLogBookItem>();
        }
        #endregion

        #endregion

        #region Fields

        private List<AircraftsLogBookItem> listAircraftsLogBookItem;

        private AircraftCollection aircrafts;


        #endregion

        #region Methods

        #region public void DisplayAircraftCollection(AircraftCollection aircrafts)

        /// <summary>
        /// ����� ������������ ����������� LogBook All Aircraft
        /// </summary>
        /// <param name="aircraftCollection">��������� LogBook</param>
        public void DisplayAircraftCollection(AircraftCollection aircraftCollection)
        {
            aircrafts = aircraftCollection;
            Clear();
            listAircraftsLogBookItem.Clear();
            FillAircraftsLogBookList(aircrafts);
            listAircraftsLogBookItem.Sort(new DefaultLogComparer());
            

            DisplayAircraftsLogBookList(listAircraftsLogBookItem);
            AdjustSize();
        }

        #endregion

        #region private void FillAircraftsLogBookList(AircraftCollection aircraftCollection)

        private void FillAircraftsLogBookList(AircraftCollection aircraftCollection)
        {
            for (int i = 0; i < aircraftCollection.Count; i++)
            {
                listAircraftsLogBookItem.Add(new AircraftsLogBookItem(aircraftCollection[i]));
            }

        }
        #endregion

        #region private void DisplayAircraftsLogBookList(List<DetailItem> listAircraftsLogBookItem, int count)

        private void DisplayAircraftsLogBookList(List<AircraftsLogBookItem> listItem)
        {
            foreach (AircraftsLogBookItem item in listItem)
            {
                Add(item);
            }
        }
        #endregion



        #region protected override void OnSizeChanged(EventArgs e)

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AdjustSize();
        }
        #endregion

        #endregion
    }
}
