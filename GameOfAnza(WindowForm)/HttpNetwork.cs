using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Windows.Forms;

namespace GameOfAnza_WindowForm_
{
	/*
	 * 정부 API와 Http통신을 담당할 클래스.
	 * 싱글톤으로 구현되어있다.
	 */
	public class HttpNetwork
	{
		// 클래스 싱글톤 구현
		private static HttpNetwork _instance;
		public static HttpNetwork GetInstance()
		{
			if (_instance == null)
			{
				_instance = new HttpNetwork();
			}
			return _instance;
		}

		// 클래스에서 담당할 API 종류들을 나타냄.
		public enum APICODE
		{
			GET_BUS_ROUTE_LIST,
			GET_ROUTE_INFO,
			GET_STATIONS_BY_ROUTE_LIST,
			GET_STATION_BY_NAME,
			APICODE_NUM
		}

		// HttpNetwork 클래스에서 반환하는 인자값을 나타냄.
		public enum HTTP_RETURN
		{
			RETURN_OK,
			INVALID_PARAM,
			RESPONSE_EXCEPTION
		}

		// API 관련 데이터들을 담을 구조체
		private struct APIData
		{
			public APICODE apiCode;
			public string apiUrl;
			public string apiParam;
			public string apiServiceKey;
			
			// Http 요청을 위한 문자열을 데이터에서 뽑아내는 메소드.
			public string GetRequestLine()
			{
				if (apiUrl == null || apiServiceKey == null)
				{
					return null;
				}

				string requestUrl = apiUrl + "?serviceKey=" + apiServiceKey;
				
				if (apiParam != null)
				{
					requestUrl = requestUrl + "&" + apiParam + "=";
				}

				return requestUrl;
			}
		}

		// 노선에 속해있는 역 정보를 담을 구조체
		public struct RouteStationInfo
		{
			public int arsId;
			public int stationId;
			public string stationNm;
			public int seq;
		}


		// 정부 API 관련 데이터가 저장된 파일.
		private string filePath = "../../Resources/KeyValues.xml";
		// 정부 API 관련 데이터를 저장할 구조체.
		private APIData[] apiData = new APIData[(int)APICODE.APICODE_NUM];

		/*
		 * 클래스 생성자.
		 * GetInstance 처음 호출시에 한번만 불려진다.
		 * 데이터가 저장된 Xml파일에서 데이터를 읽어들인다.
		 */
		protected HttpNetwork()
		{
			try
			{
				XmlDocument keyXml = new XmlDocument();
				keyXml.Load(filePath);

				for (int i = 0; i < (int)APICODE.APICODE_NUM; ++i)
				{
					XmlNodeList xmlNodeList = keyXml.SelectNodes("APIKeys/api" + i.ToString());

					foreach (XmlNode xn in xmlNodeList)
					{
						apiData[i].apiCode = (APICODE)i;
						apiData[i].apiUrl = xn["Url"].InnerText;
						apiData[i].apiParam = xn["Param"].InnerText;
						apiData[i].apiServiceKey = xn["ServiceKey"].InnerText;
					}
				}

			}
			catch (ArgumentException ex)
			{
				MessageBox.Show("XML 리딩 문제 발생 \r\n" + ex);
			}
		}

		/*
		 * HTTP Request를 보내는 메소드.
		 * API가 파라메터가 있는 경우 URI 인코딩이 필요한지 세번째 인자로 체크를 해주어야 한다.
		 * 파라메터가 영어가 아닌 경우 세 번째 인자가 true여야 한다.
		 */
		private string HttpRequest(APICODE code, string param, bool paramEncodeNeeded)
		{
			string requestLine = apiData[(int)code].GetRequestLine();
			if (param != null)
			{
				string addParam;
				if (paramEncodeNeeded)
				{
					addParam = HttpUtility.UrlEncode(param);
				}
				else
				{
					addParam = param;
				}
				requestLine = requestLine + addParam;
			}

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestLine);
			request.Method = "GET";

			try
			{
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					Encoding encode;
					if (response.CharacterSet.ToLower() == "utf-8") { encode = Encoding.UTF8; }
					else { encode = Encoding.Default; }

					Stream stReadData = response.GetResponseStream();
					StreamReader srReadData = new StreamReader(stReadData, encode);

					string strResult = srReadData.ReadToEnd();
					return strResult;
				}
			}
			catch
			{
				return null;
			}
		}

		/*
		 * 노선의 이름을 넣으면
		 * 찾는 노선의 ID를 반환해주는 메소드. 
		 */
		public int GetBusRouteList(string stationName)
		{
			if (stationName == null) return -1;

			// stationName으로 Http 요청을 보냄.
			string routeListString = HttpRequest(APICODE.GET_BUS_ROUTE_LIST, stationName, true);
			if (routeListString == null) return -1;

			// 받은 응답을 XmlDocument로 로드.
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(routeListString);

			// Xml 파일을 파싱하여 원하는 Route의 아이디 찾기.
			XmlNodeList xmlNodeList = xmlDoc.SelectNodes("ServiceResult/msgBody/itemList");

			var findBusRouteId = from XmlNode xn in xmlNodeList
								 where xn["busRouteNm"].InnerText == stationName
								 select Convert.ToInt32(xn["busRouteId"].InnerText);

			if (findBusRouteId.Any())
				return findBusRouteId.First();
			else
				return -1;
		}

		/*
		 * 노선의 이름을 넣으면 그와 비슷한 노선의 이름을 반환해주는 함수.
		 */
		public List<string> GetBusRouteListLike(string stationName)
		{
			if (stationName == null) return null;

			// stationName으로 Http 요청을 보냄.
			string routeListString = HttpRequest(APICODE.GET_BUS_ROUTE_LIST, stationName, true);
			if (routeListString == null) return null;

			// 받은 응답을 XmlDocument로 로드.
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(routeListString);

			// Xml 파일을 파싱하여 원하는 Route의 아이디 찾기.
			XmlNodeList xmlNodeList = xmlDoc.SelectNodes("ServiceResult/msgBody/itemList");

			var findBusRouteId = from XmlNode xn in xmlNodeList
								 select xn["busRouteNm"].InnerText;

			if (findBusRouteId.Any())
			{
				return findBusRouteId.ToList();
			}
			else return null;
		}


		/*
		 * 노선의 아이디 (BusRouteId)를 넣으면 하루에 할당된 배차량을 반환해주는 함수.
		 * 정부 API의 GetRouteInfo를 이용.
		 * 첫차시간과 막차시간의 차이를 배차간격으로 나눠준다.
		 */
		public int GetRouteDayDrivenNm(int busRouteId)
		{
			if (busRouteId == -1) return -1;

			// 버스 아이디로 Http 요청을 보냄.
			string routeInfoString = HttpRequest(APICODE.GET_ROUTE_INFO, busRouteId.ToString(), false);
			if (routeInfoString == null) return -1;

			// 받은 응답을 XmlDocumnet로 로드.
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(routeInfoString);

			// Xml 파일을 파싱하여 원하는 Route의 정보 찾기.
			XmlNodeList xmlNodeList = xmlDoc.SelectNodes("ServiceResult/msgBody/itemList");

			var findRouteInfo = from XmlNode xn in xmlNodeList
								where xn["busRouteId"].InnerText == busRouteId.ToString()
								select xn;

			if (findRouteInfo.Any())
			{
				var xn = findRouteInfo.First();
				string firstBusTimeString = xn["firstBusTm"].InnerText;
				string lastBusTimeString = xn["lastBusTm"].InnerText;

				// 받은 정보를 DateTime 형으로 파싱.
				DateTime firstBusTime = new DateTime(
					Convert.ToInt32(firstBusTimeString.Substring(0, 4)),
					Convert.ToInt32(firstBusTimeString.Substring(4, 2)),
					Convert.ToInt32(firstBusTimeString.Substring(6, 2)),
					Convert.ToInt32(firstBusTimeString.Substring(8, 2)),
					Convert.ToInt32(firstBusTimeString.Substring(10, 2)),
					Convert.ToInt32(firstBusTimeString.Substring(12, 2)));

				DateTime lastBusTime = new DateTime(
					Convert.ToInt32(lastBusTimeString.Substring(0, 4)),
					Convert.ToInt32(lastBusTimeString.Substring(4, 2)),
					Convert.ToInt32(lastBusTimeString.Substring(6, 2)),
					Convert.ToInt32(lastBusTimeString.Substring(8, 2)),
					Convert.ToInt32(lastBusTimeString.Substring(10, 2)),
					Convert.ToInt32(lastBusTimeString.Substring(12, 2)));

				// 파싱받은 시간의 차이를 구하고 Term 정보로 나누어준다.
				TimeSpan busDriveRange = lastBusTime.Subtract(firstBusTime);
				int busDriveRangeInMinute = busDriveRange.Hours * 60 + busDriveRange.Minutes;
				int term = Convert.ToInt32(xn["term"].InnerText);

				int dayBusDriveNm = busDriveRangeInMinute / term;
				return dayBusDriveNm;
			}
			else
				return -1;
		}

		/*
		 * RouteId를 인자로 받아 해당하는 노선의 정보를 반환해주는 메소드.
		 * RouteStationInfo 구조체의 리스트를 반환한다.
		 */
		public List<RouteStationInfo> GetStationsByRouteList(int routeId)
		{
			if (routeId == -1) return null;

			// routeId로 Http 요청을 보냄.
			string routeStationInfoString = HttpRequest(APICODE.GET_STATIONS_BY_ROUTE_LIST, routeId.ToString(), false);
			if (routeStationInfoString == null) return null;

			// 받은 응답을 XmlDocument로 로드.
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(routeStationInfoString);

			// Xml 파일을 파싱하여 원하는 정보 찾기.
			XmlNodeList xmlNodeList = xmlDoc.SelectNodes("ServiceResult/msgBody/itemList");

			var findStationInfo = from XmlNode xn in xmlNodeList
								  where xn["busRouteId"].InnerText == routeId.ToString()
								  select xn;

			if (findStationInfo.Any())
			{
				List<RouteStationInfo> stationInfoList = new List<RouteStationInfo>();
				foreach (var xn in findStationInfo)
				{
					RouteStationInfo stationInfo = new RouteStationInfo();
					stationInfo.arsId = Convert.ToInt32(xn["arsId"].InnerText);
					stationInfo.seq = Convert.ToInt32(xn["seq"].InnerText);
					stationInfo.stationId = Convert.ToInt32(xn["station"].InnerText);
					stationInfo.stationNm = xn["stationNm"].InnerText;

					stationInfoList.Add(stationInfo);
				}
				return stationInfoList;
			}
			else
				return null;
		}
	}
}
