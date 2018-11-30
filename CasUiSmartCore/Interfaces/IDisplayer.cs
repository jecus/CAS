using System;
using AvControls.AvMultitabControl;
using CAS.UI.Management.Dispatchering;

namespace CAS.UI.Interfaces
{
    /// <summary>
    /// ��������� ����� �������: 
    /// </summary>
    public interface IDisplayer
    {
        #region IDisplayingEntity Entity
        /// <summary>
        /// �����, ������������ ��������
        /// </summary>
        IDisplayingEntity Entity
        {
            get; set;
        }
        #endregion

        /// <summary>
        /// ����� ��������� �������
        /// </summary>
        string Text{ get; set;}

        /// <summary>
        /// ��������� �� �������� ����� ��������� ������������
        /// </summary>
        bool PerformCloseChecking { get; set; }

        /// <summary>
        /// Invokes displaying of default entity
        /// </summary>
        void Show();

        /// <summary>
        /// Invokes displaying of entity
        /// </summary>
        /// <param name="entity">Entity to display</param>
        void Show(IDisplayingEntity entity);

        /// <summary>
        /// Checks whether contained data of two displayers are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool ContainedDataEquals(IDisplayer obj);

        /// <summary>
        /// Checks whether displaying entities have same type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool ContainedDisplayingEntityEquals(IDisplayer obj);

        /// <summary>
        /// ��������� ��� �������� ������������
        /// </summary>
        event EventHandler DisplayerRemoving;

        /// <summary>
        /// ����������� ����������� �������� ������������
        /// </summary>
        /// <param name="arguments"></param>
        void OnDisplayerRemoving(DisplayerCancelEventArgs arguments);

        /// <summary>
        /// ��������� ����� �������� ������������
        /// </summary>
        event EventHandler DisplayerRemoved;

        /// <summary>
        /// ����������� ����������� �������� ������������
        /// </summary>
        /// <param name="arguments"></param>
        void OnDisplayerRemoved(DisplayerCancelEventArgs arguments);

        /// <summary>
        /// ��������, ������������ ��� ���������� ������������
        /// </summary>
        /// <param name="arguments"></param>
        void OnDisplayerDeselecting(DisplayerCancelEventArgs arguments);

        #region  ��� ������ ������ � �����
        /// <summary>
        /// ��� �������� ���������� �������� � ������ �������
        /// </summary>
        void PreviousPage();
        /// <summary>
        /// ��� �������� ��������� �������� � ������ �������
        /// </summary>
        void NextPage();

        ///<summary>
        /// ���������� ��� ����� ������������� ������ � ������ �������
        ///</summary>
        event EventHandler ScreenChanged;

        ///<summary>
        /// ���������� ����-�� PreviousPage
        ///</summary>
        ///<returns></returns>
        bool CanPreviousPage();

        ///<summary>
        /// ���������� ����-�� NextPage
        ///</summary>
        ///<returns></returns>
        bool CanNextPage();
        #endregion

        #region ��� ������ �������� �������

        /// <summary>
        /// �������, ��������������� �� ��������� ���������� ������� � �������� ������ �������
        /// </summary>
        event EventHandler CountScreenChanget;

        /// <summary>
        /// ���������� ���������� ������ �������� ������ �������
        /// </summary>
        /// <returns></returns>
        bool CanEnableCloseTab();
        #endregion

        #region ��������� ������ �������� �����
        /// <summary>
        /// ���������� ��� ������� ������ �������� ���������
        /// </summary>
        event EventHandler ClosingWindow;

        /// <summary>
        /// �������� ������� ClosingWindow
        /// </summary>
        void OnClosingWindow();

        /// <summary>
        /// ���������� ��� ������ �������� ��������� 
        /// </summary>
        event EventHandler CancelClosingWindow;

        /// <summary>
        /// �������� �������  CancelClosingWindow
        /// </summary>
        void OnCancelClosingWindow();
        #endregion
       
        /// <summary>
        /// ������ � ������������� ��������
        /// </summary>
        AvMultitabControl ParentControl { get; }
    }
}