using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Aircrafts.Parts.Templates;
using CAS.Core.Types.Dictionaries;
using CAS.Core.Types.ReportFilters.Templates;
using CAS.UI.Interfaces;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.TemplatesControls
{
    /// <summary>
    /// �����, ����������� ����������� ADStatus �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredTemplateADDirectiveListScreen : DispatcheredTemplateDirectiveListView
    {
        /// <summary>
        /// ��������� ������� - ����������� ADStatus �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������� �������, ��� �������� ������������ ���������</param>
        public DispatcheredTemplateADDirectiveListScreen(TemplateBaseDetail currentItem) : base(currentItem)
        {
        }

        /// <summary>
        /// ��������� ������� - ����������� ADStatus �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredTemplateADDirectiveListScreen(TemplateAircraft currentItem) : base(currentItem)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return DirectiveTypeCollection.Instance.ADDirectiveType.CommonName; }
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.ADDirectiveType; }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override TemplateDirectiveFilter[] GetCollectionFilters()
        {
            return new TemplateDirectiveFilter[1]{new TemplateADStatusFilter()};
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredTemplateADDirectiveListScreen);
        }
    }
}