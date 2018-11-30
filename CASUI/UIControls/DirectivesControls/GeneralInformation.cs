using System;
using System.Windows.Forms;
using LTR.Core.Types.Aircrafts.Parts;

namespace LTR.UI.UIControls.DirectivesControls
{
    ///<summary>
    /// ���������� ��� ����� ����������� ���������
    ///</summary>
    public enum GeneralInformationMode
    {
        ///<summary>
        /// ��� �������� �����������
        ///</summary>
        View, 
        ///<summary>
        /// ��� ��������������
        ///</summary>
        Edit
    }
    ///<summary>
    ///</summary>
    public partial class GeneralInformation : UserControl
    {
        #region Fields

        private Detail currentDetail;
        private GeneralInformationMode mode = GeneralInformationMode.View;
        
        #endregion


        #region Constructors

                #region public GeneralInformation()
                    /// <summary>
                    /// ������� ��������� ������������ ���������� �� ��������
                    /// </summary>
                    public GeneralInformation()
                    {
                        InitializeComponent();
                    }
                    #endregion

                #region public GeneralInformation(Detail currentDetail)
                    /// <summary>
                    /// ������� ��������� ������������ ���������� �� ��������
                    /// </summary>
                    /// <param name="currentDetail"></param>
                    public GeneralInformation(Detail currentDetail) : this()
                    {
                        if (null == currentDetail) throw new ArgumentNullException("currentDetail", "Argument cannot be null");
                        if (!(currentDetail.GetType() == typeof(Detail))) throw new ArgumentException("Argument must be explicitly of type Detail", "currentDetail");

                        this.currentDetail = currentDetail;
                        UpdateDataForDetail();
                        UpdateMode();
                    }
                #endregion

        #endregion

        #region  Propeties
        /// <summary>
        /// �������� �����
        /// <//summary>
        #region public GeneralInformationMode Mode
        /// <summary>
        /// ������ ��� ����� ������������� ��������
        /// </summary>
        public GeneralInformationMode Mode
        {
            get { return mode; }
            set 
            {
                mode = value; 
                UpdateMode();
            }

        }
        #endregion


        #region public Detail CurrentDetail
        /// <summary>
        /// ������ ��� ���������� ������������ �������
        /// </summary>
        public Detail CurrentDetail
        {
            get { return currentDetail; }
            set
            {
                currentDetail = value;
                if (null != value) UpdateDataForDetail();
            }
        }
        #endregion

        #endregion
        
        #region Methods
        /// <summary>
        /// ������ ����� ����������� ��������
        /// </summary>
        #region private void UpdatePresentation()
        /// <summary>
        /// ���������� ���� �����
        /// </summary>
        private void UpdateMode()
        {
            if (mode == GeneralInformationMode.Edit)
            {
                panel1Edit.Visible = true;
                panel1View.Visible = false;
                panel2Edit.Visible = true;
                panel2View.Visible = false;
            }
            else 
                if(mode == GeneralInformationMode.View)
                {
                    panel1Edit.Visible = false;
                    panel1View.Visible = true;
                    panel2Edit.Visible = false;
                    panel2View.Visible = true;
                }
            
        }
        #endregion

        #region private void UpdateDataForDetail()
        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        private void UpdateDataForDetail()
        {
            labelPartNoValue.Text = currentDetail.PartNumber;
            labelSerialNoValue.Text = currentDetail.SerialNumber;
            labelDescriptionValue.Text = currentDetail.Description;
            labelMaintFreqValue.Text = currentDetail.MaintananceType.ToString();
            labelPostionValue.Text = currentDetail.PositionNumber;
            labelModelValue.Text = currentDetail.Model;
            labelManufacturerValue.Text = currentDetail.Manufacturer;
            labelManufactureDateValue.Text = currentDetail.ManufactureDate.ToString();
            labelRemarksValue.Text = currentDetail.Remarks;
        }
        #endregion

      
        #endregion

        
    }
}