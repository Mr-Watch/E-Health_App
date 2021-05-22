import json
import re
from bs4 import BeautifulSoup
from bs4.element import Tag

import requests
from requests.models import Response
from requests.sessions import session

field_names = ['name', 'address',
               'phone', 'url', 'available_day', 'available_time', 'latitude', 'longitude']


def get_pharmacy_cordinates(url: int):
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64)',
    }

    response = requests.get(url, headers=headers).text
    cordinate_pattern = re.findall('[0-9]{2}\.[0-9]{13}', response)

    return cordinate_pattern[:2]


def extract_pharmacy_info(container: Tag):
    tag_name = container.find('a', class_='ResultName pharmacy-title')

    name = tag_name.get_text().lstrip(' ')
    address = container.find('div', class_='ResultAddr').get_text().strip('\n').lstrip(' ')
    phone = container.find('span', class_='spPhone').get_text()
    url = tag_name.get('href')
    map_url = container.find('a', class_='pharmacy-map').get('href')
    available_day = container.find('div', class_='DutyDay').get_text().strip('\n').rstrip(' ')
    available_time = container.find('span', class_='firstTime').get_text().rstrip(' ')
    cordinates = get_pharmacy_cordinates(map_url)
    latitude = float(cordinates[0])
    longitude = float(cordinates[1])
    fields = list((name, address, phone, url, available_day,
                  available_time, latitude, longitude))
    pharmacy_dict = dict(zip(field_names, fields))
    return pharmacy_dict


def get_pharmacy_list(url: str):
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64)',
    }

    page = requests.get(url, headers=headers).text
    page_soup = BeautifulSoup(page, 'lxml')
    page_containers = page_soup.find_all('section')

    pharmacy_dict = {}
    for i, container in enumerate(page_containers):
        pharmacy_dict[i] = extract_pharmacy_info(container)
    print(pharmacy_dict)

get_pharmacy_list('https://www.vrisko.gr/efimeries-farmakeion/sykies/')