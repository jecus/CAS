﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Auxiliary;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;
using CAS.UI.UIControls.Auxiliary.Comparers;
using CAS.UI.UIControls.ComponentControls;
using CAS.UI.UIControls.NewGrid;
using CAS.UI.UIControls.StoresControls;
using CASTerms;
using SmartCore.Auxiliary;
using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Interfaces;
using Telerik.WinControls.UI;
using TempUIExtentions;
using Component = SmartCore.Entities.General.Accessory.Component;
using Convert = System.Convert;

namespace CAS.UI.UIControls.Fleet
{
	///<summary>
	/// список для отображения ордеров запроса
	///</summary>
	public partial class ComponentsFleetListView : BaseGridViewControl<BaseEntityObject>
	{

		#region Constructors

		#region private DetailsListView()
		///<summary>
		///</summary>
		public ComponentsFleetListView()
		{
			InitializeComponent();
			OldColumnIndex = 0;
			EnableCustomSorting = false;

			ColumnHeaderList.Clear();
			SetHeaders();
			radGridView1.Columns.Clear();
			radGridView1.Columns.AddRange(ColumnHeaderList.ToArray());
		}

		#endregion



		#endregion

		#region Methods

		#region protected override void SetHeaders()
		/// <summary>
		/// Устанавливает заголовки
		/// </summary>
		protected override void SetHeaders()
		{
			AddColumn("Aircraft", (int)(radGridView1.Width * 0.2f));
			AddColumn("ATA", (int)(radGridView1.Width * 0.2f));
			AddColumn("Part. No", (int)(radGridView1.Width * 0.2f));
			AddColumn("Description", (int)(radGridView1.Width * 0.3f));
			AddColumn("Type", (int)(radGridView1.Width * 0.2f));
			AddColumn("Work Type", (int)(radGridView1.Width * 0.14f));
			AddColumn("Serial No", (int)(radGridView1.Width * 0.2f));
			AddColumn("MPD Item", (int)(radGridView1.Width * 0.2f));
			AddColumn("Task Card №", (int)(radGridView1.Width * 0.2f));
			AddColumn("Pos. No", (int)(radGridView1.Width * 0.2f));
			AddColumn("M.P.", (int)(radGridView1.Width * 0.14f));
			AddColumn("Zone-Area", (int)(radGridView1.Width * 0.14f));
			AddColumn("Access", (int)(radGridView1.Width * 0.14f));
			AddColumn("Inst. date", (int)(radGridView1.Width * 0.2f));
			AddColumn("IDD", (int)(radGridView1.Width * 0.2f));
			AddColumn("IDDC", (int)(radGridView1.Width * 0.2f));
			AddColumn("Life limit/1st. Perf", (int)(radGridView1.Width * 0.2f));
			AddColumn("Rpt. int.", (int)(radGridView1.Width * 0.2f));
			AddColumn("Next(E)", (int)(radGridView1.Width * 0.15f));
			AddColumn("Next Estimated Data", (int)(radGridView1.Width * 0.2f));
			AddColumn("Next Estimated Data(C)", (int)(radGridView1.Width * 0.2f));
			AddColumn("Remain(E)", (int)(radGridView1.Width * 0.2f));
			AddColumn("Next(L)", (int)(radGridView1.Width * 0.15f));
			AddColumn("Next Limit Data", (int)(radGridView1.Width * 0.2f));
			AddColumn("Next Limit Data(C)", (int)(radGridView1.Width * 0.2f));
			AddColumn("Remain(L)", (int)(radGridView1.Width * 0.24f));
			AddColumn("Remain(L)(С)", (int)(radGridView1.Width * 0.24f));
			AddColumn("Last", (int)(radGridView1.Width * 0.15f));
			AddColumn("Last Data", (int)(radGridView1.Width * 0.2f));
			AddColumn("Last Data(C)", (int)(radGridView1.Width * 0.2f));
			AddColumn("Expiry Date", (int)(radGridView1.Width * 0.24f));
			AddColumn("Expiry Remain", (int)(radGridView1.Width * 0.24f));
			AddColumn("Warranty", (int)(radGridView1.Width * 0.2f));
			AddColumn("Class", (int)(radGridView1.Width * 0.2f));
			AddColumn("Kit", (int)(radGridView1.Width * 0.2f));
			AddColumn("NDT", (int)(radGridView1.Width * 0.24f));
			AddColumn("M.H.", (int)(radGridView1.Width * 0.2f));
			AddColumn("Cost(new)", (int)(radGridView1.Width * 0.2f));
			AddColumn("Cost overhaul", (int)(radGridView1.Width * 0.2f));
			AddColumn("Cost serviceable", (int)(radGridView1.Width * 0.2f));
			AddColumn("Remarks", (int)(radGridView1.Width * 0.2f));
			AddColumn("Hidden Remarks", (int)(radGridView1.Width * 0.24f));
			AddColumn("Signer", (int)(radGridView1.Width * 0.3f));
		}
		#endregion

		#region protected override SetGroupsToItems(int columnIndex)
		protected override void GroupingItems()
		{
			Grouping("Aircraft");
		}

		#endregion

		#region protected override void SetItemColor(GridViewRowInfo listViewItem, BaseEntityObject item)

		protected override void SetItemColor(GridViewRowInfo listViewItem, BaseEntityObject item)
		{
			if (item is ComponentDirective)
			{
				foreach (GridViewCellInfo cell in listViewItem.Cells)
				{
					cell.Style.CustomizeFill = true;
					cell.Style.ForeColor = Color.Gray;
					cell.Style.BackColor = UsefulMethods.GetColor(item);
				}
				
			}
			if (item is Component)
			{
				foreach (GridViewCellInfo cell in listViewItem.Cells)
				{
					cell.Style.CustomizeFill = true;
					cell.Style.ForeColor = Color.Black;
					cell.Style.BackColor = UsefulMethods.GetColor(item);
				}
			}
		}

		#endregion

		#region protected override ListViewItem.ListViewSubItem[] GetListViewSubItems(BaseSmartCoreObject item)

		protected override List<CustomCell> GetListViewSubItems(BaseEntityObject item)
		{
			var subItems = new List<CustomCell>();
			var author = GlobalObjects.CasEnvironment.GetCorrector(item);

			DateTime? approx;
			Lifelength remains = Lifelength.Null, next;
			AtaChapter ata;
			MaintenanceControlProcess maintenanceType;
			DateTime transferDate;
			DateTime? lastPerformanceDate = null;
			DateTime? nextEstimated = null;
			DateTime? nextLimit = null;
			Lifelength firstPerformance = Lifelength.Null,
					   lastPerformance = Lifelength.Null,
					   lastPerformanceC = Lifelength.Null,
					   expiryRemain = Lifelength.Null,
					   nextEstimatedData = Lifelength.Null,
					   nextEstimatedDataC = Lifelength.Null,
					   remainEstimated = Lifelength.Null,
					   nextLimitData = Lifelength.Null,
					   nextLimitDataC = Lifelength.Null,
					   remainLimit = Lifelength.Null,
					   remainLimitC = Lifelength.Null,
					   IDD = Lifelength.Null,
					   IDDC = Lifelength.Null,
					   warranty, repeatInterval = Lifelength.Null;
			string partNumber,
				   description,
				   serialNumber,
				   position,
				   mpdString = "",
				   mpdNumString = "",
				   lastPerformanceString = "",
				   type = getGroupName(item),
				   classString = "",
				   kitRequieredString,
				   remarks,
				   hiddenRemarks,
				   workType = "",
				   zone = "",
				   destination = "",
				   access = "",
				   expiryDate = "",
				   condition = "",
				   conditionRepeat = "",
				   ndtString = "";
			double manHours,
				   cost,
				   costServiceable = 0,
				   costOverhaul = 0;
			if (item is Component)
			{
				Component componentItem = (Component)item;
				approx = componentItem.NextPerformanceDate;
				next = componentItem.NextPerformanceSource;

				destination = GlobalObjects.AircraftsCore.GetAircraftById(componentItem.ParentBaseComponent?.ParentAircraftId ?? -1)?.ToString();

				var selectedCategory = componentItem.ChangeLLPCategoryRecords.GetLast()?.ToCategory;
					if (selectedCategory != null)
					{
						var llp = componentItem.LLPData.GetItemByCatagory(selectedCategory);
						remains = llp?.Remain;
					}

				if (componentItem.LLPCategories)
				{
					nextEstimated = componentItem.NextPerformance?.PerformanceDate;
					nextEstimatedData = componentItem.NextPerformance?.PerformanceSource;
					nextEstimatedDataC = componentItem.NextPerformance?.PerformanceSourceC;
					remainEstimated = componentItem.NextPerformance?.Remains;

					nextLimit = componentItem.NextPerformance?.NextPerformanceDateNew;
					nextLimitData = componentItem.NextPerformance?.NextLimit;
					nextLimitDataC = componentItem.NextPerformance?.NextLimitC;
					remainLimit = componentItem.NextPerformance?.RemainLimit;
					remainLimitC = componentItem.NextPerformance?.RemainLimitC;

					IDD = componentItem.NextPerformance?.IDD;
					IDDC = componentItem.NextPerformance?.IDDC;
				}


				ata = componentItem.Model != null ? componentItem.Model.ATAChapter : componentItem.ATAChapter;
				partNumber = componentItem.PartNumber;
				description = componentItem.Model != null ? componentItem.Model.Description : componentItem.Description;
				serialNumber = componentItem.SerialNumber;
				position = componentItem.TransferRecords.GetLast().Position.ToUpper();
				maintenanceType = componentItem.MaintenanceControlProcess;
				transferDate = componentItem.TransferRecords.GetLast().TransferDate;
				firstPerformance = componentItem.LifeLimit;
				warranty = componentItem.Warranty;
				classString = componentItem.GoodsClass != null ? componentItem.GoodsClass.ToString() : "";
				kitRequieredString = componentItem.Kits.Count + " kits";
				manHours = componentItem.ManHours;
				cost = componentItem.Cost;
				costOverhaul = componentItem.CostOverhaul;
				costServiceable = componentItem.CostServiceable;
				remarks = componentItem.Remarks;
				hiddenRemarks = componentItem.HiddenRemarks;
				expiryDate = " ";
				expiryRemain = Lifelength.Null;
				condition = !firstPerformance.IsNullOrZero() ? (componentItem.Threshold.FirstPerformanceConditionType == ThresholdConditionType.WhicheverFirst
					? "/WF"
					: "/WL") : "";
				conditionRepeat = !componentItem.Threshold.RepeatInterval.IsNullOrZero() ? (componentItem.Threshold.RepeatPerformanceConditionType == ThresholdConditionType.WhicheverFirst
					? "/WF"
					: "/WL") : "";
			}
			else
			{
				ComponentDirective dd = (ComponentDirective)item;

				destination = GlobalObjects.AircraftsCore.GetAircraftById(dd.ParentComponent.ParentBaseComponent?.ParentAircraftId ?? -1)?.ToString();

				if (dd.Threshold.FirstPerformanceSinceNew != null && !dd.Threshold.FirstPerformanceSinceNew.IsNullOrZero())
				{
					firstPerformance = dd.Threshold.FirstPerformanceSinceNew;
				}

				if (dd.LastPerformance != null)
				{
					lastPerformanceString = SmartCore.Auxiliary.Convert.GetDateFormat(dd.LastPerformance.RecordDate);
					lastPerformance = dd.LastPerformance?.OnLifelength;
					lastPerformanceC = dd.NextPerformance?.LastDataC;
					lastPerformanceDate = dd.LastPerformance?.RecordDate;
				}
				if (dd.Threshold.RepeatInterval != null && !dd.Threshold.RepeatInterval.IsNullOrZero())
				{
					repeatInterval = dd.Threshold.RepeatInterval;
				}
				//GlobalObjects.CasEnvironment.Calculator.GetNextPerformance(dd, out next, out remains, out approx, out cond);
				//GlobalObjects.CasEnvironment.Calculator.GetNextPerformance(dd);
				approx = dd.NextPerformanceDate;
				next = dd.NextPerformanceSource;
				remains = dd.Remains;


				nextEstimated = dd.NextPerformance?.PerformanceDate;
				nextEstimatedData = dd.NextPerformance?.PerformanceSource;
				nextEstimatedDataC = dd.NextPerformance?.PerformanceSourceC;
				remainEstimated = dd.NextPerformance?.Remains;

				nextLimit = dd.NextPerformance?.NextPerformanceDateNew;
				nextLimitData = dd.NextPerformance?.NextLimit;
				nextLimitDataC = dd.NextPerformance?.NextLimitC;
				remainLimit = dd.NextPerformance?.RemainLimit;
				remainLimitC = dd.NextPerformance?.RemainLimitC;


				IDD = dd.NextPerformance?.IDD;
				IDDC = dd.NextPerformance?.IDDC;

				ata = dd.ParentComponent.Model != null ? dd.ParentComponent.Model.ATAChapter : dd.ParentComponent.ATAChapter;
				partNumber = "    " + dd.PartNumber;
				var desc = dd.ParentComponent.Model != null
					? dd.ParentComponent.Model.Description
					: dd.ParentComponent.Description;

				description = "    " + desc;
				serialNumber = "    " + dd.SerialNumber;
				position = "    " + dd.ParentComponent.TransferRecords.GetLast().Position.ToUpper();
				transferDate = dd.ParentComponent.TransferRecords.GetLast().TransferDate;
				maintenanceType = dd.ParentComponent.MaintenanceControlProcess;
				warranty = dd.Threshold.Warranty;
				classString = dd.ParentComponent.GoodsClass != null ? dd.ParentComponent.GoodsClass.ToString() : "";
				kitRequieredString = dd.Kits.Count + " kits";
				manHours = dd.ManHours;
				cost = dd.Cost;
				zone = dd.ZoneArea;
				access = dd.AccessDirective;
				remarks = dd.Remarks;
				hiddenRemarks = dd.HiddenRemarks;
				workType = dd.DirectiveType.ToString();
				ndtString = dd.NDTType.ShortName;
				condition = !firstPerformance.IsNullOrZero() ? (dd.Threshold.FirstPerformanceConditionType == ThresholdConditionType.WhicheverFirst
					? "/WF"
					: "/WL") : "";
				conditionRepeat = !dd.Threshold.RepeatInterval.IsNullOrZero() ? (dd.Threshold.RepeatPerformanceConditionType == ThresholdConditionType.WhicheverFirst
					? "/WF"
					: "/WL") : "";



				if (dd.IsExpiry)
				{
					expiryDate = dd.IsExpiry ? (dd.ExpiryDate.HasValue ? SmartCore.Auxiliary.Convert.GetDateFormat(dd.ExpiryDate.Value) : "") : "";
					expiryRemain = dd.IsExpiry ? new Lifelength((int)(dd.ExpiryDate.Value - DateTime.Today).TotalDays, 0, 0) : Lifelength.Null;
				}

				if (dd.MaintenanceDirective != null)
				{
					mpdString = dd.MaintenanceDirective.TaskNumberCheck;
					mpdNumString = dd.MaintenanceDirective.TaskCardNumber;
				}
			}

			subItems.Add(CreateRow(destination, destination));
			subItems.Add(CreateRow(ata.ToString(), ata));
			subItems.Add(CreateRow(partNumber, partNumber));
			subItems.Add(CreateRow(description, description));
			subItems.Add(CreateRow(type, type));
			subItems.Add(CreateRow(workType, workType));
			subItems.Add(CreateRow(serialNumber, serialNumber));
			subItems.Add(CreateRow(mpdString, mpdString));
			subItems.Add(CreateRow(mpdNumString, mpdNumString));
			subItems.Add(CreateRow(position, position));
			subItems.Add(CreateRow(maintenanceType.ShortName, maintenanceType));
			subItems.Add(CreateRow(zone, zone));
			subItems.Add(CreateRow(access, access));
			subItems.Add(CreateRow(transferDate > DateTimeExtend.GetCASMinDateTime()
				? SmartCore.Auxiliary.Convert.GetDateFormat(transferDate) : "", transferDate));
			subItems.Add(CreateRow(IDD?.ToString(), IDD));
			subItems.Add(CreateRow(IDDC?.ToString(), IDDC));
			subItems.Add(CreateRow($"{firstPerformance} {condition}", firstPerformance));
			subItems.Add(CreateRow($"{repeatInterval} {conditionRepeat}", repeatInterval));
			subItems.Add(CreateRow(SmartCore.Auxiliary.Convert.GetDateFormat(nextEstimated), nextEstimated));
			subItems.Add(CreateRow(nextEstimatedData?.ToString(), nextEstimatedData));
			subItems.Add(CreateRow(nextEstimatedDataC?.ToString(), nextEstimatedDataC));
			subItems.Add(CreateRow(remainEstimated?.ToString(), remainEstimated));
			subItems.Add(CreateRow(nextLimitData?.Days != null ? SmartCore.Auxiliary.Convert.GetDateFormat(nextLimit) : "", nextLimit));
			subItems.Add(CreateRow(nextLimitData?.ToString(), nextLimitData));
			subItems.Add(CreateRow(nextLimitDataC?.ToString(), nextLimitDataC));
			subItems.Add(CreateRow(remainLimit?.ToString(), remainLimit));
			subItems.Add(CreateRow(remainLimitC?.ToString(), remainLimitC));
			subItems.Add(CreateRow(lastPerformanceString, lastPerformanceDate));
			subItems.Add(CreateRow(lastPerformanceC?.ToString(), lastPerformanceC));
			subItems.Add(CreateRow(lastPerformance?.ToString(), lastPerformance));
			subItems.Add(CreateRow(expiryDate, expiryDate));
			subItems.Add(CreateRow(!expiryRemain.IsNullOrZero() ? $"{expiryRemain?.Days}d" : "", expiryRemain));
			subItems.Add(CreateRow(warranty.ToString(), warranty));
			subItems.Add(CreateRow(classString, classString));
			subItems.Add(CreateRow(kitRequieredString, kitRequieredString));
			subItems.Add(CreateRow(ndtString, ndtString));
			subItems.Add(CreateRow(manHours.ToString(), manHours));
			subItems.Add(CreateRow(cost.ToString(), cost));
			subItems.Add(CreateRow(costOverhaul.ToString(), costOverhaul));
			subItems.Add(CreateRow(costServiceable.ToString(), costServiceable));
			subItems.Add(CreateRow(remarks, remarks));
			subItems.Add(CreateRow(hiddenRemarks, hiddenRemarks));
			subItems.Add(CreateRow(author, author));

			return subItems;
		}

		#endregion

		#region protected override void CustomSort(int ColumnIndex)


		protected override void CustomSort(int ColumnIndex)
		{
			if (OldColumnIndex != ColumnIndex)
				SortDirection = SortDirection.Asc;
			if (SortDirection == SortDirection.Desc)
				SortDirection = SortDirection.Asc;
			else
				SortDirection = SortDirection.Desc;

			var resultList = new List<BaseEntityObject>();
			var list = radGridView1.Rows.Select(i => i).ToList();
			list.Sort(new GridViewDataRowInfoComparer(ColumnIndex, Convert.ToInt32(SortDirection)));
			//добавление остальных подзадач
			foreach (GridViewRowInfo item in list)
			{
				if (item.Tag is Component)
				{
					resultList.Add(item.Tag as BaseEntityObject);

					Component component = (Component) item.Tag;
					var items = list
						.Where(lvi =>
							lvi.Tag is ComponentDirective &&
							((ComponentDirective) lvi.Tag).ComponentId == component.ItemId).Select(i => i.Tag);
					resultList.AddRange(items.OfType<BaseEntityObject>());
				}
				else if (item.Tag is ComponentDirective)
				{
					ComponentDirective dd = item.Tag as ComponentDirective;
					Component d = dd.ParentComponent;
					if (d == null)
						resultList.Add(item.Tag as BaseEntityObject);
					else
					{
						var lvi =
							list.FirstOrDefault(lv => lv.Tag is Component && ((Component)lv.Tag).ItemId == d.ItemId);
						if (lvi == null)
							resultList.Add(item.Tag as BaseEntityObject);
					}
				}
			}


			SetItemsArray(resultList.ToArray());

		}

		#endregion

		#region protected override void FillDisplayerRequestedParams(ReferenceEventArgs e)

		protected override void FillDisplayerRequestedParams(ReferenceEventArgs e)
		{
			if (SelectedItem != null)
			{
				e.TypeOfReflection = ReflectionTypes.DisplayInNew;
				if (SelectedItem is Component)
				{
					Component d = (Component) SelectedItem;
					if (d.GoodsClass.IsNodeOrSubNodeOf(GoodsClass.MaintenanceMaterials) ||
						d.GoodsClass.IsNodeOrSubNodeOf(GoodsClass.Tools))
					{
						e.Cancel = true;
						ConsumablePartAndKitForm form = new ConsumablePartAndKitForm(d);
						form.ShowDialog();
					}
					else
					{
						var location = d.ParentAircraftId > 0
							? $"{d.GetParentAircraftRegNumber()}."
							: d.ParentOperator != null ? $"{d.ParentOperator.Name}." : ""; //TODO:(Evgenii Babak) заменить на использование OperatorCore 
						e.DisplayerText = location + " Component PN " + d.PartNumber;
						e.RequestedEntity = new ComponentScreenNew(d);
					}
				}
				else
				{
					var d = ((ComponentDirective)SelectedItem).ParentComponent;
					var location = d.ParentAircraftId > 0
							? $"{d.GetParentAircraftRegNumber()}."
							: d.ParentOperator != null ? $"{d.ParentOperator.Name}." : "";//TODO:(Evgenii Babak) заменить на использование OperatorCore 
					e.DisplayerText = location + " Component PN " + d.PartNumber;
					e.RequestedEntity = new ComponentScreenNew(d);
				} 
			}
		}
		#endregion


		private string getGroupName(BaseEntityObject entityObject)
		{
			var parent = (IDirective)entityObject;

			if (parent is ComponentDirective)
				parent = ((ComponentDirective)parent).ParentComponent;

			string groupName = "";
			if (parent is BaseComponent)
			{
				if (((BaseComponent)parent).BaseComponentType == BaseComponentType.Engine)
					groupName = "Engines";
				else if (((BaseComponent)parent).BaseComponentType == BaseComponentType.Apu)
					groupName = "APU";
				else if (((BaseComponent)parent).BaseComponentType == BaseComponentType.LandingGear)
					groupName = "Landing gears";
				else if (((BaseComponent)parent).BaseComponentType == BaseComponentType.Propeller)
					groupName = "Propellers";
				else if (((BaseComponent)parent).BaseComponentType == BaseComponentType.Frame)
					groupName = "Frames";
			}
			else if (parent is Component)
			{
				groupName = "Component";

				//Component component = (Component)parent;
				//if (_parentBaseComponent != null &&
				//   _parentBaseComponent.BaseComponentType == BaseComponentType.Engine) groupName = component.LLPMark ? "LLP Disk" : "Component";
				//else
				//{
				//	var ata = component.Model != null ? component.Model.ATAChapter : component.ATAChapter;
				//	groupName = ata.ShortName + " " + ata.FullName;
				//}
			}

			return groupName;

		}

		#endregion
	}
}
