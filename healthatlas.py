from requests.api import get
from healthatlas_url_builder import create_url
import requests

session = requests.session()
session.get('https://healthatlas.gov.gr/#!/')


def get_hospitals_based_on_geoId(geo_id: str):
    response = session.get(f'https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId={geo_id}&healthCareSiteCategoryId=565920DF-3A17-4BB1-BD7C-5B6D1FDEEAF1&healthCareSiteType=%CE%9D%CE%9F%CE%A3%CE%9F%CE%9A%CE%9F%CE%9C%CE%95%CE%99%CE%9F&contract=true&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId=&medicalActs=&clinics=&searchTerm=', headers={
        'Accept-Language': 'el-GR',
    })
    return response.json()


def get_pharmacies_based_on_geoId(geo_id: str):
    response = session.get(f'https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId={geo_id}&healthCareSiteType=&contract=true&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId=&medicalActs=&clinics=&searchTerm=', headers={
        'Accept-Language': 'el-GR',
    })
    return response.json()

def get_geoIds():
    response = session.get('https://healthatlas.gov.gr/api/PrefectureGeos', headers={
        'Accept-Language': 'el-GR',
    })
    return response.json()