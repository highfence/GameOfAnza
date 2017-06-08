using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

			// 노선의 마지막 역의 넘버를 나타내는 변수.
			if (routeList == null)
			{
				MessageBox.Show("정부 API 나빠요.");
				return;
			}
			int lastStationSeq = routeList.Count();

			// route에 있는 station의 승, 하차 총 승객수를 구해준다.
			foreach (var station in routeList)
			{
				Tuple<int, int> passengerNum = MongoDBManager.GetInstance().FindPassengerNumberWithStationId(searchStr, station.stationId);

				station.rideNum = passengerNum.Item1;
				station.alightNum = passengerNum.Item2;
			}

			// 그 전 역의 타고있는 승객수를 임시 저장하는 변수.
			int previousRemainPassenger = 0;

			// 노선 전체 평균 승객수를 구하기위해 누적시키는 변수.
			float totalRemainPassenger = 0;

			// station의 누적 승객수를 구해준다. 
			foreach (var station in routeList)
			{
				if (station.seq == 1)
				{
					station.remainPassenger = station.rideNum - station.alightNum;
					station.averageRemainPassenger = station.remainPassenger / searchRouteDayDriveNm;
					totalRemainPassenger += station.averageRemainPassenger;
					previousRemainPassenger = station.remainPassenger;
				}
				else  if (station.seq == lastStationSeq)
				{
					station.remainPassenger = 0;
				}
				else
				{
					station.remainPassenger = previousRemainPassenger + station.rideNum - station.alightNum;
					station.averageRemainPassenger = station.remainPassenger / searchRouteDayDriveNm;
					totalRemainPassenger += station.averageRemainPassenger;
					previousRemainPassenger = station.remainPassenger;
				}
			}

			// 노선 전체 평균 인원 점수.
			float anzaScore = (totalRemainPassenger / lastStationSeq) / 40;

			MakeAnzaScoreTab(routeList, anzaScore);

		}

		/*
		 * 컨트롤 탭에 앉아 점수를 만드는 메소드.
		 */
		private void MakeAnzaScoreTab(List<HttpNetwork.RouteStationInfo> routeList, float anzaScore)
		{
			// 앉아 점수 공개!
			AnzaScoreLabel.Text = "앉아 점수 : " + anzaScore;

			// 앉아 점수에 맞는 표정 로드.
			string anzaFaceFilePath = "../../Resources/" + selectFace(anzaScore).ToString() + ".png";
			var src = ResizeImage((Bitmap)Bitmap.FromFile(anzaFaceFilePath), AnzaImage.Width, AnzaImage.Height);
			AnzaImage.Image = src;
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

		private void AnzaScoreLabel_Click(object sender, EventArgs e)
		{

		}

		// 앉아 점수에 따른 이모티콘을 반환해주는 함수.
		private int selectFace(float anzaScore)
		{
			if (anzaScore >= 9.0) return 7;
			else if (anzaScore >= 8.0) return 6;
			else if (anzaScore >= 6.5) return 5;
			else if (anzaScore >= 4.5) return 4;
			else if (anzaScore >= 3.75) return 3;
			else if (anzaScore >= 2.0) return 2;
			else return 1;
		}

		/*
		 * https://goo.gl/RBGPhT 의 함수 참조.
		 * 이미지를 알맞게 사이즈를 조절하는 함수.
		 */
		public static Bitmap ResizeImage(Image image, int width, int height)
		{
			var destRect = new Rectangle(0, 0, width, height);
			var destImage = new Bitmap(width, height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var wrapMode = new ImageAttributes())
				{
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
				}
			}

			return destImage;
		}
	}
}
