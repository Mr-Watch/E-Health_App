import json
from bs4 import BeautifulSoup
from bs4.element import Tag

import requests
from requests.models import Response

field_names = ['name', 'specialty', 'address','phone', 'url', 'latitude', 'longitude']


def get_phone(code: int):
    payload = f"showphones=true&doctorcode={code}"
    response = requests.post(
        'https://www.doctoranytime.gr/ws/Doctors/AvailabilitiesV3.svc/GetInfobellPhoneInfoV4', data=payload)
    response_data = json.loads(response.json())
    return response_data['PracticesInfo'][0]['Phone']


def extract_doctor_info(container: Tag):
    tag_name = container.find('a', class_='gtm-doctor-name')
    tag_map = container.find('a', class_='js-popup-map gtm-doctor-map')

    name = tag_name.get_text()
    specialty = container.find('div', class_='specialty').get_text()
    address = tag_map.get_text().strip('\n')
    code = container.get('data-doctorcode')
    phone = get_phone(code)
    url = 'https://www.doctoranytime.gr' + tag_name.get('href')
    latitude = float(tag_map.get('data-lat'))
    longitude = float(tag_map.get('data-lon'))

    fields = list((name, specialty, address, phone, url, latitude, longitude))
    doctors_ = dict(zip(field_names, fields))
    return doctors_


def get_doctors_list(url: str):
    page = requests.get(url).text
    page_soup = BeautifulSoup(page, 'lxml')

    page_containers = page_soup.find_all('article')
    doctor_dict = {}
    for i,container in enumerate(page_containers):
        doctor_dict[i] = extract_doctor_info(container)
    print(doctor_dict)


get_doctors_list('https://www.doctoranytime.gr/s/Velonistis/eksarxeia')
