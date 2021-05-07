import requests
import json
session = requests.session()
'''
response = session.get('https://healthatlas.gov.gr/#!/')

print(session.cookies.get_dict())

response = session.get('https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId=9AEB40EC-A2D2-45E5-B0F5-BABC72591495&healthCareSiteCategoryId=565920DF-3A17-4BB1-BD7C-5B6D1FDEEAF1&healthCareSiteType=&contract=null&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId=&medicalActs=&clinics=&searchTerm=')

print(response.content)
'''
with open('./healthatlas_pref.json','r') as file:
    json_data = json.load(file)
    count = 0
    for i in json_data:
        print(i['Description'])
    file.close()

    