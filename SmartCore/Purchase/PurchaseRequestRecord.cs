using System;
using EFCore.DTO.General;
using SmartCore.Auxiliary.Extentions;
using SmartCore.Calculations;
using SmartCore.Entities;
using SmartCore.Entities.Collections;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Attributes;
using SmartCore.Files;
using SmartCore.Packages;

namespace SmartCore.Purchase
{

    /// <summary>
    /// ����� ��������� ��������� �����
    /// </summary>
    [Table("PurchaseRequestsRecords", "dbo", "ItemId")]
    [Dto(typeof(PurchaseRequestRecordDTO))]
	[Condition("IsDeleted", "0")]
	public class PurchaseRequestRecord : BasePackageRecord, IComparable<PurchaseRequestRecord>, IFileContainer
	{

		/*
		*  ��������
		*/
        #region public Product Product { get; set; }

        public Product Product
        {
            get { return PackageItem as Product; }
            set { PackageItem = value; }
        }
        #endregion

        #region public String AccessoryPartNumber { get; set; }
        /// <summary>
        /// ��������� �����
        /// </summary>
        [ListViewData(0.12f, "Part Number", 1)]
        public String AccessoryPartNumber
        {
            get { return Product != null ? Product.PartNumber : ""; }
            set
            {
                if (Product != null)
                    Product.PartNumber = value;
            }
        }
        #endregion

        #region public String AccessoryDescription { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [ListViewData(0.12f, "Description", 2)]
        public String AccessoryDescription
        {
            get { return Product != null ? Product.Description : ""; }
            set
            {
                if (Product != null)
                    Product.Description = value;
            }
        }
        #endregion

        #region public String AccessoryManufacturer { get; set; }
        /// <summary>
        /// �������������
        /// </summary>
        [ListViewData(0.12f, "Manufacturer", 3)]
        public String AccessoryManufacturer
        {
            get { return Product != null ? Product.Manufacturer : ""; }
            set
            {
                if (Product != null)
                    Product.Manufacturer = value;
            }
        }
        #endregion

        #region public double Quantity { get; set; }

        private double _quantity;
        /// <summary>
        /// ���������� 
        /// </summary>
        [TableColumn("Quantity")]
        [ListViewData(0.08f, "Q-ty")]
        public double Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }
        #endregion

        #region public Measure Measure { get; set; }

        private Measure _measure;
        /// <summary>
        /// ������� ���������
        /// </summary>
        [TableColumn("Measure")]
        [ListViewData(0.1f, "Measure")]
        public Measure Measure
        {
            get { return _measure; }
            set
            {
                if (_measure != value)
                {
                    _measure = value;
                    OnPropertyChanged("Measure");
                }
            }
        }
        #endregion

        #region public Double Cost { get; set; }

        private double _cost;
        /// <summary>
        /// ���� ����� ������
        /// </summary>
        [TableColumn("Cost"), MinMaxValue(0, 1000000000), ListViewData(0.08f, "Cost")]
        public Double Cost
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                {
                    _cost = value;
                    OnPropertyChanged("CostNew");
                }
            }
        }
        #endregion

        /// <summary>
        /// ���������� ��������� ������������� ���������� "����. ����������"
        /// </summary>
        [ListViewData(0.12f, "Performance", 5)]
        public string PerformanceToString
        {
            get
            {
                //if (IsSchedule && Task != null)
                //{
                //    AbstractPerformanceRecord apr = PerformanceRecords
                //        .OfType<AbstractPerformanceRecord>()
                //        .FirstOrDefault(r => r.PerformanceNum == PerformanceNumFromStart);
                //    if (apr != null)
                //        return apr.OnLifelength.ToString();

                //    NextPerformance np = NextPerformances
                //        .FirstOrDefault(n => n.PerformanceNum == PerformanceNumFromStart);
                //    if (np != null)
                //        return np.PerformanceSource.ToString();
                //    return "";
                //}
                //return RepeatInterval;
                return "";
            }
        }

        /// <summary>
        /// ��������� �����
        /// </summary>
        [ListViewData(0.12f, "Rpt. Int", 6)]
        public String RepeatInterval
        {
            get
            {
                //if (IsSchedule && Task != null)
                //    return Task.Threshold.RepeatInterval.ToString();
                //return Threshold.RepeatInterval.ToString();
                return "";
            }
        }

        /// <summary>
        /// ��������� �����
        /// </summary>
        [ListViewData(0.12f, "Perf. Date", 8)]
        public String PerformanceDateString
        {
            get
            {
                //if (IsSchedule && Task != null)
                //{
                //    AbstractPerformanceRecord apr = PerformanceRecords
                //        .OfType<AbstractPerformanceRecord>()
                //        .FirstOrDefault(r => r.PerformanceNum == PerformanceNumFromStart);
                //    if (apr != null)
                //        return Auxiliary.Convert.GetDateFormat(apr.RecordDate);

                //    NextPerformance np = NextPerformances
                //        .FirstOrDefault(n => n.PerformanceNum == PerformanceNumFromStart);
                //    if (np != null)
                //        return np.PerformanceDate == null
                //            ? "N/A"
                //            : Auxiliary.Convert.GetDateFormat((DateTime)np.PerformanceDate);
                //    return "";
                //}
                //return NextPerformanceDate == null ? "N/A" : Auxiliary.Convert.GetDateFormat((DateTime)NextPerformanceDate);
                return "";
            }
        }

        /// <summary>
        /// ���������� ������� ������� �� ���������� ���������� ������ (���� ��� ���������) ��� Lifelength.Null
        /// </summary>
        [ListViewData(0.12f, "Remain", 7)]
        public Lifelength Remains
        {
            get
            {
                //if (NextPerformances.Count == 0)
                //    return Lifelength.Null;
                //return NextPerformances[0].Remains;
                return Lifelength.Null;
            }
        }

        /// <summary>
        /// ��������� �� KIT - � 
        /// </summary>
        [ListViewData(0.12f, "Remarks")]
        public String AccessoryRemarks
        {
            get { return Product != null ? Product.Remarks : ""; }
            set
            {
                if (Product != null)
                    Product.Remarks = value;
            }
        }

        //#region public string KitParentString { get; }

        /////// <summary>
        /////// ��������� �������� ��������
        /////// </summary>
        ////[ListViewData(0.12f, "Parent", 5)]
        ////public string ParentString
        ////{
        ////    get { return _product != null ? _product.ParentString : ""; }
        ////}
        //#endregion

        #region public Supplier Supplier { get; set; }

        //ListViewData(0.2f, "Supplier", 4)]
        [TableColumn("SupplierId")]
        [Child(false)]
        public Supplier Supplier { get; set; }
        #endregion

        #region public KitCostCondition CostCondition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [TableColumn("CostCondition")]
        public ComponentStatus CostCondition { get; set; }
        #endregion

        #region public Boolean Processed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [TableColumn("Processed")]
        public Boolean Processed { get; set; }
        #endregion

        #region public string Remarks {get;set;}
        [TableColumn("Remarks")]
        public string Remarks {get;set;}
		#endregion


		#region public AttachedFile AttachedFile { get; set; }

		private AttachedFile _attachedFile;

		/// <summary>
		/// 
		/// </summary>
		public AttachedFile AttachedFile
	    {
			get
			{
				return _attachedFile ?? (Files.GetFileByFileLinkType(FileLinkType.PurchaseRequestRecordFile));
			}
			set
			{
				_attachedFile = value;
				Files.SetFileByFileLinkType(SmartCoreObjectType.ItemId, value, FileLinkType.PurchaseRequestRecordFile);
			}
		}

	    #endregion

		#region public CommonCollection<ItemFileLink> Files { get; set; }

		private CommonCollection<ItemFileLink> _files;

	    [Child(RelationType.OneToMany, "ParentId", "ParentTypeId", 1860)]
		public CommonCollection<ItemFileLink> Files
		{
			get { return _files ?? (_files = new CommonCollection<ItemFileLink>()); }
			set
			{
				if (_files != value)
				{
					if (_files != null)
						_files.Clear();
					if (value != null)
						_files = value;
				}
			}
		}

		#endregion

		/*
		*  ������ 
		*/

		#region public RequestForQuotation()
		/// <summary>
		/// ������� ��������� ����� ��� �������������� ����������
		/// </summary>
		public PurchaseRequestRecord()
        {
            ItemId = -1;
			SmartCoreObjectType = SmartCoreType.PurchaseRequestRecord;
            ParentPackageId = -1;
            Remarks = "";
            AttachedFile = null;
        }
        #endregion

        #region public PurchaseRequestRecord(int rfqId, Supplier supplier, Product accessory, double quantity):this()
        /// <summary>
        /// ������� ������ ��� �������������� ����������
        /// </summary>
        public PurchaseRequestRecord(int rfqId, Supplier supplier, Product accessory, double quantity)
            : this()
        {
            if(supplier == null)
                throw new ArgumentNullException("supplier", "Supplier must be not null");
            ParentPackageId = rfqId;

            if(accessory != null)
            {
                PackageItemId = accessory.ItemId;
                _cost = accessory.CostNew;
                _measure = accessory.Measure;
                PackageItemType = accessory.SmartCoreObjectType;
                _quantity = quantity;
            }
            else
            {
                PackageItemId = -1;
                _cost = 0;
                _measure = Measure.Unit;
                PackageItemType = SmartCoreType.Unknown;
                _quantity = 0;
            }
        }
        #endregion

        #region public override string ToString()
        /// <summary>
        /// ����������� ��� �������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
        }
        #endregion   

        #region public int CompareTo(PurchaseRequestrecord y)

        public int CompareTo(PurchaseRequestRecord y)
        {
            return ItemId.CompareTo(y.ItemId);
        }

		#endregion

	}

}
