using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CAS.UI.UIControls.AnimatedBackgroundWorker;
using CAS.UI.UIControls.Auxiliary;
using CASTerms;
using MetroFramework.Forms;
using SmartCore.Auxiliary;
using SmartCore.Calculations;
using SmartCore.Entities.Collections;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Attributes;
using SmartCore.Entities.General.Interfaces;
using SmartCore.Filters;

namespace CAS.UI.UIControls.FiltersControls
{
    /// <summary>
    /// ����� ����� ��� �������������� ��������
    /// </summary>
    public partial class CommonFilterForm : MetroForm
    {

        #region Fields

        private readonly AnimatedThreadWorker _animatedThreadWorker = new AnimatedThreadWorker();
        
        private readonly CommonFilterCollection _filters;
        private readonly ICommonCollection _items;

        #endregion

        #region Constructors

        #region private CommonFilterForm()

        /// <summary>
        /// ������� ����� ��� �������� ������� �� � ������� ���� ������
        /// </summary>
        private CommonFilterForm()
        {
	        DoubleBuffered = true;
            InitializeComponent();
        }

        #endregion

        #region public CommonFilterForm(CommonFilterCollection filterCollection) : this()
        /// <summary>
        /// ������� ����� ��� ���������� ��������� ����������� ����
        /// </summary>
        public CommonFilterForm(CommonFilterCollection filterCollection, ICommonCollection items = null)
            : this()
        {
            if (filterCollection == null)
                throw new ArgumentNullException("filterCollection", "can not be null");
            _filters = filterCollection;
            _items = items;

            _animatedThreadWorker.DoWork += AnimatedThreadWorkerDoLoad;
            _animatedThreadWorker.RunWorkerCompleted += BackgroundWorkerRunWorkerLoadCompleted;

            _animatedThreadWorker.RunWorkerAsync();
        }

        #endregion

        #endregion

        #region Properties

        #endregion

        #region Methods

        #region public static CommonFilterForm GetInstanceForType<T>() where T : BaseEntityObject
        ///<summary>
        /// ���������� ������ ����� ��� ���������� ��������� ����������� ����
        ///</summary>
        ///<typeparam name="T">���-�������� �������� ��� ��������� ����������</typeparam>
        ///<returns>������ ����� ��� ���������� ��������� ����������� ����</returns>
        public static CommonFilterForm GetInstanceForType<T>() where T : BaseEntityObject
        {
            return new CommonFilterForm();
        }
        #endregion

        #region private Control GetControl(ICommonFilter commonFilter)
        private Control GetControl(ICommonFilter commonFilter)
        {
            #region  �� ��� ������� �����

            if (commonFilter is CommonFilter<bool>)
            {
                var boolFilter = commonFilter as CommonFilter<bool>;
	            var boolFilterControl = new CommonBoolFilterControl(boolFilter)
                {
                    AutoSize = true,
                    MinimumSize = new Size(20, 17),
                    Tag = commonFilter.FilterProperty,
                };
                return boolFilterControl;
            }

            if (commonFilter is CommonFilter<int>)
            {
                var stringFilter = commonFilter as CommonFilter<int>;
                var intFilterControl = new CommonIntFilterControl(stringFilter)
                {
                    AutoSize = true,
                    MinimumSize = new Size(20, 17),
                    Tag = commonFilter.FilterProperty,
                };
                return intFilterControl;
            }

            if (commonFilter is CommonFilter<string>)
            {
                var stringFilter = commonFilter as CommonFilter<string>;
                var stringFilterControl = new CommonStringFilterControl(stringFilter)
                {
                    AutoSize = true,
                    MinimumSize = new Size(20, 17),
                    Tag = commonFilter.FilterProperty,
                };
                return stringFilterControl;
            }
            if (commonFilter is CommonFilter<DateTime>)
            {
                var dateFilter = commonFilter as CommonFilter<DateTime>;
	            DateTime minDate = DateTimeExtend.GetCASMinDateTime();
	            DateTime maxDate = DateTime.Today;

	            if (_items != null && _items.Count > 0 &&
	                commonFilter.FilterProperty.PropertyType.Name == typeof(DateTime).Name)
	            {
					var p = commonFilter.FilterProperty;

		            var preCollection = _items.OfType<BaseEntityObject>()
											  .Select(o => p.GetValue(o, null))
											  .OfType<DateTime>()
											  .OrderBy(time => time)
											  .ToList();

		            var preMaxDate = preCollection[preCollection.Count -1];
		            var preMinDate = preCollection[0];

		            if (preMinDate > minDate)
			            minDate = preMinDate;

					if(preMaxDate > maxDate)
						maxDate = preMaxDate;
				}

                var dateFilterControl = new CommonDateTimePeriodFilterControl(dateFilter, minDate, maxDate)
                {
                    AutoSize = true,
                    MinimumSize = new Size(20, 17),
                    Tag = commonFilter.FilterProperty,
                };
                return dateFilterControl;
            }
            #endregion

            #region �� ��� ENUM, IDictionaryItem, IBaseEntityObject

            Type filterType = commonFilter.GetType();
            if (filterType.IsGenericType)
            {
                Type genericArgumentType = filterType.GetGenericArguments().First();
                if (genericArgumentType.IsEnum)
                {
                    try
                    {
                        var enumFilterControl = new CommonEnumFilterControl(commonFilter)
                        {
                            AutoSize = true,
                            MinimumSize = new Size(20, 17),
                            Tag = commonFilter.FilterProperty,
                        };
                        return enumFilterControl;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                if (genericArgumentType.GetInterface(typeof(IDictionaryItem).Name) != null)
                {
                    try
                    {
                        var dictionaryFilter = (ICommonFilter<IDictionaryItem>)commonFilter;
						IDictionaryItem[] values = null;
						if (commonFilter.FilterProperty.PropertyType.GetInterface(typeof(IEnumerable<>).Name) != null)
	                    {
							Type t = commonFilter.FilterProperty.PropertyType;
							if (t != null)
							{
								Type filterPropertyGenericArgument = t.GetGenericArguments().FirstOrDefault();
								if (filterPropertyGenericArgument != null &&
								   filterPropertyGenericArgument.GetInterface(typeof(IDictionaryItem).Name) != null)
								{
									PropertyInfo p = commonFilter.FilterProperty;
									values = _items.OfType<BaseEntityObject>()
										.Select(o => p.GetValue(o, null))
										.Where(o => o != null)
										.Cast<IEnumerable>()
										.SelectMany(enumerable => enumerable.OfType<IDictionaryItem>())
										.Select(o => o)
										.ToArray();
								}
							}
						}
	                    else
	                    {
							PropertyInfo p = commonFilter.FilterProperty;
							values = _items.OfType<BaseEntityObject>()
									  .Select(o => p.GetValue(o, null))
									  .OfType<IDictionaryItem>()
									  .Distinct()
									  .ToArray();
						}


                        var dictionaryFilterControl = new CommonDictionaryFilterControl(dictionaryFilter, values)
                        {
                            AutoSize = true,
                            MinimumSize = new Size(20, 17),
                            Tag = commonFilter.FilterProperty,
                        };
                        return dictionaryFilterControl;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                if (genericArgumentType.GetInterface(typeof(IBaseEntityObject).Name) != null)
                {
                    try
                    {
                        var entitiesFilter = commonFilter as ICommonFilter<IBaseEntityObject>;
                        BaseEntityObject[] values = null;
                        if (_items != null && _items.Count > 0)
                        {
                            if(commonFilter.FilterProperty.PropertyType.GetInterface(typeof(IEnumerable<>).Name) != null)
                            {
                                //�������� ���� �������� ������������� �����, ����� ��������� �������� ��������
                                //�������� ��� ��������� ������� �������  
                                Type t = commonFilter.FilterProperty.PropertyType;
                                while (t != null)
                                {
                                    if (t.IsGenericType)
                                        break;
                                    t = t.BaseType;
                                }

                                if(t != null)
                                {
                                    Type filterPropertyGenericArgument = t.GetGenericArguments().FirstOrDefault();
                                    if(filterPropertyGenericArgument != null &&
                                       filterPropertyGenericArgument.GetInterface(typeof(IBaseEntityObject).Name) != null)
                                    {
                                        PropertyInfo p = commonFilter.FilterProperty;

                                        values = _items.OfType<BaseEntityObject>()
                                            .Select(o => p.GetValue(o, null))
                                            .Where(o => o != null)
                                            .Cast<IEnumerable>()
                                            .SelectMany(enumerable => enumerable.OfType<BaseEntityObject>())
                                            .Select(o => o)
                                            .ToArray();
                                    }    
                                }

                            }
                            if(commonFilter.FilterProperty.PropertyType.GetInterface(typeof(IBaseEntityObject).Name) != null)
                            {
                                PropertyInfo p = commonFilter.FilterProperty;

                                values = _items.OfType<BaseEntityObject>()
                                               .Select(o => p.GetValue(o, null))
                                               .OfType<BaseEntityObject>()
                                               .ToArray();
                            }
                        }

                        var baseEntityObjectFilterControl = new CommonBaseEntityObjectFilterControl(entitiesFilter, values)
                        {
                            AutoSize = true,
                            MinimumSize = new Size(20, 17),
                            Tag = commonFilter.FilterProperty,
                        };
                        return baseEntityObjectFilterControl;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }

            #endregion

            #region  �� ��� CommonFilter<Lifelength>

            if (commonFilter is CommonFilter<Lifelength>)
            {
                var lifelengthFilter = commonFilter as CommonFilter<Lifelength>;
                Lifelength[] values = null;
                if (_items != null && _items.Count > 0 && 
                    commonFilter.FilterProperty.PropertyType.Name == typeof(Lifelength).Name)
                {
                    PropertyInfo p = commonFilter.FilterProperty;

                    values = _items.OfType<BaseEntityObject>()
                                   .Select(o => p.GetValue(o, null))
                                   .OfType<Lifelength>()
                                   .ToArray();
                }

                var stringFilterControl = new CommonLifelengthFilterControl(lifelengthFilter, values)
                {
                    AutoSize = true,
                    MinimumSize = new Size(20, 17),
                    Tag = commonFilter.FilterProperty,
                };
                return stringFilterControl;
            }

            #endregion

            #region  �� ��� CommonFilter<BaseEntityObject>

            if (commonFilter is CommonFilter<IBaseEntityObject>)
            {
                var lifelengthFilter = commonFilter as CommonFilter<IBaseEntityObject>;
                BaseEntityObject[] values = null;
                if (_items != null && _items.Count > 0 &&
                    commonFilter.FilterProperty.PropertyType.Name == typeof(Lifelength).Name)
                {
                    PropertyInfo p = commonFilter.FilterProperty;

                    values = _items.OfType<BaseEntityObject>()
                                   .Select(o => p.GetValue(o, null))
                                   .OfType<BaseEntityObject>()
                                   .ToArray();
                }

                var baseEntityObjectFilterControl = new CommonBaseEntityObjectFilterControl(lifelengthFilter, values)
                {
                    AutoSize = true,
                    MinimumSize = new Size(20, 17),
                    Tag = commonFilter.FilterProperty,
                };
                return baseEntityObjectFilterControl;
            }

            #endregion

            return null;
        }
        #endregion

        #region private bool GetChangeStatus()
        /// <summary>
        /// ���������� ��������, ������������ ���� �� ��������� � ������ �������� ����������
        /// </summary>
        /// <returns></returns>
        private bool GetChangeStatus()
        {
            if (panelMain.Controls.OfType<CommonFilterControl>().Any(control => control.GetChangeStatus()))
                return true;
            if (_filters.FilterTypeAnd && radioButtonOr.Checked || 
                !_filters.FilterTypeAnd && !radioButtonOr.Checked)
                return true;

            return false;
        }

        #endregion

        #region private bool ValidateData(out string message)
        /// <summary>
        /// ���������� ��������, ������������ �������� �� �������� �������� ���������� ����������
        /// </summary>
        /// <returns></returns>
        private bool ValidateData(out string message)
        {
            message = "";
            foreach (Control control in panelMain.Controls)
            {
                if (control is CommonFilterControl)
                {
                    var csfc = (CommonFilterControl)control;
                    string m;
                    
                    if(!csfc.CheckData(out m))
                    {
                        if (message != "") 
                            message += "\n ";
                        message += m;
                        control.Focus();
                        return false;    
                    }
                }
            }

            return true;
        }

        #endregion

        #region private void ApplyChanges()
        /// <summary>
        /// ��������� � ������� ��������� ��������� �� ��������. 
        /// ���� �� ��� ������ ������������� ������� ����� (�������� ��� ����� �����), �������� ������� �� ����������, ������������ false
        /// ����� base.ApplyChanges() ����������
        /// </summary>
        /// <returns></returns>
        private void ApplyChanges()
        {
            foreach (CommonFilterControl control in panelMain.Controls.OfType<CommonFilterControl>())
            {
                if(control.GetChangeStatus())
                    control.ApplyChanges();
            }

            _filters.FilterTypeAnd = radioButtonAnd.Checked;
        }
        #endregion

        #region private void BackgroundWorkerRunWorkerLoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        private void BackgroundWorkerRunWorkerLoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        #endregion

        #region private void AnimatedThreadWorkerDoLoad(object sender, DoWorkEventArgs e)
        private void AnimatedThreadWorkerDoLoad(object sender, DoWorkEventArgs e)
        {
            _animatedThreadWorker.ReportProgress(0, "load templates");

            _animatedThreadWorker.ReportProgress(100, "binding complete");
        }
        #endregion

        #region private void buttonCancel_Click(object sender, EventArgs e)

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region private void ButtonOkClick(object sender, EventArgs e)

        private void ButtonOkClick(object sender, EventArgs e)
        {
            string message;
            if (!ValidateData(out message))
            {
                message += "\nAbort operation";
                MessageBox.Show(message, (string)new GlobalTermsProvider()["SystemName"],
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (GetChangeStatus())
            {
                ApplyChanges();
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        #region private void ButtonClearFilterClick(object sender, EventArgs e)

        private void ButtonClearFilterClick(object sender, EventArgs e)
        {
            foreach (CommonFilterControl control in panelMain.Controls.OfType<CommonFilterControl>())
            {
                control.ClearFilter();
            }
        }
        #endregion

        #region private void Form_Deactivate(object sender, EventArgs e)

        private void Form_Deactivate(object sender, EventArgs e)
        {
            StaticWaitFormProvider.WaitForm.Visible = false;
        }
        #endregion

        #region private void Form_Activated(object sender, EventArgs e)
        private void Form_Activated(object sender, EventArgs e)
        {
            if (StaticWaitFormProvider.IsActive)
                StaticWaitFormProvider.WaitForm.Visible = true;
        }
        #endregion

        #endregion

        #region private void FormLoad(object sender, EventArgs e)

        private void FormLoad(object sender, EventArgs e)
        {
			if (Owner != null)
				MaximumSize = Owner.Size;

            if (_filters == null) return;

            panelMain.Controls.Clear();
            int columnCount = _filters.Count > 5 ? 2 : 1;//���������� ������� ��

            var labels = new List<Label>();
            var controls = new List<Control>();
            var pairControls = new Dictionary<PropertyInfo, Control>();
            string errorMessage = "";

            if(string.IsNullOrEmpty(Text))
                Text = $"{_filters.FilteredType.Name} Filtered Form";

            if (!_filters.Select(f => f.FilterProperty).Any()) return;
            foreach (ICommonFilter t in _filters)
            {
                //PropertyInfo pairProperty = null;
                var attr = (FilterAttribute)t.FilterProperty.GetCustomAttributes(typeof(FilterAttribute), false).First();
                labels.Add(new Label { Text = attr.Title, AutoSize = true });

                //���� �������� ������ ���� �� ������ ��� �� NULL
                //�� ����� ������ ���������������� � ������(BOLD)
                var notNullAttribute = (NotNullAttribute)t.FilterProperty.GetCustomAttributes(typeof(NotNullAttribute), false).FirstOrDefault();
                if (notNullAttribute != null)
                    labels.Last().Font = new Font(labels.Last().Font, FontStyle.Bold);

                Control c = null;
                //if (!string.IsNullOrEmpty(attr.PairControlPropertyName))
                //    pairProperty = _currentObject.GetType().GetProperty(attr.PairControlPropertyName);

                //if (pairProperty != null)
                //{
                //    try
                //    {
                //        c = GetControl(_currentObject, pairProperty, attr.PairControlWidht, 1, attr.PairControlEnabled);
                //    }
                //    catch (Exception ex)
                //    {
                //        if (errorMessage != "") errorMessage += "\n ";
                //        errorMessage += string.Format("Pair property '{0}' raise error {1}", attr.PairControlPropertyName, ex.Message);
                //    }
                //    if (c is ThresholdControl) columnCount = 2;
                //    pairControls.Add(t, c);
                //}

                //c = null;

                try
                {
                    //int cw = pairProperty != null ? attr.Width - attr.PairControlWidht - 5 : attr.Width;
                    c = GetControl(t);
                }
                catch (Exception ex)
                {
                    if (errorMessage != "") errorMessage += "\n ";
                    errorMessage += $"'{attr.Title}' raise error {ex.Message}";
                }
                if (c is LifelengthViewer) ((LifelengthViewer)c).LeftHeader = attr.Title;
                if (c is ThresholdControl) columnCount = 2;
                controls.Add(c);
            }

            #region ���������� ��������� � ���� �������
            if (columnCount == 1)
            {
                #region ������ ����� ������� � ���������

                int maxLabelXSize = 0;
                int maxControlXSize = 0;//������������ ����� ��
                for (int i = 0; i < labels.Count; i++)
                {
                    //�� ������ ����� ���������� �� ������ ��������, 
                    //������� ��� ��������� ��������������� ������������
                    if (controls[i] != null && controls[i] is LifelengthViewer)
                    {
                        var llv = (LifelengthViewer)controls[i];
                        if (llv.LeftHeaderWidth > maxLabelXSize)
                            maxLabelXSize = llv.LeftHeaderWidth;
                        if (llv.WidthWithoutLeftHeader > maxControlXSize)
                            maxControlXSize = llv.WidthWithoutLeftHeader;
                        if (i > 0 && controls[i - 1] != null && controls[i - 1] is LifelengthViewer)
                            llv.ShowHeaders = false;
                        continue;
                    }
                    //���� ��������������� ��������, ������� ����������� ������
                    //������ ���� �������� ����� ������
                    if (labels[i].PreferredWidth > maxLabelXSize)
                        maxLabelXSize = labels[i].PreferredWidth;
                    //������� ���������
                    if (controls[i] != null && controls[i].Size.Width > maxControlXSize)
                    {
                        maxControlXSize = controls[i].Size.Width;
                    }
                }
                #endregion

                for (int i = 0; i < _filters.Count; i++)
                {
                    Control pairControl = null;
                    if (pairControls.ContainsKey(_filters[i].FilterProperty))
                        pairControl = pairControls[_filters[i].FilterProperty];

                    if (i == 0)
                    {
                        labels[i].Location = new Point(3, 9);
                        if (controls[i] != null)
                        {
                            if (controls[i] is LifelengthViewer)
                            {
                                controls[i].Location =
                                    new Point((maxLabelXSize + labels[i].Location.X) - ((LifelengthViewer)controls[i]).LeftHeaderWidth, 3);
                            }
                            else
                            {
                                if (pairControl != null)
                                {
                                    pairControl.Location = new Point(maxLabelXSize + labels[i].Location.X + 5, 3);

                                    controls[i].Location = new Point(pairControl.Location.X + pairControl.Size.Width + 5, 3);
                                    controls[i].Width = maxControlXSize - (pairControl.Size.Width + 5);
                                }
                                else
                                {
                                    controls[i].Location = new Point(maxLabelXSize + labels[i].Location.X + 5, 3);
                                    controls[i].Width = maxControlXSize;
                                }
                            }
                        }
                    }
                    else
                    {
                        Point labelLocation = new Point(3, 0);
                        if (controls[i - 1] != null)
                            labelLocation.Y = controls[i - 1].Location.Y + controls[i - 1].Size.Height + 11;
                        else labelLocation.Y = labels[i - 1].Location.Y + labels[i - 1].Size.Height + 11;

                        labels[i].Location = labelLocation;
                        if (controls[i] != null)
                        {
                            if (controls[i] is LifelengthViewer)
                            {
                                controls[i].Location =
                                    new Point((maxLabelXSize + labelLocation.X) - ((LifelengthViewer)controls[i]).LeftHeaderWidth, labelLocation.Y - 6);
                            }
                            else
                            {
                                if (pairControl != null)
                                {
                                    pairControl.Location = new Point(maxLabelXSize + labelLocation.X + 5, labelLocation.Y - 6);

                                    controls[i].Location = new Point(pairControl.Location.X + pairControl.Size.Width + 5, labelLocation.Y - 6);
                                    controls[i].Width = maxControlXSize - (pairControl.Size.Width + 5);
                                }
                                else
                                {
                                    controls[i].Location = new Point(maxLabelXSize + labelLocation.X + 5, labelLocation.Y - 6);
                                    controls[i].Width = maxControlXSize;
                                }
                            }
                        }
                    }


                    if (controls[i] == null || (controls[i] != null && !(controls[i] is LifelengthViewer)))
                    {
                        //���� ������� �� �������� LifelengthViewer-�� �� ����� �������� �����
                        panelMain.Controls.Add(labels[i]);
                    }
                    if (pairControl != null)
                        panelMain.Controls.Add(pairControl);

                    panelMain.Controls.Add(controls[i]);
                }
            }
            #endregion

            #region ���������� ��������� � ��� �������
            if (columnCount == 2)
            {
                #region ������ ����� ������� � ���������

                int fMaxLabelXSize = 0, sMaxLabelXSize = 0;
                int fMaxControlXSize = 0, sMaxControlXSize = 0;
                bool checkFirst = true;//���� ����������� �������

                for (int i = 0; i < labels.Count; i++)
                {
                    //�� ������ ����� ���������� �� ������ ��������, 
                    //������� ��� ��������� ��������������� ������������
                    if (controls[i] != null && controls[i] is ThresholdControl)
                    {
                        //������� ������ ���������� ���������
                        //������ �� ����� ������� ����� �������
                        var ddtc = (ThresholdControl)controls[i];
                        //������� ����������
                        if (ddtc.MaxFirstColLabelWidth > fMaxLabelXSize)
                            fMaxLabelXSize = ddtc.MaxFirstColLabelWidth;
                        if (ddtc.MaxSecondColLabelWidth > sMaxLabelXSize)
                            sMaxLabelXSize = ddtc.MaxSecondColLabelWidth;

                        //������� ���������
                        if (ddtc.MaxFirstColControlWidth > fMaxControlXSize)
                            fMaxControlXSize = ddtc.MaxFirstColControlWidth;
                        if (ddtc.MaxSecondColControlWidth > sMaxControlXSize)
                            sMaxControlXSize = ddtc.MaxSecondColControlWidth;

                        //�.�. DetailDirectiveThresholdControl �������� 2 �������, 
                        //�� ��������� ������� ������ ����� ������������� � ������ �������
                        checkFirst = true;
                        continue;
                    }

                    if (controls[i] != null && controls[i] is LifelengthViewer)
                    {
                        var llv = (LifelengthViewer)controls[i];

                        if (checkFirst && llv.LeftHeaderWidth > fMaxLabelXSize)
                            fMaxLabelXSize = llv.LeftHeaderWidth;
                        if (!checkFirst && llv.LeftHeaderWidth > sMaxLabelXSize)
                            sMaxLabelXSize = llv.LeftHeaderWidth;

                        //������� ���������
                        if (checkFirst && llv.WidthWithoutLeftHeader > fMaxControlXSize)
                            fMaxControlXSize = llv.WidthWithoutLeftHeader;
                        if (!checkFirst && llv.WidthWithoutLeftHeader > sMaxControlXSize)
                            sMaxControlXSize = llv.WidthWithoutLeftHeader;

                        checkFirst = !checkFirst;
                        continue;
                    }

                    //���� ��������������� ��������, ������� ����������� ������
                    //������ ���� �������� ����� ������
                    if (checkFirst && labels[i].PreferredWidth > fMaxLabelXSize)
                        fMaxLabelXSize = labels[i].PreferredWidth;
                    if (!checkFirst && labels[i].PreferredWidth > sMaxLabelXSize)
                        sMaxLabelXSize = labels[i].PreferredWidth;

                    //������� ���������
                    if (controls[i] != null)
                    {
                        if (checkFirst && controls[i].Size.Width > fMaxControlXSize)
                            fMaxControlXSize = controls[i].Size.Width;
                        if (!checkFirst && controls[i].Size.Width > sMaxControlXSize)
                            sMaxControlXSize = controls[i].Size.Width;
                    }

                    checkFirst = !checkFirst;
                }
                #endregion

                checkFirst = true;
                const int firstCol = 3;
                int secondCol = (3 + fMaxLabelXSize + 5 + fMaxControlXSize + 15);
                for (int i = 0; i < labels.Count; i++)
                {
                    int top, left, labelSize, controlSize;
                    if (i == 0)
                    {
                        top = 3;
                        left = firstCol;
                        labelSize = fMaxLabelXSize;
                        controlSize = fMaxControlXSize;
                    }
                    else
                    {
                        if (checkFirst || (controls[i] != null && controls[i] is ThresholdControl))
                        {
                            //����������� ������ ������ ��� 
                            //������� �������� ��������� ����������� ������� ����������
                            //(������� ������� ���������� ������������� � ������ �������)
                            left = firstCol;
                            labelSize = fMaxLabelXSize;
                            controlSize = fMaxControlXSize;

                            //����������� ������ ������� Bottoma 2-� ���������� ���������
                            int bottom1, bottom2;
                            //����������� ������ ����� ����������� �������� 
                            //(�� ����� ���� �� ������ ������� ����������� ����, ���� �������� ���� ���)
                            if (controls[i - 1] != null)
                            {
                                if(controls[i - 1].Left <= (firstCol + fMaxLabelXSize + 5))
                                {
                                    //���� ����. ������� ����� ������������� � ������ �������
                                    //(�������� 2 �������), �� ������� ������ ����� ������ ����� ����. ��������
                                    bottom2 = controls[i - 1].Bottom + 5;
                                }
                                else
                                {
                                    //����. ������� ������������� �� ������ �������
                                    if(controls[i] is ThresholdControl)
                                    {
                                        //������� ������� �������� ��������� ��������� ��������
                                        //������ ��� ������� ����� ����� ������������� �� ������ ����� ���� ��������
                                        bottom2 = controls[i - 1].Bottom + 5;    
                                    }
                                    else
                                    {
                                        if ((i - 2) >= 0)
                                        {
                                            if (controls[i - 2] != null)
                                                bottom2 = controls[i - 2].Bottom + 5;
                                            else bottom2 = labels[i - 2].Bottom + 5;
                                        }
                                        else bottom2 = 0;
                                    }
                                }
                            }
                            else bottom2 = labels[i - 1].Bottom + 5;
                            //����������� ������ ����� ����-����������� ��������
                            //�� ����� � �������������
                            if ((i - 2) >= 0)
                            {
                                if (controls[i - 2] != null)
                                    bottom1 = controls[i - 2].Bottom + 5;
                                else bottom1 = labels[i - 2].Bottom + 5;
                            }
                            else bottom1 = 0;
                            top = bottom1 > bottom2 ? bottom1 : bottom2;
                        }
                        else
                        {
                            left = secondCol;
                            labelSize = sMaxLabelXSize;
                            controlSize = sMaxControlXSize;

                            if ((i - 2) >= 0)
                            {
                                if (controls[i - 2] != null)
                                    top = controls[i - 2].Bottom + 5;
                                else top = labels[i - 2].Bottom + 5;
                            }
                            else top = controls[i - 1] != null ? controls[i - 1].Location.Y : labels[i - 1].Location.Y;
                        }
                    }

                    if (controls[i] != null && controls[i] is ThresholdControl)
                    {
                        var ddtc = (ThresholdControl)controls[i];
                        controls[i].Location = new Point(3, top);
                        //������������ ������ �������
                        ddtc.SetFirstColumnPos(firstCol + fMaxLabelXSize);
                        //������������ ������ �������
                        ddtc.SetSecondColumnPos(secondCol + sMaxLabelXSize);

                        panelMain.Controls.Add(controls[i]);
                        checkFirst = true;
                        continue;
                    }

                    if (controls[i] != null && controls[i] is LifelengthViewer)
                    {
                        controls[i].Location =
                            new Point((labelSize + left) - ((LifelengthViewer)controls[i]).LeftHeaderWidth, top);
                        panelMain.Controls.Add(controls[i]);
                        checkFirst = !checkFirst;
                        continue;
                    }

                    labels[i].Location = new Point(left, top);
                    if (controls[i] != null)
                    {
                        controls[i].Location = new Point(labelSize + left + 5, top);
                        controls[i].Width = controlSize;
                    }
                    panelMain.Controls.Add(labels[i]);
                    panelMain.Controls.Add(controls[i]);
                    checkFirst = !checkFirst;
                }
            }
            #endregion

            if (errorMessage != "")
            {
                MessageBox.Show(errorMessage, (string)new GlobalTermsProvider()["SystemName"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            radioButtonAnd.Checked = _filters.FilterTypeAnd;
            radioButtonOr.Checked = !radioButtonAnd.Checked;

	        var maxSize = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
	        if (Width > maxSize.Width || Height > maxSize.Height)
	        {
				//������� ������ ����� ��������� � ��������� ���������� , �.� ����� ���������� AutoSize ������� ������ ����� ����������
		        var currentSize = Size;
				//��������� �������� � ����� � � ������ ��� ����� ��������
				AutoSize = false;
				panelMain.AutoSize = false;
				panelMain.AutoScroll = true;
				//�������� ������� �����
				if (Width > maxSize.Width && Height > maxSize.Height)
		        {
					//������ � ������ ����� ������ ������� ������� ������� ������
					//� ������ ������ ������������ ������� ��� ���������� ������ ����� ��������� ������
					Width = maxSize.Width;
			        Height = maxSize.Height;
		        }
				else if (Width > maxSize.Width)
				{
					//������ ����� ������ ������ ������� �������
					Width = maxSize.Width;
					Height = currentSize.Height;
					//���� ���������, ����� �� �������� �������������� scrollbar ��� ������� �������������
					if (Height + 20 <= maxSize.Height)
						Height += 20;
				}
			    else // (Height > maxSize.Height)
				{
					//������ ����� ������ ������ ������� �������
					Height = maxSize.Height;
					Width = currentSize.Width;
					//���� ���������, ����� �� �������� ������������ scrollbar ��� ������� ���������������
					if (Width + 20 <= maxSize.Width)
						Width += 20;
				}
				//������ ������� ������ : � ������ �������� 20 ����� ������ ������ ��������� , � � ������ 85 ����� ������ ������ � ��������
				panelMain.Width = Width - 20;
				panelMain.Height = Height - 105;

			}

			if (Owner != null)
				Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
									 Owner.Location.Y + Owner.Height / 2 - Height / 2);

		}
		#endregion
	}
}