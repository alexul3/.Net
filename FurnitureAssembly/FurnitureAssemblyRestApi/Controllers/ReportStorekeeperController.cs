using FurnitureAssemblyBusinessLogic.BusinessLogics;
using FurnitureAssemblyBusinessLogic.MailWorker;
using FurnitureAssemblyContracts.BindingModels;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureAssemblyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportStorekeeperController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportStorekeeperLogic _reportLogic;
        private readonly MailWorker _mail;
        public ReportStorekeeperController(ILogger<ReportController> logger, IReportStorekeeperLogic reportLogic, MailWorker mailWorker)
        {
            _logger = logger;
            _reportLogic = reportLogic;
            _mail = mailWorker;
        }

        [HttpPost]
        public void CreateReportToDocx(ReportBindingModel model)
        {
            try
            {
                _reportLogic.SaveFurnituresToWordFile(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }
        [HttpPost]
        public void CreateReportToXlsx(ReportBindingModel model)
        {
            try
            {
                _reportLogic.SaveFurnituresToExelFile(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }
        [HttpPost]
        public void CreateReportOrdersToPdf(ReportBindingModel model)
        {
            try
            {
                _reportLogic.SaveMaterialsToPdfFile(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }
        [HttpPost]
        public void SendPdfToMail(MailSendInfoBindingModel model)
        {
            try
            {
                _mail.MailSendAsync(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка отправки письма");
                throw;
            }
        }
    }
}
