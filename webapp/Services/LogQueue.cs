using System.Threading.Channels;

namespace webapp {
    public class LogQueue {
        private readonly Channel<string> _queue = Channel.CreateUnbounded<string>();

        public async Task EnqueueAsync(string log) {
            await _queue.Writer.WriteAsync(log);
        }

        public ChannelReader<string> Reader => _queue.Reader;
    }

    public class  LogWorker : BackgroundService {
        private readonly LogQueue _queue;
        private readonly ILogger<LogWorker> _logger;

        public LogWorker(LogQueue queue, ILogger<LogWorker> logger) {
            _queue = queue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            await foreach(var log in _queue.Reader.ReadAllAsync(stoppingToken)) {
                try {
                    _logger.LogInformation("ログ保存中:{0}", log);

                    // ここで実際のログ保存処理を行う

                } catch (Exception ex) {
                    _logger.LogError(ex, "ログ保存中にエラーが発生しました");
                }
            }
        }
    }

}