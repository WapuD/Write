using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WapuD.Views;

namespace WapuD.ViewModels
{
    public class SignInViewModel
    {
        private readonly UserService _userService;
        private readonly PageService _pageService;
        private readonly DocumentService _documentService;
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorMessageButton { get; set; }

        public SignInViewModel(UserService userService, PageService pageService, DocumentService documentService)
        {
            _userService = userService;
            _pageService = pageService;
            _documentService = documentService;
        }

        public AsyncCommand SignInCommand => new(async () =>
        {
            await Task.Run(async () =>
            {
                if (await _userService.AuthorizationAsync(Username, Password))
                {
                    ErrorMessageButton = string.Empty;
                    await Application.Current.Dispatcher.InvokeAsync(async () =>
                    {
                        MessageBox.Show(UserSetting.Default.UserRole);
                        ErrorMessageButton = string.Empty;
                        await Application.Current.Dispatcher.InvokeAsync(async () =>
                        {
                            if (UserSetting.Default.UserRole == "Клиент")
                                _pageService.ChangePage(new BrowseProductPage());
                            else
                                _pageService.ChangePage(new AdminBrowseProductPage());
                        });
                    });
                }
                else
                    ErrorMessageButton = "Неверный логин или пароль";
            });
        }, bool () =>
        {
            if (string.IsNullOrWhiteSpace(Username)
                || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Пустые поля";
                ErrorMessageButton = string.Empty;
            }
            else
                ErrorMessage = string.Empty;

            if (ErrorMessage.Equals(string.Empty))
                return true; return false;
        });



        public DelegateCommand SignUpCommand => new(async () => _pageService.ChangePage(new SignUpPage()));

    }
}
