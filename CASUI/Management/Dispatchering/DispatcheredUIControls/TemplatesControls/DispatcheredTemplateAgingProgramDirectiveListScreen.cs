using CAS.Core.Types.Aircrafts;
using CAS.Core.Types.Aircrafts.Parts.Templates;
using CAS.Core.Types.Dictionaries;
using CAS.Core.Types.ReportFilters.Templates;
using CAS.UI.Interfaces;

namespace CAS.UI.Management.Dispatchering.DispatcheredUIControls.TemplatesControls
{
    /// <summary>
    /// �����, ����������� ����������� Aging Propgram �������� ��� �������� ��������
    /// </summary>
    public class DispatcheredTemplateAgingProgramDirectiveListScreen : DispatcheredTemplateDirectiveListView
    {
        /// <summary>
        /// ��������� ������� - ����������� Aging Propgram �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">������� �������, ��� �������� ������������ ���������</param>
        public DispatcheredTemplateAgingProgramDirectiveListScreen(TemplateBaseDetail currentItem) : base(currentItem)
        {
        }

        /// <summary>
        /// ��������� ������� - ����������� Aging Propgram �������� ��� �������� ��������
        /// </summary>
        /// <param name="currentItem">��, ��� �������� ������������ ���������</param>
        public DispatcheredTemplateAgingProgramDirectiveListScreen(TemplateAircraft currentItem) : base(currentItem)
        {
        }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public override string ReportTitileText
        {
            get { return DirectiveTypeCollection.Instance.AgineProgramDirectiveType.CommonName; }
        }

        /// <summary>
        /// ��� ��������� �� ���������
        /// </summary>
        public override DirectiveType DirectiveDefaultType
        {
            get { return DirectiveTypeCollection.Instance.AgineProgramDirectiveType; }
        }

        /// <summary>
        /// ���������� ��������� �������� ��� �����������
        /// </summary>
        /// <returns></returns>
        protected override TemplateDirectiveFilter[] GetCollectionFilters()
        {
            return new TemplateDirectiveFilter[1]{new TemplateAgingProgramFilter()};
        }

        /// <summary>
        /// ��� �� ��� � ������� ��� � ������� ������ ��� ���
        /// </summary>
        /// <param name="obj">����������� ������</param>
        /// <returns></returns>
        protected override bool IsSameType(IDisplayingEntity obj)
        {
            return (obj is DispatcheredTemplateAgingProgramDirectiveListScreen);
        }
    }
}