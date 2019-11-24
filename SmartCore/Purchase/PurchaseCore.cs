using System;
using System.Collections.Generic;
using System.Linq;
using EntityCore.DTO.Dictionaries;
using EntityCore.DTO.General;
using EntityCore.Filter;
using SmartCore.Calculations.Maintenance;
using SmartCore.Calculations.PerformanceCalculator;
using SmartCore.Entities;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Interfaces;
using SmartCore.Entities.General.MaintenanceWorkscope;
using SmartCore.Entities.General.Store;
using SmartCore.Entities.NewLoader;
using SmartCore.Filters;
using SmartCore.Packages;
using SmartCore.Queries;

namespace SmartCore.Purchase
{
	/// <summary>
    /// ������ �������
    /// </summary>
    public class PurchaseCore : IPurchaseCore
    {
		private readonly ICasEnvironment _casEnvironment;
	    private readonly INewLoader _newLoader;
		private readonly ILoader _loader;
		private readonly IPackageCore _packageCore;
		private readonly INewKeeper _newKeeper;
		private readonly IPerformanceCalculator _performanceCalculator;

		#region public PurchaseService(DatabaseManager databaseManager)
        /// <summary>
        /// ������� ������ �������
        /// </summary>
        public PurchaseCore(ICasEnvironment casEnvironment, INewLoader newLoader, ILoader loader, 
							IPackageCore packageService, INewKeeper newKeeper, IPerformanceCalculator performanceCalculator)
        {
	        _casEnvironment = casEnvironment;
	        _newLoader = newLoader;
			_loader = loader;
	        _packageCore = packageService;
	        _newKeeper = newKeeper;
	        _performanceCalculator = performanceCalculator;
        }
        #endregion

        #region public void LoadPurchaseOrderItems(PurchaseOrder po)
        /// <summary>
        /// ��������� ��� �������� �������� ������
        /// </summary>
        /// <param name="po"></param>
        public void LoadPurchaseOrderItems(PurchaseOrder po)
        {
            if (po == null) return;

            po.Products = GetProducts(po);
            po.PackageRecords.Clear();
            po.PackageRecords.AddRange(_newLoader.GetObjectListAll<PurchaseRequestRecordDTO, PurchaseRequestRecord>(new Filter("ParentPackageId", po.ItemId)));

            _packageCore.SetParents(po);

            if (po.ParentRequest == null && po.ParentQuotationId > 0)
                po.ParentRequest = GetRequestForQuotation(po.ParentQuotationId);

            foreach (var record in po.PackageRecords)
                record.Product = po.Products.FirstOrDefault(a => a.ItemId == record.PackageItemId);
        }

        #endregion

        public List<Product> GetProducts()
        {
            return _loader.GetObjectListAll<Product>(loadChild: true, ignoreConditions: true);
        }

        public List<Product> GetProducts(PurchaseOrder po)
        {

            #region ����� ���������

            //������ �������, ������������ ������� ��������������� ��������� ����� ������� ������������ �������
            //������������� ����������� ������������� ������ � ����������� �� ��������� �����
            var accessoryParentId =
                BaseQueries.GetSelectQueryColumnOnly<PurchaseRequestRecord>
                (BasePackageRecord.PackageItemIdProperty,
                 new ICommonFilter[]
                 {
                     new CommonFilter<int>(BasePackageRecord.ParentPackageIdProperty, po.ItemId),
                     new CommonFilter<int>(BasePackageRecord.PackageItemTypeProperty, SmartCoreType.Product.ItemId)
                 });
            //������ �� ��������� ���� ������� ������������ 
            //��� �������� ��������� ���� ������� ������ ����
            //����� ��������������� �����
            ICommonFilter idFilter = new CommonFilter<string>(BaseEntityObject.ItemIdProperty,
                                                              FilterType.In,
                                                              new[] { accessoryParentId });
            //��������� ������� �� ������� ����� �� ����������� � ��������� ��
            //������������� �������� �������� ����. �������� ��������� ����
            //����� �� ����������� �� ������ ���� ����� ������������� ������������ ����� �����
            var kits = _loader.GetObjectListAll<Product>(idFilter, true, true, true);

            #endregion

            #region �������� �������
            //������ �������, ������������ ������� ��������������� ��������� ����� ������� ������������ �������
            //������������� ����������� ������������� ������ � ����������� �� ��������� �����
            accessoryParentId =
                BaseQueries.GetSelectQueryColumnOnly<RequestForQuotationRecord>
                (BasePackageRecord.PackageItemIdProperty,
                 new ICommonFilter[]
                 {
                     new CommonFilter<int>(BasePackageRecord.ParentPackageIdProperty, po.ItemId),
                     new CommonFilter<int>(BasePackageRecord.PackageItemTypeProperty, SmartCoreType.Product.ItemId)
                 });
            //������ �� ��������� ���� ������� ������������ 
            //��� �������� ��������� ���� ������� ������ ����
            //����� ��������������� �����
            idFilter = new CommonFilter<string>(BaseEntityObject.ItemIdProperty,
                                                FilterType.In,
                                                new[] { accessoryParentId });
            //��������� ������� �� ������� ����� �� ����������� � ��������� ��
            //������������� �������� �������� ����. �������� ��������� ����
            //����� �� ����������� �� ������ ���� ����� ������������� ������������ ����� �����
            var componentModels = _loader.GetObjectListAll<ComponentModel>(idFilter, true, true);
            #endregion

            var accessories = new List<Product>();

            accessories.AddRange(kits.ToArray());
            //accessories.AddRange(baseDetails.ToArray());
            accessories.AddRange(componentModels.ToArray());

            return accessories;
        }

        public List<Product> GetProducts(Supplier supplier)
        {
            var accessories = new List<Product>();

            #region ����� ���������

            //������ �������, �������� ��������� ����������� � ����������
            //��� ������������� ���������� ����� ������������� ����������� ����������
            var accessoriesRelations = BaseQueries.GetSelectQueryColumnOnly<KitSuppliersRelation>
                (KitSuppliersRelation.KitIdProperty,
                 new ICommonFilter[] 
                 { 
                     new CommonFilter<int>(KitSuppliersRelation.SupplierProperty, supplier.ItemId), 
                     new CommonFilter<int>(KitSuppliersRelation.ParentTypeIdProperty, SmartCoreType.Product.ItemId) 
                 }
                );
            //������ �� ��������� ���� ������� ������������ 
            //��� �������� ��������� ���� ������� ������ ����
            //����� ��������������� ������������ ����� �����
            ICommonFilter idFilter = new CommonFilter<string>(BaseEntityObject.ItemIdProperty,
                                                              FilterType.In,
                                                              new[] { accessoriesRelations });
            //��������� ������� �� ������� ����� �� ����������� � ��������� ��
            //������������� �������� �������� ����. �������� ��������� ����
            //����� �� ����������� �� ������ ���� ����� ������������� ������������ ����� �����
            accessories.AddRange(_loader.GetObjectListAll<Product>(idFilter, true, ignoreConditions: true).ToArray());

            #endregion

            return accessories;
        }

		public IList<Product> GetProducts(int kitId, bool loadChild = false)
		{
			return _newLoader.GetObjectListAll<AccessoryDescriptionDTO, Product>(new List<Filter>()
			{
				new Filter("StandartId",kitId),
				new Filter("ModelingObjectTypeId",-1)
			}, loadChild);
		}

        /// <summary>
        /// ���������� ���������� ������ ���������� �����
        /// </summary>
        /// <param name="aircraft">��������� �����. ��� �������� null ������ ��� ���������� ������</param>
        /// <param name="status">������ ������� ���������� ������. (�� ��������� = WorkPackageStatus.All)</param>
        /// <param name="loadWorkPackageItems">���� �������� ��������� ����������� ������</param>
        /// <param name="includedAccessory">������, ������� ������ ��������� ������ (��� �������� ������ ��������� ������ ������ 0 ����������� ������)</param>
        /// <returns></returns>
        public IList<PurchaseOrder> GetPurchaseOrders(Aircraft aircraft,
                                                 WorkPackageStatus status = WorkPackageStatus.All,
                                                 bool loadWorkPackageItems = false,
                                                 IEnumerable<AbstractAccessory> includedAccessory = null)
        {
			var filters = new List<ICommonFilter>();

	        if (aircraft != null)
	        {
				filters.Add(new CommonFilter<int>(PurchaseOrder.ParentTypeIdProperty, aircraft.SmartCoreObjectType.ItemId));
				filters.Add(new CommonFilter<int>(PurchaseOrder.ParentIdProperty, aircraft.ItemId));
			}
            if (status != WorkPackageStatus.All)
				filters.Add(new CommonFilter<int>(PurchaseOrder.StatusProperty, (int)status));
			if (includedAccessory != null)
            {
                string filterString = "";
                if (!includedAccessory.Any())
                {
                    filterString += "(PackageItemTypeId = 0 and PackageItemId in (0))";
                }
                else
                {
                    var subs = new Dictionary<int, string>();
                    foreach (var task in includedAccessory)
                    {
                        if (subs.ContainsKey(task.SmartCoreObjectType.ItemId))
                        {
                            var s = subs[task.SmartCoreObjectType.ItemId];
                            if (s != "")
                                s += ", ";

                            s += task.ItemId;
                            subs[task.SmartCoreObjectType.ItemId] = s;
                        }
                        else subs.Add(task.SmartCoreObjectType.ItemId, task.ItemId.ToString());
                    }

                    filterString = "";
                    foreach (KeyValuePair<int, string> sub in subs)
                    {
                        if (filterString != "") filterString += "\n or";
                        filterString += $"(PackageItemTypeId = {sub.Key} and PackageItemId in ({sub.Value}))";
                    }
                }

				var purchaseRecordIn = $"{BaseQueries.GetSelectQueryColumnOnly<PurchaseRequestRecord>(PurchaseRequestRecord.ParentPackageIdProperty)} and {filterString}";
				filters.Add(new CommonFilter<string>(BaseEntityObject.ItemIdProperty, FilterType.In, new[] { purchaseRecordIn }));
			}

            var collection = _loader.GetObjectListAll<PurchaseOrder>(filters.ToArray(), true);

			foreach (var rfq in collection)
            {
                //�������� ������ �� ������������ �������
                _packageCore.SetParents(rfq);
                //�������� ��������� ������������� ������ (���� ���������)
                if (loadWorkPackageItems)
                    LoadPurchaseOrderItems(rfq);
            }

            return collection.ToArray();
        }

        /// <summary>
        /// ���������� ������������ ������ ���������� �����
        /// </summary>
        /// <param name="parent">�������� ������������� �������. ��� �������� null ������ ��� ������������ ������</param>
        /// <param name="statuses">������ ������� ������������ ������. (�� ��������� = WorkPackageStatus.All)</param>
        /// <param name="loadWorkPackageItems">���� �������� ��������� ������������� ������</param>
        /// <param name="includedAccessory">������, ������� ������ ��������� ������ (��� �������� ������ ��������� ������ ������ 0 ������������� ������)</param>
        /// <returns></returns>
        public IList<PurchaseOrder> GetPurchaseOrders(BaseEntityObject parent,
                                                 WorkPackageStatus[] statuses = null,
                                                 bool loadWorkPackageItems = false,
                                                 Product[] includedAccessory = null)
        {
            return _packageCore.GetPackages<PurchaseOrder, PurchaseRequestRecord>(parent, statuses, loadWorkPackageItems, includedAccessory)
                    .ToArray();
        }

        /// <summary>
        /// ���������� ������������ ������ ���������� �����
        /// </summary>
        /// <param name="parent">�������� ������������� �������. ��� �������� null ������ ��� ������������ ������</param>
        /// <param name="statuses">������ ������� ������������ ������. (�� ��������� = WorkPackageStatus.All)</param>
        /// <param name="loadWorkPackageItems">���� �������� ��������� ������������� ������</param>
        /// <param name="includedAccessory">������, ������� ������ ��������� ������ (��� �������� ������ ��������� ������ ������ 0 ������������� ������)</param>
        /// <returns></returns>
        public IList<RequestForQuotation> GetRequestForQuotation(BaseEntityObject parent,
                                                            WorkPackageStatus[] statuses = null,
                                                            bool loadWorkPackageItems = false,
                                                            Product[] includedAccessory = null)
        {
            return _packageCore.GetPackages<RequestForQuotation, RequestForQuotationRecord>(parent, statuses, loadWorkPackageItems, includedAccessory)
                    .ToArray();
        }

        /// <summary>
        /// ���������� ������������ ������ ���������� �����
        /// </summary>
        /// <param name="parent">�������� ������������� �������. ��� �������� null ������ ��� ������������ ������</param>
        /// <param name="statuses">������ ������� ������������ ������. (�� ��������� = WorkPackageStatus.All)</param>
        /// <param name="loadWorkPackageItems">���� �������� ��������� ������������� ������</param>
        /// <param name="includedAccessory">������, ������� ������ ��������� ������ (��� �������� ������ ��������� ������ ������ 0 ������������� ������)</param>
        /// <returns></returns>
        public IList<InitialOrder> GetInitialOrders(BaseEntityObject parent,
                                               WorkPackageStatus[] statuses = null,
                                               bool loadWorkPackageItems = false,
                                               Product[] includedAccessory = null)
        {
            return _packageCore.GetPackages<InitialOrder, InitialOrderRecord>(parent, statuses, loadWorkPackageItems, includedAccessory)
                    .ToArray();
        }

		/*
         * 
         */

		#region private List<Product> GetProducts(RequestForQuotation rfq)
		/// <summary>
		/// ���������� ��� �������������, ����������� � ������������ ������ �������
		/// </summary>
		/// <returns></returns>
		private List<Product> GetProducts(RequestForQuotation rfq)
        {
            #region ����� ���������

            //������ �������, ������������ ������� ��������������� ��������� ����� ������� ������������ �������
            //������������� ����������� ������������� ������ � ����������� �� ��������� �����
            var accessoryParentId =
                BaseQueries.GetSelectQueryColumnOnly<RequestForQuotationRecord>
                (BasePackageRecord.PackageItemIdProperty,
                 new ICommonFilter[]
                 {
                     new CommonFilter<int>(BasePackageRecord.ParentPackageIdProperty, rfq.ItemId),
                     new CommonFilter<int>(BasePackageRecord.PackageItemTypeProperty, SmartCoreType.Product.ItemId)
                 });
            //������ �� ��������� ���� ������� ������������ 
            //��� �������� ��������� ���� ������� ������ ����
            //����� ��������������� �����
            ICommonFilter idFilter = new CommonFilter<string>(BaseEntityObject.ItemIdProperty,
                                                              FilterType.In,
                                                              new[] { accessoryParentId });
            //��������� ������� �� ������� ����� �� ����������� � ��������� ��
            //������������� �������� �������� ����. �������� ��������� ����
            //����� �� ����������� �� ������ ���� ����� ������������� ������������ ����� �����
            var kits = _loader.GetObjectListAll<Product>(idFilter, true, true, true);

            #endregion

            #region �������� �������

            //������ �������, ������������ ������� ��������������� ��������� ����� ������� ������������ �������
            //������������� ����������� ������������� ������ � ����������� �� ��������� �����
            accessoryParentId =
                BaseQueries.GetSelectQueryColumnOnly<RequestForQuotationRecord>
                (BasePackageRecord.PackageItemIdProperty,
                 new ICommonFilter[]
                 {
                     new CommonFilter<int>(BasePackageRecord.ParentPackageIdProperty, rfq.ItemId),
                     new CommonFilter<int>(BasePackageRecord.PackageItemTypeProperty, SmartCoreType.Product.ItemId)
                 });
            //������ �� ��������� ���� ������� ������������ 
            //��� �������� ��������� ���� ������� ������ ����
            //����� ��������������� �����
            idFilter = new CommonFilter<string>(BaseEntityObject.ItemIdProperty,
                                                FilterType.In,
                                                new[] { accessoryParentId });
            //��������� ������� �� ������� ����� �� ����������� � ��������� ��
            //������������� �������� �������� ����. �������� ��������� ����
            //����� �� ����������� �� ������ ���� ����� ������������� ������������ ����� �����
            var componentModels = _loader.GetObjectListAll<ComponentModel>(idFilter, true, true);
            #endregion

            var accessories = new List<Product>();

            accessories.AddRange(kits.ToArray());
            accessories.AddRange(componentModels.ToArray());

            return accessories;
        }
        #endregion

        #region public void LoadRequestForQuotationItems(RequestForQuotation rfq)
        /// <summary>
        /// ��������� ��� �������� �������� ������
        /// </summary>
        /// <param name="rfq"></param>
        public void LoadRequestForQuotationItems(RequestForQuotation rfq)
        {
            if (rfq == null) return;

            rfq.Products = GetProducts(rfq);
            rfq.PackageRecords.Clear();
            rfq.PackageRecords.AddRange(_newLoader.GetObjectListAll<RequestForQuotationRecordDTO, RequestForQuotationRecord>(new Filter("ParentPackageId", rfq.ItemId)));
            
            _packageCore.SetParents(rfq);
            
            foreach (var record in rfq.PackageRecords)
                record.Product = rfq.Products.FirstOrDefault(a => a.ItemId == record.PackageItemId);
        }

        #endregion

        #region public List<Product> GetProducts(InitialOrder rfq)
        /// <summary>
        /// ���������� ��� �������������, ����������� � ������������ ������ �������
        /// </summary>
        /// <returns></returns>
        private List<Product> GetProducts(InitialOrder rfq)
        {
            #region ����� ���������

            //������ �������, ������������ ������� ��������������� ��������� ����� ������� ������������ �������
            //������������� ����������� ������������� ������ � ����������� �� ��������� �����
            var accessoryParentId =
                BaseQueries.GetSelectQueryColumnOnly<InitialOrderRecord>
                (InitialOrderRecord.ProductIdProperty,
                 new ICommonFilter[]
                 {
                     new CommonFilter<int>(BasePackageRecord.ParentPackageIdProperty, rfq.ItemId),
                     new CommonFilter<int>(InitialOrderRecord.ProductTypeProperty, SmartCoreType.Product.ItemId)
                 });
            //������ �� ��������� ���� ������� ������������ 
            //��� �������� ��������� ���� ������� ������ ����
            //����� ��������������� �����
            ICommonFilter idFilter = new CommonFilter<string>(BaseEntityObject.ItemIdProperty,
                                                              FilterType.In,
                                                              new[] { accessoryParentId });
            //��������� ������� �� ������� ����� �� ����������� � ��������� ��
            //������������� �������� �������� ����. �������� ��������� ����
            //����� �� ����������� �� ������ ���� ����� ������������� ������������ ����� �����
            var kits = _loader.GetObjectListAll<Product>(idFilter, true, true);

            #endregion

            #region �������� �������

            //������ �������, ������������ ������� ��������������� ��������� ����� ������� ������������ �������
            //������������� ����������� ������������� ������ � ����������� �� ��������� �����
            accessoryParentId =
                BaseQueries.GetSelectQueryColumnOnly<InitialOrderRecord>
                (InitialOrderRecord.ProductIdProperty,
                 new ICommonFilter[]
                 {
                     new CommonFilter<int>(BasePackageRecord.ParentPackageIdProperty, rfq.ItemId),
                     new CommonFilter<int>(InitialOrderRecord.ProductTypeProperty, SmartCoreType.Product.ItemId)
                 });
            //������ �� ��������� ���� ������� ������������ 
            //��� �������� ��������� ���� ������� ������ ����
            //����� ��������������� �����
            idFilter = new CommonFilter<string>(BaseEntityObject.ItemIdProperty,
                                                FilterType.In,
                                                new[] { accessoryParentId });
            //��������� ������� �� ������� ����� �� ����������� � ��������� ��
            //������������� �������� �������� ����. �������� ��������� ����
            //����� �� ����������� �� ������ ���� ����� ������������� ������������ ����� �����
            var componentModels = _loader.GetObjectListAll<ComponentModel>(idFilter, true, true);
            #endregion

            var accessories = new List<Product>();

            accessories.AddRange(kits.ToArray());
            accessories.AddRange(componentModels.ToArray());

            return accessories;
        }
        #endregion

        #region public void LoadInitionalOrderItems(InitialOrder rfq)
        /// <summary>
        /// ��������� ��� �������� �������� ������
        /// </summary>
        /// <param name="rfq"></param>
        public void LoadInitionalOrderItems(InitialOrder rfq)
        {
            if (rfq == null) return;

            rfq.Products = GetProducts(rfq);

            _packageCore.LoadPackageItems<InitialOrder, InitialOrderRecord>(rfq);
            _packageCore.SetParents(rfq);

            foreach (var record in rfq.PackageRecords)
                record.Product = rfq.Products.FirstOrDefault(a => a.ItemId == record.ProductId);
        }

        #endregion

        public RequestForQuotation GetRequestForQuotation(int requestForQuotationId)
        {
            var result = _loader.GetObject<RequestForQuotation>(requestForQuotationId);
            
            if (result == null) return null;

            _packageCore.SetParents(result);

            LoadRequestForQuotationItems(result);

            return result;
        }

        /// <summary>
        /// ��������� ���������� ���
        /// </summary>
        /// <param name="po"></param>
        /// <param name="date"></param>
        public void Publish(PurchaseOrder po, DateTime date)
        {
            if (po.Status != WorkPackageStatus.Closed)
            {
                po.Status = WorkPackageStatus.Published;
                po.PublishingDate = date;
            }
            else
            {
                po.Status = WorkPackageStatus.Published;
                po.Remarks = "";
            }
	        _newKeeper.Save(po);
        }

		#region public InitialOrder AddInitialOrder(List<KeyValuePair<Product, double>> quotationList, BaseEntityObject parent, DateTime effDate, out string message)
		/// <summary>
		/// ���������� ��������������� ���� ������
		/// </summary>
		public InitialOrder AddInitialOrder(IEnumerable<KeyValuePair<Product, double>> quotationList,
											BaseEntityObject parent,
											DateTime effDate,
											DeferredCategory category,
											out string message)
		{
			if (parent == null)
			{
				message = "Not set parent." +
						  "\nFailed to create empty Initial Order";
				return null;
			}

			if (!(parent is Aircraft) && !(parent is Operator) && !(parent is Store))
			{
				message = "Parent must be Aircraft or Store or Operator." +
						  "\nFailed to create empty Initial Order";
				return null;
			}

			if (quotationList == null)
			{
				message = "Selected tasks not have a accessories." +
						  "\nFailed to create empty Initial Order";
				return null;
			}

			var rqst = new InitialOrder
			{
				Description = "",
				Status = WorkPackageStatus.Opened,
				Author = _casEnvironment.Operators[0].Name,
				OpeningDate = DateTime.Today,
				PublishingDate = new DateTime(1852, 01, 01),
				ClosingDate = new DateTime(1852, 01, 01),
				Remarks = "",
				ParentType = parent.SmartCoreObjectType,
				ParentId = parent.ItemId,
				Title = parent + "-IO-" + DateTime.Now
			};
			_newKeeper.Save(rqst);

			#region ������������ ������� �������� ������
			foreach (var item in quotationList)
			{
				Product product = item.Key;
				double quantity = item.Value;

				var record =
					rqst.PackageRecords.FirstOrDefault(r => r.PackageItemType == product.SmartCoreObjectType &&
															r.PackageItemId == product.ItemId);
				if (record != null)
				{
					record.Quantity += item.Value;
				}
				else
				{
					record = new InitialOrderRecord(rqst.ItemId,
													product,
													quantity,
													parent,
													effDate,
													ComponentStatus.New | ComponentStatus.Overhaul | ComponentStatus.Repair | ComponentStatus.Serviceable,
													null,
													category);
					rqst.PackageRecords.Add(record);
				}
			}
			#endregion

			#region ���������� �������� ������ � ��� �������

			foreach (var item in rqst.PackageRecords)
			{
				item.ParentPackageId = rqst.ItemId;
				_newKeeper.Save(item);
			}

			message = "Items added successfully";

			#endregion

			return rqst;
		}
		#endregion

		#region public InitialOrder AddInitialOrder(IEnumerable<InitialOrderRecord> initialList, BaseEntityObject parent, out string message)
		/// <summary>
		/// ���������� ��������������� ���� ������
		/// </summary>
		public InitialOrder AddInitialOrder(IEnumerable<InitialOrderRecord> initialList,
											BaseEntityObject parent,
											out string message)
		{
			if (parent == null)
			{
				message = "Not set parent." +
						  "\nFailed to create empty Initial Order";
				return null;
			}

			if (!(parent is Aircraft) && !(parent is Operator) && !(parent is Store))
			{
				message = "Parent must be Aircraft or Store or Operator." +
						  "\nFailed to create empty Initial Order";
				return null;
			}

			if (initialList == null)
			{
				message = "Selected tasks not have a accessories." +
						  "\nFailed to create empty Initial Order";
				return null;
			}

			var rqst = new InitialOrder
			{
				Description = "",
				Status = WorkPackageStatus.Opened,
				Author = _casEnvironment.Operators[0].Name,
				OpeningDate = DateTime.Today,
				PublishingDate = new DateTime(1852, 01, 01),
				ClosingDate = new DateTime(1852, 01, 01),
				Remarks = "",
				ParentType = parent.SmartCoreObjectType,
				ParentId = parent.ItemId,
				Title = parent + "-IO-" + DateTime.Now
			};
			_newKeeper.Save(rqst);

			#region ������������ ������� �������� ������
			foreach (var item in initialList)
				rqst.PackageRecords.Add(item);
			#endregion

			#region ���������� �������� ������ � ��� �������

			foreach (var item in rqst.PackageRecords)
			{
				item.ParentPackageId = rqst.ItemId;
				item.DestinationObjectType = parent.SmartCoreObjectType;
				item.DestinationObjectId = parent.ItemId;
				item.DestinationObject = parent;

				_newKeeper.Save(item);
			}

			message = "Items added successfully";

			#endregion

			return rqst;
		}
		#endregion

		#region public void GetInitialOrderItemsWithCalculate(InitialOrder initialOrder)
		/// <summary>
		/// ��������� �������� ���������� ����, � ���������� �� �����������.
		/// </summary>
		/// <param name="initialOrder"></param>
		public void GetInitialOrderItemsWithCalculate(InitialOrder initialOrder)
		{
			LoadInitionalOrderItems(initialOrder);

			//������ �� ����� ������������ ����� ������������� �� ���� ����� (Schedule/Store)
			//� ������ ������ ����������, �����, ��� ������ ������ ��������� ������ � ���� ����������
			var maintenanceChecksWprs =
				initialOrder.PackageRecords.Where(w => w.IsSchedule
													&& w.Task.SmartCoreObjectType == SmartCoreType.MaintenanceCheck
													&& w.Task.ItemId > 0);
			var mcs = new List<MaintenanceCheck>();
			var rmcs = new List<MaintenanceCheck>();
			foreach (var maintenanceChecksWpr in maintenanceChecksWprs)
			{
				var mc = (MaintenanceCheck)maintenanceChecksWpr.Task;
				var apr =
					mc.PerformanceRecords
						.FirstOrDefault(pr => pr.NumGroup == maintenanceChecksWpr.PerformanceNumFromStart);
				if (apr != null)
				{
					mc.ComplianceGroupNum = apr.NumGroup;
					rmcs.Add(mc);
				}
				else
				{
					mc.ComplianceGroupNum = maintenanceChecksWpr.PerformanceNumFromStart;
					mcs.Add(mc);
				}
				mc.ResetMathData();
			}

			#region ������ ���������� ��� ����� �� ������� ������ � ������ ������� �������� ������
			//����������� �� ���� (Schedule/Store)
			var groupByMaintenanceType =
				mcs.GroupBy(mc => mc.Schedule);
			foreach (var maintenanceTypeGroup in groupByMaintenanceType)
			{
				var groupByMaintenanceNum =
					maintenanceTypeGroup.GroupBy(mc => mc.ComplianceGroupNum);
				foreach (var maintenanceComplianceGroup in groupByMaintenanceNum)
				{
					var mcg = new MaintenanceCheckGroupByType(maintenanceComplianceGroup.First().Schedule);
					foreach (var maintenanceCheck in maintenanceComplianceGroup)
						mcg.Checks.Add(maintenanceCheck);
					//���� ����������
					_performanceCalculator.GetPerformance(mcg, maintenanceComplianceGroup.Key);

				}
			}
			#endregion

			foreach (var record in initialOrder.PackageRecords)
			{

				if (!record.IsSchedule)
				{
					_performanceCalculator.GetNextPerformance(record);
					continue;
				}
				if (record.Task == null || record.Task.ItemId < 0)
					continue;

				AbstractPerformanceRecord apr = null;
				apr = record.Task.PerformanceRecords
				   .Cast<AbstractPerformanceRecord>()
				   .FirstOrDefault(r => r.PerformanceNum == record.PerformanceNumFromStart);

				if (apr == null)
				{
					IDirective task = record.Task;

					if (!task.IsClosed)
					{
						if (task is Entities.General.Accessory.Component)
							_performanceCalculator.GetPerformance((Entities.General.Accessory.Component)task, record.PerformanceNumFromStart);
						else _performanceCalculator.GetPerformance(task, record.PerformanceNumFromStart);
					}
				}
			}
		}
		#endregion

		#region public RequestForQuotation AddQuotationOrder(List<KeyValuePair<Product, double>> quotationList, BaseEntityObject parent, out string message)
		/// <summary>
		/// ���������� ���������� ������
		/// </summary>
		public RequestForQuotation AddQuotationOrder(IEnumerable<KeyValuePair<Product, double>> quotationList, BaseEntityObject parent, out string message)
		{
			if (parent == null)
			{
				message = "Not set parent." +
						  "\nFailed to create empty quotation order";
				return null;
			}

			if (!(parent is Aircraft) && !(parent is Operator) && !(parent is Store))
			{
				message = "Parent must be Aircraft or Store or Operator." +
						  "\nFailed to create empty quotation order";
				return null;
			}

			if (quotationList == null)
			{
				message = "Selected tasks not have a accessories." +
						  "\nFailed to create empty quotation order";
				return null;
			}

			var rqst = new RequestForQuotation
			{
				Description = "",
				Status = WorkPackageStatus.Opened,
				Author = _casEnvironment.Operators[0].Name,
				OpeningDate = DateTime.Today,
				PublishingDate = new DateTime(1852, 01, 01),
				ClosingDate = new DateTime(1852, 01, 01),
				Remarks = "",
				ParentType = parent.SmartCoreObjectType,
				ParentId = parent.ItemId,
				Title = parent + "-QO-" + DateTime.Now
			};
			_newKeeper.Save(rqst);

			#region ������������ ������� �������� ������
			foreach (var item in quotationList)
			{
				Product product = item.Key;
				double quantity = item.Value;

				var record =
					rqst.PackageRecords.FirstOrDefault(r => r.PackageItemType == product.SmartCoreObjectType &&
															r.PackageItemId == product.ItemId);
				if (record != null)
				{
					record.Quantity += item.Value;
				}
				else
				{
					record = new RequestForQuotationRecord(rqst.ItemId, product, quantity);
					rqst.PackageRecords.Add(record);
				}
			}
			#endregion

			#region ���������� �������� ������ � ��� �������

			foreach (var item in rqst.PackageRecords)
			{
				item.ParentPackageId = rqst.ItemId;
				_newKeeper.Save(item);
			}

			message = "Items added successfully";

			#endregion

			return rqst;
		}
		#endregion

		#region public RequestForQuotation AddQuotationOrder(IEnumerable<RequestForQuotationRecord> quotationList, BaseEntityObject parent, out string message)
		/// <summary>
		/// ���������� ���������� ������
		/// </summary>
		public RequestForQuotation AddQuotationOrder(IEnumerable<RequestForQuotationRecord> quotationList,
													 Supplier toSupplier,
													 BaseEntityObject parent,
													 out string message,
													 IORQORRelation[] iorqorRelations = null)
		{
			if (parent == null)
			{
				message = "Not set parent." +
						  "\nFailed to create empty quotation order";
				return null;
			}

			if (!(parent is Aircraft) && !(parent is Operator) && !(parent is Store))
			{
				message = "Parent must be Aircraft or Store or Operator." +
						  "\nFailed to create empty quotation order";
				return null;
			}

			if (quotationList == null)
			{
				message = "Selected tasks not have a accessories." +
						  "\nFailed to create empty quotation order";
				return null;
			}

			var rqst = new RequestForQuotation
			{
				Description = "",
				Status = WorkPackageStatus.Opened,
				Author = _casEnvironment.Operators[0].Name,
				OpeningDate = DateTime.Today,
				PublishingDate = new DateTime(1852, 01, 01),
				ClosingDate = new DateTime(1852, 01, 01),
				Remarks = "",
				ParentType = parent.SmartCoreObjectType,
				ParentId = parent.ItemId,
				Title = parent + "-QO-" + DateTime.Now,
				ToSupplier = toSupplier

			};
			_newKeeper.Save(rqst);

			#region ������������ ������� �������� ������
			foreach (var item in quotationList)
				rqst.PackageRecords.Add(item);
			#endregion

			#region ���������� �������� ������ � ��� �������

			foreach (var item in rqst.PackageRecords)
			{
				item.ParentPackageId = rqst.ItemId;

				_newKeeper.Save(item);

				var relation = iorqorRelations != null
					? iorqorRelations.FirstOrDefault(i => i.RequestForQuotationRecord == item)
					: null;

				if (relation != null)
					_newKeeper.Save(relation);
			}

			message = "Items added successfully";

			#endregion

			return rqst;
		}
		#endregion

		#region public bool AddToQuotationOrder(List<KeyValuePair<Product, double>> quotationItems, int quotationId, out string message)

		/// <summary>
		/// ���������� ��������� � ������������ �������� ��� �������������� ������� �����
		/// </summary>
		/// <param name="quotationItems">��������, ������� ���������� ��������</param>
		/// <param name="quotationId">ID ������, � ������� ���������� ��������</param>
		/// <param name="message">��������� � ������� ����������-��������� ��� �������� ������ ��� ����������</param>
		/// <return>true - ���� ���������� ������ ������� ��� false � ������ ������� </return>
		public bool AddToQuotationOrder(List<KeyValuePair<Product, double>> quotationItems, int quotationId, out string message)
		{
			if (quotationItems == null || /*parentAircraft == null ||*/ quotationId <= 0)
				throw new NullReferenceException("1504: NullReferenceException");

			#region �������� ��������� �������� ������

			var addTo = _loader.GetObject<RequestForQuotation>(quotationId, true, loadDeleted: true);
			if (addTo == null)
			{
				message = "Quotation Order with id: " + quotationId + " does not exist." +
				"\nFailed to add items to Quotation Order ";
				return false;
			}

			if (addTo.IsDeleted)
			{
				message = "Quotation Order : " + addTo.Title + " is deleted." +
						  "\nFailed to add items to deleted Quotation Order ";
				return false;
			}

			if (addTo.Status == WorkPackageStatus.Closed)
			{
				message = "Quotation Order : " + addTo.Title + " is Closed." +
						  "\nFailed to add items to closed Quotation Order ";
				return false;
			}

			#endregion

			#region �������� ���������� ��������� ��� ������������ �������� ������)

			if (!quotationItems.Any())
			{
				message = "Selected tasks not have a Items." +
						  "\nFailed to add items to Quotation Order";
				return false;
			}

			#endregion

			#region ������������ ������� �������� ������
			foreach (var item in quotationItems)
			{
				Product product = item.Key;
				double quantity = item.Value;

				var record =
					addTo.PackageRecords.FirstOrDefault(r => r.PackageItemType == product.SmartCoreObjectType &&
															 r.PackageItemId == product.ItemId);
				if (record != null)
				{
					record.Quantity += item.Value;
				}
				else
				{
					record = new RequestForQuotationRecord(quotationId, product, quantity);
					addTo.PackageRecords.Add(record);
				}
			}
			#endregion

			#region ���������� �������� ������ � ��� �������

			foreach (var item in addTo.PackageRecords)
			{
				item.ParentPackageId = addTo.ItemId;
				_newKeeper.Save(item);
			}

			message = "Items added successfully";

			#endregion

			return true;
		}
		#endregion

		#region public void Publish(RequestForQuotation rfq, DateTime date)
		/// <summary>
		/// ��������� ������� ����� - ������ ������� ����� �� ������
		/// </summary>
		/// <param name="rfq"></param>
		/// <param name="date"></param>
		public void Publish(RequestForQuotation rfq, DateTime date)
		{
			if (rfq.Status != WorkPackageStatus.Closed)
			{
				rfq.Status = WorkPackageStatus.Published;
				rfq.PublishingDate = date;
			}
			else
			{
				//CasEnvironment.Loader.LoadWorkPackageItems(wp);

				rfq.Status = WorkPackageStatus.Published;
				rfq.Remarks = "";
			}

			_newKeeper.Save(rfq);
		}
		#endregion

		#region public void Close(RequestForQuotation rfq, DateTime date, string remarks)

		/// <summary>
		/// ������������ ���������� �������� ������
		/// </summary>
		/// <param name="rfq"></param>
		/// <param name="date"></param>
		/// <param name="remarks"></param>
		public void Close(RequestForQuotation rfq, DateTime date, string remarks)
		{
			rfq.Status = WorkPackageStatus.Closed;
			rfq.ClosingDate = date;
			rfq.Remarks = remarks;

			_newKeeper.Save(rfq);
		}

		#endregion

		#region public void Close(PurchaseOrder rfq, DateTime date, string remarks)

		/// <summary>
		/// ������������ ���������� �������� ������
		/// </summary>
		/// <param name="rfq"></param>
		/// <param name="date"></param>
		/// <param name="remarks"></param>
		public void Close(PurchaseOrder rfq, DateTime date, string remarks)
		{
			rfq.Status = WorkPackageStatus.Closed;
			rfq.ClosingDate = date;
			rfq.Remarks = remarks;

			_newKeeper.Save(rfq);
		}

		#endregion

		#region public void DeleteFromRequestForQuotation(Product accessory, RequestForQuotation rfq)
		/// <summary>
		/// ������� ������ �� �������� ������
		/// </summary>
		/// <param name="accessory"></param>
		/// <param name="rfq"></param>
		public void DeleteFromRequestForQuotation(Product accessory, RequestForQuotation rfq)
		{
			var rfqRecord =
			   rfq.PackageRecords.FirstOrDefault(wpr => wpr.PackageItemId == accessory.ItemId &&
														wpr.PackageItemType == accessory.SmartCoreObjectType);
			if (rfqRecord == null)
			{
				rfqRecord = _newLoader.GetObject<RequestForQuotationRecordDTO, RequestForQuotationRecord>(new List<Filter>()
				{
					new Filter("PackageItemId",accessory.ItemId),
					new Filter("PackageItemTypeId",accessory.SmartCoreObjectType.ItemId),
					new Filter("ParentPackageId",rfq.ItemId)
				});
			}

			if (rfqRecord != null)
				_newKeeper.Delete(rfqRecord);
		}
		#endregion

	}
}
