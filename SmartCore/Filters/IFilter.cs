namespace SmartCore.Filters
{
    ///<summary>
    ///</summary>
    public interface IFilter
    {
        ///<summary>
        /// �����������, �������� �� ������� ��� ������
        ///</summary>
        ///<param name="item">����������� �������</param>
        ///<returns>��������� - �������� �� �������</returns>
        bool Acceptable(object item);
    }
}