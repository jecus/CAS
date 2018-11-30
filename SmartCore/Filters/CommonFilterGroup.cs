using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCore.Filters
{
    ///<summary>
    /// �����, ������������ ������ ��������
    ///</summary>
    public class CommonFilterGroup: IEnumerable<ICommonFilter>
    {

        #region Fields
        /// <summary>
        /// ������ �������� �������
        /// </summary>
        private List<ICommonFilter> Items = new List<ICommonFilter>();
        /// <summary>
        /// �������� ������������� ��������
        /// <br/> true - ����������� ������� ������ ��������� ��� ��� �������, 
        /// <br/> false - ����������� ������� ������ ��������� ���� �� ��� ���� ������
        /// </summary>
        private bool _filterTypeAnd;
        /// <summary>
        /// �������� ������������� �� ���� ��������
        /// <br/> true - ����������� ������� ������ ��������� ��� ��� �������, 
        /// <br/> false - ����������� ������� ������ ��������� ���� �� ��� ���� ������
        /// </summary>
        private bool _nextFilterTypeAnd;
        #endregion

        #region Constructors

        #region public CommonFilterGroup(IEnumerable<ICommonFilter> filters, bool filterTypeAnd = true, bool nextFilterTypeAnd = true)
        /// <summary>
        ///  ��������� ������ ��� ������� ���������
        /// </summary>
        /// <param name="filters">����������� ��� ������� �������</param>
        /// <param name="filterTypeAnd">������ �������� ������������� ��������
        /// <br/> true - ����������� ������� ������ ��������� ��� ��� �������, 
        /// <br/> false - ����������� ������� ������ ��������� ���� �� ��� ���� ������</param>
        /// <param name="nextFilterTypeAnd">������ �������� ������������� �� ����. ��������
        /// <br/> true - ����������� ������� ������ ��������� ��� ��� �������, 
        /// <br/> false - ����������� ������� ������ ��������� ���� �� ��� ���� ������</param>
        public CommonFilterGroup(IEnumerable<ICommonFilter> filters, bool filterTypeAnd = true, bool nextFilterTypeAnd = true)
        {
            Items.AddRange(filters);
            _filterTypeAnd = filterTypeAnd;
            _nextFilterTypeAnd = nextFilterTypeAnd;
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

        #region public bool FilterTypeAnd { get; set; }

        /// <summary>
        /// ���������� �������� ������������ ���������
        /// <br/> true - ����������� ������� ������ ��������� ��� ��� �������, 
        /// <br/> false - ����������� ������� ������ ��������� ���� �� ��� ���� ������
        /// </summary>
        public bool FilterTypeAnd
        {
            get { return _filterTypeAnd; }
        }
        #endregion

        #region public bool NextFilterTypeAnd { get; set; }

        /// <summary>
        /// ���������� �������� ������������ �� ���� ��������
        /// <br/> true - ����������� ������� ������ ��������� ��� ��� �������, 
        /// <br/> false - ����������� ������� ������ ��������� ���� �� ��� ���� ������
        /// </summary>
        public bool NextFilterTypeAnd
        {
            get { return _nextFilterTypeAnd; }
        }
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
            if (!(obj is CommonFilterGroup)) return false;
            CommonFilterGroup collectionFilter = (CommonFilterGroup)obj;
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