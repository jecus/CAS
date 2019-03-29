using System;
using System.Drawing;
using System.Windows.Forms;

namespace CAS.UI.UIControls.Auxiliary
{

    #region public enum DelimiterStyle
    /// <summary>
    /// ���������� ��� �����������
    /// </summary>
    public enum DelimiterStyle
    {
        /// <summary>
        /// ����������� � ���� �����
        /// </summary>
        Dotted,

        /// <summary>
        /// �������� ����������� � ���� �����
        /// </summary>
        Solid,
    }
    #endregion

    #region public enum DelimiterOrientation
    /// <summary>
    /// ����������� �����������
    /// </summary>
    public enum DelimiterOrientation
    {
        /// <summary>
        /// �������������� (����� �������)
        /// </summary>
        Horizontal, 
        /// <summary>
        /// ������������ (������ ����)
        /// </summary>
        Vertical,
    }
    #endregion


    /// <summary>
    /// �����������
    /// </summary>
    public partial class Delimiter : UserControl
    {

        #region public DelimiterStyle Style
        /// <summary>
        /// ��� �����������
        /// </summary>
        private DelimiterStyle _Style;
        /// <summary>
        /// ��� ����������� 
        /// </summary>
        public DelimiterStyle Style
        {
            get { return _Style; }
            set { _Style = value; ChangeStyle(); }
        }
        #endregion

        #region public DelimiterOrientation Orientation
        /// <summary>
        /// ����������� �����������
        /// </summary>
        private DelimiterOrientation _Orientation;
        /// <summary>
        /// ����������� ����������� 
        /// </summary>
        public DelimiterOrientation Orientation
        {
            get { return _Orientation; }
            set { _Orientation = value; ChangeOrientation(); }
        }
        #endregion

        /*
         * �����������
         */

        #region Delimiter()
        /// <summary>
        /// �����������
        /// </summary>
        public Delimiter()
        {
            InitializeComponent();

            // ���������� ���������� ����
            ChangeStyle(); 
            ChangeOrientation();
            BackgroundImageLayout = ImageLayout.Tile;
        }
        #endregion

        /*
         * ����������
         */

        #region private void ChangeStyle()
        /// <summary>
        /// ������ ������� �������
        /// </summary>
        private void ChangeStyle()
        {

            // ���������� ������ ��������
            if (_Style == DelimiterStyle.Dotted)
                BackgroundImage = Properties.Resources.delimiter_dotted;
            else
            {
                if (_Style == DelimiterStyle.Solid)
                    if (_Orientation == DelimiterOrientation.Horizontal)
                        BackgroundImage = Properties.Resources.delimeter_solid_horizontal;
                    else
                        BackgroundImage = Properties.Resources.delimeter_solid_vertical;
            }

            // ��������� ������
            ChangeSize();
        }
        #endregion

        #region private void ChangeOrientation()
        /// <summary>
        /// ������ ����������� �����������
        /// </summary>
        private void ChangeOrientation()
        {
            // ����������� ����������� ����� �������� �� ��������� ������� 
            if (_Style == DelimiterStyle.Solid)
                if (_Orientation == DelimiterOrientation.Horizontal)
                    BackgroundImage = Properties.Resources.delimeter_solid_horizontal;
                else
                    BackgroundImage = Properties.Resources.delimeter_solid_vertical;

            //
            ChangeSize();
        }
        #endregion

        #region private void ChangeSize()
        /// <summary>
        /// ��������� ������ �����������
        /// </summary>
        private void ChangeSize()
        {
            // ���������� ������� ������������ �������
            int max;
            if (Size.Width > Size.Height)
                max = Size.Width;
            else
                max = Size.Height;

            // � ����������� �� ���������� ������� ����������� ������� ����� ����� ��������
            int min;
            if (_Style == DelimiterStyle.Dotted)
                min = 1;
            else
                min = 2;

            // ������� ������ ���������� � �������� �����������
            if (_Orientation == DelimiterOrientation.Horizontal)
                Size = new Size(max, min);
            else
                Size = new Size(min, max);
        }
        #endregion

        #region protected override void OnSizeChanged(EventArgs e)
        /// <summary>
        /// ������ �������� ���������
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            ChangeSize();
        }
        #endregion

    }
}
