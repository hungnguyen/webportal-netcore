using Google.Analytics.Data.V1Beta;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Api.Models;
using WebPortal.Services.Common;
using WebPortal.Services.Extensions;

namespace WebPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : AuthorizeController
    {
        private readonly IStorageService storageService;
        public AnalyticsController(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        [HttpPost("Summary")]
        public async Task<IActionResult> Summary(ReportRequest reportRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Make the request
            try
            {
                var response = await RunReport(reportRequest);
                return Ok(response);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Graph")]
        public async Task<IActionResult> Graph(ReportRequest reportRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Make the request
            try
            {
                var response = await RunReport(reportRequest);
                var graphData = FillMissingDate(response, reportRequest);

                return Ok(graphData);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("TopList")]
        public async Task<IActionResult> TopList(ReportRequest reportRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Make the request
            try
            {
                var response = await RunReport(reportRequest);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private async Task<RunReportResponse> RunReport(ReportRequest reportRequest)
        {
            BetaAnalyticsDataClient client = new BetaAnalyticsDataClientBuilder
            {
                CredentialsPath = storageService.GetPhysicalPath("credentials.json")
            }.Build();

            RunReportRequest request = new RunReportRequest
            {
                Property = "properties/" + reportRequest.PropertyId,
                Metrics = { reportRequest.Metrics.Select(m => new Metric { Name = m }) },
                DateRanges = { new DateRange { StartDate = reportRequest.StartDate, EndDate = reportRequest.EndDate }, },
            };

            if(reportRequest.Dimensions != null)
            {
                request.Dimensions.AddRange(reportRequest.Dimensions.Select(d => new Dimension { Name = d }));
            }

            // Make the request
            var response = await client.RunReportAsync(request);

            return response;
        }
        private GraphData FillMissingDate(RunReportResponse response, ReportRequest request)
        {
            DateTime.TryParse(request.StartDate, out var start);
            DateTime.TryParse(request.EndDate, out var end);
            var dates = Enumerable.Range(0, 1 + end.Subtract(start).Days)
              .Select(offset => start.AddDays(offset))
              .ToArray();
            var graphdata = new GraphData();

            foreach(var date in dates)
            {
                graphdata.Labels.Add(date.ToGraphView());
                var row = response.Rows.FirstOrDefault(r => r.DimensionValues[0].Value == date.ToGoogleDate());
                if (row!=null)
                {
                    graphdata.Values.Add(row.MetricValues[0].Value);
                }
                else
                {
                    graphdata.Values.Add("0");
                }    
            }

            return graphdata;
        }
    }
}
