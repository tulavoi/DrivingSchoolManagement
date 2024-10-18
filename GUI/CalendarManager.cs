using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
	public class CalendarManager
	{
		#region Properties
		private List<List<Guna2Button>> matrix;
		private List<string> dayOfWeek;
		private FlowLayoutPanel _pnlMatrix;
		private DateTime _dtpValue;

		public List<List<Guna2Button>> Matrix => matrix;
		#endregion

		public CalendarManager(FlowLayoutPanel pnlMatrix, DateTime dtpValue)
		{
			_pnlMatrix = pnlMatrix;
			_dtpValue = dtpValue;
			this.matrix = new List<List<Guna2Button>>();
			this.dayOfWeek = new List<string>()
			{
				"Sunday",
				"Monday",
				"Tuesday",
				"Wednesday",
				"Thursday",
				"Friday",
				"Saturday",
			};
		}

		/* Tạo các button tương ứng với số lượng ngày trong tuần và tháng.
		 * Các button được thêm vào _pnlMatrix.*/
		public void LoadMatrix()
		{
			Guna2Button initialBtn = this.CreateInitialButton();

			for (int i = 0; i < Constant.DayOfColumn; i++)
			{
				var row = new List<Guna2Button>();
				for (int j = 0; j < Constant.DayOfWeek; j++)
				{
					Guna2Button btn = ButtonStyleHelper.CreateGunaButton(initialBtn);

					btn.DoubleClick += new EventHandler(Btn_DoubleClick);
					this._pnlMatrix.Controls.Add(btn);
					row.Add(btn);

					initialBtn = btn;
				}
				this.Matrix.Add(row);
				initialBtn = this.MoveToNextRow(initialBtn);
			}
		}

		/* Khi 1 button (day) trong calendar được double click sẽ
		 * gọi method OpenAssignScheduleForm() để mở form đăng ký lịch học. */
		private void Btn_DoubleClick(object sender, EventArgs e)
		{
			Guna2Button btn = sender as Guna2Button;

			if (btn != null && !string.IsNullOrEmpty(btn.Text))
			{
				DateTime date = this.GetDateTimeFromBtn(btn);

				this.OpenAssignScheduleForm(date);
			}
		}

		public void OpenAssignScheduleForm(DateTime date)
		{
			FormHelper.OpenPopupForm(new AssignScheduleForm(date));
		}

		/* Lấy ngày từ text của button (day) và ghép vào năm và tháng hiện tại (lấy từ _dtpValue) 
		 * để tạo ra một đối tượng DateTime.*/
		private DateTime GetDateTimeFromBtn(Guna2Button btn)
		{
			int year = this._dtpValue.Year;
			int month = this._dtpValue.Month;
			int day = Convert.ToInt32(btn.Text);
			return new DateTime(year, month, day);
		}

		private Guna2Button MoveToNextRow(Guna2Button initialBtn)
		{
			return new Guna2Button()
			{
				Width = 0,
				Height = 0,
				Location = new Point(0, initialBtn.Location.Y + Constant.dayButtonHeight)
			};
		}

		private Guna2Button CreateInitialButton()
		{
			return new Guna2Button()
			{
				Width = 0,
				Height = 0,
				Location = new Point(0, 0)
			};
		}

		public void AddNumberToMatrixByDate(DateTime date)
		{
			this.ResetMatrixButtons(); // Các nút sẽ được làm mới, sau đó điền các ngày vào các nút tương ứng.

			DateTime curDate = new DateTime(date.Year, date.Month, 1);

			int line = 0;
			int totalDays = DateTime.DaysInMonth(curDate.Year, curDate.Month);

			// Đặt các số ngày vào ma trận nút bấm tương ứng với tháng được chọn.
			for (int i = 1; i <= totalDays; i++)
			{
				int column = this.dayOfWeek.IndexOf(curDate.DayOfWeek.ToString());
				Guna2Button btn = this.Matrix[line][column];
				btn.Text = i.ToString();

				this.UpdateBtnStylesByDate(curDate, date, btn); // được gọi để cập nhật màu sắc của nút dựa vào ngày hiện tại hoặc ngày đang được chọn.

				if (column == Constant.DayOfWeek - 1) line++;

				curDate = curDate.AddDays(1);
			}
		}

		public void ResetMatrixButtons()
		{
			for (int i = 0; i < this.Matrix.Count; i++)
			{
				for (int j = 0; j < this.Matrix[i].Count; j++)
				{
					Guna2Button btn = this.Matrix[i][j];
					btn.Text = "";
					ButtonStyleHelper.UpdateBtnToDefaultState(btn);
				}
			}
		}

		private void UpdateBtnStylesByDate(DateTime curDate, DateTime date, Guna2Button btn)
		{
			if (this.IsEqualDate(curDate, date))
				ButtonStyleHelper.UpdateBtnForSelectedDay(btn);

			if (this.IsEqualDate(curDate, DateTime.Now))
				ButtonStyleHelper.UpdateBtnForCurrentDay(btn);
		}

		private bool IsEqualDate(DateTime dateA, DateTime dateB)
		{
			return dateA.Year == dateB.Year && dateA.Month == dateB.Month && dateA.Day == dateB.Day;
		}
	}
}
