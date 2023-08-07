using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using FurnitureAssemblyBusinessLogic.MailWorker;
using FurnitureAssemblyContracts.BindingModels;
using FurnitureAssemblyContracts.BusinessLogicContracts;
using FurnitureAssemblyContracts.SearchModels;
using FurnitureAssemblyContracts.ViewModels;
using FurnitureAssemblyDatabaseImplement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FurnitureAssemblyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly ILogger _logger;
        private readonly IReportWorkerLogic _reportWorkerLogic;
        private readonly MailWorker _mail;
        public ReportController(ILogger<ReportController> logger, IReportWorkerLogic reportWorkerLogic, MailWorker mailWorker)
        {
            _logger = logger;
            _reportWorkerLogic = reportWorkerLogic;
            _mail = mailWorker;
        }

        
        [HttpPost]
        public void CreateReportToDocx(ReportWorkerBindingModel model)
        {
            try
            {
                _reportWorkerLogic.SaveFurnitureModuleToWordFile(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }
        [HttpPost]
        public void CreateReportToXlsx(ReportWorkerBindingModel model)
        {
            try
            {
                _reportWorkerLogic.SaveSetFurnitureModuleToExcelFile(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания отчета");
                throw;
            }
        }
        [HttpPost]
        public void CreateReportOrdersToPdf(ReportWorkerBindingModel model)
        {
            try
            {
                _reportWorkerLogic.SaveOrdersToPdfFile(model);
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
