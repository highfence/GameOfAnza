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

			// 결과박스 보이지 않도록.
			ResultBox.Visible = false;

			// 탭 보이지 않도록.
			TabControl.Visible = false;
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
			TabControl.Visible = true;

			int searchRouteId = HttpNetwork.GetInstance().GetBusRouteList(searchStr);
			int searchRouteDayDriveNm = HttpNetwork.GetInstance().GetRouteDayDrivenNm(searchRouteId);
			var routeList = HttpNetwork.GetInstance().GetStationsByRouteList(searchRouteId);

			// route에 있는 station의 승, 하차 총 승객수를 구해준다.
			foreach (var station in routeList)
			{
				Tuple<int, int> passengerNum = MongoDBManager.GetInstance().FindPassengerNumberWithStationId(station.stationId);

				station.rideNum = passengerNum.Item1;
				station.alightNum = passengerNum.Item2;
			}

			// station의 누적 승객수를 구해준다. 
			int lastStationSeq = routeList.Count();
			foreach (var station in routeList)
			{
				if (station.seq == 1)
				{
					station.accRideNum = station.rideNum;
					station.accAlightNum = station.alightNum;
				}
				else  if (station.seq == lastStationSeq)
				{

				}
				else
				{

				}
			}
		}

		/*
		 * SearchBox에 검색어가 없다면, ListBox를 보이지 않도록 함.
		 * 검색어가 존재한다면, 그에 맞는 유사 검색어들을 리스트박스에 추가해줌.		 
		 */
		private void SearchBox_TextChanged(object sender, EventArgs e)
		{
			if (SearchBox.Text == "")
			{
				ResultBox.Visible = false;
			}
			else
			{
				ResultBox.Items.Clear();

				var similarNameRouteList = HttpNetwork.GetInstance().GetBusRouteListLike(SearchBox.Text);
				if (similarNameRouteList == null)
				{
					return;
				}

				foreach (string similarNameRoute in similarNameRouteList)
				{
					ResultBox.Items.Add(similarNameRoute);
				}

				ResultBox.Visible = true;
			}
		}

		/*
		 * 유사 RouteName을 선택하였을 때 발생하는 이벤트.
		 */ 
		private void ResultBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedRouteNm = ResultBox.SelectedItem;
			SearchBox.Text = selectedRouteNm.ToString();
			ResultBox.Visible = false;
		}
	}
}
