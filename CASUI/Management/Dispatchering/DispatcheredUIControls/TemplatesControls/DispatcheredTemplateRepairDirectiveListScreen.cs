using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Aircrafts.Parts.Templates;
using CAS.Core.Types.Dictionaries;
using CAS.Core.Types.ReportFilters.Templates;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering.DispatcheredUIControls.TemplatesControls;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.TemplatesControls
{
    /// <summary>
    /// �����, ����������� ����������� Repair �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredTemplateRepairDirectiveListScreen : DispatcheredTemplateDirectiveListView
    {
        /// <summary>
        /// ��������� ������� - ����������� Repair �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������� �������, ��� �������� ������������ ���������</param>
        public DispatcheredTemplateRepairDirectiveListScreen(TemplateBaseDetail currentItem) : base(currentItem)
        {
        }

        /// <summary>
        /// ��������� ������� - ����������� Repair �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredTemplateRepairDirectiveListScreen(TemplateAircraft currentItem) : base(currentItem)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return DirectiveTypeCollection.Instance.RepairDirectiveType.CommonName; }
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.RepairDirectiveType; }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override TemplateDirectiveFilter[] GetCollectionFilters()
        {
            return new TemplateDirectiveFilter[1]{new TemplateRepairFilter()};
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredTemplateRepairDirectiveListScreen);
        }
    }
}