﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CASReports.Datasets;
using CASReports.ReportTemplates;
using CASTerms;
using SmartCore.Entities.General;
using SmartCore.Filters;

namespace CASReports.Builders
{
	public class WorkScheduleReportBuilder : AbstractReportBuilder
	{
		#region Fields

		private Operator _reportedOperator;

		private string _reportTitle = "WORK SCHEDULE";
		private byte[] _operatorLogotype;
		private string _filterSelection;
		private string _dateAsOf = "";

		private IEnumerable<Document> _reportedDocuments;

		#endregion

		#region public CommonFilterCollection FilterSelection

		/// <summary>
		/// фильтры отчета
		/// </summary>
		public CommonFilterCollection FilterSelection
		{
			set { GetFilterSelection(value); }
		}

		#endregion

		#region Constructor

		public WorkScheduleReportBuilder(Operator @operator, IEnumerable<Document> documents)
		{
			_reportedOperator = @operator;

			if (_reportedOperator != null)
				_operatorLogotype = _reportedOperator.LogotypeReportLarge;

			_reportedDocuments = documents;
		}

		#endregion

		#region private void GetFilterSelection(CommonFilterCollection filterCollection)

		private void GetFilterSelection(CommonFilterCollection filterCollection)
		{
			_filterSelection = "All";
			if (filterCollection == null || filterCollection.IsEmpty)
				return;
			_filterSelection = filterCollection.ToStrings();
		}

		#endregion

		#region public override object GenerateReport()

		public override object GenerateReport()
		{
			var report = new WorkScheduleReport();
			report.SetDataSource(GenerateDataSet());
			return report;
		}

		#endregion

		#region protected virtual DataSet GenerateDataSet()

		protected virtual DataSet GenerateDataSet()
		{
			var dataset = new DocumentDataSet();
			AddAdditionalDataToDataSet(dataset);
			AddDocumentsToDataSet(dataset);
			return dataset;
		}

		#endregion

		#region private void AddAdditionalDataToDataSet(MaintenancePlanDataSet destinationDateSet)

		/// <summary>
		/// Добавление дополнительной информации 
		/// </summary>
		/// <param name="destinationDateSet"></param>
		private void AddAdditionalDataToDataSet(DocumentDataSet destinationDateSet)
		{
			var reportFooter = new GlobalTermsProvider()["ReportFooter"].ToString();
			var reportFooterPrepared = new GlobalTermsProvider()["ReportFooterPrepared"].ToString();
			var reportFooterLink = new GlobalTermsProvider()["ProductWebsite"].ToString();
			_dateAsOf = DateTime.Today.ToString("dd MM yyyy");
			destinationDateSet.AdditionalDataTAble.AddAdditionalDataTAbleRow(_reportTitle, _operatorLogotype, _filterSelection, _dateAsOf, reportFooter, reportFooterPrepared, reportFooterLink, _reportedDocuments.Count());
		}

		#endregion

		#region private void AddDocumentsToDataSet(DocumentDataSet destinationDateSet)

		private void AddDocumentsToDataSet(DocumentDataSet destinationDateSet)
		{
			foreach (var reportedDocument in _reportedDocuments)
			{
				var title = reportedDocument.DocumentSubType.ToString();
				var contractNumber = reportedDocument.ContractNumber;
				var description = reportedDocument.Description;
				var remarks = reportedDocument.Remarks;
				var nomenclature = reportedDocument.Nomenсlature.ToString();
				var issueValidFrom = reportedDocument.IssueDateValidFrom.ToString("dd MM yyyy");
				var docType = reportedDocument.DocType.ToString();
				var department = reportedDocument.Department.ToString();
				var contractor = reportedDocument.Supplier.ToString();
				var aboard = reportedDocument.Aboard ? "Yes" : "No";
				var status = reportedDocument.Status.ToString();
				var issueValidTo = reportedDocument.IssueValidTo ? reportedDocument.IssueDateValidTo.ToString("dd MM yyyy") : "";
				var issueRemain = reportedDocument.Remains?.ToString() ?? "";
				var revisonValidFrom = reportedDocument.Revision ? reportedDocument.RevisionDateFrom.ToString("dd MM yyyy") : "";
				var revisonValidTo = reportedDocument.RevisionValidTo ? reportedDocument.RevisionDateValidTo.ToString("dd MM yyyy") : "";
				var revisonRemain = reportedDocument.RevisionRemains?.ToString() ?? "";
				var prolongation = reportedDocument.ProlongationWay.ToString();
				var responsible = reportedDocument.ResponsibleOccupation.ToString();
				var specialist = reportedDocument.Specialist != null ? reportedDocument.Specialist.ToString() : reportedDocument.Parent.ToString();

				destinationDateSet.DocumentDataTable.AddDocumentDataTableRow(title, contractNumber, description,
					remarks, nomenclature, issueValidFrom, docType, department, contractor, aboard, status,
					issueValidTo, issueRemain, revisonValidFrom, revisonValidTo, revisonRemain, prolongation, responsible, specialist);
			}
		}

		#endregion
	}
}