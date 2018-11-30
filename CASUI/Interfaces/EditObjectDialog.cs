using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CAS.UI.Interfaces
{


    /// <summary>
    /// ������ �������������� �������
    /// </summary>
    public partial class EditObjectDialog : Form
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
                    ChangeObject();
                    ObjectChanged();
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

        #region public event EditObjectControlModifiedEventHandler EditObjectControlModified;
        /// <summary>
        /// ������� ���������, ����� ���� ������� ��������� � ���� �� ��������� �������
        /// </summary>
        public delegate void EditObjectControlModifiedEventHandler();
        /// <summary>
        /// ������� ���������, ����� ���� ������� ��������� � ���� �� ��������� �������
        /// </summary>
        public event EditObjectControlModifiedEventHandler EditObjectControlModified;
        #endregion

        /*
         * ������������ ������, �������� ������ ����� ����� ����������� ����� Show
         */

        #region protected EditObjectDialog()
        /// <summary>
        /// ������� ������ �� ��������� � ��������
        /// </summary>
        protected EditObjectDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region protected EditObjectDialog(Object AttachedObject) : this()
        /// <summary>
        /// ������� ������ �������������� �������
        /// </summary>
        /// <param name="AttachedObject"></param>
        protected EditObjectDialog(Object AttachedObject)
            : this()
        {
            this.AttachedObject = AttachedObject;
        }
        #endregion

        /*
         * ������ � ��������
         */

        #region protected static void RegisterDialog(Object AttachedObject)
        /// <summary>
        /// ������������ ������ � ����� ����, ����� ����� ��� �������
        /// </summary>
        /// <param name="AttachedObject"></param>
        protected static void RegisterDialog(EditObjectDialog dlg)
        {
            _OpenedDialogs.Add(dlg);
            dlg.FormClosed += new FormClosedEventHandler(dlg_FormClosed);
        }
        #endregion

        #region private static void dlg_FormClosed(object sender, FormClosedEventArgs e)
        /// <summary>
        /// ��� �������� ������� ������� ��� �� ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void dlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            EditObjectDialog dlg = sender as EditObjectDialog;
            if (dlg == null) return;
            //
            if (_OpenedDialogs.Contains(dlg))
                _OpenedDialogs.Remove(dlg);
        }
        #endregion

        #region protected static EditObjectDialog GetDialogByObject(Object AttachedObject)
        /// <summary>
        /// ������� ������ �� �������
        /// </summary>
        /// <param name="AttachedObject"></param>
        /// <returns></returns>
        protected static EditObjectDialog GetDialogByObject(Object AttachedObject)
        {
            foreach (EditObjectDialog dlg in _OpenedDialogs)
                if (dlg.AttachedObject == AttachedObject)
                    return dlg;
            
            //
            return null;
        }
        #endregion

        #region protected override void OnControlAdded(ControlEventArgs e)
        /// <summary>
        /// ������ ��� ������������ �������� EdiObjectControl
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            EditObjectControl c = e.Control as EditObjectControl;
            if (c != null)
            {
                c.ControlModified += new ControlEventHandler(EditObjectControl_ControlModified);
            }
        }
        #endregion

        #region protected override void OnControlRemoved(ControlEventArgs e)
        /// <summary>
        /// ����� ������� ��� ������, �� ������ ���������� �� ��� �������
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            EditObjectControl c = e.Control as EditObjectControl;
            if (c != null)
            {
                c.ControlModified -= EditObjectControl_ControlModified;
            }
        }
        #endregion

        #region protected void SetModified()
        /// <summary>
        /// ���������� �������� ����� Modified � ����� �������� ������� � ���, ��� ������ ��� �������
        /// </summary>
        protected void SetModified()
        {
            if (EditObjectControlModified != null) EditObjectControlModified();
        }
        #endregion

        #region protected bool CheckData()
        /// <summary>
        /// ��������� ��������� ������
        /// </summary>
        /// <returns></returns>
        protected bool CheckData()
        {
            return CheckData(this);
        }
        #endregion

        #region protected void ApplyChanges()
        /// <summary>
        /// ��������� ������� ��� ��������� ��������� � ���������
        /// </summary>
        protected void ApplyChanges()
        {
            ApplyChanges(this);
        }
        #endregion

        #region protected bool Save()
        /// <summary>
        /// ��������� ��������� ��������� � ������� � ���� ������
        /// </summary>
        /// <returns></returns>
        protected bool Save()
        {

            // ��������� ��������� ���������
            if (!CheckData()) return false;

            // ��������� ��������� � ������� 
            ApplyChanges();

            // �������� ������������� ����� ���������� �������, �.�. EditObjectDialog �� ����� ������������� �� �������
            SaveObjectCalled();

            // �������� ������ �������
            return true;
        }
        #endregion

        /*
         * ����������
         */

        #region private static List<EditObjectDialog> _OpenedDialogs = new List<EditObjectDialog>();
        /// <summary>
        /// ������ �������� ��������
        /// </summary>
        private static List<EditObjectDialog> _OpenedDialogs = new List<EditObjectDialog>();
        #endregion

        #region private void EditObjectControl_ControlModified(object sender, ControlEventArgs e)
        /// <summary>
        /// ���� ������� ��������� � ���� �� ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditObjectControl_ControlModified(object sender, ControlEventArgs e)
        {
            SetModified();
        }
        #endregion

        #region private bool CheckData(Control control)
        /// <summary>
        /// �������� ����� ��������� ��������� ������ ���� ���������
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private bool CheckData(Control control)
        {
            if (control == null || control.Controls == null) return true;


            // ������������� �� ������ "�������������" ��������
            foreach (Control c in control.Controls)
            {
                EditObjectControl cc = c as EditObjectControl;
                if (cc != null)
                    if (!cc.CheckData()) return false;
                    else // ���������� ������������� ������� ������ ��������
                        if (!CheckData(c)) return false;
            }


            // ��� �������� ����������� �������
            return true;
        }
        #endregion

        #region private void ApplyChanges(Control control)
        /// <summary>
        /// �������� ����� ApplyChanges � ������� ��������
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private void ApplyChanges(Control control)
        {
            if (control == null || control.Controls == null) return;

            // ������������� �� ������ "�������������" ��������
            foreach (Control c in control.Controls)
            {
                EditObjectControl cc = c as EditObjectControl;
                if (cc != null) cc.ApplyChanges();
                else // ���������� ������������� ������� ������ ��������
                    ApplyChanges(c);
            }
        }
        #endregion

        #region private void ObjectChanging(Control control)
        /// <summary>
        /// ����� ����������, ����� ������ ����������� � ������� ����� �������
        /// ����� �������� ������ ����������
        /// </summary>
        private void ObjectChanging(Control control)
        {
            if (control == null || control.Controls == null) return;
            //
            foreach (Control c in control.Controls)
            {
                EditObjectControl cc = c as EditObjectControl;
                if (cc != null)
                    cc.ObjectChanging();
                else // ���������� ������������� ������� ������ ��������
                    ObjectChanging(c);
            }
        }
        #endregion

        #region private void ObjectChanged(Control control)
        /// <summary>
        /// ����� ����������, ����� ������ ����������� � ������� ���������
        /// ����� �������� ������ ����������
        /// </summary>
        private void ObjectChanged(Control control)
        {
            if (control == null || control.Controls == null) return;
            //
            foreach (Control c in control.Controls)
            {
                EditObjectControl cc = c as EditObjectControl;
                if (cc != null)
                    cc.ObjectChanged();
                else // ���������� ������������� ������� ������ ��������
                    ObjectChanged(c);
            }
        }
        #endregion

        #region private void ChangeObject(Control control)
        /// <summary>
        /// �������� ������ ���� ���������
        /// </summary>
        /// <param name="control"></param>
        private void ChangeObject(Control control)
        {
            if (control == null || control.Controls == null) return;
            //
            foreach (Control c in control.Controls)
            {
                EditObjectControl cc = c as EditObjectControl;
                if (cc != null)
                    cc.AttachedObject = _AttachedObject;
                else // ���������� ������������� ������� ������ ��������
                    ChangeObject(c);
            }
        }
        #endregion


        /*
         * ������ ������ ���� ����������� - ����������� ����� � �������� � ������ ��������� - �������� virtual �� override ����������� !
         */

        #region public static void Show(Object AttachedObject)
        /// <summary>
        /// �������� ������ �������������� �������.
        /// ������ ����� �������� ������� ����������.
        /// </summary>
        /// <param name="AttachedObject"></param>
        public static void Show(Object AttachedObject)
        {
            EditObjectDialog dlg = GetDialogByObject(AttachedObject);
            if (dlg == null)
            {
                dlg = new EditObjectDialog(AttachedObject);
                RegisterDialog(dlg);
            }
            dlg.Show();
        }
        #endregion

        #region protected virtual bool SaveObjectCalled()
        /// <summary>
        /// ���� ������� ���������� �������
        /// </summary>
        protected virtual bool SaveObjectCalled()
        {
            return true;
        }
        #endregion

        #region protected virtual void ObjectChanging()
        /// <summary>
        /// ����� ����������, ����� ������ ����������� � ������� ����� �������
        /// ����� �������� ������ ����������
        /// </summary>
        protected virtual void ObjectChanging()
        {
            ObjectChanging(this);
        }
        #endregion

        #region protected virtual void ObjectChanged()
        /// <summary>
        /// ����� ����������, ����� ������ ����������� � ������� ���������
        /// ����� �������� ������ ����������
        /// </summary>
        protected virtual void ObjectChanged()
        {
            ObjectChanged(this);
        }
        #endregion

        #region protected virtual void ChangeObject()
        /// <summary>
        /// �������� ������ ���� ���������
        /// </summary>
        /// <param name="control"></param>
        protected virtual void ChangeObject()
        {
            ChangeObject(this);
        }
        #endregion

    }
}