using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Channels;

namespace webapp.Pages {
    public class IndexModel : PageModel {
    //    private readonly ILogger<IndexModel> _logger;

        private readonly LogQueue _queue;
        public string Message { get; set; }

        public IndexModel(LogQueue queue) {
            _queue = queue;
            Message = string.Empty;
        }

        public void OnGet() {
            _queue.EnqueueAsync("Indexページにアクセスされました");
            Message = "アクセスしました";
        }
    }
}
