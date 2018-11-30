using System.Collections.Generic;
using System.Windows.Forms;
using CAS.Core.Types.Aircrafts.Parts;
using CAS.Core.Core.Interfaces;
using CAS.Core.Types.ReportFilters;
using CAS.UI.UIControls.DetailsControls;

namespace CAS.UI.UIControls.DiscrepanciesControls
{
    /// <summary>
    /// ������� ����������� ���������� ��� ���������
    /// </summary>
    public partial class ComponentDiscrepancies : UserControl
    {
        #region Fields

        private string caption;
        private int count;

        /// <summary>
        /// ������ ������� ��������� � �������������
        /// </summary>
        private DirectiveFilter discrepanciesFilter = new DiscrepanciesFilter();
        /// <summary>             
        /// �������������� ������
        /// </summary>
        private DetailCollectionFilter additionalFilter;
        /// <summary>
        /// �������� ���������
        /// </summary>
        private IDetailContainer detailSource;
        /// <summary>
        ///
        /// </summary>
        private DetailListView detailList;

        #endregion

        #region Constructors

        /// <summary>
        /// ��������� ������� ����������� ���������� ��� ���������
        /// </summary>
        public ComponentDiscrepancies()
        {
            InitializeComponent();
            ShowDetails();
        }

        /// <summary>
        /// ��������� ������� ����������� ���������� ��� ���������
        /// </summary>
        /// <param name="detailSource"></param>
        public ComponentDiscrepancies(IDetailContainer detailSource) : this()
        {
            this.detailSource = detailSource;
        }

        /// <summary>
        /// ��������� ������� ����������� ���������� ��� ���������
        /// </summary>
        /// <param name="discrepanciesFilter"></param>
        /// <param name="detailSource"></param>
        /// <param name="additionalFilter"></param>
        public ComponentDiscrepancies(DirectiveFilter discrepanciesFilter, IDetailContainer detailSource, DetailCollectionFilter additionalFilter) : this()
        {
            this.discrepanciesFilter = discrepanciesFilter;
            this.detailSource = detailSource;
            this.additionalFilter = additionalFilter;
        }

        #endregion

        #region Properties

        #region public string Caption

        /// <summary>
        /// ��������� �������� ����������
        /// </summary>
        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                caption = value;
                SetCaption();
            }
        }

        #endregion

        #region public int Count

        /// <summary>
        /// ���������� ����������
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        #endregion

        #region public DirectiveFilter DiscrepanciesFilter

        /// <summary>
        /// ������ ������� ��������� � �������������
        /// </summary>
        public DirectiveFilter DiscrepanciesFilter
        {
            get { return discrepanciesFilter; }
            set { discrepanciesFilter = value; }
        }

        #endregion

        #region public DetailCollectionFilter AdditionalFilter

        /// <summary>
        /// �������������� ������
        /// </summary>
        public DetailCollectionFilter AdditionalFilter
        {
            get { return additionalFilter; }
            set { additionalFilter = value; }
        }

        #endregion

        #region public IDetailContainer DetailSource

        /// <summary>
        /// �������� ���������
        /// </summary>
        public IDetailContainer DetailSource
        {
            get { return detailSource; }
            set { detailSource = value; }
        }

        #endregion

        #endregion

        #region Methods

        #region void SetCaption()

        private void SetCaption()
        {
            if (count == 0)
            {
                extendableRichContainer1.Caption = caption + " (No Items)";
            }
            else
            {
                extendableRichContainer1.Caption = caption + " (" + count + ")";
            }
            Visible = count > 0;
        }

        #endregion

        #region public void ShowDetails()

        /// <summary>
        /// ����������� ��������� � ������������
        /// </summary>
        public void ShowDetails()
        {
            Panel tambourine = new Panel();
            
            if (detailList==null)
            {
            
                tambourine.Dock = DockStyle.Top;
                
                detailList = new DetailListView();
                detailList.Dock = DockStyle.Top;
                detailList.Scrollable = false;
                
                tambourine.Height = detailList.Height;
                tambourine.Controls.Add(detailList);

                extendableRichContainer1.MainControl = tambourine;
            }
            detailList.SetItemsArray(GatherDetails());
            detailList.ShowGroups = false;
            detailList.Height = detailList.ItemsListView.Items.Count * 20 + 50;
            tambourine.Height = detailList.Height;
            SetCaption();
        }

        #endregion

        #region public Detail[] GatherDetails()

        /// <summary>
        /// ������� ��������� � ������������
        /// </summary>
        /// <returns></returns>
        public Detail[] GatherDetails()
        {
            List <DetailFilter> filters = new List<DetailFilter>();
/*
            if (discrepanciesFilter != null) 
                filters.Add(new DetailLimitationFilter(discrepanciesFilter));todo
*/
            if (additionalFilter != null) 
                filters.AddRange(additionalFilter.Filters);
            if (detailSource == null)
            {
                count = 0;
                return new Detail[0];
            }

            IDetail[] idetails = detailSource.ContainedDetails;
            List<Detail> details = new List<Detail>(idetails.Length);
            for (int i = 0; i < idetails.Length; i++)
            {
                if (idetails[i] is Detail)
                    details.Add(idetails[i] as Detail);
            }
            DetailCollectionFilter detailFilter = new DetailCollectionFilter(details.ToArray(), filters.ToArray());
            Detail[] items = detailFilter.GatherDetails();
            count = items.Length;
            return items;
        }

        #endregion
        
        #endregion

    }
}
