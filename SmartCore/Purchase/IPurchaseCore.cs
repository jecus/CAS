using System;
using System.Collections.Generic;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Interfaces;

namespace SmartCore.Purchase
{
	public interface IPurchaseCore
	{

		IList<Product> GetProducts(int kitId, bool loadChild = false);
		/// <summary>
		/// ���������� ��� KIT -�, ����������� � ������������ ������ �������
		/// </summary>
		/// <returns></returns>
		List<Product> GetProducts(PurchaseOrder po);
		List<Product> GetProducts();
		List<Product> GetProducts(Supplier supplier);
		/// <summary>
		/// ���������� ���������� ������ ���������� �����
		/// </summary>
		/// <param name="aircraft">��������� �����. ��� �������� null ������ ��� ���������� ������</param>
		/// <param name="status">������ ������� ���������� ������. (�� ��������� = WorkPackageStatus.All)</param>
		/// <param name="loadWorkPackageItems">���� �������� ��������� ����������� ������</param>
		/// <param name="includedAccessory">������, ������� ������ ��������� ������ (��� �������� ������ ��������� ������ ������ 0 ����������� ������)</param>
		/// <returns></returns>
		IList<PurchaseOrder> GetPurchaseOrders(Aircraft aircraft, WorkPackageStatus status = WorkPackageStatus.All, bool loadWorkPackageItems = false, IEnumerable<AbstractAccessory> includedAccessory = null);
		/// <summary>
		/// ���������� ������������ ������ ���������� �����
		/// </summary>
		/// <param name="parent">�������� ������������� �������. ��� �������� null ������ ��� ������������ ������</param>
		/// <param name="statuses">������ ������� ������������ ������. (�� ��������� = WorkPackageStatus.All)</param>
		/// <param name="loadWorkPackageItems">���� �������� ��������� ������������� ������</param>
		/// <param name="includedAccessory">������, ������� ������ ��������� ������ (��� �������� ������ ��������� ������ ������ 0 ������������� ������)</param>
		/// <returns></returns>
		IList<PurchaseOrder> GetPurchaseOrders(BaseEntityObject parent, WorkPackageStatus[] statuses = null, bool loadWorkPackageItems = false, Product[] includedAccessory = null);
		/// <summary>
		/// ���������� ������������ ������ ���������� �����
		/// </summary>
		/// <param name="parent">�������� ������������� �������. ��� �������� null ������ ��� ������������ ������</param>
		/// <param name="statuses">������ ������� ������������ ������. (�� ��������� = WorkPackageStatus.All)</param>
		/// <param name="loadWorkPackageItems">���� �������� ��������� ������������� ������</param>
		/// <param name="includedAccessory">������, ������� ������ ��������� ������ (��� �������� ������ ��������� ������ ������ 0 ������������� ������)</param>
		/// <returns></returns>
		IList<RequestForQuotation> GetRequestForQuotation(BaseEntityObject parent, WorkPackageStatus[] statuses = null, bool loadWorkPackageItems = false, Product[] includedAccessory = null);
		/// <summary>
		/// ���������� ������������ ������ ���������� �����
		/// </summary>
		/// <param name="parent">�������� ������������� �������. ��� �������� null ������ ��� ������������ ������</param>
		/// <param name="statuses">������ ������� ������������ ������. (�� ��������� = WorkPackageStatus.All)</param>
		/// <param name="loadWorkPackageItems">���� �������� ��������� ������������� ������</param>
		/// <param name="includedAccessory">������, ������� ������ ��������� ������ (��� �������� ������ ��������� ������ ������ 0 ������������� ������)</param>
		/// <returns></returns>
		IList<InitialOrder> GetInitialOrders(BaseEntityObject parent, WorkPackageStatus[] statuses = null, bool loadWorkPackageItems = false, Product[] includedAccessory = null);
		/// <summary>
		/// ��������� ��� �������� �������� ������
		/// </summary>
		/// <param name="rfq"></param>
		void LoadRequestForQuotationItems(RequestForQuotation rfq);
		/// <summary>
		/// ��������� ��� �������� �������� ������
		/// </summary>
		/// <param name="po"></param>
		void LoadPurchaseOrderItems(PurchaseOrder po);
		/// <summary>
		/// ��������� ��� �������� �������� ������
		/// </summary>
		/// <param name="rfq"></param>
		void LoadInitionalOrderItems(InitialOrder rfq);
		RequestForQuotation GetRequestForQuotation(int requestForQuotationId);
		/// <summary>
		/// ��������� ���������� ���
		/// </summary>
		/// <param name="po"></param>
		/// <param name="date"></param>
		void Publish(PurchaseOrder po, DateTime date);

		InitialOrder AddInitialOrder(IEnumerable<KeyValuePair<Product, double>> quotationList,
									 BaseEntityObject parent, DateTime effDate,
									 DeferredCategory category, out string message);

		InitialOrder AddInitialOrder(IEnumerable<InitialOrderRecord> initialList,
									 BaseEntityObject parent, out string message);

		void GetInitialOrderItemsWithCalculate(InitialOrder initialOrder);

		RequestForQuotation AddQuotationOrder(IEnumerable<KeyValuePair<Product, double>> quotationList, BaseEntityObject parent, out string message);

		RequestForQuotation AddQuotationOrder(IEnumerable<RequestForQuotationRecord> quotationList,
										      Supplier toSupplier, BaseEntityObject parent,
											  out string message, IORQORRelation[] iorqorRelations = null);

		bool AddToQuotationOrder(List<KeyValuePair<Product, double>> quotationItems, int quotationId, out string message);

		void Publish(RequestForQuotation rfq, DateTime date);

		void Close(RequestForQuotation rfq, DateTime date, string remarks);

		void Close(PurchaseOrder rfq, DateTime date, string remarks);

		void DeleteFromRequestForQuotation(Product accessory, RequestForQuotation rfq);
	}
}