﻿using System.ServiceModel;
using EFCore.DTO.Dictionaries;
using EFCore.Repository;

namespace EFCore.Contract.Dictionaries
{
	[ServiceContract]
	public interface IServiceTypeService : IRepository<ServiceTypeDTO>
	{
		
	}
}