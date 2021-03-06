using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Dictionaries;
using CAS.Core.Types.Directives;
using CAS.Core.Types.Aircrafts.Parts;
using CAS.UI.Appearance;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;
using CAS.UI.Management.Dispatchering.DispatcheredUIControls.DirectiveControls;
using CAS.UI.UIControls.Auxiliary;
using Microsoft.VisualBasic.Devices;
using Auxiliary;

namespace CAS.UI.UIControls.DirectivesControls
{
    internal class ADDirectiveListView : SDList<BaseDetailDirective>, IReference
    {
        #region Fields

        private readonly Queue<int> columnIndexQueue = new Queue<int>();
        private const int SORT_MEMORY_COUNT = 6;
        private readonly float[] HEADER_WIDTH = new float[] { 0.12f, 0.340f, 0.1f, 0.1f, 0.16f, 0.16f };
        //private readonly Color[] COLORS = new Color[] { Css.CommonAppearance.Colors.BackColor, Css.ListView.Colors.NotifyColor, Css.ListView.Colors.NotSatisfactoryColor, Css.ListView.Colors.PendingColor };

        #region private IDisplayer displayer
        /// <summary>
        /// Displayer for displaying entity
        /// </summary>
        private IDisplayer displayer;
        #endregion

        #region private DisplayingEntity entity
        /// <summary>
        /// Entity to display
        /// </summary>
        private IDisplayingEntity entity;
        #endregion

        #region private ReflectionTypes reflectionType
        private string displayerText;
        private ReflectionTypes reflectionType;

        #endregion

        #endregion

        #region Constructors

        #region public ADDirectiveListView()
        /// <summary>
        /// ������� ������� ���������� ������������ listview
        /// </summary>
        public ADDirectiveListView()
        {
            selectedItemsList = new List<BaseDetailDirective>();
            ItemsListView.ColumnClick += ItemsListView_ColumnClick;
            ItemsListView.MouseDoubleClick += ItemsListView_MouseDoubleClick;
            ItemsListView.SelectedIndexChanged += ItemsListView_SelectedIndexChanged;
            ItemsListView.KeyDown += ItemsListView_KeyDown; ;
            DisplayerRequested += DirectiveListView_DisplayerRequested;
            columnIndexQueue.Enqueue(0);
            ShowGroups = true;
            AddGroup("AF");
            AddGroup("AP");
        }
        #endregion

        #region public ADDirectiveListView(BaseDetailDirective[] directiveArray, BaseDetail parentBaseDetail)
        /// <summary>
        /// ������� ������� ���������� ������������ listview �� �������� ������ ��������
        /// </summary>
        /// <param name="directiveArray">������ ��������</param>
        public ADDirectiveListView(BaseDetailDirective[] directiveArray) : this()
        {
            SetItemsArray(directiveArray);
        }

        #endregion

        #endregion

        #region Properties

        #region public IDisplayer Displayer
        /// <summary>
        /// Displayer for displaying entity
        /// </summary>
        public IDisplayer Displayer
        {
            get { return displayer; }
            set { displayer = value; }
        }

        /// <summary>
        /// Text of page's header that Reference lead to
        /// </summary>
        public string DisplayerText
        {
            get { return displayerText; }
            set { displayerText = value; }
        }
        #endregion

        #region public DisplayingEntity Entity
        /// <summary>
        /// Entity to display
        /// </summary>
        public IDisplayingEntity Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        /// <summary>
        /// Type of reflection [:|||:]
        /// </summary>
        public ReflectionTypes ReflectionType
        {
            get { return reflectionType; }
            set { reflectionType = value; }
        }
        #endregion

        #region public BaseDetailDirective SelectedItem
        /// <summary>
        /// ���������� ���������� ���������
        /// </summary>
        public override BaseDetailDirective SelectedItem
        {
            get
            {
                if (ItemsListView.SelectedItems.Count == 1) return (ItemsListView.SelectedItems[0].Tag as BaseDetailDirective);
                return null;
            }
        }
        #endregion

        #region public override List<BaseDetailDirective> SelectedItems
        /// <summary>
        /// �������� ���������� ������ ��������� ��������
        /// </summary>
        public override List<BaseDetailDirective> SelectedItems
        {
            get
            {
                return selectedItemsList;

            }
        }
        #endregion

        #endregion

        #region Methods

        #region public override void UpdateItems()

        /// <summary>
        /// ��������� �������� ListView
        /// </summary>
        public override void UpdateItems()
        {
        }

        #endregion

        #region protected override void AddItems(BaseDetailDirective[] itemsArray)

        /// <summary>
        /// ��������� �������� <see cref="BaseDetailDirective"/> � ListView
        /// </summary>
        /// <param name="itemsArray"></param>
        protected override void AddItems(BaseDetailDirective[] itemsArray)
        {
            if (itemsArray.Length != 0)
            {
                int count = itemsArray.Length;
                for (int i = 0; i < count; i++)
                {
                    AddItem(itemsArray[i]);
                }
               // ItemsListView.Items.AddRange(ListViewItemList.ToArray());
                sortMultiplier = 1;
                SortItems(oldColumnIndex);
            }
        }

        #endregion

        #region public override BaseDetailDirective[] GetItemsArray()
        /// <summary>
        /// ����� ���������� ������ ��������
        /// </summary>
        /// <returns>������ ��������</returns>
        public override BaseDetailDirective[] GetItemsArray()
        {
            int count = ListViewItemList.Count;
            BaseDetailDirective[] returnDetailArray = new BaseDetailDirective[0];
            if (count > 0)
            {
                returnDetailArray = new BaseDetailDirective[count];
                for (int i = 0; i < count; i++)
                {
                    returnDetailArray[i] = (BaseDetailDirective)ListViewItemList[i].Tag;
                }
            }
            return returnDetailArray;
        }
        #endregion

        #region protected override void AddItem(BaseDetailDirective item)
        /// <summary>
        /// ��������� ������� � ������ ��������
        /// </summary>
        /// <param name="item">����������� �������</param>
        protected override ListViewItem AddItem(BaseDetailDirective item)
        {
            ListViewItem listViewItem = PrepareItem(item);
            ItemsHash.Add(item,listViewItem);
            ListViewItemList.Add(listViewItem);
            return listViewItem;
        }
                
        #endregion


        #region private ListViewItem PrepareItem(BaseDetailDirective item)

        private ListViewItem PrepareItem(BaseDetailDirective item)
        {
            string[] itemsString = GetItemsString(item);
            ListViewItem listViewItem = new ListViewItem(itemsString);
            LoadListViewItem(item, ref listViewItem);
            return listViewItem;
        }

    

        #endregion

        #region private void PrepareItem(BaseDetailDirective item, ref ListViewItem listViewItem)

        private void PrepareItem(BaseDetailDirective item, ref ListViewItem listViewItem)
        {
            string[] itemsString = GetItemsString(item);
            listViewItem.SubItems.Clear();
            listViewItem.Text = itemsString[0];
            for (int i = 1; i < itemsString.Length; i++)
            {
                listViewItem.SubItems.Add(itemsString[i]);    
            }
            LoadListViewItem(item, ref listViewItem);
        }

        #endregion

        #region private void SetGroupsToItems()

        private void SetGroupsToItems()
        {
            for (int i = 0; i < ListViewItemList.Count; i++)
            {
                BaseDetailDirective directive = (BaseDetailDirective)ListViewItemList[i].Tag;
                if (directive.ADType == ADType.Airframe)
                    ListViewItemList[i].Group = ItemsListView.Groups["AF"];
                else
                    ListViewItemList[i].Group = ItemsListView.Groups["AP"];
            }
        }

        #endregion

        #region private void LoadListViewItem(BaseDetailDirective item, ListViewItem listViewItem)

        private void LoadListViewItem(BaseDetailDirective item, ref ListViewItem listViewItem)
        {
            if (item.AtaChapter != null)
                listViewItem.SubItems.Add(item.AtaChapter.FullName);
            else
                listViewItem.SubItems.Add("0");
            if (!item.Closed)
                listViewItem.ForeColor = Color.Black;
            listViewItem.BackColor = UsefulMethods.GetDirectiveColor(item);
            listViewItem.Tag = item;
        }

        #endregion

        #region private static string[] GetItemsString(BaseDetailDirective item)

        private static string[] GetItemsString(BaseDetailDirective item)
        {
            string description = Regex.Replace(item.Description, "\r\n", " ");
            string status = "Open";
            if (item.NotApplicableDirective)
                status = "N/A";
            else if (item.Superseded)
                status = "S/S";
            else if (item.Terminated)
                status = "Terminated";
            else if (item.Closed)
                status = "Closed";
            else if (item.RepeatedlyPerform && item.FirstPerformOccured)
                status = "Repeat";
            DateTime manufactureDate = ((BaseDetail) item.Parent).ManufactureDate;
            string lastPerfomance = "";
            if (item.LastPerformance != null && !item.NotApplicableDirective && !item.Superseded)
                lastPerfomance = item.LastPerformance.Lifelength.ToListViewItemString(manufactureDate);

            return new string[]
                {
                    item.Title + " " + item.Paragraph, description,
                    status, 
                    status != "Closed" ? ((item.RepeatPerform != null && item.RepeatPerform != Lifelength.NullLifelength) ? (item.RepeatedlyPerform ? item.RepeatPerform.ToRepeatIntervalsFormat() : "") : "") : "",
                    lastPerfomance,
                    item.NextPerformance == null ||!item.NextPerformance.Applicable ? "" : item.NextPerformance.ToListViewNextCompliance(item.ApproximateDate, item.Condition)
                };
        }

        #endregion

        #region public override void AddNewItem(BaseDetailDirective item)

        /// <summary>
        /// ��������� ����� ������� � ������
        /// </summary>
        /// <param name="item"></param>
        public override void AddNewItem(BaseDetailDirective item)
        {
            selectedItemsList.Clear();
            sortMultiplier = 1;
            AddItem(item);
            Sort();
            SetTotalText();
        }

        #endregion

        #region public override void EditItem(BaseDetailDirective oldItem, BaseDetailDirective modifedItem)

        /// <summary>
        /// �������� �������
        /// </summary>
        /// <param name="oldItem">������� �� �������</param>
        /// <param name="modifiedItem">���������� �������</param>
        public override void EditItem(BaseDetailDirective oldItem, BaseDetailDirective modifiedItem)
        {
            ListViewItem listViewItem = ItemsHash[GetDirectiveReferenceByDirectiveID(modifiedItem.ID)];
            PrepareItem(modifiedItem,ref listViewItem);
            if (modifiedItem.ADType == ADType.Airframe)
                listViewItem.Group = ItemsListView.Groups["AF"];
            else
                listViewItem.Group = ItemsListView.Groups["AP"];
            ItemsHash.Remove(modifiedItem);
            ItemsHash.Add(modifiedItem, listViewItem);
            ItemsListView.Refresh();
        }

        #endregion

        #region private BaseDetailDirective GetDirectiveReferenceByDirectiveID(int id)

        private BaseDetailDirective GetDirectiveReferenceByDirectiveID(int id)
        {
            BaseDetailDirective[] directives = GetItemsArray();
            for (int i = 0; i < directives.Length; i++)
            {
                if (directives[i].ID == id)
                    return directives[i];
            }
            return null;
        }

        #endregion
              
        #region protected override void SetHeaders()
        /// <summary>
        /// ������������� ���������
        /// </summary>
        protected override void SetHeaders()
        {
            ColumnHeader columnHeader;
            columnHeader = new ColumnHeader();
            columnHeader.Width = (int)(ItemsListView.Width * HEADER_WIDTH[0]);
            columnHeader.Text = "Title";
            ColumnHeaderList.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Width = (int)(ItemsListView.Width * HEADER_WIDTH[1]);
            columnHeader.Text = "Subject";
            ColumnHeaderList.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Width = (int)(ItemsListView.Width * HEADER_WIDTH[2]);
            columnHeader.Text = "Status";
            ColumnHeaderList.Add(columnHeader);
            
            columnHeader = new ColumnHeader();
            columnHeader.Width = (int)(ItemsListView.Width * HEADER_WIDTH[3]);
            columnHeader.Text = "Repeat Intervals";
            ColumnHeaderList.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Width = (int)(ItemsListView.Width * HEADER_WIDTH[4]);
            columnHeader.Text = "Compliance";
            ColumnHeaderList.Add(columnHeader);

            columnHeader = new ColumnHeader();
            columnHeader.Width = (int)(ItemsListView.Width * HEADER_WIDTH[5]);
            columnHeader.Text = "Next";
            ColumnHeaderList.Add(columnHeader);


            ItemsListView.Columns.AddRange(ColumnHeaderList.ToArray());
        }
        #endregion

        #region protected override void Sort()

        /// <summary>
        /// ���������� ���������� ������
        /// </summary>
        protected override void Sort()
        {
            SortItems(oldColumnIndex);
        }

        #endregion


        #region private void SortItems(int columnIndex)

        private void SortItems(int columnIndex)
        {
            if (oldColumnIndex != columnIndex)
                sortMultiplier = -1;
            if (sortMultiplier == 1)
                sortMultiplier = -1;
            else
                sortMultiplier = 1;
            if (columnIndexQueue.Count >= SORT_MEMORY_COUNT)
            {
                columnIndexQueue.Dequeue();
                columnIndexQueue.Enqueue(columnIndex);
            }
            else 
                columnIndexQueue.Enqueue(columnIndex);
            ItemsListView.Items.Clear();
            SetGroupsToItems();
            ListViewItemList.Sort(new ADDirectiveListViewComparer(columnIndex, sortMultiplier));


            ItemsListView.Items.AddRange(ListViewItemList.ToArray());
            oldColumnIndex = columnIndex;

        }
        #endregion

        #region protected void OnDisplayerRequested()
        protected void OnDisplayerRequested()
        {
            if (null != DisplayerRequested)
            {
                ReflectionTypes reflection = reflectionType;
                Keyboard k = new Keyboard();
                if (k.ShiftKeyDown && reflection == ReflectionTypes.DisplayInCurrent) reflection = ReflectionTypes.DisplayInNew;
                if (null != displayer)
                {
                    DisplayerRequested(this, new ReferenceEventArgs(entity, reflection, displayer, displayerText));
                }
                else
                {
                    DisplayerRequested(this, new ReferenceEventArgs(entity, reflection, displayerText));
                }
            }
        }
        #endregion

        #region protected override void OnSizeChanged(EventArgs e)
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            int count = ColumnHeaderList.Count;
            for (int i = 0; i < count; i++)
            {
                ColumnHeaderList[i].Width = (int)(Width * HEADER_WIDTH[i]);
            }
        }
        #endregion

        #region private void ItemsListView_ColumnClick(object sender, ColumnClickEventArgs e)

        private void ItemsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortItems(e.Column);
        }

        #endregion

        #region private void ItemsListView_MouseDoubleClick(object sender, MouseEventArgs e)

        private void ItemsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SelectedItem != null)
            {
                OnDisplayerRequested();
            }
        }

        #endregion

        #region private void DirectiveListView_DisplayerRequested(object sender, ReferenceEventArgs e)

        private void DirectiveListView_DisplayerRequested(object sender, ReferenceEventArgs e)
        {
            if (SelectedItem != null)
            {
                bool provideCurrentData = SelectedItem.ProvideCurrentData;
                if (!provideCurrentData) SelectedItem.ProvideCurrentData = true;
                string regNumber = "";
                if (SelectedItem.Parent is AircraftFrame)
                    regNumber = SelectedItem.Parent.ToString();
                else
                {
                    if ((SelectedItem.Parent).Parent is Aircraft)
                        regNumber = ((Aircraft)((SelectedItem.Parent).Parent)).RegistrationNumber + ". " + SelectedItem.Parent;
                }
                e.TypeOfReflection = ReflectionTypes.DisplayInNew;
                e.DisplayerText = regNumber + ". " + SelectedItem.DirectiveType.CommonName + ". " + SelectedItem.Title;
                e.RequestedEntity = new DispatcheredDirectiveScreen(SelectedItem);
                SelectedItem.ProvideCurrentData = provideCurrentData;
            }
        }

        #endregion

        #region private void ItemsListView_SelectedIndexChanged(object sender, EventArgs e)

        private void ItemsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedItemsChange(this, new SelectedItemsChangeEventArgs(selectedItemsList.Count));
        }

        #endregion

        #region private void ItemsListView_KeyDown(object sender, KeyEventArgs e)
        private void ItemsListView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    if (SelectedItem != null)
                    {
                        OnDisplayerRequested();
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #endregion

        #region Events

        #region public event EventHandler<ReferenceEventArgs> DisplayerRequested
        /// <summary>
        /// Occurs when linked invoker requests displaying 
        /// </summary>
        public event EventHandler<ReferenceEventArgs> DisplayerRequested;
        #endregion

        #endregion

    }

}