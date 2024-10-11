using DownloadSolution.Utilities;
using DownloadVideoYouTobe.Modals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection.Metadata;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace DownloadVideoYouTobe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DownloadVideoController : ControllerBase
    {
        private readonly string? _outputDefautl;
        private readonly ILogger<DownloadVideoController> _logger;
        public DownloadVideoController(IConfiguration config, ILogger<DownloadVideoController> logger)
        {
            _outputDefautl = config["AppSettings:OutputDirectory"];
            _logger = logger;
        }

        [HttpPost]
        [ExceptionFilters]
        public async Task<IActionResult> DownloadVideoYouTube([FromBody] ReqDownloadYT req)
        {
            var dir = Directory.GetCurrentDirectory();
            if (!ModelState.IsValid)
                return BadRequest(new ErrorModel("ModelState IsValid", ""));

            var res = await DownloadYouTubeVideo(req.LinkYT, req.OutputDirectory, req.isHighestVideoQuality);

            return Ok(res);
        }

        private async Task<bool> UrlExistsAsync(string? url)
        {
            using (HttpClient client = new HttpClient())
            {
                if (string.IsNullOrEmpty(url))
                    return false;

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        private async Task<ResultModel<bool>> DownloadYouTubeVideo(string? url, string? outDir, bool? isHighestVideoQuality = true)
        {
            bool isValidate = await UrlExistsAsync(url);
            if (!isValidate)
                return new ResultModel<bool>("-1", "he video link does not exist, please check the address again!", 0, false);

            string? outputDirectory = outDir;

            if (string.IsNullOrEmpty(outDir))
                outputDirectory = _outputDefautl;

            if (!Directory.Exists(outputDirectory))
                return new ResultModel<bool>("-4", "outputDirectory does not exist", 0, false);

            try
            {
                var yt = new YoutubeClient();
                Video video = await yt.Videos.GetAsync(url);
                string sanitizedTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));
                var streamManifest = await yt.Videos.Streams.GetManifestAsync(video.Id);
                if (isHighestVideoQuality == true)
                {
                    //var streamManifest = await yt.Videos.Streams.GetManifestAsync(url);

                    var streamInfo = streamManifest
                                        .GetVideoOnlyStreams()
                                        .Where(s => s.Container == Container.Mp4)
                                        .GetWithHighestVideoQuality();
                    var stream = await yt.Videos.Streams.GetAsync(streamInfo);

                    string outputFilePath = Path.Combine(_outputDefautl, $"{sanitizedTitle}.{streamInfo.Container}");

                    outputFilePath = GetFileName(outputFilePath);

                    using (FileStream outputStream = System.IO.File.Create(outputFilePath))
                        await stream.CopyToAsync(outputStream);
                    return new ResultModel<bool>("1", "Successful", 1, true);
                }
                else
                {
                    //var streamManifest = await yt.Videos.Streams.GetManifestAsync(video.Id);

                    var muxedStreams = streamManifest.GetMuxedStreams().OrderByDescending(s => s.VideoQuality).ToList();

                    if (muxedStreams != null && muxedStreams.Any())
                    {
                        var streamInfo = muxedStreams.First();
                        using var httpClient = new HttpClient();

                        var stream = await httpClient.GetStreamAsync(streamInfo.Url);

                        string outputFilePath = Path.Combine(outputDirectory, $"{sanitizedTitle}.{streamInfo.Container}");

                        using (FileStream outputStream = System.IO.File.Create(outputFilePath))
                            await stream.CopyToAsync(outputStream);
                        return new ResultModel<bool>("1", "Successful", 1, true);
                    }
                }
                return new ResultModel<bool>("-1", "The operation was unsuccessful", 1, false);

            }
            catch (Exception ex)
            {
                return new ResultModel<bool>("1", ex.Message, 1, false);
            }
        }

        private string GetFileName(string path)
        {
            if (!System.IO.File.Exists(path))
                return path;

            var extension = Path.GetExtension(path);
            var fileName = Path.GetFileNameWithoutExtension(path);
            var directory = Path.GetDirectoryName(path) ?? string.Empty;

            int counter = 1;
            //string newPath = path;

            while (System.IO.File.Exists(path))
            {
                path = Path.Combine(directory, $@"{fileName}({counter}){extension}");
                counter++;
            }

            return path;
        }
    }
}
