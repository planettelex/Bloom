using System;

namespace Bloom.Analytics.HomeModule.ViewModels
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
