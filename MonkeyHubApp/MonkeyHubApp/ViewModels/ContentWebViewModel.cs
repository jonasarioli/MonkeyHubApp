using MonkeyHubApp.Models;

namespace MonkeyHubApp.ViewModels
{
    public class ContentWebViewModel : BaseViewModel
    {
        public Content Content { get; }

        public ContentWebViewModel(Content content)
        {
            Content = content;
        }
    }
}