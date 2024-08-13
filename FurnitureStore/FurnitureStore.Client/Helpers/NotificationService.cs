using CurrieTechnologies.Razor.SweetAlert2;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Client.Helpers
{
    public class NotificationService(SweetAlertService Swal)
    {
        public async Task DisplaySuccessMessage(string message)
        {
            await Swal.FireAsync(new SweetAlertOptions
            {
                Title = message,
                Icon = SweetAlertIcon.Success,
                Timer = 3000,
                ShowConfirmButton = false
            });
        }
        public async Task DisplayErrorMessage(string message)
        {
            await Swal.FireAsync(new SweetAlertOptions
            {
                Title = message,

                Icon = SweetAlertIcon.Error,
                Timer = 3000,
                ShowConfirmButton = false
            });
        }
        public async Task<bool> DisplayAreUSureMesssage(string message)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = message,
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Yes",
                CancelButtonText = "No"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                return true;
            }
            else if (result.Dismiss == DismissReason.Cancel)
            {
                return false;
            }

            return false; // Default return if no condition is met
        }
    }
    }
