using System;
using System.ComponentModel;
using CAS.UI.Management.Dispatchering;

namespace CAS.UI.Interfaces
{
    /// <summary>
    /// ���������, ���������� ��������, ������� ����� ������������ � �������� IDisplayer
    /// </summary>
    public interface IDisplayingEntity
    {
        /// <summary>
        /// Represents data being displayed
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        object ContainedData{ get; set;}

        /// <summary>
        /// Checks whether represented data equals to corresponding data of object
        /// </summary>
        /// <param name="obj">Compared object</param>
        /// <returns></returns>
        bool ContainedDataEquals(IDisplayingEntity obj);

        /// <summary>
        /// Invoke control to show data
        /// </summary>
        void Show();

        /// <summary>
        /// �����, ���������� ����� ���������� ���������� �� ������������(�������)
        /// </summary>
        void OnInitCompletion(object sender);

        /// <summary>
        /// ��������� �� ����� �������� ������������ ������� �����������
        /// </summary>
        /// <param name="arguments"></param>
        void OnDisplayerRemoving(DisplayerCancelEventArgs arguments);

        /// <summary>
        /// ��������� ����� �������� ������������ ������� �����������
        /// </summary>
        void OnDisplayerRemoved();

        /// <summary>
        /// ��������, ������������ ��� ����������� �������, ���������� ������ ��������
        /// </summary>
        /// <param name="arguments"></param>
        void OnDisplayerDeselecting(DisplayerCancelEventArgs arguments);

        /// <summary>
        /// ����� �������� ��������� ��������� [:|||:]
        /// </summary>
        /// <param name="isEnbaled">���������</param>
        void SetEnabled(bool isEnbaled);

        /// <summary>
        /// �������, ���������� ����� ���������� ����������� �� ������������ (�������)
        /// </summary>
        event EventHandler InitComplition;
       
        /// <summary>
        /// �������, ����������� � ������ �������� �����������
        /// </summary>
        event EventHandler<EntityCancelEventArgs> EntityRemoving;

        /// <summary>
        /// ��������� ����� �������� �����������
        /// </summary>
        event EventHandler EntityRemoved;

    }
}