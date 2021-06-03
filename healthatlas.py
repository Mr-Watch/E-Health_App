import requests
import json


session = requests.session()
session.get('https://healthatlas.gov.gr/#!/')


def get_healthatlas_file(path: str):
    with open(path, 'r', encoding="utf8") as file:
        json_data = json.load(file)
        file.close()
        return json_data


healthatlas_prefecture_geos = get_healthatlas_file(
    './healthatlas_prefecture_geos.json')
healthatlas_specialties = get_healthatlas_file(
    './healthatlas_specialties.json')


def create_hospitals_url(prefecture: str):
    item = get_healthatlas_prefecture_item(prefecture)
    prefecture_id = item['prefecture-id']
    url = f'https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId={prefecture_id}&healthCareSiteCategoryId=565920DF-3A17-4BB1-BD7C-5B6D1FDEEAF1&healthCareSiteType=ΝΟΣΟΚΟΜΕΙΟ&contract=null&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId=&medicalActs=&clinics=&searchTerm='

    return url


def create_pharmacies_url(prefecture: str):
    item = get_healthatlas_prefecture_item(prefecture)
    prefecture_id = item['prefecture-id']
    url = f'https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId={prefecture_id}&healthCareSiteCategoryId=530577D9-DD7A-4954-95F7-F9E46B5478BC&healthCareSiteType=&contract=null&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId=&medicalActs=&clinics=&searchTerm='

    return url


def create_doctors_url(prefecture: str, specialty: str):
    item_specialty = get_healthatlas_specialty_item(specialty)
    item_prefecture = get_healthatlas_prefecture_item(prefecture)
    specialty_id = item_specialty['specialty-id']
    prefecture_id = item_prefecture['prefecture-id']
    url = f'https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId={prefecture_id}&healthCareSiteCategoryId=A15A4873-4B7D-4209-8A25-AFD777BBCA11&healthCareSiteType=&contract=null&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId={specialty_id}&medicalActs=&clinics=&searchTerm='
    return url


def get_healthatlas_prefecture_item(prefecture: str):
    return list(filter(lambda x: x["prefecture-name"] == prefecture, healthatlas_prefecture_geos))[0]


def get_healthatlas_specialty_item(specialty: str):
    return list(filter(lambda x: x["specialty"] == specialty.upper(), healthatlas_specialties))[0]


def get_hospitals_based_on_prefecture(prefecture: str):
    response = session.get(create_hospitals_url(prefecture), headers={
        'Accept-Language': 'el-GR',
    })
    return response.json()


def get_pharmacies_based_on_prefecture(prefecture: str):
    response = session.get(create_pharmacies_url(prefecture), headers={
        'Accept-Language': 'el-GR',
    })
    return response.json()


def get_doctors_based_on_prefecture(prefecture: str, specialty: str):
    response = session.get(create_doctors_url(prefecture, specialty), headers={
        'Accept-Language': 'el-GR',
    })
    return response.json()


def get_hospitals_based_on_geoId(geo_id: str):
    response = session.get(f'https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId={geo_id}&healthCareSiteCategoryId=565920DF-3A17-4BB1-BD7C-5B6D1FDEEAF1&healthCareSiteType=%CE%9D%CE%9F%CE%A3%CE%9F%CE%9A%CE%9F%CE%9C%CE%95%CE%99%CE%9F&contract=true&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId=&medicalActs=&clinics=&searchTerm=', headers={
        'Accept-Language': 'el-GR',
    })
    return response.json()


def get_pharmacies_based_on_geoId(geo_id: str):
    response = session.get(f'https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId={geo_id}&healthCareSiteCategoryId=530577D9-DD7A-4954-95F7-F9E46B5478BC&healthCareSiteType=&contract=&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId=&medicalActs=&clinics=&searchTerm=', headers={
        'Accept-Language': 'el-GR',
    })
    return response.json()


def get_geoIds():
    response = session.get('https://healthatlas.gov.gr/api/PrefectureGeos', headers={
        'Accept-Language': 'el-GR',
    })
    return response.json()
