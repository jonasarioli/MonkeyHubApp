using MonkeyHubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyHubApp.Services
{
    public interface IMonkeyHubApiService
    {
        Task<List<Tag>> GetTagsAsync();
        Task<List<Content>> GetContentsByTagIdAsync(string tagId);
        Task<List<Content>> GetContentsByFilterAsync(string filter);

    }
}
