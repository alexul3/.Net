using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperEnums;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels.StorekeeperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage.StorekeeperSaveToFile
{
    public abstract class AbstractSaveToExcel 
    {
		/// <summary>
		/// Создание отчета
		/// </summary>
		/// <param name="info"></param>
		public void CreateReport(ExcelStoreKeeperInfo info)
		{
			CreateExcel(info);
			InsertCellInWorksheet(new ExcelCellParameters
			{
				ColumnName = "A",
				RowIndex = 1,
				Text = info.Title,
				StyleInfo = ExcelStyleInfoType.Title
			});
			MergeCells(new ExcelMergeParameters
			{
				CellFromName = "A1",
				CellToName = "C1"
			});
			uint rowIndex = 2;
			foreach (var furniture in info.FurnitureMaterials)
			{
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = furniture.FurnitureName,
					StyleInfo = ExcelStyleInfoType.Text
				});
				rowIndex++;
				foreach (var materials in furniture.Materials)
				{
					InsertCellInWorksheet(new ExcelCellParameters
					{
						ColumnName = "B",
						RowIndex = rowIndex,
						Text = materials.Item1,
						StyleInfo = ExcelStyleInfoType.TextWithBroder
					});
					InsertCellInWorksheet(new ExcelCellParameters
					{
						ColumnName = "C",
						RowIndex = rowIndex,
						Text = materials.Item2.ToString(),
						StyleInfo = ExcelStyleInfoType.TextWithBroder
					});
					rowIndex++;
				}
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = "Итого",
					StyleInfo = ExcelStyleInfoType.Text
				});
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "C",
					RowIndex = rowIndex,
					Text = furniture.TotalCount.ToString(),
					StyleInfo = ExcelStyleInfoType.Text
				});
				rowIndex++;
			}
			SaveExcel(info);
		}
		/// <summary>
		/// Создание excel-файла
		/// </summary>
		/// <param name="info"></param>
		protected abstract void CreateExcel(ExcelStoreKeeperInfo info);
		/// <summary>
		/// Добавляем новую ячейку в лист
		/// </summary>
		/// <param name="cellParameters"></param>
		protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);
		/// <summary>
		/// Объединение ячеек
		/// </summary>
		/// <param name="mergeParameters"></param>
		protected abstract void MergeCells(ExcelMergeParameters excelParams);
		/// <summary>
		/// Сохранение файла
		/// </summary>
		/// <param name="info"></param>
		protected abstract void SaveExcel(ExcelStoreKeeperInfo info);
	}
}
