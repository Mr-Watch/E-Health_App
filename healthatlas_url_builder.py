import json

def create_url(prefecture:str):
    item = get_healthatlas_item(prefecture)
    prefecture_id = item['prefecture-id']
    url = f"https://healthatlas.gov.gr/api/HealthCareSitesLight?prefectureGeoId={prefecture_id}&healthCareSiteCategoryId=565920DF-3A17-4BB1-BD7C-5B6D1FDEEAF1&healthCareSiteType=ΝΟΣΟΚΟΜΕΙΟ&contract=null&teleMedDocPat=undefined&teleMedConsDoc=undefined&specialtyId=&medicalActs=&clinics=&searchTerm="

    return url

def get_healthatlas_item(prefecture:str):
    with open('./healthatlas.json','r') as file:
        json_data = json.load(file)
        json_item = list(filter(lambda x:x["prefecture-name"]==prefecture,json_data))
        file.close()
        return json_item[0]