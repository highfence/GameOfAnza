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
			ResultBox.Visible = false;
			searchStr = SearchBox.Text;
			TabControl.Visible = true;

			int searchRouteId = HttpNetwork.GetInstance().GetBusRouteList(searchStr);
			int routeDayDriveNm = HttpNetwork.GetInstance().GetRouteDayDrivenNm(searchRouteId);
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
					station.averageRemainPassenger = station.remainPassenger / routeDayDriveNm;
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
					station.averageRemainPassenger = station.remainPassenger / routeDayDriveNm;
					totalRemainPassenger += station.averageRemainPassenger;
					previousRemainPassenger = station.remainPassenger;
				}
			}

			// 노선 전체 평균 인원 점수.
			float anzaScore = (totalRemainPassenger / lastStationSeq) / 20;

			MakeAnzaScoreTab(routeList, anzaScore, routeDayDriveNm);

		}

		/*
		 * 컨트롤 탭에 앉아 점수를 만드는 메소드.
		 */
		private void MakeAnzaScoreTab(List<HttpNetwork.RouteStationInfo> routeList, float anzaScore, int routeDayDriveNm)
		{
			// 앉아 점수 공개!
			AnzaScoreLabel.Text = "노선 앉아 점수 : " + anzaScore;

			// 앉아 점수에 맞는 표정 로드.
			FaceLoadAccordWithScore(AnzaImage, anzaScore);

			// 가장 앉기 쉬운 역 공개!
			LoadHeavenTopThreeStation(routeList);
			// 어려운 역도 공개!
			LoadHellTopThreeStation(routeList);
;
			// 노선별 점수도 공개!
			FillRouteGridView(routeList, routeDayDriveNm);
		}

		// Image에 알맞은 아이콘을 넣어주는 함수.
		private void FaceLoadAccordWithScore(PictureBox img, float score)
		{
			string anzaFaceFilePath = "../../Resources/" + SelectFace(score).ToString() + ".png";
			var src = ResizeImage((Bitmap)Bitmap.FromFile(anzaFaceFilePath), img.Width, img.Height);
			img.Image = src;
		}

		// 두 번째 탭의 DataGridView를 채우는 함수.
		private void FillRouteGridView(List<HttpNetwork.RouteStationInfo> routeList, int routeDayDriveNm)
		{
			// 데이터 그리드 뷰 세팅.
			SetupRouteGridView();
			RouteGridView.Rows.Clear();

			// 루트 리스트를 seq에 대해 소팅.
			routeList.Sort(
				delegate (HttpNetwork.RouteStationInfo r1, HttpNetwork.RouteStationInfo r2) 
				{ return r1.seq.CompareTo(r2.seq); });

			int rowIndex = 0;
			foreach (var stationInfo in routeList)
			{
				int seq = stationInfo.seq;
				string stationNm = stationInfo.stationNm;
				float score = ValueCorrectionToPositive(stationInfo.averageRemainPassenger / 20);
				int ridePssn = stationInfo.rideNum;
				int alightPssn = stationInfo.alightNum;

				RouteGridView.Rows.Add(seq, stationNm, score, ridePssn, alightPssn);
				RouteGridView.Rows[rowIndex].Cells[2].Style.BackColor = SelectColor(score);
				++rowIndex;
			}

		}

		// RouteGridView세팅
		private void SetupRouteGridView()
		{
			RouteGridView.ColumnCount = 5;
			RouteGridView.Columns[0].Name = "순서";
			RouteGridView.Columns[1].Name = "정류장 이름";
			RouteGridView.Columns[2].Name = "앉아 점수";
			RouteGridView.Columns[3].Name = "탑승 승객 수";
			RouteGridView.Columns[4].Name = "하차 승객 수";
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
			else if (SearchBox.Text.Length < 2)
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
		 * routeList에서 가장 평균 인원이 많은 세 역의 seq을 찾아주는 함수. 
		 */
		private void LoadHellTopThreeStation(List<HttpNetwork.RouteStationInfo> routeList)
		{
			// 소팅해줌.
			routeList.Sort(
				delegate (HttpNetwork.RouteStationInfo r1, HttpNetwork.RouteStationInfo r2) 
				{ return r2.remainPassenger.CompareTo(r1.remainPassenger); });

			HellSt1.Text = routeList.ElementAt(0).stationNm;
			HellSt2.Text = routeList.ElementAt(1).stationNm;
			HellSt3.Text = routeList.ElementAt(2).stationNm;

			float score1 = ValueCorrectionToPositive(routeList.ElementAt(0).averageRemainPassenger / 20);
			float score2 = ValueCorrectionToPositive(routeList.ElementAt(1).averageRemainPassenger / 20);
			float score3 = ValueCorrectionToPositive(routeList.ElementAt(2).averageRemainPassenger / 20);

			HellScore1.Text = "앉아 점수 : " + score1;
			HellScore2.Text = "앉아 점수 : " + score2;
			HellScore3.Text = "앉아 점수 : " + score3;

			FaceLoadAccordWithScore(HellImg1, score1);
			FaceLoadAccordWithScore(HellImg2, score2);
			FaceLoadAccordWithScore(HellImg3, score3); ;
		}

		/*
		 * routeList에서 가장 평균 인원이 적은 세 역의 seq를 찾아주는 함수.
		 */
		private void LoadHeavenTopThreeStation(List<HttpNetwork.RouteStationInfo> routeList)
		{
			// 역으로 소팅해줌
			routeList.Sort(
				delegate (HttpNetwork.RouteStationInfo r1, HttpNetwork.RouteStationInfo r2) 
				{ return r1.remainPassenger.CompareTo(r2.remainPassenger); });

			HeavenSt1.Text = routeList.ElementAt(0).stationNm;
			HeavenSt2.Text = routeList.ElementAt(1).stationNm;
			HeavenSt3.Text = routeList.ElementAt(2).stationNm;

			float score1 = ValueCorrectionToPositive(routeList.ElementAt(0).averageRemainPassenger / 20);
			float score2 = ValueCorrectionToPositive(routeList.ElementAt(1).averageRemainPassenger / 20);
			float score3 = ValueCorrectionToPositive(routeList.ElementAt(2).averageRemainPassenger / 20);

			HeavenScore1.Text = "앉아 점수 : " + score1;
			HeavenScore2.Text = "앉아 점수 : " + score2;
			HeavenScore3.Text = "앉아 점수 : " + score3;

			FaceLoadAccordWithScore(HeavenImg1, score1);
			FaceLoadAccordWithScore(HeavenImg2, score2);
			FaceLoadAccordWithScore(HeavenImg3, score3);
		}

		private float ValueCorrectionToPositive(float value)
		{
			if (value < 0) return 0;
			return value;
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
		private int SelectFace(float anzaScore)
		{
			if (anzaScore >= 15.0) return 7;
			else if (anzaScore >= 13.0) return 6;
			else if (anzaScore >= 10.5) return 5;
			else if (anzaScore >= 8.5) return 4;
			else if (anzaScore >= 5) return 3;
			else if (anzaScore >= 3.0) return 2;
			else return 1;
		}

		// 앉아 점수에 따른 색상을 반환해주는 함수.
		private Color SelectColor(float anzaScore)
		{
			if (anzaScore >= 15.0) return Color.Maroon;
			else if (anzaScore >= 13.0) return Color.Crimson;
			else if (anzaScore >= 10.5) return Color.Orange;
			else if (anzaScore >= 8.5) return Color.Yellow;
			else if (anzaScore >= 5) return Color.GreenYellow;
			else if (anzaScore >= 3.0) return Color.LawnGreen;
			else return Color.PaleGreen;
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
