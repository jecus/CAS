namespace CASTerms
{
    /// <summary>
    /// ������������ ����� ���������
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// ������ �����������
        /// </summary>
        ConnectionError = 0,
        /// <summary>
        /// ���������� ������������� � ������� �� �������� � ���������� ��������� �� USB �����
        /// </summary>
        LicenseInformationNotEqual = 1,
        /// <summary>
        /// ��������� USB �����
        /// </summary>
        USBKeyAbsence = 2,
        /// <summary>
        /// �� ������ USB ����
        /// </summary>
        USBKeyNotValid = 3,
        /// <summary>
        /// ��������� ����� �������� ��������
        /// </summary>
        LicenseExpired = 4,
        /// <summary>
        /// �� ������ ����������� �� Expires
        /// </summary>
        LicenseExpiresNULL = 5,
        /// <summary>
        /// ���� �������� �� ������
        /// </summary>
        LicenseNotFound = 6,
        /// <summary>
        /// ������ ��� �������� ����-����
        /// </summary>
        DeleteError = 7,
        /// <summary>
        /// ������ ��� �������� ����-����
        /// </summary>
        LoadError = 8,
        /// <summary>
        /// �������� �������� ���������
        /// </summary>
        InvalidValue = 9,
        /// <summary>
        /// ������ ��� ���������� ����-����
        /// </summary>
        SaveError = 10,
        /// <summary>
        /// ��������� ��������
        /// </summary>
        LicenseViolation = 11,

    }
}