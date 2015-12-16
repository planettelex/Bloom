using System;

namespace Bloom.Browser.HomeModule.ViewModels
{
    public class GettingStartedViewModel
    {
        public GettingStartedViewModel(Guid tabId)
        {
            TabId = tabId;
        }

        public Guid TabId { get; set; }
    }
}
