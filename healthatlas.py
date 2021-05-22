from healthatlas_url_builder import create_url
import requests

session = requests.session()
session.get('https://healthatlas.gov.gr/#!/')

url = create_url('Αιτωλοακαρνανίας')
response = session.get(url, headers={
    'Accept-Language': 'el-GR',
})

print(response.json()[1])
