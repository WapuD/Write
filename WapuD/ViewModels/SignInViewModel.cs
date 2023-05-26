using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WapuD.ViewModels
{
    internal class SignInViewModel
    {
        private readonly PageService _pageService;
        public string Username { get; set; }
        public string Password { get; set; }
        public SignInViewModel(PageService pageService)
        {
            _pageService = pageService;
        }
    }
}
