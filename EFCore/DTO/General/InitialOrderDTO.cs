﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EFCore.Attributte;

namespace EFCore.DTO.General
{
	[DataContract(IsReference = true)]
	[Condition("IsDeleted", 0)]
	public class InitialOrderDTO : BaseEntity
	{
		[DataMember]
		public string Title { get; set; }

		[DataMember]
		public int PublishedById { get; set; }

		[DataMember]
		public int ClosedById { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public string Author { get; set; }

		[DataMember]
		public int? ParentId { get; set; }

		[DataMember]
		public int? ParentTypeId { get; set; }

		[DataMember]
		public short? Status { get; set; }

		[DataMember]
		public DateTime? OpeningDate { get; set; }

		[DataMember]
		public DateTime? PublishingDate { get; set; }

		[DataMember]
		public DateTime? ClosingDate { get; set; }

		[DataMember]
		public string Remarks { get; set; }

		[DataMember]
		public string PublishedByUser { get; set; }

		[DataMember]
		public string CloseByUser { get; set; }


		//[DataMember]
		//[Include]
		//public SpecialistDTO PublishedBy { get; set; }

		//[DataMember]
		//[Include]
		//public SpecialistDTO ClosedBy { get; set; }


		[DataMember]
		[Child(FilterType.Equal, "ParentTypeId", 1560)]
		public ICollection<ItemFileLinkDTO> Files { get; set; }
		
		[DataMember]
		[Child]
		public ICollection<InitialOrderRecordDTO> PackageRecords { get; set; }
	}
}