import requests
import json

session = requests.session()


session.get("https://symptoms.webmd.com/")
session_cookies = session.cookies.get_dict()

headers = {
    "enc_data": session_cookies['__cfduid'] + '=',
    "timestamp": "Fri, 30 Apr 2021 13:25:04 GMT",
    "client_id": session_cookies['VisitorId'],
    "accept" : "application/json; charset=utf-8"
}
test = session.get(
    "https://symptoms.webmd.com/search/2/api/scbodytypeahead?q=&cache_2=true&gender=M&part=909&count=1000",
    headers=headers
)

#print(type(test))
print(session.headers['accept'])
print(session.cookies.get_dict())
print(test.content)