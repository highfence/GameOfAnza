using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfAnza_WindowForm_
{
	public partial class Form1 : Form
	{
		private string searchStr;

		public Form1()
		{
			InitializeComponent();
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			searchStr = SearchBox.Text;

			int searchRouteId = HttpNetwork.GetInstance().GetBusRouteList(searchStr);
			int searchRouteDayDriveNm = HttpNetwork.GetInstance().GetRouteDayDrivenNm(searchRouteId);
			var list = HttpNetwork.GetInstance().GetStationsByRouteList(searchRouteId);
		}
	}
}
