import requests

session = requests.session()


data = {"mode": "LoadRegionBySpc", "spcVal": "-1"}
payload = '{\n"category": "Παιδίατροι",\n"region": "Θεσσαλονίκη ΘΕΣΣΑΛΟΝΙΚΗΣ",\n"insuranceFund": null,\n"freeText": null,\n"referer": "doctor.vrisko.gr",\n"fromRedirect": false\n}'
# get_specialties = session.post('https://doctor.vrisko.gr/AjaxRequests.ashx', data=data)

# print(get_specialties.text)
headers = {
    "User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:88.0) Gecko/20100101 Firefox/88.0",
    "Accept": "*/*",
    "Accept-Language": "en-US,en;q=0.7,el;q=0.3",
    "X-Requested-With": "XMLHttpRequest",
    "Content-Type": "application/json; charset=utf-8",
    "Origin": "https://doctor.vrisko.gr",
    "DNT": "1",
    "Connection": "keep-alive",
    "Referer": "https://doctor.vrisko.gr/patients/registerappointmentstep1?specialty=paidiatroi&location=%CE%98%CE%B5%CF%83%CF%83%CE%B1%CE%BB%CE%BF%CE%BD%CE%AF%CE%BA%CE%B7%20%CE%98%CE%95%CE%A3%CE%A3%CE%91%CE%9B%CE%9F%CE%9D%CE%99%CE%9A%CE%97%CE%A3&insurance=-1&brandname=",
    "Cookie": "ASP.NET_SessionId=eyx1mmhv1wpo4dahvtc13igj; ASP.NET_SessionId=ddoyvnuhzbdfegsp2w0znppn",
    "Sec-GPC": "1",
    "TE": "Trailers",
}
get_specialties = session.post(
    "https://doctor.vrisko.gr/Home.aspx/GetSpecialties", headers=headers
)

# print(get_specialties.text)

get_doctors = session.post(
    "https://doctor.vrisko.gr/patients/registerappointmentstep1.aspx/GetVriskoObjects",
    data=payload,
)
print(get_doctors.text)