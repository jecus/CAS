﻿using System.Collections.Generic;
using SmartCore.Entities.General.Interfaces;
using SmartCore.Entities.General.MTOP;

namespace SmartCore.Calculations.MTOP
{
	public interface IMtopCalc : IDirective
	{
		Lifelength PhaseThresh { get; set; }
		Lifelength PhaseRepeat { get; set; }
		Phase MTOPPhase { get; set; }

		bool RecalculateTenPercent { get; set; }
		bool APUCalc { get; set; } //MPD-Only
		int ParentAircraftId { get; }

		//Для пересчета
		bool IsExtension { get; set; } 
		double Extension { get; set; }


		List<NextPerformance> MtopNextPerformances { get; set; }
	}
}