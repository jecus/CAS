using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CAS.UI.UIControls.ReferenceControls
{
    ///<summary>
    /// ����������� ������� 
    ///</summary>
    public partial class RichReferenceContainer : UserControl
    {

        #region Constructor

        ///<summary>
        /// ��������� ����� ������ ������� ������
        ///</summary>
        public RichReferenceContainer()
        {
            InitializeComponent();
        }

        #endregion

        #region Fields

        private Color descriptionTextColor = Color.DimGray;

        #endregion

        #region Properties

        #region public Control AfterCaptionControl
        /// <summary>
        /// ������� ����������, ������������ ����� ���������
        /// </summary>
        public Control AfterCaptionControl
        {
            get
            {
                return tableLayoutPanel.GetControlFromPosition(2, 0);
            }
            set
            {
                if (AfterCaptionControl != null)
                    tableLayoutPanel.Controls.Remove(AfterCaptionControl);
                if (value == null) return;
                tableLayoutPanel.Controls.Add(value, 2, 0);
            }
        }
        #endregion

        #region public Control AfterCaptionControl2
        /// <summary>
        /// ������� ����������, ������������ 2 ����� ���������
        /// </summary>
        public Control AfterCaptionControl2
        {
            get
            {
                return tableLayoutPanel.GetControlFromPosition(3, 0);
            }
            set
            {
                if (AfterCaptionControl2 != null)
                    tableLayoutPanel.Controls.Remove(AfterCaptionControl2);
                if (value == null) return;
                tableLayoutPanel.Controls.Add(value, 3, 0);
            }
        }
        #endregion

        #region public Control AfterCaptionControl3
        /// <summary>
        /// ������� ����������, ������������ 3 ����� ���������
        /// </summary>
        public Control AfterCaptionControl3
        {
            get
            {
                return tableLayoutPanel.GetControlFromPosition(4, 0);
            }
            set
            {
                if (AfterCaptionControl3 != null)
                    tableLayoutPanel.Controls.Remove(AfterCaptionControl3);
                if (value == null) return;
                tableLayoutPanel.Controls.Add(value, 4, 0);
            }
        }
        #endregion

        #region public string Caption
        ///<summary>
        /// ��������� �������
        ///</summary>
        public string Caption
        {
            get
            {
                return labelCaption.Text;
            }
            set
            {
                labelCaption.Text = value;
            }
        }
        #endregion

        #region public PictureBox PictureBoxIcon

        /// <summary>
        /// ���������� PictureBox ������
        /// </summary>
        public PictureBox PictureBoxIcon
        {
            get
            {
                return pictureBoxIcon;
            }
        }

        #endregion

        #region public Label LabelCaption
        ///<summary>
        /// Label ���������
        ///</summary>
        public Label LabelCaption
        {
            get { return labelCaption; }
        }
        #endregion

        #region public Image UpperLeftIcon
        ///<summary>
        /// ������ ������ �������� ����
        ///</summary>
        public Image UpperLeftIcon
        {
            get
            {
                return pictureBoxIcon.Image;
            }
            set
            {
                pictureBoxIcon.Image = value;
            }
        }
        #endregion

        #region public Control MainControl
        ///<summary>
        /// ������, ������������ ��� ��������
        ///</summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual Control MainControl
        {
            get
            {
                return tableLayoutPanel.GetControlFromPosition(1, 1);
            }
            set
            {
                if (value == null)return;
                tableLayoutPanel.Controls.Add(value, 1, 1);
                this.tableLayoutPanel.SetColumnSpan(value, 4);
            }
        }
        #endregion

        #region public Color DescriptionTextColor
        ///<summary>
        /// ���� ������ ��������
        ///</summary>
        public Color DescriptionTextColor
        {
            get
            {
                return descriptionTextColor;
            }
            set
            {
                descriptionTextColor = value;
            }
        }
        #endregion

        #endregion

        #region protected override void OnDockChanged(EventArgs e)
        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.DockChanged"></see> event.
        ///</summary>
        ///
        ///<param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
        protected override void OnDockChanged(EventArgs e)
        {
            tableLayoutPanel.Dock = Dock;
            base.OnDockChanged(e);
        }
        #endregion

    }
}