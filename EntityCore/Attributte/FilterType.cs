﻿using System.ComponentModel;
using System.Runtime.Serialization;

namespace EntityCore.Attributte
{
	
	public enum FilterType
	{
		/// <summary>
		/// Находится в определенном множестве
		/// </summary>
		[Description("In")]
		[EnumMember]
		In = 1,
		/// <summary>
		/// Меньше 
		/// </summary>
		[Description("<")]
		[EnumMember]
		Less = 10,
		/// <summary>
		/// Меньше или равно
		/// </summary>
		[Description("<=")]
		[EnumMember]
		LessOrEqual = 11,
		/// <summary>
		/// Эквивалентно (равно)
		/// </summary>
		[Description("=")]
		[EnumMember]
		Equal = 12,
		/// <summary>
		/// Больше или равно
		/// </summary>
		[Description(">=")]
		[EnumMember]
		GratherOrEqual = 13,
		/// <summary>
		/// Больше
		/// </summary>
		[Description(">")]
		[EnumMember]
		Grather = 14,
		/// <summary>
		/// Не эквивалентно (не равно)
		/// </summary>
		[Description("!=")]
		[EnumMember]
		NotEqual = 15,
		/// <summary>
		/// Между 2-мя значениями
		/// </summary>
		[Description("Between")]
		[EnumMember]
		Between = 20,
		// <summary>
		/// Между 2-мя значениями
		/// </summary>
		[Description("Between")]
		[EnumMember]
		Contains = 21
	}
}