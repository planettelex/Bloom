using System;

namespace Bloom.Analytics.HomeModule.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel(Guid tabId)
        {
            TabId = tabId;
        }

        public Guid TabId { get; set; }
    }
}
