﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityCore.Attributte;

namespace EntityCore.DTO.General
{
	[Table("MTOPCheck", Schema = "dbo")]
	
	[Condition("IsDeleted", 0)]
	public class MTOPCheckDTO : BaseEntity
	{
		
		[Column("Name"), MaxLength(150)]
		public string Name { get; set; }

		
		[Column("ParentAircraftId")]
		public int ParentAircraftId { get; set; }

		
		[Column("CheckTypeId")]
		public int? CheckTypeId { get; set; }

		
		[Column("Thresh")]
		public byte[] Thresh { get; set; }

		
		[Column("Repeat")]
		public byte[] Repeat { get; set; }

		
		[Column("Notify")]
		public byte[] Notify { get; set; }

		
		[Column("IsZeroPhase")]
		public bool IsZeroPhase { get; set; }

		
		[Include]
		public MaintenanceCheckTypeDTO CheckType { get; set; }

		
		[Child]
		public ICollection<MTOPCheckRecordDTO> PerformanceRecords { get; set; }
	}
}