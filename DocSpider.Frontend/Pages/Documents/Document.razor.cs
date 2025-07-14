using DocSpider.Domain.DTOs;
using DocSpider.Domain.Interfaces;
using DocSpider.Domain.Models.Request;
using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace DocSpider.Frontend.Pages.Documents
{
    public class DocumentPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<DocumentDTO> Documents { get; set; } = [];
        public string SearchTerm { get; set; } = string.Empty;

        #endregion


        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;
        [Inject]
        public IGetDocumentHandler Handler { get; set; } = null!;
        

        #endregion

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetDocumentRequest();
                var result = await Handler.GetDocumentsAsync(request);
                if (result.IsSuccess)
                    Documents = result.Data ?? [];
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Func<DocumentDTO, bool> Filter => doc =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;

            if (doc.DocumentName.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (doc.DocumentDescription.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };
    }
}
