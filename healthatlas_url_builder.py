import json


def get_healthatlas_file(path: str):
    with open(path, 'r', encoding='utf8') as file:
        json_data = json.load(file)
        file.close()
        return json_data


healthatlas_prefecture_geos = get_healthatlas_file(
    './healthatlas_prefecture_geos.json')
healthatlas_specialties = get_healthatlas_file(
    './healthatlas_specialties.json')


def create_hospital_url(prefecture: str):
    item = get_healthatlas_prefecture_item(prefecture)
    prefecture_id = item['prefecture-id']
    url = f"https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId={prefecture_id}&healthCareSiteCategoryId=565920DF-3A17-4BB1-BD7C-5B6D1FDEEAF1&healthCareSiteType=ΝΟΣΟΚΟΜΕΙΟ&contract=null&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId=&medicalActs=&clinics=&searchTerm="

    return url


def create_doctors_url(prefecture: str, specialty: str):
    item_specialty = get_healthatlas_specialty_item(specialty)
    item_prefecture = get_healthatlas_prefecture_item(prefecture)
    specialty_id = item_specialty['specialty-id']
    prefecture_id = item_prefecture['prefecture-id']
    url = f'''https://healthatlas.gov.gr/api/HealthCareSitesLight?
    prefectureGeoId={prefecture_id}
    &healthCareSiteCategoryId=A15A4873-4B7D-4209-8A25-AFD777BBCA11
    &healthCareSiteType=
    &contract=null
    &teleMedDocPat=undefined
    &teleMedConsDoc=undefined
    &specialtyId={specialty_id}
    &medicalActs=
    &clinics=&searchTerm='''
    return url


def get_healthatlas_prefecture_item(prefecture: str):
    return list(filter(lambda x: x["prefecture-name"] == prefecture, healthatlas_prefecture_geos))[0]


def get_healthatlas_specialty_item(specialty: str):
    
    return list(filter(lambda x: x["specialty"] == specialty.upper(), healthatlas_specialties))[0]


print(create_doctors_url('Αττική','Αιματολογια'))
