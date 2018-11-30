using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Aircrafts.Parts.Templates;
using CAS.Core.Types.Dictionaries;
using CAS.Core.Types.ReportFilters.Templates;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering.DispatcheredUIControls.TemplatesControls;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.TemplatesControls
{
    /// <summary>
    /// �����, ����������� ����������� SB �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredTemplateSBDirectiveListScreen : DispatcheredTemplateDirectiveListView
    {
        /// <summary>
        /// ��������� ������� - ����������� SB �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������� �������, ��� �������� ������������ ���������</param>
        public DispatcheredTemplateSBDirectiveListScreen(TemplateBaseDetail currentItem) : base(currentItem)
        {
        }

        /// <summary>
        /// ��������� ������� - ����������� SB �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredTemplateSBDirectiveListScreen(TemplateAircraft currentItem) : base(currentItem)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return DirectiveTypeCollection.Instance.SBDirectiveType.CommonName; }
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.SBDirectiveType; }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override TemplateDirectiveFilter[] GetCollectionFilters()
        {
            return new TemplateDirectiveFilter[1]{new TemplateSBStatusFilter()};
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredTemplateSBDirectiveListScreen);
        }
    }
}