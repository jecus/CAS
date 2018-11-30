using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SmartCore.Calculations;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Attributes;
using SmartCore.Entities.General.Interfaces;

namespace SmartCore.Filters
{
    ///<summary>
    /// ����� ��� ������� �������� �� ��������� ��������
    ///</summary>
    public class CommonFilterCollection: IEnumerable<ICommonFilter>
    {

        #region Fields
        /// <summary>
        /// �������� ��������� ��������
        /// </summary>
        private BaseEntityObject[] _initialCollection;
        /// <summary>
        /// ������ �������� �������
        /// </summary>
        private List<ICommonFilter> Items = new List<ICommonFilter>();
        /// <summary>
        /// ��� ��� ����������
        /// </summary>
        private Type _filteredType;

        #endregion

        #region Constructors

        #region public CommonFilterCollection(Type filteredType)

        ///<summary>
        /// ��������� ������ ��� ������� ���������
        ///</summary>
        ///<param name="filteredType">��� ��� ����������</param>
        public CommonFilterCollection(Type filteredType)
        {
            _initialCollection = new BaseEntityObject[0];
            _filteredType = filteredType;
            FilterTypeAnd = true;

            if (_filteredType == null) 
                return;
            //����������� �������, ������� ������� "������������ � ������"
            List<PropertyInfo> properties =
                _filteredType.GetProperties().Where(p => p.GetCustomAttributes(typeof(FilterAttribute), false).Length != 0).ToList();

            //����� ������� � ������� ����� ������� �����������
            //��������, ������� ������� �����������
            Dictionary<int, PropertyInfo> orderedProperties = new Dictionary<int, PropertyInfo>();
            //��������, �� ������� ������� �����������
            List<PropertyInfo> unOrderedProperties = new List<PropertyInfo>();
            foreach (PropertyInfo propertyInfo in properties)
            {
                FilterAttribute lvda = (FilterAttribute)
                    propertyInfo.GetCustomAttributes(typeof(FilterAttribute), false).First();
                if (lvda.Order > 0) orderedProperties.Add(lvda.Order, propertyInfo);
                else unOrderedProperties.Add(propertyInfo);
            }

            var ordered = orderedProperties.OrderBy(p => p.Key).ToList();

            properties.Clear();
            properties.AddRange(ordered.Select(keyValuePair => keyValuePair.Value));
            properties.AddRange(unOrderedProperties);

            foreach (PropertyInfo property in properties)
            {
                Type propertyType = property.PropertyType;

                #region ������ ��� IEmunerable

                if (propertyType.Name.ToLower() != "string" && 
                    propertyType.GetInterface(typeof(IEnumerable<>).Name) != null)
                {
                    //���� �������� �� string (string ��������� ��������� IEnumerable<>)
                    //� ��������� ��������� IEnumerable<> ��
                    //������������ ����� ��������� �������������� ����
                    Type t = propertyType;

                    while (t != null)
                    {
                        if (t.IsGenericType)
                        {
                            propertyType = t.GetGenericArguments().FirstOrDefault();
                            break;
                        }
                        t = t.BaseType;
                    }
                }
                #endregion

                if(propertyType == null)
                    continue;

                #region ������ ��� StaticDictionary

                if (propertyType.IsSubclassOf(typeof(StaticDictionary)) ||
                    propertyType.IsSubclassOf(typeof(AbstractDictionary)))
                {
                    Type genericType = typeof(CommonFilter<>);
                    Type genericList = genericType.MakeGenericType(propertyType);

                    ICommonFilter commonFilter = (ICommonFilter)Activator.CreateInstance(genericList,new object[]{property, FilterType.Equal, null});

                    Items.Add(commonFilter);
                }
                #endregion

                #region ������ ��� Lifelength

                else if (propertyType.Name == typeof(Lifelength).Name)
                {
                    Items.Add(new CommonFilter<Lifelength>(property, FilterType.Equal, new Lifelength[0]));
                }
                #endregion

                #region ������ ��� BaseEntityObject

                else if (propertyType.GetInterface(typeof(IBaseEntityObject).Name) != null)
                {
                    Type genericType = typeof(CommonFilter<>);
                    Type genericList = genericType.MakeGenericType(propertyType);

                    ICommonFilter commonFilter = (ICommonFilter)Activator.CreateInstance(genericList, new object[] { property, FilterType.Equal, null });

                    Items.Add(commonFilter);
                }

                #endregion

                #region ������ ��� ENUM

                else if (propertyType.IsEnum)
                {
                    Type genericType = typeof(CommonFilter<>);
                    Type genericList = genericType.MakeGenericType(propertyType);

                    ICommonFilter commonFilter = (ICommonFilter)Activator.CreateInstance(genericList, new object[] { property, FilterType.Equal, null });

                    Items.Add(commonFilter);
                }

                #endregion

                #region ������ ��� ������� �����

                string typeName = propertyType.Name.ToLower();
                switch (typeName)
                {
                    case "bool":
                    case "boolean":
                        {
                            Items.Add(new CommonFilter<bool>(property, FilterType.Equal, new bool[0]));
                        }
                        break;
					case "int32":
					case "int":
                        {
                            Items.Add(new CommonFilter<int>(property, FilterType.Equal, new int[0]));
                        }
                        break;
                    case "string":
                        {
                            Items.Add(new CommonFilter<string>(property, FilterType.Equal, new string[0]));
                        }
                        break;
                    case "datetime":
                        {
                            Items.Add(new CommonFilter<DateTime>(property, FilterType.Between, new DateTime[0]));
                        }
                        break;
                }
                #endregion
            }
        }

        #endregion

        #region public CommonFilterCollection(IEnumerable<ICommonFilter> filters, bool filterTypeAnd = true)
        /// <summary>
        ///  ��������� ������ ��� ������� ���������
        /// </summary>
        /// <param name="filters">����������� ��� ������� �������</param>
        /// <param name="filterTypeAnd">������ �������� ������������� ��������
        /// <br/> true - ����������� ������� ������ ��������� ��� ��� �������, 
        /// <br/> false - ����������� ������� ������ ��������� ���� �� ��� ���� ������</param>
        public CommonFilterCollection(IEnumerable<ICommonFilter> filters, bool filterTypeAnd = true)
        {
            Items.AddRange(filters);
            FilterTypeAnd = filterTypeAnd;
        }

        #endregion

        #region public CommonFilterCollection(Type filteredType, IEnumerable<ICommonFilter> filters) : this(new BaseEntityObject[0], filteredType, filters)

        ///<summary>
        /// ��������� ������ ��� ������� ���������
        ///</summary>
        ///<param name="filteredType">��� ��� ����������</param>
        ///<param name="filters">����������� ��� ������� �������</param>
        public CommonFilterCollection(Type filteredType, IEnumerable<ICommonFilter> filters)
            : this(new BaseEntityObject[0], filteredType, filters)
        {
        }

        #endregion

        #region public CommonFilterCollection(BaseEntityObject[] initialCollection, Type filteredType, IEnumerable<ICommonFilter> filters)

        ///<summary>
        /// ��������� ������ ��� ������� ��������
        ///</summary>
        ///<param name="initialCollection">�������� ��������� ��������</param>
        ///<param name="filteredType">��� ��� ����������</param>
        ///<param name="filters">����������� ��� ������� �������</param>
        public CommonFilterCollection(BaseEntityObject[] initialCollection, Type filteredType, IEnumerable<ICommonFilter> filters)
        {
            _initialCollection = initialCollection;
            _filteredType = filteredType;
            
            Items.AddRange(filters);
        }

        #endregion

        #endregion

        #region Properties

        #region public ICommonFilter this[Int32 indexCollection]

        /// <summary>
        /// ���������� ������ �� �������� �� ��������� �������
        /// </summary>
        /// <param name="indexCollection">���������� ����� �������� � ��������</param>
        /// <returns></returns>
        public ICommonFilter this[Int32 indexCollection]
        {
            get
            {
                try
                {
                    return Items[indexCollection];
                }
                catch
                {
                    //return default(T);
                    //��������� ���� �� ������ ����������� �����/��������� ��� ��������, ����������� ��������� IBaseEntityObject
                    return null;
                }
            }
        }

        #endregion

        #region public BaseEntityObject InitialCollection
        /// <summary>
        /// �������� ��������� ��������
        /// </summary>
        public BaseEntityObject[] InitialCollection
        {
            get { return _initialCollection; }
            set { _initialCollection = value; }
        }
        #endregion

        #region public bool FilterTypeAnd { get; set; }
        /// <summary>
        /// ���������� ��� ������ �������� ������������ ���������
        /// <br/> true - ����������� ������� ������ ��������� ��� ��� �������, 
        /// <br/> false - ����������� ������� ������ ��������� ���� �� ��� ���� ������
        /// </summary>
        public bool FilterTypeAnd { get; set; }
        #endregion

        #region public Type FilteredType { get; }
        /// <summary>
        /// ���������� �������� ���� ��� ����������
        /// </summary>
        public Type FilteredType { get { return _filteredType; } }
        #endregion

        #region public int Count
        /// <summary>
        /// ���������� ���������� �������� � ���������
        /// </summary>
        public int Count { get { return Items.Count; } }
        #endregion

        #region public bool IsEmpty
        /// <summary>
        /// ���������� ��������, �������� �� ������ ������. �.�. ��� ���� �������� � ���������� �� ������ �������� ����������
        /// </summary>
        public bool IsEmpty
        {
            get { return Items.Count(i => i.Values.Length > 0) == 0; }
        }
        #endregion

        #region public List<ICommonFilter> Filters
        /// <summary>
        /// ���������� ��� ������� � ���������
        /// </summary>
        public List<ICommonFilter> Filters
        {
            get { return Items; }
        }
        #endregion

        #endregion

        #region Methods

        #region public BaseEntityObject[] GatherDirectives()

        ///<summary>
        /// ������������ ������� ��������
        ///</summary>
        ///<returns>��������� ���������</returns>
        public BaseEntityObject[] GatherDirectives()
        {
            List<BaseEntityObject> directives = new List<BaseEntityObject>();
            for (int i = 0; i < _initialCollection.Length; i++)
            {
                if (Acceptable(_initialCollection[i])) directives.Add(_initialCollection[i]);
            }
            return directives.ToArray();
        }

        #endregion

        #region public Acceptable(BaseEntityObject item)
        ///<summary>
        /// �����������, �������� �� ������� ��� ������
        ///</summary>
        ///<param name="item">����������� �������</param>
        ///<returns>��������� - �������� �� �������</returns>
        public bool Acceptable(BaseEntityObject item)
        {
            bool acceptable = true;
            for (int j = 0; j < Items.Count; j++)
            {
                if (!Items[j].Acceptable(item))
                {
                    acceptable = false;
                    break;
                }
            }
            return acceptable;
        }
        #endregion

        #region public override bool Equals(object obj)
        ///<summary>
        ///Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
        ///</returns>
        ///
        ///<param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (!(obj is CommonFilterCollection)) return false;
            CommonFilterCollection collectionFilter = (CommonFilterCollection)obj;
            if (Items.Count != collectionFilter.Items.Count)
                return false;
            for (int i = 0; i < Items.Count; i++)
            {
                if (!(Items[i].Equals(collectionFilter.Items[i])))
                    return false;
            }
            return true;
        }

        #endregion
       
        #region public override int GetHashCode()
        ///<summary>
        ///Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        ///</summary>
        ///
        ///<returns>
        ///A hash code for the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion

        #region public override string ToString()
        public override string ToString()
        {
            return Items.Where(filter => filter.GetValidValuesCount() > 0).Aggregate("", (current, filter) => current + (filter + "    "));
        }
        #endregion

        #region public string ToStrings()
        public string ToStrings()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ICommonFilter filter in Items.Where(filter => filter.GetValidValuesCount() > 0))
            {
                stringBuilder.AppendLine(filter.ToString());
            }

            return stringBuilder.ToString();
        }
        #endregion

        #endregion

        #region ����� IEnumerable<T>
        /// <summary>
        /// ���������� �������������, ����������� ������� ��������� � ���������.
        /// </summary>
        /// <returns>
        /// ��������� <see cref="T:System.Collections.Generic.IEnumerator`1"/>, ������� ����� �������������� ��� �������� ��������� ���������.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<ICommonFilter> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        #endregion

        #region ����� IEnumerable
        /// <summary>
        /// ���������� �������������, ������� ������������ ������� ��������� ���������.
        /// </summary>
        /// <returns>
        /// ������ <see cref="T:System.Collections.IEnumerator"/>, ������� ����� �������������� ��� �������� ��������� ���������.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        #endregion
    }
}