﻿using System;
using System.Reflection;
using SmartCore.Entities.General.Accessory;
using SmartCore.Entities.General.Attributes;

namespace SmartCore.Entities.Dictionaries
{

    /// <summary>
    /// модель агрегата/ВС/и т.д.
    /// </summary>
    [Serializable]
    public abstract class AbstractModel : Product// AbstractDictionary
    {
		private static Type _thisType;

		
		#region Implement of Dictionary

		#region public string FullName { get; set; }

		private string _fullName;
        /// <summary>
        /// Полное название серии модели самолета Н:Boeing 737
        /// </summary>
        [TableColumn("FullName")]
        [FormControl(150, "Full Name", 1, Order = 5)]
        //[ListViewData(0.2f, "Full Name", 4)]
        [NotNull]
        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }

		public static PropertyInfo FullNameProperty
		{
			get { return GetCurrentType().GetProperty("FullName"); }
		}

		#endregion

		#region public string ShortName { get; set; }

		private string _shortName;
        /// <summary>
        /// Сокращенное название модели самолета
        /// </summary>
        [TableColumn("ShortName")]
        [FormControl(150, "Short Name", Order =6)]
        //[ListViewData(0.08f, "Short Name",5)]
        [NotNull]
        public string ShortName
        {
            get { return _shortName; }
            set
            {
                if (_shortName != value)
                {
                    _shortName = value;
                    OnPropertyChanged("ShortName");
                }
            }
        }

        #endregion

       #region public string Category
        private string _series;
        /// <summary>
        /// название модели в серии самолета Н:500
        /// </summary>
        [TableColumn("SubModel")]
        [FormControl(150, "Series", 1, Order = 2)]
        [ListViewData(0.08f, "Series")]
        public string Series
        {
            get { return _series; }
            set
            {
                if (_series != value)
                {
                    _series = value;
                    OnPropertyChanged("Category");
                }
            }
        }
        #endregion

        #endregion

        #region  public string Designer { get; set; }

        private string _designer;
        /// <summary>
        /// Конструктор самолета
        /// </summary>
        [TableColumn("Designer")]
        [FormControl(150, "Designer", 1, Order = 7)]
        //[ListViewData(0.1f, "Designer")]
        [NotNull]
        public string Designer
        {
            get { return _designer; }
            set
            {
                if (_designer != value)
                {
                    _designer = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        #endregion

        
        #region public SmartCoreType ModelingObjectType

        /// <summary>
        /// Тип родительской задачи
        /// </summary>
        public abstract SmartCoreType ModelingObjectType { get; }
        #endregion

        #region public ManufactureRegion ManufactureReg

        private ManufactureRegion _manufactureReg;
        /// <summary>
        /// Регион производства самолета
        /// </summary>
        [TableColumn("ModelingObjectSubTypeId")]
        [FormControl(150, "Manufacture Region", Order = 9)]
        //[ListViewData(0.1f, "Manufacture Region")]
        [NotNull]
        public ManufactureRegion ManufactureReg
        {
            get { return _manufactureReg; }
            set
            {
                if (_manufactureReg != value)
                {
                    _manufactureReg = value;
                    OnPropertyChanged("ManufactureReg");
                }
            }
        }
        #endregion



       #region public override string ToString()
        /// <summary>
        /// Переводит тип директивы в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name + (_series.Trim() != "" ? "-" + _series : "");
        }
        #endregion

        /*
         * Реализация
         */
        #region public AbstractModel()
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public AbstractModel()
        {
            _fullName = "";
            _shortName = "";
            _series = "";
            _name = "";
            _manufactureReg = ManufactureRegion.Unknown;
            _manufacturer = "";
            _designer = "";
        }
        #endregion

        #region public AbstractModel(string manufacturer, String shortName, String fullName, AircraftManufactureRegion region)

        /// <summary>
        /// Конструктор создает объект типа директивы
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="shortName"></param>
        /// <param name="fullName"></param>
        /// <param name="region"></param>
        public AbstractModel(string manufacturer, String shortName, String fullName, ManufactureRegion region)
        {
            _manufacturer = manufacturer;
            _shortName = shortName;
            _fullName = fullName;
            _manufactureReg = region;
        }
        #endregion

        #region public override int CompareTo(object y)
        public override int CompareTo(object y)
        {
            if (y is AbstractModel)
                return FullName.CompareTo(((AbstractModel)y).FullName);
            return 0;
        }
		#endregion

		#region private static Type GetCurrentType()
		private static Type GetCurrentType()
		{
			return _thisType ?? (_thisType = typeof(AbstractModel));
		}
		#endregion
	}
}
