using System;

namespace CASTerms
{
    /// <summary>
    /// ����� �������� ��������� � �������� �������
    /// </summary>
    public class LicenseInformation
    {
        #region Fields

        /// <summary>
        /// �������� ��������, ��� ������� ������� ����
        /// </summary>
        private string _company = "";

        /// <summary>
        /// ���� ��������� ������ ��� ����-������
        /// </summary>
        private DateTime _expires= DateTime.MinValue;

        /// <summary>
        /// ����, ������ ��� ��������
        /// </summary>
        private string _productKey = "";

        /// <summary>
        /// ��������� ����� �����
        /// </summary>
        private string _keyNo = "";

        /// <summary>
        /// ������ ���������
        /// </summary>
        private string _version = "";

        /// <summary>
        /// �������� ��������� ������������ ������ ������ 
        /// </summary>
        private bool _singleOperator;

        /// <summary>
        /// ����������� �� ���-�� ��
        /// </summary>
        private int _maxAircrafts;

        /// <summary>
        /// �������� �� CAS � �������� BiWeekly
        /// </summary>
        private bool _biWeeklies = true;

        /// <summary>
        /// �������� �� �������������� �������
        /// </summary>
        private bool _editTemplates = true;

        /// <summary>
        /// ���� ��������� �������
        /// </summary>
        private DateTime _installationDate;


        private const int EvaluationPeriod = 45;

        #endregion

        #region Constructors

        #region public LicenseInformation(string company, DateTime expires, string productKey, string keyNo, string version, bool singleOperator, int maxAircrafts, bool biWeeklies, bool editTemplates)


        /// <summary>
        /// ��������� ����� �������� ��������� � �������� �������
        /// </summary>
        /// <param name="company">�������� ��������, ��� ������� ������� ����</param>
        /// <param name="expires">���� ��������� ������ ��� ����-������</param>
        /// <param name="productKey">����, ������ ��� ��������</param>
        /// <param name="keyNo">��������� ����� �����</param>
        /// <param name="version">������ ���������</param>
        /// <param name="biWeeklies">�������� �� CAS � �������� BiWeekly</param>
        /// <param name="editTemplates">�������� �� �������������� �������</param>
        /// <param name="maxAircrafts">����������� �� ���-�� ��</param>
        /// <param name="singleOperator">�������� ��������� ������������ ������ ������ </param>
        public LicenseInformation(string company, DateTime expires, string productKey, string keyNo, string version, bool singleOperator, int maxAircrafts, bool biWeeklies, bool editTemplates)
        {
            this._company = company;
            this._expires = expires;
            this._productKey = productKey;
            this._keyNo = keyNo;
            this._version = version;
            this._singleOperator = singleOperator;
            this._maxAircrafts = maxAircrafts;
            this._biWeeklies = biWeeklies;
            this._editTemplates = editTemplates;
        }

        #region public LicenseInformation(string productKey, string company)

        /// <summary>
        /// ��������� ����� �������� ��������� � �������� �������
        /// </summary>
        /// <param name="productKey">�������� ��������, ��� ������� ������� ����</param>
        /// <param name="company">����, ������ ��� ��������</param>
        public LicenseInformation(string productKey, string company)
        {
            this._productKey = productKey;
            this._company = company;
        }

        #endregion

        #region public LicenseInformation(string productKey, string company, DateTime installationDate)

        /// <summary>
        /// ��������� ����� �������� ��������� � �������� �������
        /// </summary>
        /// <param name="company">����, ������ ��� ��������</param>
        /// <param name="installationDate">���� ��������� �������</param>
        public LicenseInformation(string company, DateTime installationDate)
        {
            this._company = company;
            this._installationDate = installationDate;
            _expires = installationDate.AddDays(EvaluationPeriod);

        }

        #endregion


        #region         public LicenseInformation(string company, DateTime expires, string productKey, string version, bool singleOperator, int maxAircrafts, bool biWeeklies, bool editTemplates)

        /// <summary>
        /// ��������� ����� �������� ��������� � �������� �������
        /// </summary>
        /// <param name="company">�������� ��������, ��� ������� ������� ����</param>
        /// <param name="expires">���� ��������� ������ ��� ����-������</param>
        /// <param name="productKey">����, ������ ��� ��������</param>
        /// <param name="version">������ ���������</param>
        /// <param name="biWeeklies">�������� �� CAS � �������� BiWeekly</param>
        /// <param name="editTemplates">�������� �� �������������� �������</param>
        /// <param name="maxAircrafts">����������� �� ���-�� ��</param>
        /// <param name="singleOperator">�������� ��������� ������������ ������ ������ </param>
        public LicenseInformation(string company, DateTime expires, string productKey, string version, bool singleOperator, int maxAircrafts, bool biWeeklies, bool editTemplates)
        {
            this._company = company;
            this._expires = expires;
            this._productKey = productKey;
            this._version = version;
            this._singleOperator = singleOperator;
            this._maxAircrafts = maxAircrafts;
            this._biWeeklies = biWeeklies;
            this._editTemplates = editTemplates;
        }

        #endregion

        #endregion

        public LicenseInformation()
        {
            
        }
        #endregion


        #region Properties

        #region public DateTime InstallationDate
        /// <summary>
        /// ���� ��������� �������
        /// </summary>
        public DateTime InstallationDate
        {
            get { return _installationDate; }
            set { _installationDate = value; }
        }

        #endregion

        #region public string Company

        /// <summary>
        /// �������� ��������, ��� ������� ������� ����
        /// </summary>
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        #endregion

        #region public DateTime Expires

        /// <summary>
        /// ���� ��������� ������ ��� ����-������
        /// </summary>
        public DateTime Expires
        {
            get { return _expires; }
            set { _expires = value; }
        }

        #endregion

        #region public string ProductKey

        /// <summary>
        /// ����, ������ ��� ��������
        /// </summary>
        public string ProductKey
        {
            get { return _productKey; }
            set { _productKey = value; }
        }

        #endregion

        #region public string KeyNo

        /// <summary>
        /// ��������� ����� �����
        /// </summary>
        public string KeyNo
        {
            get { return _keyNo; }
            set { _keyNo = value; }
        }

        #endregion

        #region public string Version
        /// <summary>
        /// ������ ���������
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        #endregion

        #region public bool SingleOperator

        /// <summary>
        /// �������� ��������� ������������ ������ ������ 
        /// </summary>
        public bool SingleOperator
        {
            get { return _singleOperator; }
            set { _singleOperator = value; }
        }

        #endregion

        #region public int MaxAircrafts

        /// <summary>
        /// ����������� �� ���-�� ��
        /// </summary>
        public int MaxAircrafts
        {
            get { return _maxAircrafts; }
            set { _maxAircrafts = value; }
        }

        #endregion

        #region public bool BiWeeklies

        /// <summary>
        /// �������� �� CAS � �������� BiWeekly
        /// </summary>
        public bool BiWeeklies
        {
            get { return _biWeeklies; }
            set { _biWeeklies = value; }
        }

        #endregion

        #region public bool EditTemplates

        /// <summary>
        /// �������� �� �������������� �������
        /// </summary>
        public bool EditTemplates
        {
            get { return _editTemplates; }
            set { _editTemplates = value; }
        }

        #endregion

        #endregion
    }
}
