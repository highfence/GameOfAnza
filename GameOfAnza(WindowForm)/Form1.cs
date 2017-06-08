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

			// 싱글톤 클래스들 초기화.
			MongoDBManager.GetInstance();
			HttpNetwork.GetInstance();
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			SearchStart();
		}

		private void SearchBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SearchStart();
			}
		}

		private void SearchStart()
		{
			searchStr = SearchBox.Text;

			int searchRouteId = HttpNetwork.GetInstance().GetBusRouteList(searchStr);
			int searchRouteDayDriveNm = HttpNetwork.GetInstance().GetRouteDayDrivenNm(searchRouteId);
			var list = HttpNetwork.GetInstance().GetStationsByRouteList(searchRouteId);
		}
	}
}
