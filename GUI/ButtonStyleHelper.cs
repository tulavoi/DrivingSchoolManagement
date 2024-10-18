using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
	public static class ButtonStyleHelper
	{
		public static Guna2Button CreateGunaButton(Guna2Button initialBtn)
		{
			return new Guna2Button()
			{
				Width = Constant.dayButtonWidth,
				Height = Constant.dayButtonHeight,
				Location = new Point(initialBtn.Location.X + initialBtn.Width, initialBtn.Location.Y),
				FillColor = Color.White,
				Cursor = Cursors.Hand,
				BorderRadius = 5,
				ForeColor = Color.FromArgb(49, 50, 52),
				HoverState = new Guna.UI2.WinForms.Suite.ButtonState()
				{
					FillColor = Color.FromArgb(247, 247, 247),
				},
				PressedDepth = 10,
			};
		}

		public static void UpdateButtonProperties(Guna2Button btn, Color fillColor, Color foreColor, Color fillColorHoverState, Color foreColorHoverState, Color borderColor, int borderThickness)
		{
			btn.BorderThickness = borderThickness;
			btn.BorderColor = borderColor;
			btn.FillColor = fillColor;
			btn.ForeColor = foreColor;
			btn.HoverState = new Guna.UI2.WinForms.Suite.ButtonState()
			{
				FillColor = fillColorHoverState,
				ForeColor = foreColorHoverState,
			};
		}

		public static void UpdateBtnForCurrentDay(Guna2Button btn)
		{
			UpdateButtonProperties(btn, Constant.BrightBlue, Color.White, Constant.BrightBlue, Color.White, Constant.BrightBlue, 0);
		}

		public static void UpdateBtnForSelectedDay(Guna2Button btn)
		{
			UpdateButtonProperties(btn, Color.White, Constant.BrightBlack, Constant.OffWhite, Constant.BrightBlack, Constant.BrightBlue, 1);
		}

		public static void UpdateBtnToDefaultState(Guna2Button btn)
		{
			UpdateButtonProperties(btn, Color.White, Constant.BrightBlack, Constant.OffWhite, Constant.BrightBlack, Color.White, 0);
		}
	}
}
