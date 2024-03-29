﻿using FurnitureAssemblyBusinessLogic.OfficePackage.HelperEnums;
using FurnitureAssemblyBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureAssemblyBusinessLogic.OfficePackage
{
	public abstract class AbstractWorkerSaveToExcel
	{
		/// <summary>
		/// Создание отчета
		/// </summary>
		/// <param name="info"></param>
		public void CreateReport(ExcelInfo info)
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
			foreach (var set in info.SetFurnitureModules)
			{
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = set.SetName,
					StyleInfo = ExcelStyleInfoType.Text
				});
				rowIndex++;
				foreach (var furnitureModule in set.FurnitureModules)
				{
					InsertCellInWorksheet(new ExcelCellParameters
					{
						ColumnName = "B",
						RowIndex = rowIndex,
						Text = furnitureModule.Item1,
						StyleInfo = ExcelStyleInfoType.TextWithBroder
					});
					InsertCellInWorksheet(new ExcelCellParameters
					{
						ColumnName = "C",
						RowIndex = rowIndex,
						Text = furnitureModule.Count.ToString(),
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
					Text = set.TotalCount.ToString(),
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
		protected abstract void CreateExcel(ExcelInfo info);
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
		protected abstract void SaveExcel(ExcelInfo info);
	}
}
