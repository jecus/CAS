using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CAS.UI.Interfaces
{

    /*
     * ��� ���������� ��� ���� TextBox (��������) ������ ���� ��������� �� ������� OnChanging � �������� ����� SetModified()
     */

    /// <summary>
    /// ������������ ����� ���� ��� ��������, ������� ����� ������������� �������� ������-���� �������
    /// </summary>
    public partial class EditObjectControl : UserControl
    {

        /*
         * ��������
         */

        #region public Object AttachedObject
        /// <summary>
        /// ������������� ������
        /// </summary>
        private Object _AttachedObject = null;
        /// <summary>
        /// ������������� ������
        /// </summary>
        public Object AttachedObject
        {
            get { return _AttachedObject; }
            set 
            {
                if (AttachedObject != value)
                {
                    ObjectChanging();
                    _AttachedObject = value;
                    ObjectChanged();
                    FillControls();
                }
            }
        }
        #endregion

        #region public bool Modified { get; }
        /// <summary>
        /// ���� �� ������� ��������� ������ ��������
        /// </summary>
        private bool _Modified; 
        /// <summary>
        /// ���� �� ������� ��������� ������ ��������
        /// </summary>
        public bool Modified { get { return _Modified; } }
        #endregion

        #region public event ControlEventHandler ControlModified;
        /// <summary>
        /// ������� ����������� � ������ ��������� ������ �������������
        /// </summary>
        public event ControlEventHandler ControlModified;
        #endregion

        /*
         * ��� ������������ ������������ �������� ������ ��������� �� ������� � ����������
         */

        #region public EditObjectControl()
        /// <summary>
        /// ������� ������� �� ��������� � ��������
        /// </summary>
        public EditObjectControl()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }
        #endregion

        #region public EditObjectControl(Object AttachedObject) : this()
        /// <summary>
        /// ������� ������� �������������� ������� ������� � ����������� � ���� ������
        /// </summary>
        /// <param name="AttachedObject"></param>
        public EditObjectControl(Object AttachedObject)
            : this()
        {
            FillControls();
        }
        #endregion

        /*
         * ������ ������ ���� �����������
         */

        #region public virtual void ApplyChanges()
        /// <summary>
        /// ��������� � ������� ��������� ��������� �� ��������. 
        /// ���� �� ��� ������ ������������� ������� ����� (�������� ��� ����� �����), �������� ������� �� ����������, ������������ false
        /// ����� base.ApplyChanges() ����������
        /// </summary>
        /// <returns></returns>
        public virtual void ApplyChanges()
        {
            _Modified = false;
        }
        #endregion

        #region public virtual void FillControls()
        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        public virtual void FillControls()
        {
            /*
             * ����������� ��� ��������� �������� � BeginUpdate() � EndUpdate()
             * 
             * BeginUpdate();
             * textBox1.Text = "";
             * textBox2.Text = "";
             * ...
             * EndUpdate();
             */
        }
        #endregion

        #region public virtual bool CheckData()
        /// <summary>
        /// ��������� ��������� ������.
        /// ���� �����-���� ���� �� �������� �� �������, ������� ����� ������ MessageBox, ������� ������ � ����������� ���� � ���������� false � �������� ���������� ������
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckData()
        {
            return true;
        }
        #endregion

        #region public virtual void ObjectChanging()
        /// <summary>
        /// ��������� ����������, ����� ��������� ������ ����� ��������.
        /// ����� ������� ���������� �� ������� ������� �������
        /// </summary>
        public virtual void ObjectChanging()
        {
        }
        #endregion

        #region public virtual void ObjectChanged()
        /// <summary>
        /// ��������� ����������, ����� ������ ���������
        /// ������� ����������� �� ������� ������ �������
        /// ����� FillControls ���������� �������������
        /// </summary>
        public virtual void ObjectChanged()
        {
        }
        #endregion

        /*
         * ������ � ��������� 
         */

        #region protected void SetModified()
        /// <summary>
        /// ���������� �������� ����� Modified � ����� �������� ������� � ���, ��� ������ ��� �������
        /// </summary>
        protected void SetModified()
        {
            if (_IsUpdating) return;

            //
            _Modified = true;
            if (ControlModified != null) ControlModified(this, new ControlEventArgs(this));
        }
        #endregion

        #region protected void BeginUpdate()
        /// <summary>
        /// ���������������� ����������� ������� ��������� � ��������
        /// </summary>
        protected void BeginUpdate()
        {
            _IsUpdating = true;
        }
        #endregion

        #region protected void EndUpdate()
        /// <summary>
        /// ������������ ����������� ������� ��������� � ��������
        /// </summary>
        protected void EndUpdate()
        {
            _IsUpdating = false;
        }
        #endregion

        /*
         * ����������
         */

        #region private bool _IsUpdating = false;
        /// <summary>
        /// ��������� �� ������� � ������ ������
        /// </summary>
        private bool _IsUpdating = false;
        #endregion

        
    }
}
