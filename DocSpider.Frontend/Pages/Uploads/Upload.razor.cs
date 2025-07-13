using DocSpider.Domain.Interfaces;
using DocSpider.Domain.Models.Request;
using DocSpider.Frontend.Commom;
using DocSpider.Frontend.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using MudBlazor;

namespace DocSpider.Frontend.Pages.Uploads;

public class UploadPage : ComponentBase
{
    public bool IsBusy { get; set; } = false;
    public IList<IBrowserFile> _files = new List<IBrowserFile>();
    public bool HasUploadedFiles => _files.Count == 0 ? true : false;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;
    [Inject]
    public IUploadHandler Handler { get; set; } = null!;

    public void UploadFiles(IBrowserFile file)
    {
        _files.Add(file);
    }

    public void SendFiles()
    {
        foreach (var file in _files)
        {
            try
            {
                var formFile = new BrowserFileFormFile(file);
                var request = new UploadDocumentRequest(formFile as IFormFile);
                Handler.UploadFiles(request);
                Snackbar.Add("Document Uploaded", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error to Upload: {ex.Message}", Severity.Error);
            }

        }
    }
}
