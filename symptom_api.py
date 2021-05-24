from time import gmtime, strftime
from pathlib import Path
import requests
import hashlib
import base64
import hmac
import json

session = requests.session()
session.get('https://symptoms.webmd.com/')

client_id = 'e4e3f73a-0ceb-4d37-939e-90ddb1238360'
secret_key = b'8720f366-3769-47e9-8156-187b9c6583b5'


def get_webmd_enc_data():
    gmt_time = strftime("%a, %d %b %Y %I:%M:%S %Z", gmtime())
    total_params = f"{{{{timestamp:{gmt_time},client_id:{client_id}}}}}"

    total_params = bytearray(total_params, 'ascii')
    enc_data_digest = hmac.new(
        secret_key, total_params, hashlib.sha256).digest()

    enc_data_bytes_64 = base64.b64encode(enc_data_digest)
    enc_data = enc_data_bytes_64.decode('ascii')

    return enc_data, gmt_time


def get_request_headers():
    enc_data_digest, gmt_time = get_webmd_enc_data()

    headers = {
        'Accept': 'application/json',
        'enc_data': enc_data_digest,
        'client_id': client_id,
        'timestamp': gmt_time,
    }
    return headers


def get_body_part_list():
    with open('./webmd_body_part_ids.json', 'r') as file:
        json_data = json.load(file)
        file.close()
        return json_data


data = get_body_part_list()


def get_body_part_ids():
    return data


def get_body_part_url(body_section: str, section_part: str):
    body_section_list = list(
        filter(lambda x: x['body-section'] == body_section, data['data']))
    body_section_id = body_section_list[0]['body-section-id']

    section_parts_list = body_section_list[0]['section-parts']
    part_id = list(filter(lambda x: x['part'] == section_part, section_parts_list))[
        0]['part-id']

    url = f'https://symptoms.webmd.com/search/2/api/scbodytypeahead?q=&cache_2=true&gender=M&part={body_section_id}{part_id}&count=1000'

    return url


def get_single_body_part_url(body_section: str):
    body_section_list = list(
        filter(lambda x: x['body-section'] == body_section, data['data']))
    body_section_id = body_section_list[0]['body-section-id']

    url = f'https://symptoms.webmd.com/search/2/api/scbodytypeahead?q=&cache_2=true&gender=M&part={body_section_id}&count=1000'

    return url


def make_body_symptoms_single_api_request(body_section: str):
    url = get_single_body_part_url(body_section)

    response = session.get(url, headers=get_request_headers())

    return response.json()


def make_body_symptoms_api_request(body_section: str, section_part: str):
    url = get_body_part_url(body_section, section_part)

    response = session.get(url, headers=get_request_headers())

    return response.json()


def make_symptoms_api_request(age: str, gender: str, symptoms_list: str):
    payload = json.dumps({
        'request': {
            'user': {
                'age': age,
                'gender': gender,
                'zipcode': '',
                'odata': '[CS]v1|294C0D80851D13B4-60000109600025DB[CE]'
            },
            'pregnancy': 2,
            'refinement': [],
            'symptoms': json.loads(symptoms_list),
            'medications': [],
            'prexistingdisease': []
        },
        'site_nm': 'core',
        'maxconditions': 200,
        'maxrefinements': 4,
        'step': 0
    })

    response = session.post(
        'https://symptoms.webmd.com/search/2/api/scapp/conditions', headers=get_request_headers(), data=payload)

    return response.json()