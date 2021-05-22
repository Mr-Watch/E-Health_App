from webmd_enc_data import client_id, get_webmd_enc_data
import requests
from pathlib import Path
import json
""" enc_data_digest, gmt_time = get_webmd_enc_data()

headers = {
      'Accept': 'application/json',
      'enc_data': enc_data_digest,
      'client_id': client_id,
      'timestamp': gmt_time,
  }

session = requests.session()
page = session.get('https://symptoms.webmd.com/')

response = session.get('https://symptoms.webmd.com/search/2/api/scbodytypeahead?q=&cache_2=true&gender=M&part=11&count=1000', headers=headers)

print(response.text) """


def get_body_part_list():
    data_path = Path('./')
    with open(data_path / 'webmd_body_urls.json', 'r') as file:
        json_data = json.load(file)
        file.close()
        return json_data


data = get_body_part_list()


def get_body_url():
    return data


def get_body_part_url(body_section: str, section_part: str):

    print(len(data))
    # body_section_list = list(filter(lambda x:x['body-section']==body_section,data))
    # body_id = body_section_list[0]['body-id']
    # part_id = list(filter(lambda x:x['section-parts']==section_part,body_section_list[0]['part']))
    # print(body_section_list[0]['section-parts'][0])
    # # url = f'https://symptoms.webmd.com/search/2/api/scbodytypeahead?q=&cache_2=true&gender=M&part={body_part}{part_id}&count=1000'