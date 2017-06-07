﻿using System;
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

		// 정부 API 관련 데이터가 저장된 파일.
		private string filePath = "C:/Users/NEXT/Desktop/Project/GameOfAnza(WindowForm)/GameOfAnza(WindowForm)/Resources/KeyValues.xml";
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
		 * 이 기능은 xml 파일을 따로 만들어서 그냥 Request가 아니라 파일 접근으로 처리해버려도 괜찮겠다 싶음.
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

			foreach (XmlNode xn in xmlNodeList)
			{
				if (stationName == xn["busRouteNm"].InnerText)
				{
					return Convert.ToInt32(xn["busRouteId"].InnerText);
				}
			}

			// 찾는 노선이 없음.
			return -1;
		}

		/*
		 * 노선의 아이디 (BusRouteId)를 넣으면 하루에 할당된 배차량을 반환해주는 함수.
		 * 정부 API의 GetRouteInfo를 이용.
		 * 첫차시간과 막차시간의 차이를 배차간격으로 나눠준다.
		 */
		public int GetRouteDayDrivenNm(int busRouteId)
		{
			if (busRouteId == -1) return -1;

			return -1;
		}
	}
}
